﻿/* Copyright (C) 2015 by John Cronin
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace pci
{
    partial class hostbridge : tysos.lib.VirtualDirectoryServer
    {
        internal tysos.Resources.PhysicalMemoryRangeManager pmems = new tysos.Resources.PhysicalMemoryRangeManager();
        internal tysos.Resources.VirtualMemoryRangeManager vmems = new tysos.Resources.VirtualMemoryRangeManager();
        internal tysos.x86_64.IORangeManager ios = new tysos.x86_64.IORangeManager();

        internal tysos.x86_64.IOResource CONFIG_ADDRESS, CONFIG_DATA;

        string acpiname;
        tysos.ServerObject acpi;
        Dictionary<int, PRTEntry[]> prts = new Dictionary<int, PRTEntry[]>(new tysos.Program.MyGenericEqualityComparer<int>());

        Dictionary<string, int> next_device_id = new Dictionary<string, int>(new tysos.Program.MyGenericEqualityComparer<string>());

        public hostbridge(tysos.lib.File.Property[] Properties)
        {
            root = new List<tysos.lib.File.Property>(Properties);
        }

        public override bool InitServer()
        {
            System.Diagnostics.Debugger.Log(0, "pci", "hostbridge started with " + root.Count.ToString() + " properties");

            /* Extract the resources we have been given */
            vmems.Init(root);
            pmems.Init(root);
            ios.Init(root);
            acpiname = GetFirstProperty(root, "acpiname") as string;
            tysos.Process p_acpipc = tysos.Syscalls.ProcessFunctions.GetProcessByName("acpipc");
            if (p_acpipc != null)
                acpi = p_acpipc.MessageServer;

            /* Get CONFIG_DATA and CONFIG_ADDRESS ports */
            CONFIG_ADDRESS = ios.AllocFixed(0xcf8, 4);
            CONFIG_DATA = ios.AllocFixed(0xcfc, 4);
            if (CONFIG_ADDRESS == null)
                throw new Exception("Unable to obtain CONFIG_ADDRESS");
            if (CONFIG_DATA == null)
                throw new Exception("Unable to obtain CONFIG_DATA");

            /* Evaluate _PRT if it exists */
            if(acpi != null && acpiname != null)
            {
                acpipc.Aml.ACPIObject prt = acpi.Invoke("EvaluateObject",
                    new object[] { acpiname + "._PRT" }, new Type[] { typeof(string) })
                    as acpipc.Aml.ACPIObject;
                if(prt != null && prt.Type == acpipc.Aml.ACPIObject.DataType.Package)
                {
                    System.Diagnostics.Debugger.Log(0, "pci", acpiname + "._PRT succeeded");
                    foreach(var prtentry in (acpipc.Aml.ACPIObject[])prt.Data)
                    {
                        if(prtentry.Type == acpipc.Aml.ACPIObject.DataType.Package)
                        {
                            var prtobj = prt.Data as acpipc.Aml.ACPIObject[];
                            int dev = (int)((prtobj[0].IntegerData >> 16) & 0xffffU);
                            if (!prts.ContainsKey(dev))
                                prts[dev] = new PRTEntry[4];

                            int pin = (int)prtobj[1].IntegerData;

                            System.Diagnostics.Debugger.Log(0, "pci", "PRT: dev: " + dev.ToString() + ", pin: " + pin.ToString() + ", source.Type: " + prtobj[2].Type.ToString() + ", sourceIndex: " + prtobj[3].IntegerData.ToString());
                        }
                    }
                }
                else
                    System.Diagnostics.Debugger.Log(0, "pci", acpiname + "._PRT failed");
            }

            /* Enumerate the devices on this bus */
            for(int dev = 0; dev < 32; dev++)
            {
                CheckDevice(0, dev, 0);
            }

            return true;
        }

        private void CheckDevice(int bus, int dev, int func)
        {
            uint vendor_devID = ReadConfig(bus, dev, func, 0);
            uint vendorID = vendor_devID & 0xffff;
            if (vendorID == 0xffff)
                return;

            /* Get the basic fields of the device */
            uint command_status = ReadConfig(bus, dev, func, 4);
            uint revID_progIF_subclass_class = ReadConfig(bus, dev, func, 8);
            uint cacheline_latency_ht_bist = ReadConfig(bus, dev, func, 0xc);

            /* We have found a valid device */
            uint deviceID = vendor_devID >> 16;
            uint classcode = (revID_progIF_subclass_class >> 24) & 0xffU;
            uint subclasscode = (revID_progIF_subclass_class >> 16) & 0xffU;
            uint prog_IF = (revID_progIF_subclass_class >> 8) & 0xffU;
            uint revisionID = revID_progIF_subclass_class & 0xffU;

            System.Diagnostics.Debugger.Log(0, "pci", bus.ToString() + ":" + dev.ToString() + ":" + func.ToString() + " : " + vendorID.ToString("X4") + ":" + deviceID.ToString("X4") + "   " + classcode.ToString("X2") + ":" + subclasscode.ToString("X2") + ":" + prog_IF.ToString("X2"));

            DeviceDBEntry details = DeviceDB.GetDeviceDetails(new DeviceDBKey
            {
                VendorID = vendorID,
                DeviceID = deviceID,
                RevisionID = revisionID,
                ClassCode = classcode,
                SubclassCode = subclasscode,
                ProgIF = prog_IF
            });

            if (details == null)
                System.Diagnostics.Debugger.Log(0, "pci", "unknown device");
            else
            {
                System.Diagnostics.Debugger.Log(0, "pci", details.ToString());

                if (details.DriverName != null)
                {
                    /* Build a device node */
                    List<tysos.lib.File.Property> props = new List<tysos.lib.File.Property>();
                    props.Add(new tysos.lib.File.Property { Name = "driver", Value = details.DriverName });
                    if (details.SubdriverName != null)
                        props.Add(new tysos.lib.File.Property { Name = "subdriver", Value = details.SubdriverName });
                    if (details.HumanDeviceName != null)
                        props.Add(new tysos.lib.File.Property { Name = "name", Value = details.HumanDeviceName });
                    if (details.HumanManufacturerName != null)
                        props.Add(new tysos.lib.File.Property { Name = "manufacturer", Value = details.HumanManufacturerName });
                    PCIConfiguration conf = new PCIConfiguration(CONFIG_ADDRESS,
                        CONFIG_DATA, bus, dev, func, this, details.BAROverrides);
                    props.Add(new tysos.lib.File.Property { Name = "pciconf", Value = conf });

                    /* Generate a unique name for the device */
                    int dev_no = 0;
                    StringBuilder sb = new StringBuilder(details.DriverName);
                    if (details.SubdriverName != null)
                    {
                        sb.Append("_");
                        sb.Append(details.SubdriverName);
                    }
                    string base_dev = sb.ToString();
                    if (next_device_id.ContainsKey(base_dev))
                        dev_no = next_device_id[base_dev];
                    sb.Append("_");
                    sb.Append(dev_no.ToString());
                    next_device_id[base_dev] = dev_no + 1;
                    string dev_name = sb.ToString();

                    children[dev_name] = props;
                }
            }

            /* If header type == 0x80, it is a multifunction device */
            uint header_type = (cacheline_latency_ht_bist >> 16) & 0xffU;
            if (header_type == 0x80 && func == 0)
            {
                for (int subfunc = 1; subfunc < 8; subfunc++)
                    CheckDevice(bus, dev, subfunc);
            }
        }

        uint ReadConfig(int bus, int dev, int func, int reg_no)
        {
            uint address = (uint)reg_no;
            address &= 0xfc;        // zero out lower 2 bits
            address |= ((uint)func & 0x7U) << 8;
            address |= ((uint)dev & 0x1fU) << 11;
            address |= ((uint)bus & 0xffU) << 16;
            address |= 0x80000000U;

            CONFIG_ADDRESS.Write(CONFIG_ADDRESS.Addr32, 4, address);
            return CONFIG_DATA.Read(CONFIG_DATA.Addr32, 4);
        }

        void WriteConfig(int bus, int dev, int func, int reg_no, uint val)
        {
            uint address = (uint)reg_no;
            address &= 0xfc;        // zero out lower 2 bits
            address |= ((uint)func & 0x7U) << 8;
            address |= ((uint)dev & 0x1fU) << 11;
            address |= ((uint)bus & 0xffU) << 16;
            address |= 0x80000000U;

            CONFIG_ADDRESS.Write(CONFIG_ADDRESS.Addr32, 4, address);
            CONFIG_DATA.Write(CONFIG_DATA.Addr32, 4, val);
        }
    }

    class PRTEntry
    {
        public acpipc.Aml.ACPIName Source;
        public int SourceIndex;
    }
}
