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
        void MakeWorklist(tybel.Tybel.TybelCode code)
        {
            IEnumerable<vara> initial_list = code.Liveness.defs.Keys;
            //if (initial.Count != 0)
            //    initial_list = initial;
            foreach (vara n in initial_list)
            {
                if (n.VarType == vara.vara_type.Logical)
                {
                    if (degree[n] >= K)
                        spillWorklist.Add(n);
                    else if (MoveRelated(n))
                        freezeWorklist.Add(n);
                    else
                        simplifyWorklist.Add(n);
                }
            }
        }
    }
}
