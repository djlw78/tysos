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
using System.IO;
using System.Text;

namespace libtysila4.target
{
    class MCNode : graph.NodeContents
    {
        public int bb_idx;
        public List<MCInst> phis = new List<MCInst>();
        public List<MCInst> insts;

        public IEnumerable<MCInst> all_insts
        {
            get
            {
                foreach (var phi in phis)
                    yield return phi;
                foreach (var inst in insts)
                    yield return inst;
            }
        }

        public IEnumerable<MCInst> all_insts_rev
        {
            get
            {
                for (int i = insts.Count - 1; i >= 0; i--)
                    yield return insts[i];
                for (int i = phis.Count - 1; i >= 0; i--)
                    yield return phis[i];
            }
        }

        public List<ir.Param> uses, defs, locals;

        public util.Set live_in = new util.Set();
        public util.Set live_out = new util.Set();
        public util.Set gen = new util.Set();
        public util.Set kill = new util.Set();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var i in all_insts)
            {
                sb.Append(i.ToString());
                sb.Append(';');
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        internal class MCNodeId : IEquatable<MCNodeId>
        {
            public int ls_idx;
            public int mc_idx;
            public bool is_phi;
            public graph.Graph g;

            public MCInst inst
            {
                get
                {
                    var ls = g.LinearStream[ls_idx];
                    var mcn = ls.c as MCNode;
                    if (is_phi)
                        return mcn.phis[mc_idx];
                    else
                        return mcn.insts[mc_idx];
                }
            }

            public bool Equals(MCNodeId other)
            {
                if (other == null)
                    return false;
                if (ls_idx != other.ls_idx)
                    return false;
                if (mc_idx != other.mc_idx)
                    return false;
                return is_phi == other.is_phi;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as MCNodeId);
            }

            public override int GetHashCode()
            {
                return ls_idx.GetHashCode() ^
                    (mc_idx.GetHashCode() << 10) ^
                    (is_phi.GetHashCode() << 20);
            }
        }
    }
}
