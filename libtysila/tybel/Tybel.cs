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

/* tybel = tysila back-end language */

using System;
using System.Collections.Generic;

namespace libtysila.tybel
{
    public partial class Tybel : timple.BaseGraph
    {
        public Dictionary<timple.TreeNode, IList<Node>> TimpleMap;
        public int NextVar;

        public static Tybel GenerateTybel(timple.Optimizer.OptimizeReturn code, Assembler ass, IList<libasm.hardware_location> las)
        {
            /* Choose instructions */
            libtysila.tybel.Tybel tybel = libtysila.tybel.Tybel.BuildGraph(code, ass, las);
            libtysila.timple.Liveness tybel_l = libtysila.timple.Liveness.LivenessAnalysis(tybel);

            /* Prepare register allocator */
            libtysila.regalloc.RegAlloc r = new libtysila.regalloc.RegAlloc();
            Dictionary<vara, vara> regs = r.Main(new libtysila.tybel.Tybel.TybelCode(tybel, tybel_l), ass);

            /* Rename registers in the code */
            libtysila.tybel.Tybel tybel_2 = tybel.RenameRegisters(regs);

            /* Resolve special instructions */
            libtysila.timple.Liveness tybel2_l = libtysila.timple.Liveness.LivenessAnalysis(tybel_2);
            libtysila.tybel.Tybel tybel_3 = tybel_2.ResolveSpecialNodes(tybel2_l, ass, las);

            return tybel_3;
        }

        private static Tybel BuildGraph(timple.Optimizer.OptimizeReturn code, Assembler ass, IList<libasm.hardware_location> las)
        {
            Tybel ret = new Tybel();
            ret.TimpleMap = new Dictionary<timple.TreeNode, IList<Node>>();
            ret.NextVar = 0;

            /* Determine the greatest logical var in use */
            foreach (vara v in code.Liveness.defs.Keys)
            {
                if (v.LogicalVar >= ret.NextVar)
                    ret.NextVar = v.LogicalVar + 1;
            }

            foreach (timple.TreeNode n in code.Code)
            {
                IList<Node> tybel = ass.SelectInstruction(n, ref ret.NextVar, las);
                ret.TimpleMap[n] = tybel;
            }

            util.Set<timple.TreeNode> visited = new util.Set<timple.TreeNode>();
            foreach (timple.TreeNode n in code.CodeTree.Starts)
            {
                ret.Starts.Add(ret.TimpleMap[n][0]);
                ret.DFAdd(n, visited);
            }

            return ret;
        }

        private void DFAdd(timple.TreeNode n, util.Set<timple.TreeNode> visited)
        {
            if (visited.Contains(n))
                return;

            visited.Add(n);

            for (int i = 1; i < TimpleMap[n].Count; i++)
                AddEdge(TimpleMap[n][i - 1], TimpleMap[n][i]);

            Node last = TimpleMap[n][TimpleMap[n].Count - 1];

            if (n.next.Count == 0)
                Ends.Add(last);
            else
            {
                foreach (timple.TreeNode next in n.next)
                {
                    AddEdge(last, TimpleMap[next][0]);
                    DFAdd(next, visited);
                }
            }
        }

        private void AddEdge(Node parent, Node c)
        {
            if (c.next == null)
                c.next = new List<timple.BaseNode>();
            if (c.prev == null)
                c.prev = new List<timple.BaseNode>();

            if (parent == null)
                Starts.Add(c);
            else
            {
                if (parent.next == null)
                    parent.next = new List<timple.BaseNode>();
                if (parent.prev == null)
                    parent.prev = new List<timple.BaseNode>();

                parent.Next.Add(c);
                c.Prev.Add(parent);
            }
        }

        public class TybelCode
        {
            public timple.BaseGraph CodeGraph;
            public IList<timple.BaseNode> Code { get { return CodeGraph.LinearStream; } }
            public timple.Liveness Liveness;

            public TybelCode(timple.BaseGraph code, timple.Liveness l) { CodeGraph = code; Liveness = l; }
        }
    }
}
