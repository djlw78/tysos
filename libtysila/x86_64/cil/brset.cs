﻿/* Copyright (C) 2014 by John Cronin
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
using libtysila.frontend.cil;
using libasm;
using libtysila.x86_64;

namespace libtysila.x86_64.cil
{
    class brset
    {
        public static void tybel_br(frontend.cil.CilNode il, Assembler ass, Assembler.MethodToCompile mtc, ref int next_block,
            Encoder.EncoderState state, Assembler.MethodAttributes attrs)
        {
            int il_target = il.il.il_offset_after + il.il.inline_int;
            CilNode target = state.offset_map[il_target];
            ((x86_64_Assembler)ass).ChooseInstruction(x86_64_asm.opcode.JMP, il.il.tybel, vara.Label("L" + target.il_label.ToString(), false));
        }

        public static void tybel_brset(frontend.cil.CilNode il, Assembler ass, Assembler.MethodToCompile mtc, ref int next_block,
            Encoder.EncoderState state, Assembler.MethodAttributes attrs)
        {
            libasm.hardware_location a_loc, b_loc;
            Signature.Param a_p, b_p;

            switch (il.il.opcode.opcode1)
            {
                case Opcode.SingleOpcodes.brtrue:
                case Opcode.SingleOpcodes.brtrue_s:
                    a_loc = il.stack_vars_after.Pop(ass);
                    b_loc = new libasm.const_location { c = 1 };
                    a_p = il.stack_after.Pop();
                    b_p = a_p;
                    break;
                case Opcode.SingleOpcodes.brfalse:
                case Opcode.SingleOpcodes.brfalse_s:
                    a_loc = il.stack_vars_after.Pop(ass);
                    b_loc = new const_location { c = 0 };
                    a_p = il.stack_after.Pop();
                    b_p = a_p;
                    break;
                default:
                    b_loc = il.stack_vars_after.Pop(ass);
                    a_loc = il.stack_vars_after.Pop(ass);
                    b_p = il.stack_after.Pop();
                    a_p = il.stack_after.Pop();
                    break;
            }

            Assembler.CliType a_ct = ass.ResolveNativeInt(a_p.CliType(ass));
            Assembler.CliType b_ct = ass.ResolveNativeInt(b_p.CliType(ass));

            x86_64_Assembler a = ass as x86_64_Assembler;

            a_loc = x86_64_Assembler.ResolveStackLoc(a, state, a_loc);
            b_loc = x86_64_Assembler.ResolveStackLoc(a, state, b_loc);

            if(a_ct != b_ct)
                throw new Exception("Mimatched datatypes on " + il.ToString() + ": " + a_p.ToString() + " and " + b_p.ToString());

            /* decide on the br/set opcode */
            ThreeAddressCode.OpName op = ThreeAddressCode.OpName.invalid;
            bool is_set = false;
            switch (il.il.opcode)
            {
                case 0xfe01:
                    // ceq
                    op = ThreeAddressCode.OpName.seteq;
                    is_set = true;
                    break;
                case 0xfe02:
                    // cgt
                    op = ThreeAddressCode.OpName.setg;
                    is_set = true;
                    break;
                case 0xfe03:
                    // cgt.un
                    op = ThreeAddressCode.OpName.seta;
                    is_set = true;
                    break;
                case 0xfe04:
                    // clt
                    op = ThreeAddressCode.OpName.setl;
                    is_set = true;
                    break;
                case 0xfe05:
                    // clt.un
                    op = ThreeAddressCode.OpName.setb;
                    is_set = true;
                    break;
                default:
                    switch (il.il.opcode.opcode1)
                    {
                        case Opcode.SingleOpcodes.brtrue:
                        case Opcode.SingleOpcodes.brtrue_s:
                        case Opcode.SingleOpcodes.brfalse:
                        case Opcode.SingleOpcodes.brfalse_s:
                        case Opcode.SingleOpcodes.beq:
                        case Opcode.SingleOpcodes.beq_s:
                            op = ThreeAddressCode.OpName.beq;
                            break;
                        case Opcode.SingleOpcodes.bge:
                        case Opcode.SingleOpcodes.bge_s:
                            op = ThreeAddressCode.OpName.bge;
                            break;
                        case Opcode.SingleOpcodes.bge_un:
                        case Opcode.SingleOpcodes.bge_un_s:
                            op = ThreeAddressCode.OpName.bge_un;
                            break;
                        case Opcode.SingleOpcodes.bgt:
                        case Opcode.SingleOpcodes.bgt_s:
                            op = ThreeAddressCode.OpName.bg;
                            break;
                        case Opcode.SingleOpcodes.bgt_un:
                        case Opcode.SingleOpcodes.bgt_un_s:
                            op = ThreeAddressCode.OpName.bg_un;
                            break;
                        case Opcode.SingleOpcodes.ble:
                        case Opcode.SingleOpcodes.ble_s:
                            op = ThreeAddressCode.OpName.ble;
                            break;
                        case Opcode.SingleOpcodes.ble_un:
                        case Opcode.SingleOpcodes.ble_un_s:
                            op = ThreeAddressCode.OpName.ble_un;
                            break;
                        case Opcode.SingleOpcodes.blt:
                        case Opcode.SingleOpcodes.blt_s:
                            op = ThreeAddressCode.OpName.bl;
                            break;
                        case Opcode.SingleOpcodes.blt_un:
                        case Opcode.SingleOpcodes.blt_un_s:
                            op = ThreeAddressCode.OpName.bl_un;
                            break;
                        case Opcode.SingleOpcodes.bne_un:
                        case Opcode.SingleOpcodes.bne_un_s:
                            op = ThreeAddressCode.OpName.bne;
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                    break;
            }

            /* determine the target */
            libasm.hardware_location loc_target;
            if (is_set)
            {
                loc_target = il.stack_vars_after.GetAddressFor(new Signature.Param(BaseType_Type.I4), ass);
                il.stack_after.Push(new Signature.Param(BaseType_Type.I4));
            }
            else
            {
                int il_target = il.il.il_offset_after + il.il.inline_int;
                CilNode target = state.offset_map[il_target];
                loc_target = new hardware_addressoflabel("L" + target.il_label.ToString(), false);
            }

            ass.BrIf(state, il.stack_vars_before, loc_target, a_loc, b_loc, op, a_ct, il.il.tybel);
        }
    }
}

