﻿/* Copyright (C) 2017 by John Cronin
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
using System.Runtime.CompilerServices;

namespace libsupcs
{
    unsafe class exceptions
    {
        static void** start, end, cur;
        const int stack_length = 1024;

        [MethodImpl(MethodImplOptions.InternalCall)]
        [MethodReferenceAlias("push_ehdr")]
        static extern void PushEhdr(void* eh, void* fp);

        [MethodImpl(MethodImplOptions.InternalCall)]
        [MethodReferenceAlias("pop_ehdr")]
        static extern void* PopEhdr();

        [MethodImpl(MethodImplOptions.InternalCall)]
        [MethodReferenceAlias("peek_ehdr")]
        static extern void* PeekEhdr();

        [MethodImpl(MethodImplOptions.InternalCall)]
        [MethodReferenceAlias("pop_fp")]
        static extern void* PopFramePointer();
        
        // Default versions of push/pop ehdr, not thread safe
        [AlwaysCompile]
        [WeakLinkage]
        [MethodAlias("push_ehdr")]
        static void push_ehdr(void *eh, void *fp)
        {
            if(start == null)
            {
                start = (void**)MemoryOperations.GcMalloc(stack_length * sizeof(void*));
                end = start + stack_length;
                cur = start;
            }

            if ((cur + 1) >= end)
            {
                System.Diagnostics.Debugger.Break();
                throw new OutOfMemoryException("exception header stack overflowed");
            }

            *cur = eh;
            cur++;
            *cur = fp;
            cur++;
        }

        [AlwaysCompile]
        [WeakLinkage]
        [MethodAlias("pop_ehdr")]
        static void* pop_ehdr()
        {
            if (start == null || cur <= start)
            {
                System.Diagnostics.Debugger.Break();
                throw new OutOfMemoryException("exception header stack underflowed");
            }

            cur--;
            return *cur;
        }

        [AlwaysCompile]
        [WeakLinkage]
        [MethodAlias("peek_ehdr")]
        static void* peek_ehdr()
        {
            if (start == null || cur <= start)
            {
                return null;
            }

            return *(cur - 2);
        }

        [AlwaysCompile]
        [WeakLinkage]
        [MethodAlias("pop_fp")]
        static void* pop_fp()
        {
            if (start == null || cur <= start)
            {
                System.Diagnostics.Debugger.Break();
                throw new OutOfMemoryException("exception header stack underflowed");
            }

            cur--;
            return *cur;
        }

        [AlwaysCompile]
        [WeakLinkage]
        [MethodAlias("enter_try")]
        internal static void enter_try(void *eh, void *fp)
        {
            if (eh == PeekEhdr())
                System.Diagnostics.Debugger.Break();
            PushEhdr(eh, fp);
        }

        [AlwaysCompile]
        [WeakLinkage]
        [MethodAlias("enter_catch")]
        internal static void enter_catch(void* eh, void *fp)
        {
            PushEhdr(eh, fp);
        }

        [AlwaysCompile]
        [WeakLinkage]
        [MethodAlias("leave_try")]
        internal static void leave_try(void* eh)
        {
            void* fp = PopFramePointer();
            void* popped = PopEhdr();
            if(eh != popped)
                while (true) ;

            void** ehdr = (void**)eh;

            int eh_type = *(int*)ehdr;
            if (eh_type == 2)
            {
                // handle finally clause
                void* handler = *(ehdr + 1);
                OtherOperations.CallI(fp, handler);
                PopFramePointer();
                PopEhdr();
            }
        }

        [AlwaysCompile]
        [WeakLinkage]
        [MethodAlias("leave_handler")]
        internal static void leave_handler(void* eh)
        {
            while (true) ;
        }

        [AlwaysCompile]
        [WeakLinkage]
        [MethodAlias("rethrow")]
        internal static void rethrow(void* eh)
        {
            while (true) ;
        }
    }
}
