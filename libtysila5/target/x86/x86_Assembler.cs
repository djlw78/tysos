﻿/* Copyright (C) 2016 by John Cronin
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
using binary_library;
using libtysila5.ir;
using libtysila5.util;
using metadata;

namespace libtysila5.target.x86
{
    partial class x86_Assembler : Target
    {
        static x86_Assembler()
        {
            init_instrs();
        }

        bool has_cpu_feature(string name)
        {
            // get cpu family, model, stepping
            int family = 0;
#if HAVE_SYSTEM
            var lvls = Environment.GetEnvironmentVariable("PROCESSOR_LEVEL");
            if(lvls != null)
            {
                int.TryParse(lvls, out family);
            }
#endif

            int model = 0;
            int stepping = 0;

#if HAVE_SYSTEM
            var rev = Environment.GetEnvironmentVariable("PROCESSOR_REVISION");
            if(rev != null)
            {
                if(rev.Length == 4)
                {
                    string mods = rev.Substring(0, 2);
                    string steps = rev.Substring(2, 2);

                    int.TryParse(mods, System.Globalization.NumberStyles.HexNumber, null, out model);
                    int.TryParse(steps, System.Globalization.NumberStyles.HexNumber, null, out stepping);
                }
            }
#endif

            bool is_amd64 = false;
#if HAVE_SYSTEM
            var parch = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            if (parch != null && parch == "AMD64")
                is_amd64 = true;
#endif

            if(name == "sse2")
            {
                if (is_amd64)
                    return true;
                if (family == 6 || family == 15)
                    return true;
                return false;
            }

            if(name == "sse4_1")
            {
                if (family == 6 && model >= 6)
                    return true;
                return false;
            }

            throw new NotImplementedException();
        }

        void init_cpu_feature_option(string s)
        {
            Options.InternalAdd(s, has_cpu_feature(s));
        }

        protected void init_options()
        {
            // cpu features
            init_cpu_feature_option("sse4_1");
        }

        public override void InitIntcalls()
        {
            ConvertToIR.intcalls["_ZN14libsupcs#2Edll8libsupcs12IoOperations_7PortOut_Rv_P2th"] = portout_byte;
            ConvertToIR.intcalls["_ZN14libsupcs#2Edll8libsupcs12IoOperations_7PortOut_Rv_P2tt"] = portout_word;
            ConvertToIR.intcalls["_ZN14libsupcs#2Edll8libsupcs12IoOperations_7PortOut_Rv_P2tj"] = portout_dword;
            ConvertToIR.intcalls["_ZN14libsupcs#2Edll8libsupcs12IoOperations_7PortInb_Rh_P1t"] = portin_byte;
            ConvertToIR.intcalls["_ZN14libsupcs#2Edll8libsupcs12IoOperations_7PortInw_Rt_P1t"] = portin_word;
            ConvertToIR.intcalls["_ZN14libsupcs#2Edll8libsupcs12IoOperations_7PortInd_Rj_P1t"] = portin_dword;
            ConvertToIR.intcalls["_ZW6System4Math_5Round_Rd_P1d"] = math_Round;
        }

        private static util.Stack<StackItem> math_Round(cil.CilNode n, Code c, util.Stack<StackItem> stack_before)
        {
            var stack_after = new util.Stack<StackItem>(stack_before);

            var old = stack_after.Pop();

            // propagate immediate value if there is one
            double min_imm = Math.Round(BitConverter.ToDouble(BitConverter.GetBytes(old.min_ul), 0));
            double max_imm = Math.Round(BitConverter.ToDouble(BitConverter.GetBytes(old.max_ul), 0));

            stack_after.Push(new StackItem { ts = c.ms.m.GetSimpleTypeSpec(0xd), min_ul = BitConverter.ToUInt64(BitConverter.GetBytes(min_imm), 0), max_ul = BitConverter.ToUInt64(BitConverter.GetBytes(max_imm), 0) });

            n.irnodes.Add(new cil.CilNode.IRNode { parent = n, opcode = ir.Opcode.oc_target_specific, imm_l = x86_roundsd_xmm_xmmm64_imm8, stack_before = stack_before, stack_after = stack_after, arg_a = 0, res_a = 0 });

            return stack_after;
        }

        private static util.Stack<StackItem> portin_byte(cil.CilNode n, Code c, util.Stack<StackItem> stack_before)
        {
            var stack_after = new util.Stack<StackItem>(stack_before);

            stack_after.Pop();
            stack_after.Push(new StackItem { ts = c.ms.m.SystemByte, min_ul = 0, max_ul = 255 });

            n.irnodes.Add(new cil.CilNode.IRNode { parent = n, opcode = ir.Opcode.oc_x86_portin, vt_size = 1, stack_before = stack_before, stack_after = stack_after, arg_a = 0, res_a = 0 });

            return stack_after;
        }

        private static util.Stack<StackItem> portin_word(cil.CilNode n, Code c, util.Stack<StackItem> stack_before)
        {
            var stack_after = new util.Stack<StackItem>(stack_before);

            stack_after.Pop();
            stack_after.Push(new StackItem { ts = c.ms.m.SystemUInt16, min_ul = 0, max_ul = ushort.MaxValue });

            n.irnodes.Add(new cil.CilNode.IRNode { parent = n, opcode = ir.Opcode.oc_x86_portin, vt_size = 2, stack_before = stack_before, stack_after = stack_after, arg_a = 0, res_a = 0 });

            return stack_after;
        }

        private static util.Stack<StackItem> portin_dword(cil.CilNode n, Code c, util.Stack<StackItem> stack_before)
        {
            var stack_after = new util.Stack<StackItem>(stack_before);

            stack_after.Pop();
            stack_after.Push(new StackItem { ts = c.ms.m.SystemUInt32, min_ul = 0, max_ul = uint.MaxValue });

            n.irnodes.Add(new cil.CilNode.IRNode { parent = n, opcode = ir.Opcode.oc_x86_portin, vt_size = 4, stack_before = stack_before, stack_after = stack_after, arg_a = 0, res_a = 0 });

            return stack_after;
        }

        private static util.Stack<StackItem> portout_byte(cil.CilNode n, Code c, util.Stack<StackItem> stack_before)
        {
            var stack_after = new util.Stack<StackItem>(stack_before);

            stack_after.Pop();
            stack_after.Pop();

            n.irnodes.Add(new cil.CilNode.IRNode { parent = n, opcode = ir.Opcode.oc_x86_portout, vt_size = 1, stack_before = stack_before, stack_after = stack_after, arg_a = 1, arg_b = 0 });

            return stack_after;
        }

        private static util.Stack<StackItem> portout_word(cil.CilNode n, Code c, util.Stack<StackItem> stack_before)
        {
            var stack_after = new util.Stack<StackItem>(stack_before);

            stack_after.Pop();
            stack_after.Pop();

            n.irnodes.Add(new cil.CilNode.IRNode { parent = n, opcode = ir.Opcode.oc_x86_portout, vt_size = 2, stack_before = stack_before, stack_after = stack_after, arg_a = 1, arg_b = 0 });

            return stack_after;
        }

        private static util.Stack<StackItem> portout_dword(cil.CilNode n, Code c, util.Stack<StackItem> stack_before)
        {
            var stack_after = new util.Stack<StackItem>(stack_before);

            stack_after.Pop();
            stack_after.Pop();

            n.irnodes.Add(new cil.CilNode.IRNode { parent = n, opcode = ir.Opcode.oc_x86_portout, vt_size = 4, stack_before = stack_before, stack_after = stack_after, arg_a = 1, arg_b = 0 });

            return stack_after;
        }

        protected internal override Reg GetMoveDest(MCInst i)
        {
            return i.p[1].mreg;
        }

        protected internal override Reg GetMoveSrc(MCInst i)
        {
            return i.p[2].mreg;
        }

        protected internal override int GetCondCode(MCInst i)
        {
            if (i.p == null || i.p.Length < 2 ||
                i.p[1].t != Opcode.vl_cc)
                return Opcode.cc_always;
            return (int)i.p[1].v;
        }

        protected internal override bool IsBranch(MCInst i)
        {
            if (i.p != null && i.p.Length > 0 &&
                i.p[0].t == Opcode.vl_str &&
                (i.p[0].v == x86_jmp_rel32 ||
                i.p[0].v == x86_jcc_rel32))
                return true;
            return false;
        }

        protected internal override bool IsCall(MCInst i)
        {
            if (i.p != null && i.p.Length > 0 &&
                i.p[0].t == Opcode.vl_str &&
                (i.p[0].v == x86_call_rel32))
                return true;
            return false;
        }


        protected internal override void SetBranchDest(MCInst i, int d)
        {
            if (!IsBranch(i))
                throw new NotSupportedException();
            if (i.p[0].v == x86_jcc_rel32)
                i.p[2] = new Param { t = Opcode.vl_br_target, v = d };
            else
                i.p[1] = new Param { t = Opcode.vl_br_target, v = d };
        }

        protected internal override int GetBranchDest(MCInst i)
        {
            if (!IsBranch(i))
                throw new NotSupportedException();
            if (i.p[0].v == x86_jcc_rel32)
                return (int)i.p[2].v;
            else
                return (int)i.p[1].v;
        }

        protected internal override MCInst SaveRegister(Reg r)
        {
            MCInst ret = new MCInst
            {
                p = new Param[]
                {
                    new Param { t = Opcode.vl_str, str = "push", v = x86_push_r32 },
                    new Param { t = Opcode.vl_mreg, mreg = r }
                }
            };
            return ret;
        }

        protected internal override MCInst RestoreRegister(Reg r)
        {
            MCInst ret = new MCInst
            {
                p = new Param[]
                {
                    new Param { t = Opcode.vl_str, str = "pop", v = x86_pop_r32 },
                    new Param { t = Opcode.vl_mreg, mreg = r }
                }
            };
            return ret;
        }

        /* Access to variables on the incoming stack is encoded as -address - 1 */
        protected internal override Reg GetLVLocation(int lv_loc, int lv_size, Code c)
        {
            if (Opcode.GetCTFromType(c.ret_ts) == Opcode.ct_vt)
                lv_loc += psize;

            int disp = 0;
            disp = -lv_size - lv_loc;
            return new ContentsReg
            {
                basereg = r_ebp,
                disp = disp,
                size = lv_size
            };
        }

        /* Access to variables on the incoming stack is encoded as -address - 1 */
        protected internal override Reg GetLALocation(int la_loc, int la_size, Code c)
        {
            if (Opcode.GetCTFromType(c.ret_ts) == Opcode.ct_vt)
                la_loc += psize;

            return new ContentsReg
            {
                basereg = r_ebp,
                disp = la_loc + 2 * psize,
                size = la_size
            };
        }

        protected internal override MCInst[] CreateMove(Reg src, Reg dest)
        {
            throw new NotImplementedException();
        }

        protected internal override MCInst[] SetupStack(int lv_size)
        {
            if (lv_size == 0)
                return new MCInst[0];
            else
                return new MCInst[]
                {
                    new MCInst { p = new Param[]
                    {
                        new Param { t = Opcode.vl_str, str = "sub", v = x86_sub_rm32_imm32 },
                        new Param { t = Opcode.vl_mreg, mreg = r_esp },
                        new Param { t = Opcode.vl_mreg, mreg = r_esp },
                        new Param { t = Opcode.vl_c, v = lv_size }
                    } }
                };
        }

        protected internal override IRelocationType GetDataToDataReloc()
        {
            return new binary_library.elf.ElfFile.Rel_386_32();
        }

        protected internal override IRelocationType GetDataToCodeReloc()
        {
            return new binary_library.elf.ElfFile.Rel_386_32();
        }

        public override Reg AllocateStackLocation(Code c, int size, ref int cur_stack)
        {
            size = util.util.align(size, psize);
            cur_stack -= size;

            if (-cur_stack > c.stack_total_size)
                c.stack_total_size = -cur_stack;

            return new ContentsReg { basereg = r_ebp, disp = cur_stack - c.lv_total_size, size = size };
        }

        protected internal override int GetCTFromTypeForCC(TypeSpec t)
        {
            if (t.Equals(t.m.SystemRuntimeTypeHandle) || t.Equals(t.m.SystemRuntimeMethodHandle) ||
                t.Equals(t.m.SystemRuntimeFieldHandle))
                return ir.Opcode.ct_int32;
            return base.GetCTFromTypeForCC(t);
        }

        protected internal override Code AssembleBoxedMethod(MethodSpec ms)
        {
            /* To unbox, we simply add the size of system.object to 
             * first argument, then jmp to the actual method
             */

            var c = new Code();
            c.mc = new List<MCInst>();

            var this_reg = psize == 4 ? new ContentsReg { basereg = r_ebp, disp = 8, size = 4 } : x86_64.x86_64_Assembler.r_rdi;
            var sysobjsize = layout.Layout.GetTypeSize(ms.m.SystemObject, this);
            c.mc.Add(inst(psize == 4 ? x86_add_rm32_imm8 : x86_add_rm64_imm8, this_reg, sysobjsize, null));

            var unboxed = ms.Unbox;
            var act_meth = unboxed.MangleMethod();
            r.MethodRequestor.Request(unboxed);


            c.mc.Add(inst(x86_jmp_rel32, new Param { t = Opcode.vl_str, str = act_meth }, null));

            return c;
        }
    }
}

