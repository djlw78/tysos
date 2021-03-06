﻿/* Copyright (C) 2011 by John Cronin
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

namespace tydisasm.x86_64
{
    partial class x86_64_disasm
    {
        void init_opcodes()
        {
            // 0x00
            opcodes.Add(0x00, new opcode { name = "add", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x01, new opcode { name = "add", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x02, new opcode { name = "add", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x03, new opcode { name = "add", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x04, new opcode { name = "add", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["al"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x05, new opcode { name = "add", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["eax"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0x08, new opcode { name = "or", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x09, new opcode { name = "or", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x0a, new opcode { name = "or", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0b, new opcode { name = "or", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x0c, new opcode { name = "or", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["al"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x0d, new opcode { name = "or", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["eax"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });

            // 0x10
            opcodes.Add(0x10, new opcode { name = "adc", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x11, new opcode { name = "adc", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x12, new opcode { name = "adc", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x13, new opcode { name = "adc", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x14, new opcode { name = "adc", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["al"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x15, new opcode { name = "adc", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["eax"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0x18, new opcode { name = "sbb", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x19, new opcode { name = "sbb", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x1a, new opcode { name = "sbb", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x1b, new opcode { name = "sbb", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x1c, new opcode { name = "sbb", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["al"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x1d, new opcode { name = "sbb", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["eax"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });

            // 0x20
            opcodes.Add(0x20, new opcode { name = "and", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x21, new opcode { name = "and", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x22, new opcode { name = "and", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x23, new opcode { name = "and", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x24, new opcode { name = "and", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["al"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x25, new opcode { name = "and", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["eax"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0x28, new opcode { name = "sub", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x29, new opcode { name = "sub", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x2a, new opcode { name = "sub", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x2b, new opcode { name = "sub", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x2c, new opcode { name = "sub", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["al"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x2d, new opcode { name = "sub", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["eax"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });

            // 0x30
            opcodes.Add(0x30, new opcode { name = "xor", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x31, new opcode { name = "xor", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x32, new opcode { name = "xor", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x33, new opcode { name = "xor", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x34, new opcode { name = "xor", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["al"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x35, new opcode { name = "xor", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["eax"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0x38, new opcode { name = "cmp", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x39, new opcode { name = "cmp", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x3a, new opcode { name = "cmp", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x3b, new opcode { name = "cmp", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x3c, new opcode { name = "cmp", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["al"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x3d, new opcode { name = "cmp", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["eax"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });

            // 0x40

            // 0x50
            opcodes.Add(0x50, new opcode { name = "push", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rax"] }, extends_on_rexb = true } } });
            opcodes.Add(0x51, new opcode { name = "push", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rcx"] }, extends_on_rexb = true } } });
            opcodes.Add(0x52, new opcode { name = "push", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rdx"] }, extends_on_rexb = true } } });
            opcodes.Add(0x53, new opcode { name = "push", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rbx"] }, extends_on_rexb = true } } });
            opcodes.Add(0x54, new opcode { name = "push", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rsp"] }, extends_on_rexb = true } } });
            opcodes.Add(0x55, new opcode { name = "push", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rbp"] }, extends_on_rexb = true } } });
            opcodes.Add(0x56, new opcode { name = "push", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rsi"] }, extends_on_rexb = true } } });
            opcodes.Add(0x57, new opcode { name = "push", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rdi"] }, extends_on_rexb = true } } });
            opcodes.Add(0x58, new opcode { name = "pop", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rax"] }, extends_on_rexb = true } } });
            opcodes.Add(0x59, new opcode { name = "pop", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rcx"] }, extends_on_rexb = true } } });
            opcodes.Add(0x5a, new opcode { name = "pop", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rdx"] }, extends_on_rexb = true } } });
            opcodes.Add(0x5b, new opcode { name = "pop", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rbx"] }, extends_on_rexb = true } } });
            opcodes.Add(0x5c, new opcode { name = "pop", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rsp"] }, extends_on_rexb = true } } });
            opcodes.Add(0x5d, new opcode { name = "pop", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rbp"] }, extends_on_rexb = true } } });
            opcodes.Add(0x5e, new opcode { name = "pop", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rsi"] }, extends_on_rexb = true } } });
            opcodes.Add(0x5f, new opcode { name = "pop", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rdi"] }, extends_on_rexb = true } } });

            // 0x60
            opcodes.Add(0x63, new opcode { name = "movsxd", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32, extends_on_rexw = false } } });
            opcodes.Add(0x69, new opcode { name = "imul", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = false });
            opcodes.Add(0x6b, new opcode { name = "imul", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1, immediate_extends_on_rexw = false });

            // 0x70
            opcodes.Add(0x70, new opcode { name = "jo", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x71, new opcode { name = "jno", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x72, new opcode { name = "jb", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x73, new opcode { name = "jnb", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x74, new opcode { name = "jz", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x75, new opcode { name = "jnz", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x76, new opcode { name = "jbe", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x77, new opcode { name = "ja", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x78, new opcode { name = "js", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x79, new opcode { name = "jns", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x7a, new opcode { name = "jp", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x7b, new opcode { name = "jnp", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x7c, new opcode { name = "jl", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x7d, new opcode { name = "jnl", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x7e, new opcode { name = "jle", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });
            opcodes.Add(0x7f, new opcode { name = "jg", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });


            // 0x80
            opcodes.Add(0x80, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x17000080, new opcode { name = "cmp", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x81, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x10000081, new opcode { name = "add", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4 });
            opcodes.Add(0x14000081, new opcode { name = "and", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4 });
            opcodes.Add(0x15000081, new opcode { name = "sub", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4 });
            opcodes.Add(0x17000081, new opcode { name = "cmp", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4 });
            opcodes.Add(0x82, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x83, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x10000083, new opcode { name = "add", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1, immediate_extends_on_rexw = false });
            opcodes.Add(0x11000083, new opcode { name = "or", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1, immediate_extends_on_rexw = false });
            opcodes.Add(0x15000083, new opcode { name = "sub", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x17000083, new opcode { name = "cmp", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x88, new opcode { name = "mov", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x89, new opcode { name = "mov", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x8a, new opcode { name = "mov", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x8b, new opcode { name = "mov", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x8d, new opcode { name = "lea", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });

            // 0x90
            opcodes.Add(0x9c, new opcode { name = "pushf", has_rm = false, operand_sources = new List<opcode.operand_source> { } });
            opcodes.Add(0x9d, new opcode { name = "popf", has_rm = false, operand_sources = new List<opcode.operand_source> { } });

            // 0xa0
            opcodes.Add(0xa4, new opcode { name = "movs", has_rm = false, operand_sources = new List<opcode.operand_source> { } });
            opcodes.Add(0xa5, new opcode { name = "movs", has_rm = false, operand_sources = new List<opcode.operand_source> { } });
            opcodes.Add(0xab, new opcode { name = "stos", has_rm = false, operand_sources = new List<opcode.operand_source> { } });

            // 0xb0
            opcodes.Add(0xb8, new opcode { name = "mov", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["eax"] }, extends_on_rexb = true }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0xb9, new opcode { name = "mov", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["ecx"] }, extends_on_rexb = true }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0xba, new opcode { name = "mov", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["edx"] }, extends_on_rexb = true }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0xbb, new opcode { name = "mov", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["ebx"] }, extends_on_rexb = true }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0xbc, new opcode { name = "mov", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["esp"] }, extends_on_rexb = true }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0xbd, new opcode { name = "mov", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["ebp"] }, extends_on_rexb = true }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0xbe, new opcode { name = "mov", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["esi"] }, extends_on_rexb = true }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });
            opcodes.Add(0xbf, new opcode { name = "mov", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["edi"] }, extends_on_rexb = true }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4, immediate_extends_on_rexw = true });

            // 0xc0
            opcodes.Add(0xc0, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x140000c0, new opcode { name = "sal", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x150000c0, new opcode { name = "shr", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x170000c0, new opcode { name = "sar", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0xc1, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x140000c1, new opcode { name = "sal", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x150000c1, new opcode { name = "shr", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0x170000c1, new opcode { name = "sar", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0xc3, new opcode { name = "ret", has_rm = false, operand_sources = new List<opcode.operand_source> { } });
            opcodes.Add(0xc6, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x100000c6, new opcode { name = "mov", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 1 });
            opcodes.Add(0xc7, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x100000c7, new opcode { name = "mov", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_length = 4 });
            opcodes.Add(0xc9, new opcode { name = "leave", has_rm = false, operand_sources = new List<opcode.operand_source> { } });
            opcodes.Add(0xcc, new opcode { name = "int3", has_rm = false, operand_sources = new List<opcode.operand_source> { } });
            opcodes.Add(0xcd, new opcode { name = "int", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm } }, immediate_extends_on_rexw = false, immediate_length = 1 });
            opcodes.Add(0xce, new opcode { name = "into", has_rm = false, operand_sources = new List<opcode.operand_source> { } });
            opcodes.Add(0xcf, new opcode { name = "iret", has_rm = false, operand_sources = new List<opcode.operand_source> { } });

            // 0xd0
            opcodes.Add(0xd0, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x140000d0, new opcode { name = "sal", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Const, immediate = 1 } } } });
            opcodes.Add(0x150000d0, new opcode { name = "shr", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Const, immediate = 1 } } } });
            opcodes.Add(0x170000d0, new opcode { name = "sar", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Const, immediate = 1 } } } });
            opcodes.Add(0xd1, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x140000d1, new opcode { name = "sal", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Const, immediate = 1 } } } });
            opcodes.Add(0x150000d1, new opcode { name = "shr", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Const, immediate = 1 } } } });
            opcodes.Add(0x170000d1, new opcode { name = "sar", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Const, immediate = 1 } } } });
            opcodes.Add(0xd2, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x140000d2, new opcode { name = "sal", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["cl"] } } } });
            opcodes.Add(0x150000d2, new opcode { name = "shr", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["cl"] } } } });
            opcodes.Add(0x170000d2, new opcode { name = "sar", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["cl"] } } } });
            opcodes.Add(0xd3, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x140000d3, new opcode { name = "sal", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["cl"] } } } });
            opcodes.Add(0x150000d3, new opcode { name = "shr", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["cl"] } } } });
            opcodes.Add(0x170000d3, new opcode { name = "sar", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["cl"] } } } });

            // 0xe0
            opcodes.Add(0xe8, new opcode { name = "call", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0xe9, new opcode { name = "jmp", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0xeb, new opcode { name = "jmp", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 1 });

            // 0xf0
            opcodes.Add(0xf6, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0xf7, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x100000f7, new opcode { name = "test", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm, length = opcode.operand_source.reg_length.r32 } }, immediate_length = 4, immediate_extends_on_rexw = false });
            opcodes.Add(0x120000f7, new opcode { name = "not", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x130000f7, new opcode { name = "neg", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x140000f7, new opcode { name = "mul", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Fixed, Fixed_Location = new location { type = location.location_type.Register, reg_no = reg_nos["rax"] } }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r64 } } });
            opcodes.Add(0x160000f7, new opcode { name = "div", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0x170000f7, new opcode { name = "idiv", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });
            opcodes.Add(0xfa, new opcode { name = "cli", has_rm = false, operand_sources = new List<opcode.operand_source> { } });
            opcodes.Add(0xfb, new opcode { name = "sti", has_rm = false, operand_sources = new List<opcode.operand_source> { } });
            opcodes.Add(0xfe, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0xff, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x120000ff, new opcode { name = "call", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });



            // two opcodes
            // 0f 00
            opcodes.Add(0x0f01, new opcode { name = "invalid", has_rm = true, reinterpret_after_r = true });
            opcodes.Add(0x12000f01, new opcode { name = "lgdt", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r64 } } });
            opcodes.Add(0x13000f01, new opcode { name = "lidt", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r64 } } });

            // 0f 10
            opcodes.Add(0x0f10, new opcode { name = "movups", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm } } });
            opcodes.Add(0x0f11, new opcode { name = "movups", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm } } });

            // 0f 20
            opcodes.Add(0x0f20, new opcode { name = "mov", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r64 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.cr } } });
            opcodes.Add(0x0f22, new opcode { name = "mov", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.cr }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r64 } } });

            // 0f 30

            // 0f 40
            
            // 0f 50
                        
            // 0f 60

            // 0f 70
            opcodes.Add(0x0f7e, new opcode { name = "movq", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm } } });

            // 0f 80
            opcodes.Add(0x0f80, new opcode { name = "jo", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f81, new opcode { name = "jno", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f82, new opcode { name = "jb", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f83, new opcode { name = "jae", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f84, new opcode { name = "je", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f85, new opcode { name = "jne", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f86, new opcode { name = "jbe", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f87, new opcode { name = "ja", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f88, new opcode { name = "js", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f89, new opcode { name = "jns", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f8a, new opcode { name = "jp", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f8b, new opcode { name = "jnp", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f8c, new opcode { name = "jl", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f8d, new opcode { name = "jnl", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f8e, new opcode { name = "jle", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });
            opcodes.Add(0x0f8f, new opcode { name = "jg", has_rm = false, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.Imm, is_pc_relative = true } }, immediate_length = 4 });

            // 0f 90
            opcodes.Add(0x0f90, new opcode { name = "seto", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f91, new opcode { name = "setno", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f92, new opcode { name = "setb", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f93, new opcode { name = "setae", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f94, new opcode { name = "sete", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f95, new opcode { name = "setne", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f96, new opcode { name = "setbe", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f97, new opcode { name = "seta", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f98, new opcode { name = "sets", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f99, new opcode { name = "setns", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f9a, new opcode { name = "setp", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f9b, new opcode { name = "setnp", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f9c, new opcode { name = "setl", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f9d, new opcode { name = "setnl", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f9e, new opcode { name = "setle", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0f9f, new opcode { name = "setg", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });

            // 0f a0
            opcodes.Add(0x0faf, new opcode { name = "imul", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });

            // 0f b0
            opcodes.Add(0x0fb6, new opcode { name = "movzx", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0fb7, new opcode { name = "movzx", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r16 } } });
            opcodes.Add(0x0fbe, new opcode { name = "movsx", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r8 } } });
            opcodes.Add(0x0fbf, new opcode { name = "movsx", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.r32 }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r16 } } });


            // 0f c0

            // 0f d0
            opcodes.Add(0x0fd6, new opcode { name = "movq", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm } } });

            // 0f e0

            // 0f f0


            // three opcodes

            // f2 0f 00

            // f2 0f 10
            opcodes.Add(0xf20f10, new opcode { name = "movsd", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm } } });
            opcodes.Add(0xf20f11, new opcode { name = "movsd", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm } } });

            // f2 0f 20

            // f2 0f 30

            // f2 0f 40

            // f2 0f 50
            opcodes.Add(0xf20f5e, new opcode { name = "divsd", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm } } });

            // f2 0f 60

            // f2 0f 70

            // f2 0f 80

            // f2 0f 90

            // f2 0f a0

            // f2 0f b0

            // f2 0f c0

            // f2 0f d0

            // f2 0f e0


            // f3 0f 00

            // f3 0f 10

            // f3 0f 20

            // f3 0f 30

            // f3 0f 40

            // f3 0f 50

            // f3 0f 60

            // f3 0f 70
            opcodes.Add(0xf30f7e, new opcode { name = "movq", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm } } });

            // f3 0f 80

            // f3 0f 90

            // f3 0f a0

            // f3 0f b0

            // f3 0f c0

            // f3 0f d0

            // f3 0f e0


            // 66 0f 00

            // 66 0f 10

            // 66 0f 20
            opcodes.Add(0x660f2f, new opcode { name = "comisd", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm } } });
            opcodes.Add(0x660f2e, new opcode { name = "ucomisd", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm } } });

            // 66 0f 30

            // 66 0f 40

            // 66 0f 50

            // 66 0f 60
            opcodes.Add(0x660f6e, new opcode { name = "movdq", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.r32 } } });

            // 66 0f 70

            // 66 0f 80

            // 66 0f 90

            // 66 0f a0

            // 66 0f b0

            // 66 0f c0

            // 66 0f d0
            opcodes.Add(0x660fd6, new opcode { name = "movq", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm } } });

            // 66 0f e0


            // 0f 3a 00
            opcodes.Add(0x0f3a0b, new opcode { name = "roundsd", has_rm = true, operand_sources = new List<opcode.operand_source> { new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_Reg, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.ModRM_RM, length = opcode.operand_source.reg_length.xmm }, new opcode.operand_source { type = opcode.operand_source.src_type.Imm, length = opcode.operand_source.reg_length.r8 } }, immediate_length = 1 });
        }
    }
}
