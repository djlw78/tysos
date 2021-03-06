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
    class DecomposeComplexOpcodes
    {
        internal static Dictionary<int, DecomposeFunc> DecomposeOpcodeList = new Dictionary<int, DecomposeFunc>(new libtysila.GenericEqualityComparer<int>());
        internal delegate CilNode DecomposeFunc(CilNode n, Assembler ass, Assembler.MethodToCompile mtc, ref int next_block, Assembler.MethodAttributes attrs);

        static DecomposeComplexOpcodes()
        {
            DecomposeOpcodeList[Opcode.OpcodeVal(Opcode.SingleOpcodes.isinst)] = DecomposeOpcodes.isinst.Decompose_isinst;
            DecomposeOpcodeList[Opcode.OpcodeVal(Opcode.SingleOpcodes.unbox)] = DecomposeOpcodes.box.Decompose_unbox;
            //DecomposeOpcodeList[Opcode.OpcodeVal(Opcode.SingleOpcodes.unbox_any)] = DecomposeOpcodes.box.Decompose_unboxany;
            DecomposeOpcodeList[Opcode.OpcodeVal(Opcode.SingleOpcodes.castclass)] = DecomposeOpcodes.isinst.Decompose_castclass;
            DecomposeOpcodeList[Opcode.OpcodeVal(Opcode.SingleOpcodes.ldtoken)] = DecomposeOpcodes.ldtoken.Decompose_ldtoken;
        }

        internal static CilNode DecomposeComplexOpts(CilNode n, Assembler ass, Assembler.MethodToCompile mtc, ref int next_block, Assembler.MethodAttributes attrs)
        {
            // Split complex operations into simpler ones
            DecomposeFunc df;
            if (DecomposeOpcodeList.TryGetValue(n.il.opcode, out df))
            {
                CilNode ret = df(n, ass, mtc, ref next_block, attrs);

                if (ret != n)
                {
                    ret.stack_before = n.stack_before;
                    ret.stack_vars_before = n.stack_vars_before;
                    ret.il.stack_before = n.il.stack_before;
                    ret.il.stack_vars_before = n.il.stack_vars_before;

                    CilNode first = n.replaced_by[0];
                    CilNode last = n.replaced_by[n.replaced_by.Count - 1];

                    first.Prev.Clear();
                    foreach (timple.BaseNode prev in n.Prev)
                    {
                        first.Prev.Add(prev);
                        prev.Next[prev.Next.IndexOf(n)] = first;
                    }

                    last.Next.Clear();
                    foreach (timple.BaseNode next in n.Next)
                    {
                        last.Next.Add(next);
                        next.Prev[next.Prev.IndexOf(n)] = last;
                    }

                    if(n.replaced_by.Count > 0)
                        n.replaced_by[0].il_label = n.il_label;

                    for (int i = 0; i < n.replaced_by.Count; i++)
                    {
                        if (i != 0)
                        {
                            n.replaced_by[i].Prev.Clear();
                            n.replaced_by[i].Prev.Add(n.replaced_by[i - 1]);
                            n.replaced_by[i].il_label = next_block++;
                        }
                        if (i != (n.replaced_by.Count - 1))
                        {
                            n.replaced_by[i].Next.Clear();
                            n.replaced_by[i].Next.Add(n.replaced_by[i + 1]);
                        }
                    }
                }
                return ret;
            }
            return n;
        }
    }
}