namespace libtysila
{
    partial class x86_64_Assembler
    {
        internal override void BrIf(Encoder.EncoderState state, Stack regs_in_use, hardware_location br_target, hardware_location a_loc, hardware_location b_loc, ThreeAddressCode.OpName op, CliType dt, List<tybel.Node> ret)
        {
            a_loc = ResolveStackLoc(this, state, a_loc);
            b_loc = ResolveStackLoc(this, state, b_loc);
            br_target = ResolveStackLoc(this, state, br_target);
            switch (dt)
            {
                case Assembler.CliType.int32:
                case Assembler.CliType.int64:
                case Assembler.CliType.native_int:
                    if (!(a_loc is x86_64_gpr) && !(b_loc is x86_64_gpr))
                    {
                        if (!(((a_loc is hardware_contentsof) || (a_loc is hardware_stackloc)) && (b_loc is const_location)))
                        {
                            x86_64_Assembler.EncMov(this, state, x86_64_Assembler.Rax, a_loc, dt, ret);
                            a_loc = x86_64_Assembler.Rax;
                        }
                    }
                    break;

                default:
                    throw new NotImplementedException();
            }

            x86_64_asm.opcode cmp_op;
            dt = ResolveNativeInt(dt);
            switch (dt)
            {
                case Assembler.CliType.int32:
                    cmp_op = x86_64_asm.opcode.CMPL;
                    break;
                case Assembler.CliType.int64:
                    if (ia == x86_64_Assembler.IA.i586)
                        throw new NotImplementedException();
                    else
                        cmp_op = x86_64_asm.opcode.CMPQ;
                    break;
                default:
                    throw new NotImplementedException();
            }

            ChooseInstruction(cmp_op, ret, vara.MachineReg(a_loc), vara.MachineReg(b_loc));

            x86_64_asm.opcode setbr_op;
            bool is_set = false;
            switch (op)
            {
                case ThreeAddressCode.OpName.seteq:
                    setbr_op = x86_64_asm.opcode.SETZ;
                    is_set = true;
                    break;
                case ThreeAddressCode.OpName.setg:
                    setbr_op = x86_64_asm.opcode.SETNLE;
                    is_set = true;
                    break;
                case ThreeAddressCode.OpName.seta:
                    setbr_op = x86_64_asm.opcode.SETNBE;
                    is_set = true;
                    break;
                case ThreeAddressCode.OpName.setl:
                    setbr_op = x86_64_asm.opcode.SETL;
                    is_set = true;
                    break;
                case ThreeAddressCode.OpName.setb:
                    setbr_op = x86_64_asm.opcode.SETB;
                    is_set = true;
                    break;
                case ThreeAddressCode.OpName.beq:
                    setbr_op = x86_64_asm.opcode.JZ;
                    break;
                case ThreeAddressCode.OpName.bge:
                    setbr_op = x86_64_asm.opcode.JNL;
                    break;
                case ThreeAddressCode.OpName.bge_un:
                    setbr_op = x86_64_asm.opcode.JNB;
                    break;
                case ThreeAddressCode.OpName.bg:
                    setbr_op = x86_64_asm.opcode.JNLE;
                    break;
                case ThreeAddressCode.OpName.bg_un:
                    setbr_op = x86_64_asm.opcode.JNBE;
                    break;
                case ThreeAddressCode.OpName.ble:
                    setbr_op = x86_64_asm.opcode.JLE;
                    break;
                case ThreeAddressCode.OpName.ble_un:
                    setbr_op = x86_64_asm.opcode.JBE;
                    break;
                case ThreeAddressCode.OpName.bl:
                    setbr_op = x86_64_asm.opcode.JL;
                    break;
                case ThreeAddressCode.OpName.bl_un:
                    setbr_op = x86_64_asm.opcode.JB;
                    break;
                case ThreeAddressCode.OpName.bne:
                    setbr_op = x86_64_asm.opcode.JNZ;
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (is_set)
            {
                ChooseInstruction(setbr_op, ret, vara.MachineReg(x86_64_Assembler.Rax));
                ChooseInstruction(x86_64_asm.opcode.MOVZXB, ret, vara.MachineReg(x86_64_Assembler.Rax), vara.MachineReg(x86_64_Assembler.Rax));

                Assign(state, regs_in_use, br_target, x86_64_Assembler.Rax, Assembler.CliType.int32, ret);
            }
            else
            {
                if(!(br_target is libasm.hardware_addressoflabel))
                    throw new Exception("BrIf without addressoflabel as target");
                libasm.hardware_addressoflabel aol = br_target as libasm.hardware_addressoflabel;
                ChooseInstruction(setbr_op, ret, vara.Label(aol.label, aol.const_offset, aol.is_object));
            }
        }
    }
}

