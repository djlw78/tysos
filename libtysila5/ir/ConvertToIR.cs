﻿/* Copyright (C) 2017 by John Cronin
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
using System.Text;
using libtysila5.cil;
using metadata;
using libtysila5.util;

namespace libtysila5.ir
{
    public class StackItem
    {
        public TypeSpec ts;
        public int _ct = 0;

        public int ct { get { if (ts == null) return _ct; return Opcode.GetCTFromType(ts); } }

        public long min_l = long.MinValue;
        public long max_l = long.MaxValue;

        public ulong min_ul = ulong.MinValue;
        public ulong max_ul = ulong.MaxValue;

        public string str_val = null;

        public target.Target.Reg reg;
    }

    public partial class ConvertToIR
    {
        static ConvertToIR()
        {
            init_intcalls();
        }
        
        public static void DoConversion(Code c)
        {
            foreach (var n in c.starts)
            {
                if (n.il_offset == 0)
                    n.is_meth_start = true;
                else
                    n.is_eh_start = true;
                DoConversion(n, c);
            }

            c.ir = new System.Collections.Generic.List<CilNode.IRNode>();
            foreach (var n in c.cil)
                c.ir.AddRange(n.irnodes);
        }

        public static void DoConversion(CilNode n, Code c)
        {
            DoConversion(n, c, new Stack<StackItem>());
        }

        private static void DoConversion(CilNode n, Code c, Stack<StackItem> stack_before)
        {
            // TODO ensure stack integrity
            if (n.visited)
                return;

            Stack<StackItem> stack_after = null;
            StackItem si = null;
            long imm = 0;
            TypeSpec ts = null;

            if (n.is_meth_start)
                n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_enter, stack_before = stack_before, stack_after = stack_before });

            switch(n.opcode.opcode1)
            {
                case cil.Opcode.SingleOpcodes.nop:
                    stack_after = stack_before;
                    break;

                case cil.Opcode.SingleOpcodes.ldc_i4_0:
                case cil.Opcode.SingleOpcodes.ldc_i4_1:
                case cil.Opcode.SingleOpcodes.ldc_i4_2:
                case cil.Opcode.SingleOpcodes.ldc_i4_3:
                case cil.Opcode.SingleOpcodes.ldc_i4_4:
                case cil.Opcode.SingleOpcodes.ldc_i4_5:
                case cil.Opcode.SingleOpcodes.ldc_i4_6:
                case cil.Opcode.SingleOpcodes.ldc_i4_7:
                case cil.Opcode.SingleOpcodes.ldc_i4_8:
                case cil.Opcode.SingleOpcodes.ldc_i4_m1:
                case cil.Opcode.SingleOpcodes.ldc_i4_s:
                case cil.Opcode.SingleOpcodes.ldc_i4:
                    stack_after = new Stack<StackItem>(stack_before);
                    si = new StackItem();
                    si.ts = c.ms.m.GetSimpleTypeSpec(0x08);

                    stack_after.Add(si);

                    switch(n.opcode.opcode1)
                    {
                        case cil.Opcode.SingleOpcodes.ldc_i4:
                        case cil.Opcode.SingleOpcodes.ldc_i4_s:
                            imm = n.inline_long;
                            break;
                        case cil.Opcode.SingleOpcodes.ldc_i4_m1:
                            imm = -1;
                            break;
                        default:
                            imm = n.opcode.opcode1 - cil.Opcode.SingleOpcodes.ldc_i4_0;
                            break;
                    }

                    n.irnodes.Add(new CilNode.IRNode { parent = n, imm_l = imm, opcode = Opcode.oc_ldc, ct = Opcode.ct_int32, vt_size = 4, stack_after = stack_after, stack_before = stack_before });
                    break;

                case cil.Opcode.SingleOpcodes.ldnull:
                    stack_after = new Stack<StackItem>(stack_before);
                    si = new StackItem();
                    si.ts = c.ms.m.GetSimpleTypeSpec(0x1c);

                    stack_after.Add(si);
                    imm = 0;
                    n.irnodes.Add(new CilNode.IRNode { parent = n, imm_l = imm, opcode = Opcode.oc_ldc, ct = Opcode.ct_object, vt_size = c.t.GetPointerSize(), stack_after = stack_after, stack_before = stack_before });
                    break;

                case cil.Opcode.SingleOpcodes.stloc_0:
                    stack_after = stloc(n, c, stack_before, 0);
                    break;
                case cil.Opcode.SingleOpcodes.stloc_1:
                    stack_after = stloc(n, c, stack_before, 1);
                    break;
                case cil.Opcode.SingleOpcodes.stloc_2:
                    stack_after = stloc(n, c, stack_before, 2);
                    break;
                case cil.Opcode.SingleOpcodes.stloc_3:
                    stack_after = stloc(n, c, stack_before, 3);
                    break;
                case cil.Opcode.SingleOpcodes.stloc_s:
                    stack_after = stloc(n, c, stack_before, n.inline_int);
                    break;

                case cil.Opcode.SingleOpcodes.ldloc_0:
                    stack_after = ldloc(n, c, stack_before, 0);
                    break;
                case cil.Opcode.SingleOpcodes.ldloc_1:
                    stack_after = ldloc(n, c, stack_before, 1);
                    break;
                case cil.Opcode.SingleOpcodes.ldloc_2:
                    stack_after = ldloc(n, c, stack_before, 2);
                    break;
                case cil.Opcode.SingleOpcodes.ldloc_3:
                    stack_after = ldloc(n, c, stack_before, 3);
                    break;
                case cil.Opcode.SingleOpcodes.ldloc_s:
                    stack_after = ldloc(n, c, stack_before, n.inline_int);
                    break;

                case cil.Opcode.SingleOpcodes.ldarg_s:
                    stack_after = ldarg(n, c, stack_before, n.inline_int);
                    break;
                case cil.Opcode.SingleOpcodes.ldarg_0:
                    stack_after = ldarg(n, c, stack_before, 0);
                    break;
                case cil.Opcode.SingleOpcodes.ldarg_1:
                    stack_after = ldarg(n, c, stack_before, 1);
                    break;
                case cil.Opcode.SingleOpcodes.ldarg_2:
                    stack_after = ldarg(n, c, stack_before, 2);
                    break;
                case cil.Opcode.SingleOpcodes.ldarg_3:
                    stack_after = ldarg(n, c, stack_before, 3);
                    break;

                case cil.Opcode.SingleOpcodes.ldsfld:
                    stack_after = ldflda(n, c, stack_before, true, out ts);
                    stack_after = ldind(n, c, stack_after, ts);
                    break;

                case cil.Opcode.SingleOpcodes.ldfld:
                    stack_after = ldflda(n, c, stack_before, false, out ts);
                    stack_after = binnumop(n, c, stack_after, cil.Opcode.SingleOpcodes.add, Opcode.ct_intptr);
                    stack_after = ldind(n, c, stack_after, ts);
                    break;

                case cil.Opcode.SingleOpcodes.ldsflda:
                    stack_after = ldflda(n, c, stack_before, true, out ts);
                    break;

                case cil.Opcode.SingleOpcodes.ldflda:
                    stack_after = ldflda(n, c, stack_before, false, out ts);
                    stack_after = binnumop(n, c, stack_after, cil.Opcode.SingleOpcodes.add, Opcode.ct_intptr);
                    break;

                case cil.Opcode.SingleOpcodes.stfld:
                    stack_after = ldflda(n, c, stack_before, false, out ts);
                    stack_after = binnumop(n, c, stack_after, cil.Opcode.SingleOpcodes.add, Opcode.ct_intptr);
                    stack_after = stind(n, c, stack_after, c.t.GetSize(ts), true);
                    break;

                case cil.Opcode.SingleOpcodes.stsfld:
                    stack_after = ldflda(n, c, stack_before, true, out ts);
                    stack_after = stind(n, c, stack_after, c.t.GetSize(ts), true);
                    break;

                case cil.Opcode.SingleOpcodes.br:
                case cil.Opcode.SingleOpcodes.br_s:
                    stack_after = stack_before;

                    n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_br, imm_l = n.il_offsets_after[0], stack_after = stack_after, stack_before = stack_before });
                    break;

                case cil.Opcode.SingleOpcodes.brtrue:
                case cil.Opcode.SingleOpcodes.brtrue_s:
                case cil.Opcode.SingleOpcodes.brfalse:
                case cil.Opcode.SingleOpcodes.brfalse_s:
                    // first push zero
                    stack_after = new Stack<StackItem>(stack_before);
                    stack_after.Push(new StackItem { ts = c.ms.m.GetSimpleTypeSpec(0x18), min_l = 0, max_l = 0, min_ul = 0, max_ul = 0 });
                    n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_ldc, ct = Opcode.ct_intptr, imm_l = 0, imm_ul = 0, stack_before = stack_before, stack_after = stack_after });

                    switch(n.opcode.opcode1)
                    {
                        case cil.Opcode.SingleOpcodes.brtrue:
                        case cil.Opcode.SingleOpcodes.brtrue_s:
                            stack_after = brif(n, c, stack_after, Opcode.cc_ne);
                            break;
                        case cil.Opcode.SingleOpcodes.brfalse:
                        case cil.Opcode.SingleOpcodes.brfalse_s:
                            stack_after = brif(n, c, stack_after, Opcode.cc_eq);
                            break;
                    }
                    break;

                case cil.Opcode.SingleOpcodes.beq:
                case cil.Opcode.SingleOpcodes.beq_s:
                    stack_after = brif(n, c, stack_before, Opcode.cc_eq);
                    break;

                case cil.Opcode.SingleOpcodes.bge:
                case cil.Opcode.SingleOpcodes.bge_s:
                    stack_after = brif(n, c, stack_before, Opcode.cc_ge);
                    break;

                case cil.Opcode.SingleOpcodes.bge_un:
                case cil.Opcode.SingleOpcodes.bge_un_s:
                    stack_after = brif(n, c, stack_before, Opcode.cc_ae);
                    break;

                case cil.Opcode.SingleOpcodes.bgt:
                case cil.Opcode.SingleOpcodes.bgt_s:
                    stack_after = brif(n, c, stack_before, Opcode.cc_gt);
                    break;

                case cil.Opcode.SingleOpcodes.bgt_un:
                case cil.Opcode.SingleOpcodes.bgt_un_s:
                    stack_after = brif(n, c, stack_before, Opcode.cc_a);
                    break;

                case cil.Opcode.SingleOpcodes.ble:
                case cil.Opcode.SingleOpcodes.ble_s:
                    stack_after = brif(n, c, stack_before, Opcode.cc_le);
                    break;

                case cil.Opcode.SingleOpcodes.ble_un:
                case cil.Opcode.SingleOpcodes.ble_un_s:
                    stack_after = brif(n, c, stack_before, Opcode.cc_be);
                    break;

                case cil.Opcode.SingleOpcodes.blt:
                case cil.Opcode.SingleOpcodes.blt_s:
                    stack_after = brif(n, c, stack_before, Opcode.cc_lt);
                    break;

                case cil.Opcode.SingleOpcodes.blt_un:
                case cil.Opcode.SingleOpcodes.blt_un_s:
                    stack_after = brif(n, c, stack_before, Opcode.cc_b);
                    break;

                case cil.Opcode.SingleOpcodes.bne_un:
                case cil.Opcode.SingleOpcodes.bne_un_s:
                    stack_after = brif(n, c, stack_before, Opcode.cc_ne);
                    break;

                case cil.Opcode.SingleOpcodes.ldstr:
                    stack_after = ldstr(n, c, stack_before);
                    break;

                case cil.Opcode.SingleOpcodes.call:
                    stack_after = call(n, c, stack_before);
                    break;

                case cil.Opcode.SingleOpcodes.calli:
                    stack_after = call(n, c, stack_before, true);
                    break;

                case cil.Opcode.SingleOpcodes.callvirt:
                    {
                        var call_ms = c.ms.m.GetMethodSpec(n.inline_uint, c.ms.gtparams, c.ms.gmparams);
                        var call_ms_flags = call_ms.m.GetIntEntry(MetadataStream.tid_MethodDef,
                            call_ms.mdrow, 2);

                        if((call_ms_flags & 0x40) == 0x40)
                        {
                            // Calling a virtual function
                            stack_after = copy_this_to_front(n, c, stack_before);
                            stack_after = get_virt_ftn_ptr(n, c, stack_after);
                            stack_after = call(n, c, stack_after, true);
                        }
                        else
                        {
                            // Calling an instance function
                            stack_after = call(n, c, stack_before);
                        }
                    }                    
                    break;

                case cil.Opcode.SingleOpcodes.ret:
                    stack_after = ret(n, c, stack_before);
                    break;

                case cil.Opcode.SingleOpcodes.add:
                case cil.Opcode.SingleOpcodes.add_ovf:
                case cil.Opcode.SingleOpcodes.add_ovf_un:
                case cil.Opcode.SingleOpcodes.sub:
                case cil.Opcode.SingleOpcodes.sub_ovf:
                case cil.Opcode.SingleOpcodes.sub_ovf_un:
                case cil.Opcode.SingleOpcodes.mul:
                case cil.Opcode.SingleOpcodes.mul_ovf:
                case cil.Opcode.SingleOpcodes.mul_ovf_un:
                case cil.Opcode.SingleOpcodes.div:
                case cil.Opcode.SingleOpcodes.div_un:
                case cil.Opcode.SingleOpcodes.rem:
                case cil.Opcode.SingleOpcodes.rem_un:
                    stack_after = binnumop(n, c, stack_before, n.opcode.opcode1);
                    break;

                case cil.Opcode.SingleOpcodes.conv_i:
                    stack_after = conv(n, c, stack_before, 0x18);
                    break;
                case cil.Opcode.SingleOpcodes.conv_i1:
                    stack_after = conv(n, c, stack_before, 0x04);
                    break;
                case cil.Opcode.SingleOpcodes.conv_i2:
                    stack_after = conv(n, c, stack_before, 0x06);
                    break;
                case cil.Opcode.SingleOpcodes.conv_i4:
                    stack_after = conv(n, c, stack_before, 0x08);
                    break;
                case cil.Opcode.SingleOpcodes.conv_i8:
                    stack_after = conv(n, c, stack_before, 0x0a);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_i:
                    stack_after = conv(n, c, stack_before, 0x18, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_i1:
                    stack_after = conv(n, c, stack_before, 0x04, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_i1_un:
                    stack_after = conv(n, c, stack_before, 0x04, true, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_i2:
                    stack_after = conv(n, c, stack_before, 0x06, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_i2_un:
                    stack_after = conv(n, c, stack_before, 0x06, true, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_i4:
                    stack_after = conv(n, c, stack_before, 0x08, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_i4_un:
                    stack_after = conv(n, c, stack_before, 0x08, true, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_i8:
                    stack_after = conv(n, c, stack_before, 0x0a, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_i8_un:
                    stack_after = conv(n, c, stack_before, 0x0a, true, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_i_un:
                    stack_after = conv(n, c, stack_before, 0x18, true, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_u:
                    stack_after = conv(n, c, stack_before, 0x19, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_u1:
                    stack_after = conv(n, c, stack_before, 0x05, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_u1_un:
                    stack_after = conv(n, c, stack_before, 0x05, true, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_u2:
                    stack_after = conv(n, c, stack_before, 0x07, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_u2_un:
                    stack_after = conv(n, c, stack_before, 0x07, true, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_u4:
                    stack_after = conv(n, c, stack_before, 0x09, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_u4_un:
                    stack_after = conv(n, c, stack_before, 0x09, true, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_u8:
                    stack_after = conv(n, c, stack_before, 0x0b, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_u8_un:
                    stack_after = conv(n, c, stack_before, 0x0b, true, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_ovf_u_un:
                    stack_after = conv(n, c, stack_before, 0x19, true, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_r4:
                    stack_after = conv(n, c, stack_before, 0x0c);
                    break;
                case cil.Opcode.SingleOpcodes.conv_r8:
                    stack_after = conv(n, c, stack_before, 0x0d);
                    break;
                case cil.Opcode.SingleOpcodes.conv_r_un:
                    stack_after = conv(n, c, stack_before, 0x0d, false, true);
                    break;
                case cil.Opcode.SingleOpcodes.conv_u:
                    stack_after = conv(n, c, stack_before, 0x19);
                    break;
                case cil.Opcode.SingleOpcodes.conv_u1:
                    stack_after = conv(n, c, stack_before, 0x05);
                    break;
                case cil.Opcode.SingleOpcodes.conv_u2:
                    stack_after = conv(n, c, stack_before, 0x07);
                    break;
                case cil.Opcode.SingleOpcodes.conv_u4:
                    stack_after = conv(n, c, stack_before, 0x09);
                    break;
                case cil.Opcode.SingleOpcodes.conv_u8:
                    stack_after = conv(n, c, stack_before, 0x0b);
                    break;

                case cil.Opcode.SingleOpcodes.stind_i:
                case cil.Opcode.SingleOpcodes.stind_ref:
                    stack_after = stind(n, c, stack_before, c.t.GetPointerSize());
                    break;
                case cil.Opcode.SingleOpcodes.stind_i1:
                    stack_after = stind(n, c, stack_before, 1);
                    break;
                case cil.Opcode.SingleOpcodes.stind_i2:
                    stack_after = stind(n, c, stack_before, 2);
                    break;
                case cil.Opcode.SingleOpcodes.stind_i4:
                case cil.Opcode.SingleOpcodes.stind_r4:
                    stack_after = stind(n, c, stack_before, 4);
                    break;
                case cil.Opcode.SingleOpcodes.stind_i8:
                case cil.Opcode.SingleOpcodes.stind_r8:
                    stack_after = stind(n, c, stack_before, 8);
                    break;

                case cil.Opcode.SingleOpcodes.ldind_i:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x18));
                    break;
                case cil.Opcode.SingleOpcodes.ldind_ref:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x1c));
                    break;
                case cil.Opcode.SingleOpcodes.ldind_i1:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x04));
                    break;
                case cil.Opcode.SingleOpcodes.ldind_i2:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x06));
                    break;
                case cil.Opcode.SingleOpcodes.ldind_i4:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x08));
                    break;
                case cil.Opcode.SingleOpcodes.ldind_i8:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x0a));
                    break;
                case cil.Opcode.SingleOpcodes.ldind_r4:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x0c));
                    break;
                case cil.Opcode.SingleOpcodes.ldind_r8:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x0d));
                    break;
                case cil.Opcode.SingleOpcodes.ldind_u1:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x05));
                    break;
                case cil.Opcode.SingleOpcodes.ldind_u2:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x07));
                    break;
                case cil.Opcode.SingleOpcodes.ldind_u4:
                    stack_after = ldind(n, c, stack_before, c.ms.m.GetSimpleTypeSpec(0x09));
                    break;




                case cil.Opcode.SingleOpcodes.double_:
                    switch(n.opcode.opcode2)
                    {
                        case cil.Opcode.DoubleOpcodes.ceq:
                            stack_after = cmp(n, c, stack_before, Opcode.cc_eq);
                            break;
                        case cil.Opcode.DoubleOpcodes.cgt:
                            stack_after = cmp(n, c, stack_before, Opcode.cc_gt);
                            break;
                        case cil.Opcode.DoubleOpcodes.cgt_un:
                            stack_after = cmp(n, c, stack_before, Opcode.cc_a);
                            break;
                        case cil.Opcode.DoubleOpcodes.clt:
                            stack_after = cmp(n, c, stack_before, Opcode.cc_lt);
                            break;
                        case cil.Opcode.DoubleOpcodes.clt_un:
                            stack_after = cmp(n, c, stack_before, Opcode.cc_b);
                            break;
                        default:
                            throw new NotImplementedException(n.ToString());
                    }
                    break;

                default:
                    throw new NotImplementedException(n.ToString());
            }

            n.visited = true;

            foreach (var after in n.il_offsets_after)
                DoConversion(c.offset_map[after], c, stack_after);
        }

        private static Stack<StackItem> ldind(CilNode n, Code c, Stack<StackItem> stack_before, TypeSpec ts)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var st_src = stack_after.Pop();

            stack_after.Push(new StackItem { ts = ts });

            var ct_src = st_src.ct;

            switch (ct_src)
            {
                case Opcode.ct_intptr:
                case Opcode.ct_ref:
                    break;
                default:
                    throw new Exception("Cannot perform " + n.opcode.ToString() + " from address type " + Opcode.ct_names[ct_src]);
            }

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_ldind, ct = Opcode.GetCTFromType(ts), vt_size = c.t.GetSize(ts), stack_before = stack_before, stack_after = stack_after });

            return stack_after;
        }

        private static Stack<StackItem> ldflda(CilNode n, Code c, Stack<StackItem> stack_before, bool is_static, out TypeSpec fld_ts)
        {
            int table_id, row;
            c.ms.m.InterpretToken(n.inline_uint, out table_id, out row);

            TypeSpec ts;
            MethodSpec fs;
            if (!c.ms.m.GetFieldDefRow(table_id, row, out ts, out fs))
                throw new Exception("Field not found");

            fld_ts = c.ms.m.GetFieldType(fs, c.ms.gtparams, c.ms.gmparams);

            var fld_addr = layout.Layout.GetFieldOffset(ts, fs, c.t, is_static);

            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);
            StackItem si = new StackItem
            {
                _ct = Opcode.ct_intptr,
                max_l = fld_addr,
                max_ul = (ulong)fld_addr,
                min_l = fld_addr,
                min_ul = (ulong)fld_addr
            };

            if(is_static)
            {
                var static_name = c.ms.m.MangleType(ts) + "S";
                si.str_val = static_name;

                n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_ldlabaddr, ct = Opcode.ct_intptr, imm_lab = static_name, imm_l = fld_addr, stack_before = stack_before, stack_after = stack_after });

                c.t.r.StaticFieldRequestor.Request(ts);
            }
            else
            {
                n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_ldc, ct = Opcode.ct_intptr, imm_l = fld_addr, stack_before = stack_before, stack_after = stack_after });
            }

            stack_after.Push(si);
            return stack_after;
        }

        private static Stack<StackItem> ret(CilNode n, Code c, Stack<StackItem> stack_before)
        {
            var rt_idx = c.ms.m.GetMethodDefSigRetTypeIndex(c.ms.msig);
            var rt_ts = c.ms.m.GetTypeSpec(ref rt_idx, c.ms.gtparams, c.ms.gmparams);

            int ret_ct = ir.Opcode.ct_unknown;
            int ret_vt_size = 0;

            if (rt_ts == null && stack_before.Count != 0)
                throw new Exception("Inconsistent stack on ret");
            else if (rt_ts != null)
            {
                if (stack_before.Count != 1)
                    throw new Exception("Inconsistent stack on ret");
                ret_ct = ir.Opcode.GetCTFromType(rt_ts);
                ret_vt_size = c.t.GetSize(rt_ts);
            }

            var stack_after = new Stack<StackItem>();

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_ret, ct = ret_ct, vt_size = ret_vt_size, stack_before = stack_before, stack_after = stack_after });

            return stack_after;
        }

        private static Stack<StackItem> get_virt_ftn_ptr(CilNode n, Code c, Stack<StackItem> stack_before)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var ms = c.ms.m.GetMethodSpec(n.inline_uint, c.ms.gtparams, c.ms.gmparams);
            var ts = ms.type;

            var l = layout.Layout.GetVTableOffset(ms);


            throw new NotImplementedException();
        }

        private static Stack<StackItem> copy_this_to_front(CilNode n, Code c, Stack<StackItem> stack_before)
        {
            var ms = c.ms.m.GetMethodSpec(n.inline_uint, c.ms.gtparams, c.ms.gmparams);

            uint sig_idx = ms.m.GetIntEntry(MetadataStream.tid_MethodDef, ms.mdrow,
                4);

            var pc = ms.m.GetMethodDefSigParamCountIncludeThis((int)sig_idx);

            var stack_after = copy_to_front(n, c, stack_before, pc - 1);
            return stack_after;
        }

        private static Stack<StackItem> copy_to_front(CilNode n, Code c, Stack<StackItem> stack_before, int v)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var si = stack_before.Peek(v);

            stack_after.Push(si);

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_stackcopy, ct = Opcode.GetCTFromType(si.ts), vt_size = c.t.GetSize(si.ts), imm_l = v, imm_ul = 0, stack_before = stack_before, stack_after = stack_after });

            return stack_after;
        }

        private static Stack<StackItem> ldarg(CilNode n, Code c, Stack<StackItem> stack_before, int v)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var si = new StackItem();
            var ts = c.la_types[v];
            si.ts = ts;
            stack_after.Push(si);

            var vt_size = c.t.GetSize(ts);
            var ct = Opcode.GetCTFromType(ts);

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_ldarg, ct = ct, vt_size = vt_size, imm_l = v, stack_before = stack_before, stack_after = stack_after });

            return stack_after;
        }

        private static Stack<StackItem> stind(CilNode n, Code c, Stack<StackItem> stack_before, int v, bool swap = false)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var st_src = stack_after.Pop();
            var st_dest = stack_after.Pop();

            if(swap)
            {
                var st_tmp = st_src;
                st_src = st_dest;
                st_dest = st_tmp;
            }

            var ct_dest = st_dest.ct;

            switch(ct_dest)
            {
                case Opcode.ct_intptr:
                case Opcode.ct_ref:
                    break;
                default:
                    throw new Exception("Cannot perform " + n.opcode.ToString() + " to address type " + Opcode.ct_names[ct_dest]);
            }

            var ct_src = st_src.ct;

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_stind, ct = ct_src, vt_size = v, imm_l = swap ? 1 : 0, stack_before = stack_before, stack_after = stack_after });

            return stack_after;
        }

        private static Stack<StackItem> conv(CilNode n, Code c, Stack<StackItem> stack_before, int to_stype, bool is_ovf = false, bool is_un = false)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var st = stack_after.Pop();
            var ct = st.ct;

            var to_ct = Opcode.GetCTFromType(to_stype);

            if (!conv_op_valid(ct, to_stype))
                throw new Exception("Cannot perform " + n.opcode.ToString() + " with " + Opcode.ct_names[ct]);

            StackItem si = new StackItem();
            si.ts = c.ms.m.GetSimpleTypeSpec(to_stype);
            stack_after.Push(si);

            long imm = 0;
            if (is_un)
                imm |= 1;
            if (is_ovf)
                imm |= 2;

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_conv, imm_l = imm, ct = ct, stack_before = stack_before, stack_after = stack_after });

            return stack_after;
        }

        private static bool conv_op_valid(int ct, int to_stype)
        {
            switch(ct)
            {
                case Opcode.ct_int32:
                case Opcode.ct_int64:
                case Opcode.ct_intptr:
                case Opcode.ct_float:
                    return true;

                case Opcode.ct_ref:
                case Opcode.ct_object:
                    switch(to_stype)
                    {
                        case 0x0a:
                        case 0x0b:
                        case 0x18:
                        case 0x19:
                            return true;
                        default:
                            return false;
                    }

                default:
                    return false;
            }
        }

        private static Stack<StackItem> call(CilNode n, Code c, Stack<StackItem> stack_before, bool is_calli = false)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var ms = c.ms.m.GetMethodSpec(n.inline_uint, c.ms.gtparams, c.ms.gmparams);
            var mangled_meth = c.ms.m.MangleMethod(ms);

            intcall_delegate intcall;
            if(is_calli == false && intcalls.TryGetValue(mangled_meth, out intcall))
            {
                var r = intcall(n, c, stack_before);
                if (r != null)
                    return r;
            }

            uint sig_idx = ms.m.GetIntEntry(MetadataStream.tid_MethodDef, ms.mdrow,
                4);

            var pc = ms.m.GetMethodDefSigParamCountIncludeThis((int)sig_idx);
            var rt_idx = ms.m.GetMethodDefSigRetTypeIndex((int)sig_idx);
            var rt = ms.m.GetTypeSpec(ref rt_idx, c.ms.gtparams, c.ms.gmparams);

            while (pc-- > 0)
                stack_after.Pop();
            if (is_calli)
                stack_after.Pop();

            int ct = Opcode.ct_unknown;

            if(rt != null)
            {
                StackItem r = new StackItem();
                r.ts = rt;
                stack_after.Push(r);
                ct = Opcode.GetCTFromType(rt);
            }

            int oc = Opcode.oc_call;
            if (is_calli)
                oc = Opcode.oc_calli;
            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = oc, imm_ms = ms, ct = ct, stack_before = stack_before, stack_after = stack_after });

            c.t.r.MethodRequestor.Request(ms);

            return stack_after;
        }

        private static Stack<StackItem> ldstr(CilNode n, Code c, Stack<StackItem> stack_before)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var tok = n.inline_uint;
            if ((tok & 0x70000000) != 0x70000000)
                throw new Exception("Invalid string token");

            var str = c.ms.m.GetUserString((int)(tok & 0x00ffffffUL));

            var str_addr = c.t.st.GetStringAddress(str, c.t);
            var st_name = c.t.st.GetStringTableName();

            StackItem si = new StackItem();
            si.ts = c.ms.m.GetSimpleTypeSpec(0x0e);
            si.str_val = str;

            stack_after.Push(si);

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_ldlabaddr, ct = Opcode.ct_object, imm_l = str_addr, imm_lab = st_name, stack_after = stack_after, stack_before = stack_before });

            return stack_after;
        }

        private static Stack<StackItem> brif(CilNode n, Code c, Stack<StackItem> stack_before, int cc)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var si_b = stack_after.Pop();
            var si_a = stack_after.Pop();

            var ct_a = si_a.ct;
            var ct_b = si_b.ct;

            if (!bin_comp_valid(ct_a, ct_b, cc, false))
                throw new Exception("Invalid comparison between " + Opcode.ct_names[ct_a] + " and " + Opcode.ct_names[ct_b]);

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_brif, imm_l = n.il_offsets_after[1], imm_ul = (uint)cc, ct = ct_a, ct2 = ct_b, stack_after = stack_after, stack_before = stack_before });

            return stack_after;
        }

        private static Stack<StackItem> cmp(CilNode n, Code c, Stack<StackItem> stack_before, int cc)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var si_b = stack_after.Pop();
            var si_a = stack_after.Pop();

            var ct_a = si_a.ct;
            var ct_b = si_b.ct;

            if (!bin_comp_valid(ct_a, ct_b, cc, true))
                throw new Exception("Invalid comparison between " + Opcode.ct_names[ct_a] + " and " + Opcode.ct_names[ct_b]);

            var si = new StackItem();
            TypeSpec ts = c.ms.m.GetSimpleTypeSpec(0x8);
            si.ts = ts;
            stack_after.Push(si);

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_cmp, ct = ct_a, ct2 = ct_b, imm_ul = (uint)cc, stack_after = stack_after, stack_before = stack_before });

            return stack_after;
        }

        private static Stack<StackItem> binnumop(CilNode n, Code c, Stack<StackItem> stack_before, cil.Opcode.SingleOpcodes oc,
            int ct_ret = Opcode.ct_unknown)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var si_b = stack_after.Pop();
            var si_a = stack_after.Pop();

            var ct_a = si_a.ct;
            var ct_b = si_b.ct;

            if (ct_ret == Opcode.ct_unknown)
            {
                ct_ret = bin_op_valid(ct_a, ct_b, oc);
                if (ct_ret == Opcode.ct_unknown)
                    throw new Exception("Invalid binary operation between " + Opcode.ct_names[ct_a] + " and " + Opcode.ct_names[ct_b]);
            }

            bool is_un = false;
            bool is_ovf = false;
            
            switch(oc)
            {
                case cil.Opcode.SingleOpcodes.add_ovf:
                    is_ovf = true;
                    break;
                case cil.Opcode.SingleOpcodes.add_ovf_un:
                    is_ovf = true;
                    is_un = true;
                    break;
                case cil.Opcode.SingleOpcodes.sub_ovf:
                    is_ovf = true;
                    break;
                case cil.Opcode.SingleOpcodes.sub_ovf_un:
                    is_ovf = true;
                    is_un = true;
                    break;
                case cil.Opcode.SingleOpcodes.mul_ovf:
                    is_ovf = true;
                    break;
                case cil.Opcode.SingleOpcodes.mul_ovf_un:
                    is_ovf = true;
                    is_un = true;
                    break;
                case cil.Opcode.SingleOpcodes.div_un:
                    is_un = true;
                    break;
                case cil.Opcode.SingleOpcodes.rem_un:
                    is_un = true;
                    break;
            }

            long imm = 0;
            if (is_un)
                imm |= 1;
            if (is_ovf)
                imm |= 2;

            StackItem si = new StackItem();
            si._ct = ct_ret;

            switch(ct_ret)
            {
                case Opcode.ct_int32:
                    si.ts = c.ms.m.GetSimpleTypeSpec(0x8);
                    break;
                case Opcode.ct_int64:
                    si.ts = c.ms.m.GetSimpleTypeSpec(0xa);
                    break;
                case Opcode.ct_intptr:
                    si.ts = c.ms.m.GetSimpleTypeSpec(0x18);
                    break;
                case Opcode.ct_float:
                    si.ts = c.ms.m.GetSimpleTypeSpec(0xd);
                    break;
                case Opcode.ct_object:
                    si.ts = c.ms.m.GetSimpleTypeSpec(0x1c);
                    break;
            }

            stack_after.Push(si);

            int noc = 0;
            switch(oc)
            {
                case cil.Opcode.SingleOpcodes.add:
                case cil.Opcode.SingleOpcodes.add_ovf:
                case cil.Opcode.SingleOpcodes.add_ovf_un:
                    noc = Opcode.oc_add;
                    break;
                case cil.Opcode.SingleOpcodes.sub:
                case cil.Opcode.SingleOpcodes.sub_ovf:
                case cil.Opcode.SingleOpcodes.sub_ovf_un:
                    noc = Opcode.oc_sub;
                    break;
                case cil.Opcode.SingleOpcodes.mul:
                case cil.Opcode.SingleOpcodes.mul_ovf:
                case cil.Opcode.SingleOpcodes.mul_ovf_un:
                    noc = Opcode.oc_mul;
                    break;
                case cil.Opcode.SingleOpcodes.div:
                case cil.Opcode.SingleOpcodes.div_un:
                    noc = Opcode.oc_div;
                    break;
                case cil.Opcode.SingleOpcodes.rem:
                case cil.Opcode.SingleOpcodes.rem_un:
                    noc = Opcode.oc_rem;
                    break;
            }

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = noc, ct = ct_a, ct2 = ct_b, stack_before = stack_before, stack_after = stack_after });

            return stack_after;
        }

        private static int bin_op_valid(int ct_a, int ct_b, cil.Opcode.SingleOpcodes oc)
        {
            switch(ct_a)
            {
                case Opcode.ct_int32:
                    switch(ct_b)
                    {
                        case Opcode.ct_int32:
                            return Opcode.ct_int32;
                        case Opcode.ct_intptr:
                            return Opcode.ct_intptr;
                        case Opcode.ct_ref:
                            if (oc == cil.Opcode.SingleOpcodes.add)
                                return Opcode.ct_ref;
                            break;
                    }
                    return Opcode.ct_unknown;

                case Opcode.ct_int64:
                    if (ct_b == Opcode.ct_int64)
                        return Opcode.ct_int64;
                    break;

                case Opcode.ct_intptr:
                    switch (ct_b)
                    {
                        case Opcode.ct_int32:
                            return Opcode.ct_intptr;
                        case Opcode.ct_intptr:
                            return Opcode.ct_intptr;
                        case Opcode.ct_ref:
                            if (oc == cil.Opcode.SingleOpcodes.add)
                                return Opcode.ct_ref;
                            break;
                    }
                    return Opcode.ct_unknown;

                case Opcode.ct_float:
                    if (ct_b == Opcode.ct_float)
                        return Opcode.ct_float;
                    return Opcode.ct_unknown;

                case Opcode.ct_ref:
                    switch(ct_b)
                    {
                        case Opcode.ct_int32:
                        case Opcode.ct_intptr:
                            switch(oc)
                            {
                                case cil.Opcode.SingleOpcodes.add:
                                case cil.Opcode.SingleOpcodes.sub:
                                    return Opcode.ct_ref;
                            }
                            break;

                        case Opcode.ct_ref:
                            if (oc == cil.Opcode.SingleOpcodes.sub)
                                return Opcode.ct_intptr;
                            break;
                    }
                    break;
            }
            return Opcode.ct_unknown;
        }

        private static bool bin_comp_valid(int ct_a, int ct_b, int cc, bool is_cmp)
        {
            switch(ct_a)
            {
                case Opcode.ct_int32:
                    switch(ct_b)
                    {
                        case Opcode.ct_int32:
                            return true;
                        case Opcode.ct_intptr:
                            return true;
                    }
                    return false;

                case Opcode.ct_int64:
                    switch(ct_b)
                    {
                        case Opcode.ct_int64:
                            return true;
                    }
                    return false;

                case Opcode.ct_intptr:
                    switch (ct_b)
                    {
                        case Opcode.ct_int32:
                            return true;
                        case Opcode.ct_intptr:
                            return true;
                        case Opcode.ct_ref:
                            if (!is_cmp &&
                                (cc == Opcode.cc_eq || cc == Opcode.cc_ne))
                                return true;
                            if (is_cmp &&
                                (cc == Opcode.cc_eq))
                                return true;
                            return false;
                    }
                    return false;

                case Opcode.ct_float:
                    return ct_b == Opcode.ct_float;

                case Opcode.ct_ref:
                    switch (ct_b)
                    {
                        case Opcode.ct_intptr:
                            if (!is_cmp &&
                                (cc == Opcode.cc_eq || cc == Opcode.cc_ne))
                                return true;
                            if (is_cmp &&
                                (cc == Opcode.cc_eq))
                                return true;
                            return false;

                        case Opcode.ct_ref:
                            return true;
                    }
                    return false;

                case Opcode.ct_object:
                    if (ct_b != Opcode.ct_object)
                        return false;

                    if (is_cmp &&
                        (cc == Opcode.cc_eq || cc == Opcode.cc_a))
                        return true;
                    if (!is_cmp &&
                        (cc == Opcode.cc_eq || cc == Opcode.cc_ne))
                        return true;

                    return false;
            }
            return false;
        }

        private static Stack<StackItem> ldloc(CilNode n, Code c, Stack<StackItem> stack_before, int v)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);

            var si = new StackItem();
            var ts = c.lv_types[v];
            si.ts = ts;
            stack_after.Push(si);

            var vt_size = c.t.GetSize(ts);
            var ct = Opcode.GetCTFromType(ts);

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_ldloc, ct = ct, vt_size = vt_size, imm_l = v, stack_before = stack_before, stack_after = stack_after });

            return stack_after;
        }

        private static Stack<StackItem> stloc(CilNode n, Code c, Stack<StackItem> stack_before, int v)
        {
            Stack<StackItem> stack_after = new Stack<StackItem>(stack_before);
            var ts = stack_after.Pop().ts;

            var to_ts = c.lv_types[v];
            // TODO: ensure top of stack can be assigned to lv

            var vt_size = c.t.GetSize(ts);
            var ct = Opcode.GetCTFromType(ts);

            n.irnodes.Add(new CilNode.IRNode { parent = n, opcode = Opcode.oc_stloc, ct = ct, vt_size = vt_size, imm_l = v, stack_before = stack_before, stack_after = stack_after });

            return stack_after;
        }
    }
}
