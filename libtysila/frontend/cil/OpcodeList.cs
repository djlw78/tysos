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

namespace libtysila.frontend.cil
{
    public class OpcodeList
    {
        public static Dictionary<int, Opcode> Opcodes = new Dictionary<int, Opcode>(new libtysila.GenericEqualityComparer<int>());

        static OpcodeList()
        {
            InitOpcodes();
        }

        public static void InitOpcodes()
        {
            Opcodes.Add(0x00, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x00, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "nop", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x01, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x01, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "break", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.BREAK });
            Opcodes.Add(0x02, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x02, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldarg.0", Encoder = OpcodeEncodings.args.ldarg, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x03, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x03, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldarg.1", Encoder = OpcodeEncodings.args.ldarg, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x04, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x04, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldarg.2", Encoder = OpcodeEncodings.args.ldarg, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x05, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x05, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldarg.3", Encoder = OpcodeEncodings.args.ldarg, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x06, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x06, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldloc.0", Encoder = OpcodeEncodings.loc.ldloc, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x07, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x07, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldloc.1", Encoder = OpcodeEncodings.loc.ldloc, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x08, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x08, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldloc.2", Encoder = OpcodeEncodings.loc.ldloc, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x09, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x09, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldloc.3", Encoder = OpcodeEncodings.loc.ldloc, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x0A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x0A, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stloc.0", Encoder = OpcodeEncodings.loc.stloc, pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x0B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x0B, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stloc.1", Encoder = OpcodeEncodings.loc.stloc, pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x0C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x0C, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stloc.2", Encoder = OpcodeEncodings.loc.stloc, pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x0D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x0D, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stloc.3", Encoder = OpcodeEncodings.loc.stloc, pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x0E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x0E, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldarg.s", Encoder = OpcodeEncodings.args.ldarg, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.ShortInlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x0F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x0F, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldarga.s", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.ShortInlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x10, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x10, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "starg.s", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x11, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x11, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldloc.s", Encoder = OpcodeEncodings.loc.ldloc, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.ShortInlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x12, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x12, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldloca.s", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.ShortInlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x13, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x13, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stloc.s", Encoder = OpcodeEncodings.loc.stloc, pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x14, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x14, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldnull", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushRef, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x15, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x15, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.m1", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x16, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x16, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.0", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x17, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x17, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.1", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x18, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x18, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.2", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x19, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x19, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.3", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x1A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x1A, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.4", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x1B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x1B, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.5", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x1C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x1C, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.6", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x1D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x1D, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.7", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x1E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x1E, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.8", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x1F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x1F, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4.s", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.ShortInlineI, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x20, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x20, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i4", Encoder = OpcodeEncodings.ldc.ldc_i4, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineI, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x21, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x21, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.i8", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI8, inline = Opcode.InlineVar.InlineI8, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x22, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x22, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.r4", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushR4, inline = Opcode.InlineVar.ShortInlineR, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x23, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x23, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldc.r8", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushR8, inline = Opcode.InlineVar.InlineR, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x24, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x24, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x25, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x25, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "dup", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1 + (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x26, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x26, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "pop", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x27, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x27, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "jmp", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineMethod, ctrl = Opcode.ControlFlow.CALL });
            Opcodes.Add(0x28, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x28, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "call", Encoder = OpcodeEncodings.call.enc_call, pop = (int)Opcode.PopBehaviour.VarPop, push = (int)Opcode.PushBehaviour.VarPush, inline = Opcode.InlineVar.InlineMethod, ctrl = Opcode.ControlFlow.CALL });
            Opcodes.Add(0x29, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x29, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "calli", Encoder = OpcodeEncodings.call.enc_call, pop = (int)Opcode.PopBehaviour.VarPop, push = (int)Opcode.PushBehaviour.VarPush, inline = Opcode.InlineVar.InlineSig, ctrl = Opcode.ControlFlow.CALL });
            Opcodes.Add(0x2A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x2A, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ret", Encoder = OpcodeEncodings.Return.ret, pop = (int)Opcode.PopBehaviour.VarPop, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.RETURN });
            Opcodes.Add(0x2B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x2B, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "br.s", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.BRANCH });
            Opcodes.Add(0x2C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x2C, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "brfalse.s", Encoder = OpcodeEncodings.br.br_one, pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x2D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x2D, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "brtrue.s", Encoder = OpcodeEncodings.br.br_one, pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x2E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x2E, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "beq.s", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x2F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x2F, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "bge.s", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x30, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x30, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "bgt.s", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x31, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x31, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ble.s", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x32, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x32, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "blt.s", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x33, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x33, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "bne.un.s", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x34, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x34, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "bge.un.s", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x35, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x35, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "bgt.un.s", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x36, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x36, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ble.un.s", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x37, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x37, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "blt.un.s", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x38, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x38, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "br", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.BRANCH });
            Opcodes.Add(0x39, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x39, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "brfalse", Encoder = OpcodeEncodings.br.br_one, pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x3A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x3A, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "brtrue", Encoder = OpcodeEncodings.br.br_one, pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x3B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x3B, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "beq", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x3C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x3C, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "bge", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x3D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x3D, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "bgt", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x3E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x3E, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ble", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x3F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x3F, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "blt", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x40, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x40, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "bne.un", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x41, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x41, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "bge.un", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x42, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x42, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "bgt.un", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x43, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x43, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ble.un", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x44, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x44, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "blt.un", Encoder = OpcodeEncodings.br.br_two, pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x45, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x45, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "switch", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineSwitch, ctrl = Opcode.ControlFlow.COND_BRANCH });
            Opcodes.Add(0x46, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x46, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.i1", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x47, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x47, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.u1", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x48, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x48, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.i2", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x49, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x49, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.u2", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x4A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x4A, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.i4", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x4B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x4B, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.u4", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x4C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x4C, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.i8", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x4D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x4D, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.i", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x4E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x4E, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.r4", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushR4, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x4F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x4F, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.r8", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushR8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x50, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x50, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldind.ref", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushRef, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x51, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x51, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stind.ref", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x52, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x52, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stind.i1", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x53, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x53, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stind.i2", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x54, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x54, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stind.i4", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x55, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x55, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stind.i8", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI8, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x56, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x56, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stind.r4", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopR4, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x57, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x57, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stind.r8", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopR8, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x58, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x58, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "add", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x59, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x59, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "sub", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x5A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x5A, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "mul", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x5B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x5B, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "div", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x5C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x5C, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "div.un", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x5D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x5D, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "rem", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x5E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x5E, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "rem.un", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x5F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x5F, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "and", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x60, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x60, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "or", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x61, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x61, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "xor", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x62, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x62, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "shl", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x63, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x63, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "shr", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x64, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x64, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "shr.un", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x65, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x65, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "neg", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x66, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x66, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "not", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x67, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x67, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.i1", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x68, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x68, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.i2", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x69, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x69, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.i4", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x6A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x6A, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.i8", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x6B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x6B, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.r4", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushR4, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x6C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x6C, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.r8", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushR8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x6D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x6D, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.u4", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x6E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x6E, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.u8", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x6F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x6F, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "callvirt", Encoder = OpcodeEncodings.call.enc_call, pop = (int)Opcode.PopBehaviour.VarPop, push = (int)Opcode.PushBehaviour.VarPush, inline = Opcode.InlineVar.InlineMethod, ctrl = Opcode.ControlFlow.CALL });
            Opcodes.Add(0x70, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x70, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "cpobj", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x71, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x71, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldobj", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x72, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x72, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldstr", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushRef, inline = Opcode.InlineVar.InlineString, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x73, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x73, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "newobj", pop = (int)Opcode.PopBehaviour.VarPop, push = (int)Opcode.PushBehaviour.PushRef, inline = Opcode.InlineVar.InlineMethod, ctrl = Opcode.ControlFlow.CALL });
            Opcodes.Add(0x74, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x74, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "castclass", pop = (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.PushRef, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x75, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x75, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "isinst", pop = (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x76, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x76, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.r.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushR8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x77, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x77, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x78, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x78, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x79, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x79, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unbox", pop = (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x7A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x7A, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "throw", pop = (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.THROW });
            Opcodes.Add(0x7B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x7B, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldfld", pop = (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineField, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x7C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x7C, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldflda", pop = (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineField, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x7D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x7D, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stfld", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineField, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x7E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x7E, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldsfld", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineField, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x7F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x7F, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldsflda", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineField, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x80, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x80, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stsfld", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineField, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x81, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x81, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stobj", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x82, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x82, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.i1.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x83, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x83, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.i2.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x84, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x84, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.i4.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x85, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x85, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.i8.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x86, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x86, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.u1.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x87, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x87, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.u2.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x88, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x88, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.u4.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x89, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x89, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.u8.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x8A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x8A, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.i.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x8B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x8B, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.u.un", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x8C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x8C, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "box", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushRef, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x8D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x8D, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "newarr", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushRef, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x8E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x8E, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldlen", pop = (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x8F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x8F, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelema", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x90, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x90, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.i1", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x91, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x91, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.u1", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x92, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x92, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.i2", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x93, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x93, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.u2", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x94, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x94, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.i4", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x95, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x95, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.u4", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x96, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x96, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.i8", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x97, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x97, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.i", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x98, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x98, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.r4", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushR4, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x99, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x99, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.r8", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushR8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x9A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x9A, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem.ref", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushRef, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x9B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x9B, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stelem.i", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x9C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x9C, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stelem.i1", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x9D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x9D, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stelem.i2", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x9E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x9E, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stelem.i4", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0x9F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0x9F, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stelem.i8", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI8, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xA0, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xA0, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stelem.r4", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopR4, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xA1, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xA1, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stelem.r8", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopR8, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xA2, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xA2, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stelem.ref", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xA3, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xA3, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldelem", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xA4, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xA4, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stelem", pop = (int)Opcode.PopBehaviour.PopRef + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xA5, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xA5, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unbox.any", pop = (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xA6, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xA6, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xA7, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xA7, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xA8, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xA8, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xA9, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xA9, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xAA, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xAA, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xAB, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xAB, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xAC, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xAC, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xAD, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xAD, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xAE, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xAE, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xAF, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xAF, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xB0, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xB0, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xB1, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xB1, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xB2, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xB2, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xB3, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xB3, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.i1", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xB4, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xB4, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.u1", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xB5, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xB5, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.i2", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xB6, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xB6, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.u2", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xB7, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xB7, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.i4", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xB8, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xB8, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.u4", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xB9, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xB9, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.i8", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xBA, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xBA, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.u8", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xBB, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xBB, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xBC, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xBC, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xBD, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xBD, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xBE, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xBE, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xBF, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xBF, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xC0, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xC0, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xC1, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xC1, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xC2, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xC2, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "refanyval", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xC3, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xC3, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ckfinite", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushR8, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xC4, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xC4, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xC5, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xC5, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xC6, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xC6, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "mkrefany", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xC7, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xC7, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xC8, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xC8, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xC9, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xC9, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xCA, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xCA, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xCB, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xCB, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xCC, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xCC, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xCD, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xCD, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xCE, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xCE, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xCF, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xCF, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xD0, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xD0, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "ldtoken", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineTok, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xD1, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xD1, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.u2", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xD2, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xD2, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.u1", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xD3, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xD3, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.i", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xD4, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xD4, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.i", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xD5, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xD5, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.ovf.u", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xD6, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xD6, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "add.ovf", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xD7, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xD7, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "add.ovf.un", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xD8, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xD8, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "mul.ovf", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xD9, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xD9, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "mul.ovf.un", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xDA, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xDA, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "sub.ovf", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xDB, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xDB, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "sub.ovf.un", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xDC, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xDC, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "endfinally", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.RETURN });
            Opcodes.Add(0xDD, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xDD, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "leave", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineBrTarget, ctrl = Opcode.ControlFlow.BRANCH, directly_modifies_stack = true });
            Opcodes.Add(0xDE, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xDE, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "leave.s", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineBrTarget, ctrl = Opcode.ControlFlow.BRANCH, directly_modifies_stack = true });
            Opcodes.Add(0xDF, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xDF, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "stind.i", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xE0, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xE0, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "conv.u", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xE1, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xE1, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xE2, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xE2, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xE3, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xE3, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xE4, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xE4, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xE5, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xE5, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xE6, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xE6, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xE7, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xE7, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xE8, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xE8, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xE9, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xE9, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xEA, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xEA, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xEB, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xEB, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xEC, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xEC, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xED, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xED, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xEE, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xEE, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xEF, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xEF, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xF0, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xF0, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xF1, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xF1, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xF2, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xF2, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xF3, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xF3, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xF4, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xF4, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xF5, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xF5, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xF6, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xF6, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xF7, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xF7, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xF8, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xF8, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "prefix7", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xF9, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xF9, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "prefix6", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xFA, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFA, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "prefix5", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xFB, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFB, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "prefix4", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xFC, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFC, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "prefix3", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xFD, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFD, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "prefix2", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xFE, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "prefix1", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xFF, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFF, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "prefixref", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xFE00, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x00, name = "arglist", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE01, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x01, name = "ceq", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE02, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x02, name = "cgt", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE03, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x03, name = "cgt.un", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE04, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x04, name = "clt", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE05, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x05, name = "clt.un", pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE06, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x06, name = "ldftn", Encoder = OpcodeEncodings.call.enc_call, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineMethod, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE07, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x07, name = "ldvirtftn", Encoder = OpcodeEncodings.call.enc_call, pop = (int)Opcode.PopBehaviour.PopRef, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineMethod, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE08, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x08, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE09, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x09, name = "ldarg", Encoder = OpcodeEncodings.args.ldarg, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE0A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x0A, name = "ldarga", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE0B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x0B, name = "starg", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE0C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x0C, name = "ldloc", Encoder = OpcodeEncodings.loc.ldloc, pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push1, inline = Opcode.InlineVar.InlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE0D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x0D, name = "ldloca", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE0E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x0E, name = "stloc", Encoder = OpcodeEncodings.loc.stloc, pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineVar, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE0F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x0F, name = "localloc", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE10, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x10, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE11, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x11, name = "endfilter", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.RETURN });
            Opcodes.Add(0xFE12, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x12, name = "unaligned.", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.ShortInlineI, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xFE13, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x13, name = "volatile.", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xFE14, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x14, name = "tail.", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.META });
            Opcodes.Add(0xFE15, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x15, name = "initobj", pop = (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE16, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x16, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE17, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x17, name = "cpblk", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE18, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x18, name = "initblk", pop = (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI + (int)Opcode.PopBehaviour.PopI, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE19, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x19, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE1A, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x1A, name = "rethrow", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.THROW });
            Opcodes.Add(0xFE1B, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x1B, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE1C, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x1C, name = "sizeof", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineType, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE1D, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x1D, name = "refanytype", pop = (int)Opcode.PopBehaviour.Pop1, push = (int)Opcode.PushBehaviour.PushI, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE1E, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x1E, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE1F, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x1F, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE20, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x20, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE21, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x21, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });
            Opcodes.Add(0xFE22, new Opcode { opcode1 = (Opcode.SingleOpcodes)0xFE, opcode2 = (Opcode.DoubleOpcodes)0x22, name = "unused", pop = (int)Opcode.PopBehaviour.Pop0, push = (int)Opcode.PushBehaviour.Push0, inline = Opcode.InlineVar.InlineNone, ctrl = Opcode.ControlFlow.NEXT });

            // The following are tysila dependent
            Opcodes.Add(0xFD20, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.flip,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "flip",
                directly_modifies_stack = true
            });
            Opcodes.Add(0xFD21, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.flip3,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "flip3",
                directly_modifies_stack = true
            });
            Opcodes.Add(0xfd22, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.init_rth,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "init_rth"
            });
            Opcodes.Add(0xfd23, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.castclassex,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "castclassex",
                directly_modifies_stack = true
            });
            Opcodes.Add(0xfd24, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.throwfalse,
                pop = (int)Opcode.PopBehaviour.PopI,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "throwfalse"
            });
            Opcodes.Add(0xfd25, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.ldelem_vt,
                pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1,
                push = (int)Opcode.PushBehaviour.Push1,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "ldelem_vt"
            });
            Opcodes.Add(0xfd26, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.init_rmh,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "init_mth"
            });
            Opcodes.Add(0xfd27, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.init_rfh,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "init_rfh"
            });
            Opcodes.Add(0xfd28, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.stelem_vt,
                pop = (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1 + (int)Opcode.PopBehaviour.Pop1,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "stelem_vt"
            });
            Opcodes.Add(0xfd29, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.profile,
                pop = (int)Opcode.PopBehaviour.PopI,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "profile"
            });
            Opcodes.Add(0xfd2a, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.gcmalloc,
                pop = (int)Opcode.PopBehaviour.Pop1,
                push = (int)Opcode.PushBehaviour.PushI,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "gcmalloc"
            });
            Opcodes.Add(0xfd2b, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.ldobj_addr,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.PushI,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "ldobj_addr"
            });
            Opcodes.Add(0xfd2c, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.mbstrlen,
                pop = (int)Opcode.PopBehaviour.Pop1,
                push = (int)Opcode.PushBehaviour.PushI,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "mbstrlen"
            });
            Opcodes.Add(0xfd2d, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.loadcatchobj,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.PushI,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "loadcatchobj"
            });
            Opcodes.Add(0xfd2e, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.instruction_label,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "instruction_label"
            });
            Opcodes.Add(0xfd2f, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.pushback,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.Push0,
                directly_modifies_stack = true,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "pushback"
            });
            Opcodes.Add(0xfd30, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.throwtrue,
                pop = (int)Opcode.PopBehaviour.PopI,
                push = (int)Opcode.PushBehaviour.Push0,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "throwtrue"
            });
            Opcodes.Add(0xfd31, new Opcode
            {
                opcode1 = Opcode.SingleOpcodes.tysila,
                opcode2 = Opcode.DoubleOpcodes.bringforward,
                pop = (int)Opcode.PopBehaviour.Pop0,
                push = (int)Opcode.PushBehaviour.Push0,
                directly_modifies_stack = true,
                ctrl = Opcode.ControlFlow.NEXT,
                name = "bringforward"
            });
        }
    }
}
