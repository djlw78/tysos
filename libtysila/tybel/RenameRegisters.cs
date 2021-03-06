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

namespace libtysila.tybel
{
    partial class Tybel
    {
        public Tybel RenameRegisters(Dictionary<vara, vara> regs)
        {
            Tybel ret = new Tybel();
            ret.innergraph = this;

            util.Set<timple.BaseNode> visited = new util.Set<timple.BaseNode>();
            util.Set<tybel.SpecialNode> missed_special = new util.Set<SpecialNode>();
            foreach (timple.BaseNode start in Starts)
                RenameRegisters(ret, start, null, regs, visited, missed_special);

            foreach (tybel.SpecialNode missed in missed_special)
                missed.SaveNode = (Node)ret.InnerToOuter[missed.SaveNode];

            /* Ensure next/prev and end links match up */
            visited.Clear();
            foreach (timple.BaseNode start in Starts)
                DFMatchUpNextPrev(ret, ret.InnerToOuter[start], visited);
            ret.Ends.Clear();
            foreach (timple.BaseNode end in Ends)
                ret.Ends.Add(ret.InnerToOuter[end]);

            return ret;
        }

        private void DFMatchUpNextPrev(Tybel ret, timple.BaseNode node, util.Set<timple.BaseNode> visited)
        {
            if (visited.Contains(node))
                return;
            visited.Add(node);

            node.Next.Clear();
            foreach (timple.BaseNode next in node.InnerNode.Next)
                node.Next.Add(ret.InnerToOuter[next]);
            node.Prev.Clear();
            foreach (timple.BaseNode prev in node.InnerNode.Prev)
                    node.Prev.Add(ret.InnerToOuter[prev]);

            foreach (timple.BaseNode next in node.Next)
                DFMatchUpNextPrev(ret, next, visited);
        }

        void RenameRegisters(Tybel ret, timple.BaseNode inner_node, timple.BaseNode outer_parent, Dictionary<vara, vara> regs,
            util.Set<timple.BaseNode> visited, util.Set<tybel.SpecialNode> missed_special)
        {
            if (visited.Contains(inner_node))
                return;

            visited.Add(inner_node);

            tybel.Node outer_node = (tybel.Node)inner_node.MemberwiseClone();
            outer_node.InnerNode = inner_node;
            outer_node.next = new List<timple.BaseNode>();
            outer_node.prev = new List<timple.BaseNode>();

            if (outer_node is tybel.MultipleNode)
            {
                tybel.MultipleNode mn = outer_node as tybel.MultipleNode;
                List<Node> outer_nodes = new List<Node>();
                foreach (tybel.Node n2 in mn.Nodes)
                {
                    tybel.Node outer_mnode = (tybel.Node)n2.MemberwiseClone();
                    for (int i = 0; i < n2.VarList.Count; i++)
                    {
                        if (outer_mnode.VarList[i].HasLogicalVar || outer_mnode.VarList[i].VarType == vara.vara_type.MachineReg)
                            outer_mnode.VarList[i] = RenameRegister(outer_mnode.VarList[i], regs);
                    }
                    outer_nodes.Add(outer_mnode);
                }
                mn.Nodes = outer_nodes;

                List<vara> mn_defs = new List<vara>(mn.defs);
                for (int j = 0; j < mn_defs.Count; j++)
                {
                    if (mn_defs[j].HasLogicalVar || mn_defs[j].VarType == vara.vara_type.MachineReg)
                        mn_defs[j] = RenameRegister(mn_defs[j], regs);
                }
                mn._defs = mn_defs;

                List<vara> mn_uses = new List<vara>(mn.uses);
                for (int j = 0; j < mn_uses.Count; j++)
                {
                    if (mn_uses[j].HasLogicalVar || mn_uses[j].VarType == vara.vara_type.MachineReg)
                        mn_uses[j] = RenameRegister(mn_uses[j], regs);
                }
                mn._uses = mn_uses;
            }
            else
            {
                for (int i = 0; i < outer_node.VarList.Count; i++)
                {
                    if (outer_node.VarList[i].HasLogicalVar || outer_node.VarList[i].VarType == vara.vara_type.MachineReg)
                        outer_node.VarList[i] = RenameRegister(outer_node.VarList[i], regs);
                }
            }

            bool add = true;
            /*if (outer_node.IsMove)
            {
                if ((outer_node.defs.Count == 1) && (outer_node.uses.Count == 1))
                {
                    IEnumerator<vara> def_enum = outer_node.defs.GetEnumerator();
                    def_enum.MoveNext();
                    IEnumerator<vara> use_enum = outer_node.uses.GetEnumerator();
                    use_enum.MoveNext();

                    if(def_enum.Current.Equals(use_enum.Current))
                        add = false;
                }
            }*/

            if (add)
            {
                if (outer_parent == null)
                    ret.Starts.Add(outer_node);
                else
                {
                    outer_parent.Next.Add(outer_node);
                    outer_node.Prev.Add(outer_parent);
                }
                outer_parent = outer_node;
                ret.InnerToOuter[inner_node] = outer_node;

                if (outer_node is SpecialNode)
                {
                    SpecialNode outer_sn = outer_node as SpecialNode;
                    if (outer_sn.SaveNode != null)
                    {
                        if (ret.InnerToOuter.ContainsKey(outer_sn.SaveNode))
                            outer_sn.SaveNode = (SpecialNode)ret.InnerToOuter[outer_sn.SaveNode];
                        else
                            missed_special.Add(outer_sn);
                    }
                }
            }

            foreach (timple.BaseNode inner_node_nexts in inner_node.Next)
                RenameRegisters(ret, inner_node_nexts, outer_parent, regs, visited, missed_special);
        }

        private vara RenameRegister(vara vara, Dictionary<vara, vara> regs)
        {
            switch (vara.VarType)
            {
                case libtysila.vara.vara_type.Logical:
                    return regs[vara];

                case libtysila.vara.vara_type.ContentsOf:
                    return vara.MachineReg(new libasm.hardware_contentsof { base_loc = regs[vara.Logical(vara.LogicalVar, vara.SSA, vara.DataType)].MachineRegVal, const_offset = (int)vara.Offset });

                case libtysila.vara.vara_type.AddrOf:
                    if (vara.Offset != 0)
                        throw new NotSupportedException();
                    return vara.MachineReg(new libasm.hardware_addressof { base_loc = regs[vara.Logical(vara.LogicalVar, vara.SSA, vara.DataType)].MachineRegVal });

                case libtysila.vara.vara_type.MachineReg:
                    if (vara.MachineRegVal is libasm.hardware_stackloc)
                        return regs[vara];
                    return vara;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
