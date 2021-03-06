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
    class numop
    {
        class binnumop_key
        {
            public Assembler.CliType a_type;
            public Assembler.CliType b_type;
            public int opcode;

            public override int GetHashCode()
            {
                int hc = a_type.GetHashCode();
                hc <<= 12;
                hc ^= b_type.GetHashCode();
                hc <<= 12;
                hc ^= opcode.GetHashCode();
                return hc;
            }

            public override bool Equals(object obj)
            {
                binnumop_key other = obj as binnumop_key;
                if (obj == null)
                    return false;
                if (a_type != other.a_type)
                    return false;
                if (b_type != other.b_type)
                    return false;
                if (opcode != other.opcode)
                    return false;
                return true;
            }

            public override string ToString()
            {
                return a_type.ToString() + ", " + b_type.ToString() + ", " + OpcodeList.Opcodes[opcode].ToString();
            }
        }

        class numop_val
        {
            public Assembler.CliType dt;
            public ThreeAddressCode.Op op;
            public ThreeAddressCode.Op throw_op = ThreeAddressCode.Op.OpNull(ThreeAddressCode.OpName.invalid);
            public bool ovf = false;
            public bool un = false;
        }

        static Dictionary<binnumop_key, numop_val> binnumops = new Dictionary<binnumop_key, numop_val>();

        static numop()
        {
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.add) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.add) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.reference, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.reference, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.reference, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.reference, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F32, b_type = Assembler.CliType.F32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.F32, op = ThreeAddressCode.Op.OpR4(ThreeAddressCode.OpName.add) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F64, b_type = Assembler.CliType.F64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add) }] = new numop_val { dt = Assembler.CliType.F64, op = ThreeAddressCode.Op.OpR8(ThreeAddressCode.OpName.add) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.add), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.add), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), ovf = true };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf_un) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.add), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf_un) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.add), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.reference, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf_un) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.reference, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf_un) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.reference, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf_un) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.reference, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.add_ovf_un) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.add), ovf = true, un = true };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.sub) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.sub) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.reference, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.sub) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.reference, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F32, b_type = Assembler.CliType.F32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub) }] = new numop_val { dt = Assembler.CliType.F32, op = ThreeAddressCode.Op.OpR4(ThreeAddressCode.OpName.sub) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F64, b_type = Assembler.CliType.F64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub) }] = new numop_val { dt = Assembler.CliType.F64, op = ThreeAddressCode.Op.OpR8(ThreeAddressCode.OpName.sub) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.reference, b_type = Assembler.CliType.reference, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.sub), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.sub), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub), ovf = true };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf_un) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.sub), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf_un) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.sub), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.reference, b_type = Assembler.CliType.reference, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.reference, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf_un) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub), ovf = true, un = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.reference, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.sub_ovf_un) }] = new numop_val { dt = Assembler.CliType.reference, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.sub), ovf = true, un = true };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.mul) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.mul) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.mul) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.mul) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.mul) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F32, b_type = Assembler.CliType.F32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul) }] = new numop_val { dt = Assembler.CliType.F32, op = ThreeAddressCode.Op.OpR4(ThreeAddressCode.OpName.mul) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F64, b_type = Assembler.CliType.F64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul) }] = new numop_val { dt = Assembler.CliType.F64, op = ThreeAddressCode.Op.OpR8(ThreeAddressCode.OpName.mul) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul_ovf) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.mul), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul_ovf) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.mul), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul_ovf) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.mul), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul_ovf) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.mul), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul_ovf) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.mul), ovf = true };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul_ovf_un) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.mul_un), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul_ovf_un) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.mul_un), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul_ovf_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.mul_un), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul_ovf_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.mul_un), ovf = true };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.mul_ovf_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.mul_un), ovf = true };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div_un) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.div_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div_un) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.div_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.div_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.div_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.div_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F32, b_type = Assembler.CliType.F32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div_un) }] = new numop_val { dt = Assembler.CliType.F32, op = ThreeAddressCode.Op.OpR4(ThreeAddressCode.OpName.div_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F64, b_type = Assembler.CliType.F64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div_un) }] = new numop_val { dt = Assembler.CliType.F64, op = ThreeAddressCode.Op.OpR8(ThreeAddressCode.OpName.div_un) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.div) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.div) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.div) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.div) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.div) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F32, b_type = Assembler.CliType.F32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div) }] = new numop_val { dt = Assembler.CliType.F32, op = ThreeAddressCode.Op.OpR4(ThreeAddressCode.OpName.div) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F64, b_type = Assembler.CliType.F64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.div) }] = new numop_val { dt = Assembler.CliType.F64, op = ThreeAddressCode.Op.OpR8(ThreeAddressCode.OpName.div) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.rem) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.rem) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.rem) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.rem) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.rem) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F32, b_type = Assembler.CliType.F32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem) }] = new numop_val { dt = Assembler.CliType.F32, op = ThreeAddressCode.Op.OpR4(ThreeAddressCode.OpName.rem) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F64, b_type = Assembler.CliType.F64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem) }] = new numop_val { dt = Assembler.CliType.F64, op = ThreeAddressCode.Op.OpR8(ThreeAddressCode.OpName.rem) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem_un) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.rem_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem_un) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.rem_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.rem_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.rem_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.rem_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F32, b_type = Assembler.CliType.F32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem_un) }] = new numop_val { dt = Assembler.CliType.F32, op = ThreeAddressCode.Op.OpR4(ThreeAddressCode.OpName.rem_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F64, b_type = Assembler.CliType.F64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.rem_un) }] = new numop_val { dt = Assembler.CliType.F64, op = ThreeAddressCode.Op.OpR8(ThreeAddressCode.OpName.rem_un) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.and) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.and) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.and) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.and) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.and) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.and) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.and) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.and) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.and) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.and) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.or) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.or) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.or) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.or) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.or) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.or) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.or) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.or) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.or) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.or) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.xor) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.xor) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int64, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.xor) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.xor) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.xor) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.xor) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.xor) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.xor) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.xor) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.xor) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shl) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.shl) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shl) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.shl) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shl) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.shl) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shl) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.shl) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shl) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.shl) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shl) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.shl) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.shr) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.shr) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.shr) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.shr) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.shr) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.shr) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr_un) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.shr_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr_un) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.shr_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.int32, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.shr_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr_un) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.shr_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr_un) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.shr_un) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.native_int, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.shr_un) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.shr_un) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.none, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.neg) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.neg) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.none, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.neg) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.neg) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.none, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.neg) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.neg) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F32, b_type = Assembler.CliType.none, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.neg) }] = new numop_val { dt = Assembler.CliType.F32, op = ThreeAddressCode.Op.OpR4(ThreeAddressCode.OpName.neg) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.F64, b_type = Assembler.CliType.none, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.neg) }] = new numop_val { dt = Assembler.CliType.F64, op = ThreeAddressCode.Op.OpR8(ThreeAddressCode.OpName.neg) };

            binnumops[new binnumop_key { a_type = Assembler.CliType.int32, b_type = Assembler.CliType.none, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.not) }] = new numop_val { dt = Assembler.CliType.int32, op = ThreeAddressCode.Op.OpI4(ThreeAddressCode.OpName.not) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.int64, b_type = Assembler.CliType.none, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.not) }] = new numop_val { dt = Assembler.CliType.int64, op = ThreeAddressCode.Op.OpI8(ThreeAddressCode.OpName.not) };
            binnumops[new binnumop_key { a_type = Assembler.CliType.native_int, b_type = Assembler.CliType.none, opcode = Opcode.OpcodeVal(Opcode.SingleOpcodes.not) }] = new numop_val { dt = Assembler.CliType.native_int, op = ThreeAddressCode.Op.OpI(ThreeAddressCode.OpName.not) };
        }

        public static void tybel_binnumop(frontend.cil.CilNode il, Assembler ass, Assembler.MethodToCompile mtc, ref int next_block,
            Encoder.EncoderState state, Assembler.MethodAttributes attrs)
        {
            libasm.hardware_location b_loc = il.stack_vars_after.Pop(ass);
            libasm.hardware_location a_loc = il.stack_vars_after.Pop(ass);
            Signature.Param b_p = il.stack_after.Pop();
            Signature.Param a_p = il.stack_after.Pop();

            Assembler.CliType a_ct = a_p.CliType(ass);
            Assembler.CliType b_ct = b_p.CliType(ass);

            binnumop_key k = new binnumop_key { a_type = a_ct, b_type = b_ct, opcode = il.il.opcode.opcode };
            numop_val v;
            if(!binnumops.TryGetValue(k, out v))
                throw new Exception("Invalid binary num op combination: " + k.ToString());

            Signature.Param d_p = new Signature.Param(v.dt);
            libasm.hardware_location d_loc = il.stack_vars_after.GetAddressFor(d_p, ass);
            il.stack_after.Push(d_p);

            if(v.ovf)
            {
                ass.EmitWarning(new Assembler.AssemblerException(il.il.opcode.ToString() + ": request for overflow check " +
                    "but this is currently not implemented", il.il, mtc));
            }

            ass.NumOp(state, il.stack_vars_before, d_loc, a_loc, b_loc, v.op, il.il.tybel);

            if(v.throw_op.Operator != ThreeAddressCode.OpName.invalid)
                throw new NotImplementedException();
        }

        public static void tybel_unnumop(frontend.cil.CilNode il, Assembler ass, Assembler.MethodToCompile mtc, ref int next_block,
            Encoder.EncoderState state, Assembler.MethodAttributes attrs)
        {
            libasm.hardware_location a_loc = il.stack_vars_after.Pop(ass);
            Signature.Param a_p = il.stack_after.Pop();

            Assembler.CliType a_ct = a_p.CliType(ass);

            binnumop_key k = new binnumop_key { a_type = a_ct, b_type = Assembler.CliType.none, opcode = il.il.opcode.opcode };
            numop_val v;
            if (!binnumops.TryGetValue(k, out v))
                throw new Exception("Invalid binary num op combination: " + k.ToString());

            Signature.Param d_p = new Signature.Param(v.dt);
            libasm.hardware_location d_loc = il.stack_vars_after.GetAddressFor(d_p, ass);
            il.stack_after.Push(d_p);

            ass.NumOp(state, il.stack_vars_before, d_loc, a_loc, null, v.op, il.il.tybel);

            if (v.throw_op.Operator != ThreeAddressCode.OpName.invalid)
                throw new NotImplementedException();
        }

        public static void binnumop(InstructionLine il, Assembler ass, Assembler.MethodToCompile mtc, ref int next_variable,
            ref int next_block, List<vara> la_vars, List<vara> lv_vars, List<Signature.Param> las, List<Signature.Param> lvs,
            Assembler.MethodAttributes attrs)
        {
            vara b = il.stack_vars_after.Pop();
            vara a = il.stack_vars_after.Pop();
            il.stack_after.Pop();
            il.stack_after.Pop();

            binnumop_key k = new binnumop_key { a_type = a.DataType, b_type = b.DataType, opcode = il.opcode.opcode };
            numop_val v;
            if (!binnumops.TryGetValue(k, out v))
                throw new Exception("Invalid binary num op combination: " + k.ToString());

            vara r = vara.Logical(next_variable++, v.dt);
            il.tacs.Add(new timple.TimpleNode(v.op, r, a, b));
            if (v.throw_op != ThreeAddressCode.Op.OpNull(ThreeAddressCode.OpName.invalid))
                il.tacs.Add(new timple.TimpleNode(v.throw_op, vara.Void(), vara.Void(), vara.Void()));

            il.stack_vars_after.Push(r);
            il.stack_after.Push(new Signature.Param(v.dt));
        }
    }
}
