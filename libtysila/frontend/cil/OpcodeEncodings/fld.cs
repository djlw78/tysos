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

namespace libtysila.frontend.cil.OpcodeEncodings
{
    class fld
    {
        public static void ldstfld(InstructionLine il, Assembler ass, Assembler.MethodToCompile mtc, ref int next_variable,
            ref int next_block, List<vara> la_vars, List<vara> lv_vars, List<Signature.Param> las, List<Signature.Param> lvs,
            Assembler.MethodAttributes attrs)
        {
            Assembler.FieldToCompile ftc = (il.inline_tok is FTCToken) ? ((FTCToken)il.inline_tok).ftc : Metadata.GetFTC(il.inline_tok, mtc.GetTTC(ass), mtc.msig, ass);

            bool fld_is_static = ftc.field.IsStatic;
            bool request_is_static = false;
            bool request_is_load = false;
            bool request_is_address = false;
            switch (il.opcode.opcode1)
            {
                case Opcode.SingleOpcodes.ldsfld:
                    request_is_load = true;
                    request_is_address = false;
                    request_is_static = true;
                    break;

                case Opcode.SingleOpcodes.ldsflda:
                    request_is_load = true;
                    request_is_address = true;
                    request_is_static = true;
                    break;

                case Opcode.SingleOpcodes.ldfld:
                    request_is_load = true;
                    request_is_address = false;
                    request_is_static = false;
                    break;

                case Opcode.SingleOpcodes.ldflda:
                    request_is_load = true;
                    request_is_address = true;
                    request_is_static = false;
                    break;

                case Opcode.SingleOpcodes.stsfld:
                    request_is_load = false;
                    request_is_address = false;
                    request_is_static = true;
                    break;

                case Opcode.SingleOpcodes.stfld:
                    request_is_load = false;
                    request_is_address = false;
                    request_is_static = false;
                    break;

                default:
                    throw new Exception("Invalid fld opcode: " + il.ToString());
            }

            bool do_static = false;
            if (request_is_static && fld_is_static)
                do_static = true;
            else if (!request_is_static && fld_is_static)
                do_static = true;
            else if (request_is_static && !fld_is_static)
                throw new Exception("Request for non-static field at " + il.ToString());
            else
                do_static = false;

            vara val;
            Signature.Param val_type;
            if (!request_is_load)
            {
                val = il.stack_vars_after.Pop();
                val_type = il.stack_after.Pop();
            }
            else
                val = vara.Void();

            vara obj;
            Signature.Param obj_type;
            if (!request_is_static)
            {
                obj = il.stack_vars_after.Pop();
                obj_type = il.stack_after.Pop();
            }
            else
            {
                obj = vara.Void();
                obj_type = ftc.definedin_tsig;
            }

            Assembler.TypeToCompile obj_ttc = new Assembler.TypeToCompile(obj_type, ass);
            Layout obj_l = Layout.GetLayout(obj_ttc, ass);
            int fld_offset = obj_l.GetField(ftc.field.Name, do_static).offset;

            /* Load field address */
            vara fld_address = vara.Logical(next_variable++, Assembler.CliType.native_int);
            if (do_static)
                il.tacs.Add(new timple.TimpleNode(ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.assign), fld_address, vara.Label(obj_l.static_object_name, fld_offset, true), vara.Void()));
            else
            {
                if (fld_offset != 0)
                    il.tacs.Add(new timple.TimpleNode(ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), fld_address, obj, vara.Const(new IntPtr(fld_offset), Assembler.CliType.native_int)));
                else
                    fld_address = obj;
            }

            if (request_is_address)
            {
                il.stack_after.Push(new Signature.Param(BaseType_Type.I));
                il.stack_vars_after.Push(fld_address);
                return;
            }

            /* Do load/store */
            Assembler.CliType dt = ftc.fsig.CliType(ass);
            if (request_is_load)
            {
                ThreeAddressCode.Op op = Assembler.GetPeekTac(ftc.fsig, ass);
                vara ret = vara.Logical(next_variable++, dt);
                il.tacs.Add(new timple.TimpleNode(op, ret, fld_address, vara.Void()));
                il.stack_vars_after.Push(ret);
                il.stack_after.Push(ftc.fsig);
            }
            else
            {
                ThreeAddressCode.Op op = Assembler.GetPokeTac(ftc.fsig, ass);
                il.tacs.Add(new timple.TimpleNode(op, vara.Void(), fld_address, val));
            }
        }
    }
}