namespace libtysila5.target.x86_64
{
    partial class x86_64_Assembler : x86.x86_Assembler
    {
        public override int GetCCClassFromCT(int ct, int size, TypeSpec ts, string cc)
        {
            if (cc == "sysv" | cc == "default")
            {
                switch (ct)
                {
                    case Opcode.ct_vt:
                        // breaks spec - need to only use MEMORY for those more than 32 bytes
                        //  but we don't wupport splitting arguments up yet
                        return sysvc_MEMORY;
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            return base.GetCCClassFromCT(ct, size, ts, cc);
        }

        /* runtime handles are void* pointers - we use the following to ensure they are passed in
         * registers rather than on the stack */
        protected internal override int GetCTFromTypeForCC(TypeSpec t)
        {
            if (t.Equals(t.m.SystemRuntimeTypeHandle) || t.Equals(t.m.SystemRuntimeMethodHandle) ||
                t.Equals(t.m.SystemRuntimeFieldHandle))
                return ir.Opcode.ct_int64;
            return base.GetCTFromTypeForCC(t);
        }

        protected override Reg GetRegLoc(Param csite, ref int stack_loc, int cc_next, int ct, TypeSpec ts, string cc)
        {
            if(cc == "isr")
            {
                /* ISR with error codes have stack as:
                 * 
                 * cur_rax          - pushed at start of isr so we have a scratch reg [rbp-8]
                 *      this is exchanged with the regs pointer as part of the init phase
                 * old_rbp          - pushed at start as part of push ebp; mov ebp, esp sequence [rbp]
                 * ret_rip          - [rbp + 8]
                 * ret_cs           - [rbp + 16]
                 * rflags           - [rbp + 24]
                 * ret_rsp          - [rbp + 32]
                 * ret_ss           - [rbp + 40]
                 */

                switch (cc_next)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return new ContentsReg { basereg = r_ebp, disp = 8 + 8 * cc_next, size = 8 };
                    case 5:
                        return new ContentsReg { basereg = r_ebp, disp = -8, size = 8 };
                }
            }
            else if(cc == "isrec")
            {
                /* ISR with error codes have stack as:
                 * 
                 * cur_rax          - pushed at start of isr so we have a scratch reg [rbp-8]
                 *      this is exchanged with the regs pointer as part of the init phase
                 * old_rbp          - pushed at start as part of push ebp; mov ebp, esp sequence [rbp]
                 * error_code       - [rbp + 8]
                 * ret_rip          - [rbp + 16]
                 * ret_cs           - [rbp + 24]
                 * rflags           - [rbp + 32]
                 * ret_rsp          - [rbp + 40]
                 * ret_ss           - [rbp + 48]
                 */
                
                switch(cc_next)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return new ContentsReg { basereg = r_ebp, disp = 8 + 8 * cc_next, size = 8 };
                    case 6:
                        return new ContentsReg { basereg = r_ebp, disp = -8, size = 8 };
                }
            }

            return base.GetRegLoc(csite, ref stack_loc, cc_next, ct, ts, cc);
        }

        internal override int GetCCStackReserve(string cc)
        {
            // isrs use stack space for the address of the regs array
            if (cc == "isr" || cc == "isrec")
                return psize;
            else
                return base.GetCCStackReserve(cc);
        }
    }
}
