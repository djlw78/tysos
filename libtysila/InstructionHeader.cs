﻿/* Copyright (C) 2008 - 2011 by John Cronin
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
using libasm;

namespace libtysila
{
    public class InstructionHeader : OutputBlock
    {
        public Assembler.InstructionLine instr;
        public int il_offset;
        public int compiled_offset;
        internal Assembler ass;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("IL_" + il_offset.ToString("x4") + ": " + instr.opcode.name);

            try
            {
                switch (instr.opcode.inline)
                {
                    case Assembler.InlineVar.InlineMethod:
                        {
                            Token t = instr.inline_tok;

                            if ((t.Value is Metadata.MethodDefRow) || (t.Value is Metadata.MethodSpecRow) || (t.Value is Metadata.MemberRefRow))
                            {
                                sb.Append(" ");
                                sb.Append(Mangler2.MangleMethod(Metadata.GetMTC(new Metadata.TableIndex(t), instr.cfg_node.containing_meth.GetTTC(ass), instr.cfg_node.containing_meth.msig, ass), ass));
                            }
                        }
                        break;
                }
            }
            catch (Exception) { }

            return sb.ToString();            
        }
    }

}
