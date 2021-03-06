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

namespace bga
{
    class Factory : tysos.ServerObject
    {
        static void Main(string[] args)
        {
            Factory f = new Factory();
            f.MessageLoop();
        }

        public tysos.ServerObject CreateFSHandler(tysos.lib.File src)
        {
            // Get the properties of the source file
            tysos.lib.File.Property[] props = src.GetAllProperties();
            if (props == null)
                props = new tysos.lib.File.Property[] { };

            // Ensure the device is a BGA device
            tysos.lib.File.Property driver = src.GetPropertyByName("driver");
            if(driver == null || driver.Value == null || !(driver.Value is string) || ((string)driver.Value).Equals("bga") == false)
            {
                System.Diagnostics.Debugger.Log(0, "bga", "driver property is invalid");
                return null;
            }

            // Get the subdriver
            string subdriver_str = "";
            tysos.lib.File.Property subdriver = src.GetPropertyByName("subdriver");
            if (subdriver != null && subdriver.Value != null && (subdriver.Value is string))
                subdriver_str = subdriver.Value as string;

            System.Diagnostics.Debugger.Log(0, "bga", "subdriver " + subdriver_str + " not found");

            bga fs = new bga(props);
            tysos.Process p = tysos.Process.CreateProcess("bga: " + src.Name,
                    new System.Threading.ThreadStart(fs.MessageLoop), new object[] { fs });
            p.Start();

            return fs;
        }
    }
}
