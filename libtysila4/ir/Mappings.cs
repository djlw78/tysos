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
using System.Text;
using libtysila4.cil;

namespace libtysila4.ir
{
    partial class IrGraph
    {
        static Dictionary<OCList<cil.Opcode.SimpleOpcode>, Handler> simple_map
            = new Dictionary<OCList<cil.Opcode.SimpleOpcode>, Handler>(
                new GenericEqualityComparer<OCList<cil.Opcode.SimpleOpcode>>());
        static Dictionary<OCList<cil.Opcode.SingleOpcodes>, Handler> single_map
            = new Dictionary<OCList<cil.Opcode.SingleOpcodes>, Handler>(
                new GenericEqualityComparer<OCList<cil.Opcode.SingleOpcodes>>());

        delegate Opcode[] Handler(cil.CilNode start);

        static IrGraph()
        {
            InitMappings();
        }

        static void InitMappings()
        {
            init_simple_map();
            init_single_map();
        }

        static Opcode[] ld_ld_cmp(CilNode start)
        {
            var lv1 = GetParam(start);
            var lv2 = GetParam((CilNode)start.n.Next1.c);
            var cmp_node = start.n.Next1.Next1.c as CilNode;

            var cops = cmp(cmp_node);
            if (cops == null)
                return null;

            cops[0].uses[0] = lv1;
            cops[0].uses[1] = lv2;

            return cops;
        }

        static Opcode[] ld_cmp(CilNode start)
        {
            var lv1 = GetParam(start);
            var cmp_node = start.n.Next1.c as CilNode;

            var cops = cmp(cmp_node);
            if (cops == null)
                return null;

            cops[0].uses[0].v = 0;
            cops[0].uses[1] = lv1;

            return cops;
        }

        static Opcode[] cmp(CilNode start)
        {
            var cc = Opcode.cc_double_map[start.opcode.opcode2];
            Opcode oc = new Opcode
            {
                oc = Opcode.oc_cmp,
                uses = new Param[] { new Param { t = Opcode.vl_stack, v = 1 }, new Param { t = Opcode.vl_stack, v = 0 } },
                defs = new Param[] { new Param { t = Opcode.vl_stack, v = 0, ct = Opcode.ct_int32 } },
                cc = cc
            };

            return new Opcode[] { oc };
        }

        static Opcode[] cmp_ldc_cmp(CilNode start)
        {
            var cmp1 = start;
            var ldc = start.n.Next1.c as CilNode;
            var cmp2 = start.n.Next1.Next1.c as CilNode;

            var c = ldc.GetConstant();
            if (c != 0)
                return null;

            var cmp2_cc = cmp2.GetCondCode();
            if (cmp2_cc != Opcode.cc_eq)
                return null;

            return new Opcode[]
            {
                new Opcode
                {
                    oc = Opcode.oc_cmp,
                    uses = new Param[] { new Param { t = Opcode.vl_stack, v = 1 }, new Param { t = Opcode.vl_stack, v = 0 } },
                    defs = new Param[] { new Param { t = Opcode.vl_stack, v = 0, ct = Opcode.ct_int32 } },
                    cc = Opcode.cc_invert_map[cmp1.GetCondCode()]
                }
            };
        }

        static Opcode[] ld_st(cil.CilNode start)
        {
            var lva = GetParam(start);
            var lvb = GetParam((cil.CilNode)start.n.Next1.c);

            if (lva.t == Opcode.vl_lv && lvb.t == Opcode.vl_lv)
                return null;

            return new Opcode[] { new Opcode
            {
                oc = Opcode.oc_store,
                defs = new Param[] { lvb },
                uses = new Param[] { lva }
            } };
        }

        static Opcode[] st_lv_st(cil.CilNode start)
        {
            var lv = GetParam(start);

            return new Opcode[] { new Opcode
            {
                oc = Opcode.oc_store,
                defs = new Param[] { new Param { t = Opcode.vl_stack, v = 0 } },
                uses = new Param[] { lv }
            } };
        }



        static Opcode[] call(cil.CilNode start)
        {
            metadata.MetadataStream m;
            uint token;
            start.GetToken(out m, out token);
            int table_id, row;
            m.InterpretToken(token, out table_id, out row);

            metadata.MetadataStream method_m;
            int sig_idx;
            int m_row = m.GetMethodDefRow(table_id, row, out method_m,
                out sig_idx);

            int param_count = method_m.GetMethodDefSigParamCountIncludeThis(sig_idx);
            int ret_idx = method_m.GetMethodDefSigRetTypeIndex(sig_idx);
            uint ret_token;
            int ret_type = method_m.GetType(ref ret_idx, out ret_token);

            bool is_void_ret = false;
            if (ret_type == 0x01)
                is_void_ret = true;

            Param[] defs;
            if (is_void_ret)
                defs = new Param[] { };
            else
                defs = new Param[] { new Param { t = Opcode.vl_stack, v = 0 } };

            Param[] uses = new Param[param_count + 1];
            uses[0] = new Param { t = Opcode.vl_call_target, v = m_row, v2 = sig_idx, m = method_m };
            for (int i = 0; i < param_count; i++)
                uses[param_count - i] = new Param { t = Opcode.vl_stack, v = i };

            return new Opcode[] { new Opcode { oc = Opcode.oc_call, defs = defs, uses = uses } };
        }

