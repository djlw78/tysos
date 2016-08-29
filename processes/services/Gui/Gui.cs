﻿/* Copyright (C) 2015 by John Cronin
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

namespace gui
{
    partial class gui : tysos.ServerObject
    {
        tysos.ServerObject vfs;

        static void Main()
        {
            gui g = new gui();
            tysos.Syscalls.ProcessFunctions.RegisterSpecialProcess(g, tysos.Syscalls.ProcessFunctions.SpecialProcessType.Gui);
            g.MessageLoop();
        }

        public override bool InitServer()
        {
            // Register handlers for devices
            while (tysos.Syscalls.ProcessFunctions.GetSpecialProcess(tysos.Syscalls.ProcessFunctions.SpecialProcessType.Vfs) == null) ;
            vfs = tysos.Syscalls.ProcessFunctions.GetSpecialProcess(tysos.Syscalls.ProcessFunctions.SpecialProcessType.Vfs);

            vfs.InvokeAsync("RegisterAddHandler",
                new object[] { "class", "framebuffer", "RegisterDisplay", true },
                new Type[] { typeof(string), typeof(string), typeof(string), typeof(bool) });
            vfs.InvokeAsync("RegisterAddHandler",
                new object[] { "class", "input", "RegisterInput", true },
                new Type[] { typeof(string), typeof(string), typeof(string), typeof(bool) });

            return true;
        }
    }
}
