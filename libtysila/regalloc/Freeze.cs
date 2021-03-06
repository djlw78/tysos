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

namespace libtysila.regalloc
{
    partial class RegAlloc
    {
        void Freeze(tybel.Tybel.TybelCode code)
        {
            vara u = freezeWorklist.ItemAtIndex(0);
            freezeWorklist.Remove(u);
            simplifyWorklist.Add(u);
            FreezeMoves(u);
        }

        void FreezeMoves(vara u)
        {
            foreach (timple.BaseNode m in NodeMoves(u))
            {
                IEnumerator<vara> enum_x = m.defs.GetEnumerator();
                enum_x.MoveNext();
                vara x = enum_x.Current;

                IEnumerator<vara> enum_y = m.uses.GetEnumerator();
                enum_y.MoveNext();
                vara y = enum_y.Current;

                vara v;
                if (GetAlias(y).Equals(GetAlias(u)))
                    v = GetAlias(x);
                else
                    v = GetAlias(y);

                activeMoves.Remove(m);
                frozenMoves.Add(m);

                if ((NodeMoves(v).Count == 0) && (degree[v] < K))
                {
                    freezeWorklist.Remove(v);
                    simplifyWorklist.Add(v);
                }
            }
        }
    }
}
