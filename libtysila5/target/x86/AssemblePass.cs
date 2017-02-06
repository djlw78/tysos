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
using libtysila5.util;

namespace libtysila5.target.x86
{
    partial class x86_Assembler
    {
        public void AssemblePass(Code c)
        { 
            var Code = text_section.Data as List<byte>;
            var code_start = Code.Count;

            Dictionary<int, int> il_starts = new Dictionary<int, int>(
                new GenericEqualityComparer<int>());

            /* Get maximum il offset of method */
            int max_il = 0;
            foreach(var cil in c.cil)
            {
                if (cil.il_offset > max_il)
                    max_il = cil.il_offset;
            }
            max_il++;

            for (int i = 0; i < max_il; i++)
                il_starts[i] = -1;

            List<int> rel_srcs = new List<int>();
            List<int> rel_dests = new List<int>();

            foreach(var I in c.mc)
            {
                var cil = I.parent.parent;
                var mc_offset = Code.Count - code_start;
                I.offset = mc_offset;

                if (il_starts[cil.il_offset] == -1)
                {
                    il_starts[cil.il_offset] = mc_offset;
                    cil.mc_offset = mc_offset;
                }

                if (I.p.Length == 0)
                    continue;

                switch (I.p[0].v)
                {
                    case Generic.g_mclabel:
                        il_starts[(int)I.p[1].v] = mc_offset;
                        break;

                    case x86_push_rm32:
                        Code.Add(0xff);
                        Code.AddRange(ModRMSIB(6, I.p[1].mreg));
                        break;
                    case x86_push_r32:
                        Code.Add(PlusRD(0x50, I.p[1].mreg));
                        break;
                    case x86_pop_rm32:
                        Code.Add(0x8f);
                        Code.AddRange(ModRMSIB(0, I.p[1].mreg));
                        break;
                    case x86_pop_r32:
                        Code.Add(PlusRD(0x58, I.p[1].mreg));
                        break;
                    case x86_mov_r32_rm32:
                    case x86_mov_r8_rm8:
                    case x86_mov_r16_rm16:
                        if (I.p[0].v == x86_mov_r8_rm8)
                            Code.Add(0x8a);
                        else if(I.p[0].v == x86_mov_r16_rm16)
                        {
                            Code.Add(0x67);
                            Code.Add(0x8b);
                        }
                        else
                            Code.Add(0x8b);

                        switch(I.p[2].t)
                        {
                            case ir.Opcode.vl_mreg:
                                Code.AddRange(ModRMSIB(I.p[1].mreg, I.p[2].mreg));
                                break;
                            case ir.Opcode.vl_str:
                                Code.AddRange(ModRMSIB(GetR(I.p[1].mreg), 5, 0, -1, 0, 0, false));
                                var reloc = bf.CreateRelocation();
                                reloc.DefinedIn = text_section;
                                reloc.Type = new binary_library.elf.ElfFile.Rel_386_32();
                                reloc.Addend = I.p[2].v;
                                reloc.References = bf.CreateSymbol();
                                reloc.References.DefinedIn = null;
                                reloc.References.Name = I.p[2].str;
                                reloc.Offset = (ulong)Code.Count;
                                bf.AddRelocation(reloc);
                                AddImm32(Code, 0);
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                        break;
                    case x86_mov_rm32_r32:
                        Code.Add(0x89);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[1].mreg));
                        break;
                    case x86_mov_rm32_imm32:
                        Code.Add(0xc7);
                        Code.AddRange(ModRMSIB(0, I.p[1].mreg));

                        switch (I.p[2].t)
                        {
                            case ir.Opcode.vl_c:
                            case ir.Opcode.vl_c32:
                                AddImm32(Code, I.p[2].v);
                                break;
                            case ir.Opcode.vl_str:
                                var reloc = bf.CreateRelocation();
                                reloc.DefinedIn = text_section;
                                reloc.Type = new binary_library.elf.ElfFile.Rel_386_32();
                                reloc.Addend = I.p[2].v;
                                reloc.References = bf.CreateSymbol();
                                reloc.References.DefinedIn = null;
                                reloc.References.Name = I.p[2].str;
                                reloc.Offset = (ulong)Code.Count;
                                bf.AddRelocation(reloc);
                                AddImm32(Code, 0);
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                        break;
                    case x86_add_rm32_imm32:
                        Code.Add(0x81);
                        Code.AddRange(ModRMSIB(0, I.p[1].mreg));
                        AddImm32(Code, I.p[3].v);
                        break;
                    case x86_add_rm32_imm8:
                        Code.Add(0x83);
                        Code.AddRange(ModRMSIB(0, I.p[1].mreg));
                        AddImm8(Code, I.p[3].v);
                        break;
                    case x86_adc_r32_rm32:
                        Code.Add(0x13);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[3].mreg));
                        break;
                    case x86_sub_rm32_imm32:
                        Code.Add(0x81);
                        Code.AddRange(ModRMSIB(5, I.p[1].mreg));
                        AddImm32(Code, I.p[3].v);
                        break;
                    case x86_sub_rm32_imm8:
                        Code.Add(0x83);
                        Code.AddRange(ModRMSIB(5, I.p[1].mreg));
                        AddImm8(Code, I.p[3].v);
                        break;
                    case x86_sub_r32_rm32:
                        Code.Add(0x2b);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[3].mreg));
                        break;
                    case x86_and_r32_rm32:
                        Code.Add(0x23);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[3].mreg));
                        break;
                    case x86_and_rm32_r32:
                        Code.Add(0x21);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[3].mreg));
                        break;
                    case x86_or_r32_rm32:
                        Code.Add(0x0b);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[3].mreg));
                        break;
                    case x86_or_rm32_r32:
                        Code.Add(0x09);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[3].mreg));
                        break;
                    case x86_xor_r32_rm32:
                        Code.Add(0x33);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[3].mreg));
                        break;
                    case x86_xor_rm32_r32:
                        Code.Add(0x31);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[3].mreg));
                        break;
                    case x86_sbb_r32_rm32:
                        Code.Add(0x1b);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[3].mreg));
                        break;
                    case x86_add_r32_rm32:
                        {
                            var dreg = I.p[1].mreg;
                            var sreg = I.p[2].mreg;
                            if (dreg.Equals(sreg))
                                sreg = I.p[3].mreg;

                            Code.Add(0x03);
                            Code.AddRange(ModRMSIB(dreg, sreg));
                        }
                        break;
                    case x86_call_rel32:
                        {
                            Code.Add(0xe8);
                            var reloc = bf.CreateRelocation();
                            reloc.DefinedIn = text_section;
                            reloc.Type = new binary_library.elf.ElfFile.Rel_386_PC32();
                            reloc.Addend = -4;
                            reloc.References = bf.CreateSymbol();
                            reloc.References.DefinedIn = null;
                            reloc.References.Name = I.p[1].str;
                            reloc.References.ObjectType = binary_library.SymbolObjectType.Function;
                            reloc.Offset = (ulong)Code.Count;
                            bf.AddRelocation(reloc);
                            AddImm32(Code, 0);
                        }
                        break;
                    case x86_call_rm32:
                        {
                            Code.Add(0xff);
                            var obj = I.p[1];
                            Code.AddRange(ModRMSIB(2, obj.mreg));
                            break;
                        }
                    case x86_cmp_rm32_r32:
                        Code.Add(0x39);
                        Code.AddRange(ModRMSIB(I.p[2].mreg, I.p[1].mreg));
                        break;
                    case x86_cmp_r32_rm32:
                        Code.Add(0x3b);
                        Code.AddRange(ModRMSIB(I.p[1].mreg, I.p[2].mreg));
                        break;
                    case x86_cmp_rm32_imm32:
                        if (I.p[1].mreg == r_eax)
                            Code.Add(0x3d);
                        else
                        {
                            Code.Add(0x81);
                            Code.AddRange(ModRMSIB(7, I.p[1].mreg));
                        }
                        AddImm32(Code, I.p[2].v);
                        break;
                    case x86_cmp_rm32_imm8:
                        Code.Add(0x83);
                        Code.AddRange(ModRMSIB(7, I.p[1].mreg));
                        AddImm8(Code, I.p[2].v);
                        break;
                    case x86_set_rm32:
                        if (I.p[1].v != ir.Opcode.cc_never)
                        {
                            Code.Add(0x0f);
                            switch (I.p[1].v)
                            {
                                case ir.Opcode.cc_a:
                                    Code.Add(0x97);
                                    break;
                                case ir.Opcode.cc_ae:
                                    Code.Add(0x93);
                                    break;
                                case ir.Opcode.cc_b:
                                    Code.Add(0x92);
                                    break;
                                case ir.Opcode.cc_be:
                                    Code.Add(0x96);
                                    break;
                                case ir.Opcode.cc_eq:
                                    Code.Add(0x94);
                                    break;
                                case ir.Opcode.cc_ge:
                                    Code.Add(0x9d);
                                    break;
                                case ir.Opcode.cc_gt:
                                    Code.Add(0x9f);
                                    break;
                                case ir.Opcode.cc_le:
                                    Code.Add(0x9e);
                                    break;
                                case ir.Opcode.cc_lt:
                                    Code.Add(0x9c);
                                    break;
                                case ir.Opcode.cc_ne:
                                    Code.Add(0x95);
                                    break;
                                case ir.Opcode.cc_always:
                                    throw new NotImplementedException();
                            }
                            Code.AddRange(ModRMSIB(2, I.p[2].mreg));
                        }
                        break;
                    case x86_movsxbd:
                        Code.Add(0x0f);
                        Code.Add(0xbe);
                        Code.AddRange(ModRMSIB(I.p[1].mreg, I.p[2].mreg));
                        break;
                    case x86_movzxbd:
                        Code.Add(0x0f);
                        Code.Add(0xb6);
                        Code.AddRange(ModRMSIB(I.p[1].mreg, I.p[2].mreg));
                        break;
                    case x86_jcc_rel32:
                        if (I.p[1].v != ir.Opcode.cc_never)
                        {
                            Code.Add(0x0f);
                            switch (I.p[1].v)
                            {
                                case ir.Opcode.cc_a:
                                    Code.Add(0x87);
                                    break;
                                case ir.Opcode.cc_ae:
                                    Code.Add(0x83);
                                    break;
                                case ir.Opcode.cc_b:
                                    Code.Add(0x82);
                                    break;
                                case ir.Opcode.cc_be:
                                    Code.Add(0x86);
                                    break;
                                case ir.Opcode.cc_eq:
                                    Code.Add(0x84);
                                    break;
                                case ir.Opcode.cc_ge:
                                    Code.Add(0x8d);
                                    break;
                                case ir.Opcode.cc_gt:
                                    Code.Add(0x8f);
                                    break;
                                case ir.Opcode.cc_le:
                                    Code.Add(0x8e);
                                    break;
                                case ir.Opcode.cc_lt:
                                    Code.Add(0x8c);
                                    break;
                                case ir.Opcode.cc_ne:
                                    Code.Add(0x85);
                                    break;
                                case ir.Opcode.cc_always:
                                    throw new NotImplementedException();
                            }

                            rel_srcs.Add(Code.Count);
                            rel_dests.Add((int)I.p[2].v);
                            AddImm32(Code, 0);
                        }
                        break;
                    case x86_jmp_rel32:
                        Code.Add(0xe9);
                        rel_srcs.Add(Code.Count);
                        rel_dests.Add((int)I.p[1].v);
                        AddImm32(Code, 0);
                        break;
                    case x86_ret:
                        Code.Add(0xc3);
                        break;
                    case Generic.g_loadaddress:
                        {
                            Code.Add(0xc7);
                            Code.AddRange(ModRMSIB(0, I.p[1].mreg));
                            var reloc = bf.CreateRelocation();
                            reloc.DefinedIn = text_section;
                            reloc.Type = new binary_library.elf.ElfFile.Rel_386_32();
                            reloc.Addend = I.p[2].v;
                            reloc.References = bf.CreateSymbol();
                            reloc.References.DefinedIn = null;
                            reloc.References.Name = I.p[2].str;
                            reloc.References.ObjectType = binary_library.SymbolObjectType.Function;
                            reloc.Offset = (ulong)Code.Count;
                            bf.AddRelocation(reloc);
                            AddImm32(Code, 0);
                        }
                        break;

                    case x86_mov_r32_lab:
                        {
                            Code.Add(0x8b);
                            Code.AddRange(ModRMSIB(GetR(I.p[1].mreg), 5, 0, -1, 0, 0, false));
                            var reloc = bf.CreateRelocation();
                            reloc.DefinedIn = text_section;
                            reloc.Type = new binary_library.elf.ElfFile.Rel_386_32();
                            reloc.Addend = I.p[2].v;
                            reloc.References = bf.CreateSymbol();
                            reloc.References.DefinedIn = null;
                            reloc.References.Name = I.p[2].str;
                            reloc.References.ObjectType = binary_library.SymbolObjectType.Object;
                            reloc.Offset = (ulong)Code.Count;
                            bf.AddRelocation(reloc);
                            AddImm32(Code, 0);
                        }
                        break;

                    case x86_mov_lab_r32:
                        {
                            Code.Add(0x89);
                            Code.AddRange(ModRMSIB(GetR(I.p[2].mreg), 5, 0, -1, 0, 0, false));
                            var reloc = bf.CreateRelocation();
                            reloc.DefinedIn = text_section;
                            reloc.Type = new binary_library.elf.ElfFile.Rel_386_32();
                            reloc.Addend = I.p[1].v;
                            reloc.References = bf.CreateSymbol();
                            reloc.References.DefinedIn = null;
                            reloc.References.Name = I.p[1].str;
                            reloc.References.ObjectType = binary_library.SymbolObjectType.Object;
                            reloc.Offset = (ulong)Code.Count;
                            bf.AddRelocation(reloc);
                            AddImm32(Code, 0);
                        }
                        break;

                    case x86_imul_r32_rm32_imm32:
                        Code.Add(0x69);
                        Code.AddRange(ModRMSIB(I.p[1].mreg, I.p[2].mreg));
                        AddImm32(Code, I.p[3].v);
                        break;

                    case x86_imul_r32_rm32:
                        Code.Add(0x0f);
                        Code.Add(0xaf);
                        Code.AddRange(ModRMSIB(I.p[1].mreg, I.p[2].mreg));
                        break;

                    case x86_mov_r32_rm32sib:
                        Code.Add(0x8b);
                        Code.AddRange(ModRMSIB(GetR(I.p[1].mreg), GetRM(I.p[3].mreg), 0, GetR(I.p[2].mreg)));
                        break;

                    case x86_mov_r32_rm32disp:
                        Code.Add(0x8b);
                        Code.AddRange(ModRMSIB(GetR(I.p[1].mreg), GetRM(I.p[2].mreg), 2, -1, -1, (int)I.p[3].v));
                        break;
                    case x86_mov_r32_rm16disp:
                        Code.Add(0x66);
                        Code.Add(0x8b);
                        Code.AddRange(ModRMSIB(GetR(I.p[1].mreg), GetRM(I.p[2].mreg), 2, -1, -1, (int)I.p[3].v));
                        break;
                    case x86_mov_r32_rm8disp:
                        Code.Add(0x8a);
                        Code.AddRange(ModRMSIB(GetR(I.p[1].mreg), GetRM(I.p[2].mreg), 2, -1, -1, (int)I.p[3].v));
                        break;


                    case x86_mov_rm32disp_imm32:
                        Code.Add(0xc7);
                        Code.AddRange(ModRMSIB(0, GetRM(I.p[1].mreg), 2, -1, -1, (int)I.p[2].v));
                        AddImm32(Code, I.p[3].v);
                        break;
                    case x86_mov_rm16disp_imm32:
                        Code.Add(0x67); // CHECK
                        Code.Add(0xc7);
                        Code.AddRange(ModRMSIB(0, GetRM(I.p[1].mreg), 2, -1, -1, (int)I.p[2].v));
                        AddImm16(Code, I.p[3].v);
                        break;
                    case x86_mov_rm8disp_imm32:
                        Code.Add(0xc6);
                        Code.AddRange(ModRMSIB(0, GetRM(I.p[1].mreg), 2, -1, -1, (int)I.p[2].v));
                        AddImm8(Code, I.p[3].v);
                        break;


                    case x86_mov_rm8disp_r32:
                        Code.Add(0x88);
                        Code.AddRange(ModRMSIB(GetR(I.p[3].mreg), GetRM(I.p[1].mreg), 2, -1, -1, (int)I.p[2].v));
                        break;
                    case x86_mov_rm16disp_r32:
                        Code.Add(0x67); // CHECK
                        Code.Add(0x89);
                        Code.AddRange(ModRMSIB(GetR(I.p[3].mreg), GetRM(I.p[1].mreg), 2, -1, -1, (int)I.p[2].v));
                        break;
                    case x86_mov_rm32disp_r32:
                        Code.Add(0x89);
                        Code.AddRange(ModRMSIB(GetR(I.p[3].mreg), GetRM(I.p[1].mreg), 2, -1, -1, (int)I.p[2].v));
                        break;

                    case x86_mov_r32_rm32sibscaledisp:
                        Code.Add(0x8b);
                        Code.AddRange(ModRMSIB(GetR(I.p[1].mreg), GetRM(I.p[2].mreg), 2, GetRM(I.p[3].mreg), -1, (int)I.p[5].v, false, (int)I.p[4].v));
                        break;

                    case x86_movzxb_r32_rm32sibscaledisp:
                        Code.Add(0x0f);
                        Code.Add(0xb6);
                        Code.AddRange(ModRMSIB(GetR(I.p[1].mreg), GetRM(I.p[2].mreg), 2, GetRM(I.p[3].mreg), -1, (int)I.p[5].v, false, (int)I.p[4].v));
                        break;

                    case x86_movzxw_r32_rm32sibscaledisp:
                        Code.Add(0x0f);
                        Code.Add(0xb7);
                        Code.AddRange(ModRMSIB(GetR(I.p[1].mreg), GetRM(I.p[2].mreg), 2, GetRM(I.p[3].mreg), -1, (int)I.p[5].v, false, (int)I.p[4].v));
                        break;

                    case x86_lea_r32:
                        Code.Add(0x8d);
                        Code.AddRange(ModRMSIB(I.p[1].mreg, I.p[2].mreg));
                        break;

                    case x86_sar_rm32_imm8:
                        Code.Add(0xc1);
                        Code.AddRange(ModRMSIB(7, I.p[1].mreg));
                        AddImm8(Code, I.p[2].v);
                        break;

                    default:
                        throw new NotImplementedException(insts[(int)I.p[0].v]);
                }
            }

            // Patch up references

            for(int i = 0; i < rel_srcs.Count; i++)
            {
                var src = rel_srcs[i];
                var dest = rel_dests[i];
                var dest_offset = il_starts[dest] + code_start;
                var offset = dest_offset - src - 4;
                InsertImm32(Code, offset, src);
            }
        }

        private void InsertImm32(List<byte> c, int v, int offset)
        {
            c[offset] = (byte)(v & 0xff);
            c[offset + 1] = (byte)((v >> 8) & 0xff);
            c[offset + 2] = (byte)((v >> 16) & 0xff);
            c[offset + 3] = (byte)((v >> 24) & 0xff);
        }

        private void AddImm32(List<byte> c, long v)
        {
            c.Add((byte)(v & 0xff));
            c.Add((byte)((v >> 8) & 0xff));
            c.Add((byte)((v >> 16) & 0xff));
            c.Add((byte)((v >> 24) & 0xff));
        }

        private void AddImm16(List<byte> c, long v)
        {
            c.Add((byte)(v & 0xff));
            c.Add((byte)((v >> 8) & 0xff));
        }

        private void AddImm8(List<byte> c, long v)
        {
            c.Add((byte)(v & 0xff));
        }

        private IEnumerable<byte> ModRMSIB(Reg r, Reg rm)
        {
            int r_val = GetR(r);
            int rm_val, mod_val, disp_len, disp_val;
            bool rm_is_ebp;
            GetModRM(rm, out rm_val, out mod_val, out disp_len, out disp_val, out rm_is_ebp);
            return ModRMSIB(r_val, rm_val, mod_val, -1, disp_len, disp_val, rm_is_ebp);
        }

        private IEnumerable<byte> ModRMSIB(int r, Target.Reg rm, bool is_rm8 = false)
        {
            int rm_val, mod_val, disp_len, disp_val;
            bool rm_is_ebp;
            GetModRM(rm, out rm_val, out mod_val, out disp_len, out disp_val, out rm_is_ebp);
            return ModRMSIB(r, rm_val, mod_val, -1, disp_len, disp_val, rm_is_ebp);
        }
        /* private IEnumerable<byte> ModRM(Reg r, Reg rm)
        {
            int r_val = GetR(r);
            int rm_val, mod_val, disp_len, disp_val;
            GetModRM(rm, out rm_val, out mod_val, out disp_len, out disp_val);
            return ModRM(r_val, rm_val, mod_val, disp_len, disp_val);
        } */

        private int GetR(Reg r)
        {
            if (r == r_eax)
                return 0;
            else if (r == r_ecx)
                return 1;
            else if (r == r_edx)
                return 2;
            else if (r == r_ebx)
                return 3;
            else if (r == r_esp)
                return 4;
            else if (r == r_ebp)
                return 5;
            else if (r == r_esi)
                return 6;
            else if (r == r_edi)
                return 7;

            throw new NotSupportedException();
        }

        private byte PlusRD(int v, Reg mreg)
        {
            if (mreg == r_eax)
                return (byte)(v + 0);
            else if (mreg == r_ecx)
                return (byte)(v + 1);
            else if (mreg == r_edx)
                return (byte)(v + 2);
            else if (mreg == r_ebx)
                return (byte)(v + 3);
            else if (mreg == r_esp)
                return (byte)(v + 4);
            else if (mreg == r_ebp)
                return (byte)(v + 5);
            else if (mreg == r_esi)
                return (byte)(v + 6);
            else if (mreg == r_edi)
                return (byte)(v + 7);

            throw new NotSupportedException();
        }

        /* private IEnumerable<byte> ModRM(int r, Target.Reg rm)
        {
            int rm_val, mod_val, disp_len, disp_val;
            GetModRM(rm, out rm_val, out mod_val, out disp_len, out disp_val);
            return ModRM(r, rm_val, mod_val, disp_len, disp_val);
        } */

        private void GetModRM(Reg rm, out int rm_val, out int mod_val, out int disp_len, out int disp_val, out bool rm_is_ebp)
        {
            if (rm is Target.ContentsReg)
            {
                var cr = rm as Target.ContentsReg;
                if (cr.disp == 0 && !cr.basereg.Equals(r_ebp))
                {
                    mod_val = 0;
                    disp_len = 0;
                }
                else if (cr.disp >= -128 && cr.disp < 127)
                {
                    mod_val = 1;
                    disp_len = 1;
                }
                else
                {
                    mod_val = 2;
                    disp_len = 4;
                }
                disp_val = (int)cr.disp;
                rm_val = GetRM(cr.basereg);
                rm_is_ebp = cr.basereg.Equals(r_ebp);
            }
            else
            {
                mod_val = 3;
                disp_len = 0;
                disp_val = 0;
                rm_val = GetRM(rm);
                rm_is_ebp = false;
            }
        }

        private int GetMod(Target.Reg rm)
        {
            if(rm is Target.ContentsReg)
            {
                var cr = rm as Target.ContentsReg;
                if (cr.disp == 0)
                    return 0;
                else if (cr.disp >= -128 && cr.disp < 127)
                    return 1;
                else
                    return 2;
            }
            return 3;
        }

        private int GetRM(Target.Reg rm)
        {
            if(rm is Target.ContentsReg)
            {
                var cr = rm as Target.ContentsReg;
                rm = cr.basereg;
            }

            if (rm == r_eax)
                return 0;
            else if (rm == r_ecx)
                return 1;
            else if (rm == r_edx)
                return 2;
            else if (rm == r_ebx)
                return 3;
            else if (rm == r_esp)
                return 4;
            else if (rm == r_ebp)
                return 5;
            else if (rm == r_esi)
                return 6;
            else if (rm == r_edi)
                return 7;

            throw new NotSupportedException();
        }

        /* private IEnumerable<byte> ModRM(int r, int rm, int mod, int disp_len = 0, int disp = 0)
        {
            yield return (byte)(mod << 6 | r << 3 | rm);
            for (int i = 0; i < disp_len; i++)
                yield return (byte)(disp >> (8 * i));
        } */

        private IEnumerable<byte> ModRMSIB(int r, int rm, int mod,
            int index = -1, int disp_len = 0,
            int disp = 0, bool rm_is_ebp = true, int scale = -1)
        {
            /* catch the case where we're trying to do something to esp
                or ebp without an sib byte */

            int _base = -1;
            int ss = -1;
            bool has_sib = false;

            if (index >= 0)
            {
                _base = rm;
                has_sib = true;
                rm = 4;
                if (mod == 3)
                    throw new NotSupportedException("SIB addressing with mod == 3");
                if (scale == -1)
                    scale = 1;
            }
            else if (rm == 4 && mod != 3)
            {
                _base = 4;
                index = 4;
                has_sib = true;
            }
            else if(rm == 5 && mod == 0 && rm_is_ebp)
            {
                _base = 5;
                index = 4;
                ss = 0;
                if (disp_len == 0)
                {
                    disp = 0;
                    disp_len = 1;
                }
                has_sib = true;
            }

            if(disp_len == -1 && mod == 2)
            {
                if (disp == 0)
                {
                    mod = 0;
                    disp_len = 0;
                }
                else if (disp >= sbyte.MinValue && disp <= sbyte.MaxValue)
                    disp_len = 1;
                else
                    disp_len = 4;
            }

            if (disp_len == 1)
                mod = 1;
            else if (disp_len == 4)
                mod = 2;

            yield return (byte)(mod << 6 | r << 3 | rm);

            if(has_sib)
            {
                if(ss == -1)
                {
                    switch(scale)
                    {
                        case -1:
                        case 1:
                            ss = 0;
                            break;
                        case 2:
                            ss = 1;
                            break;
                        case 4:
                            ss = 2;
                            break;
                        case 8:
                            ss = 4;
                            break;
                        default:
                            throw new NotSupportedException("Invalid SIB scale: " + scale.ToString());
                    }
                }

                yield return (byte)(ss << 6 | index << 3 | _base);
            }

            for (int i = 0; i < disp_len; i++)
                yield return (byte)(disp >> (8 * i));
        }
    }
}