        static Opcode[] st_st_lv(cil.CilNode start)
        {
            var lv = GetParam(start);

            return new Opcode[] { new Opcode
            {
                oc = Opcode.oc_store,
                defs = new Param[] { lv },
                uses = new Param[] { new Param { t = Opcode.vl_stack, v = 0 } }
            } };
        }

        static Opcode[] call_stloc(cil.CilNode start)
        {
            var cops = call(start);
            if (cops == null)
                return null;

            var lv = GetParam((cil.CilNode)start.n.Next1.c);

            foreach (var cop in cops)
            {
                if (cop.oc == Opcode.oc_call)
                {
                    if (cop.defs.Length == 1)
                    {
                        cop.defs[0] = lv;
                        return cops;
                    }
                    return null;
                }
            }
            return null;
        }

        static Opcode[] nop(CilNode start)
        { return new Opcode[] { new Opcode { oc = Opcode.oc_nop } }; }

        static Opcode[] brtrue(CilNode start)
        {
            return new Opcode[] { new Opcode { oc = Opcode.oc_brif,
                cc = Opcode.cc_ne,
                uses = new Param[] { new Param { t = Opcode.vl_stack, v = 0 }, new Param { t = Opcode.vl_c, v = 0 } },
                defs = new Param[] { }
            } };
        }

        static Opcode[] brfalse(CilNode start)
        {
            return new Opcode[] { new Opcode { oc = Opcode.oc_brif,
                cc = Opcode.cc_eq,
                uses = new Param[] { new Param { t = Opcode.vl_stack, v = 0 }, new Param { t = Opcode.vl_c, v = 0 } },
                defs = new Param[] { }
            } };
        }

        static Opcode[] brif1(CilNode start)
        {
            var cc = Opcode.cc_single_map[start.opcode.opcode1];
            return new Opcode[] { new Opcode { oc = Opcode.oc_brif,
                cc = cc,
                uses = new Param[] { new Param { t = Opcode.vl_stack, v = 0 }, new Param { t = Opcode.vl_c, v = 0 } },
                defs = new Param[] { }
            } };
        }

        static Opcode[] br(CilNode start)
        {
            return new Opcode[] { new Opcode { oc = Opcode.oc_br,
                uses = new Param[] { },
                defs = new Param[] { }
            } };
        }

        static Opcode[] ret(cil.CilNode start)
        {
            var cg = (cil.CilGraph)start.n.g;
            int ret_idx = cg._m.GetMethodDefSigRetTypeIndex(cg._mdef_sig);
            uint ret_token;
            int ret_type = cg._m.GetType(ref ret_idx, out ret_token);

            Param[] uses;
            if (ret_type == 0x01)
                uses = new Param[1];
            else
                uses = new Param[2];

            uses[0] = new Param { t = Opcode.vl_call_target, v = cg._md_row, v2 = cg._mdef_sig, m = cg._m };
            if (ret_type != 0x01)
                uses[1] = new Param { t = Opcode.vl_stack, v = 0 };

            return new Opcode[] { new Opcode { oc = Opcode.oc_ret, defs = new Param[] { }, uses = uses } };
        }

        static Opcode[] ld_ret(cil.CilNode start)
        {
            var rn = (cil.CilNode)start.n.Next1.c;
            var rops = ret(rn);
            if (rops == null)
                return null;

            if (rops.Length != 1)
                return null;

            if (rops[0].uses.Length != 2)
                return null;

            var lv = GetParam(start);
            rops[0].uses[1] = lv;
            return rops;
        }

        static Opcode[] stloc_ldloc(cil.CilNode start)
        {
            int lv1 = start.GetLocalVarLoc();
            int lv2 = ((cil.CilNode)start.n.Next1.c).GetLocalVarLoc();

            if(lv1 == lv2)
            {
                // replace stloc_n, ldloc_n with store { st } -> { st, lv }
                return new Opcode[] { new Opcode
                {
                    oc = Opcode.oc_store,
                    defs = new Param[] { new Param { t = Opcode.vl_lv, v = lv2 }, new Param { t = Opcode.vl_stack, v = 0 } },
                    uses = new Param[] { new Param { t = Opcode.vl_stack, v = 0 } }
                } };
            }
            else
            {
                // they are not the same local var, so fail
                return null;
            }
        }
    }

    class OCList<T> : IEquatable<OCList<T>>
    {
        T[] _arr;
        int _hc;

        public OCList(T[] arr)
        {
            int hc = 0;

            foreach(T a in arr)
            {
                hc ^= a.GetHashCode();
                hc <<= 1;
            }

            _hc = hc;
            _arr = arr;
        }

        public static implicit operator OCList<T>(T[] arr)
        { return new OCList<T>(arr); }

        public bool Equals(OCList<T> other)
        {
            if (other == null)
                return false;
            if (other._arr.Length != _arr.Length)
                return false;
            for(int i = 0; i < _arr.Length; i++)
            {
                if (!_arr[i].Equals(other._arr[i]))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return _hc;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _arr.Length; i++)
            {
                if (i != 0)
                    sb.Append(", ");
                sb.Append(_arr[i].ToString());
            }
            return sb.ToString();
        }
    }

}
