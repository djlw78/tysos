//
// System.ArgIterator.cs
//
// Authors:
//   Dick Porter (dick@ximian.com)
//   Paolo Molaro (lupus@ximian.com)
//
// (C) Ximian, Inc.  http://www.ximian.com
//

//
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System 
{
	[StructLayout (LayoutKind.Auto)]
	public struct ArgIterator
	{
		IntPtr sig;
		IntPtr args;
		int next_arg;
		int num_args;

		[MethodImpl (MethodImplOptions.InternalCall)]
		extern void Setup (IntPtr argsp, IntPtr start);

		public ArgIterator (RuntimeArgumentHandle arglist)
		{
			sig = IntPtr.Zero;
			args = IntPtr.Zero;
			next_arg = num_args = 0;
			Setup (arglist.args, IntPtr.Zero);
		}

		[CLSCompliant (false)]
		unsafe public ArgIterator (RuntimeArgumentHandle arglist, void *ptr)
		{
			sig = IntPtr.Zero;
			args = IntPtr.Zero;
			next_arg = num_args = 0;
			Setup (arglist.args, (IntPtr) ptr);
		}

		public void End ()
		{
			next_arg = num_args;
		}

		public override bool Equals (object o)
		{
			throw new NotSupportedException (Locale.GetText ("ArgIterator does not support Equals."));
		}

		public override int GetHashCode ()
		{
			return sig.GetHashCode ();
		}

		[CLSCompliant (false)]
		public TypedReference GetNextArg ()
		{
			if (num_args == next_arg)
				throw new InvalidOperationException (Locale.GetText ("Invalid iterator position."));
			return IntGetNextArg ();
		}

		[MethodImpl (MethodImplOptions.InternalCall)]
		extern TypedReference IntGetNextArg ();

		[CLSCompliant (false)]
		public TypedReference GetNextArg (RuntimeTypeHandle rth)
		{
			if (num_args == next_arg)
				throw new InvalidOperationException (Locale.GetText ("Invalid iterator position."));
			return IntGetNextArg (rth.Value);
		}

		[MethodImpl (MethodImplOptions.InternalCall)]
		extern TypedReference IntGetNextArg (IntPtr rth);

		public RuntimeTypeHandle GetNextArgType ()
		{
			if (num_args == next_arg)
				throw new InvalidOperationException (Locale.GetText ("Invalid iterator position."));
			return new RuntimeTypeHandle (IntGetNextArgType ());
		}

		[MethodImpl (MethodImplOptions.InternalCall)]
		extern IntPtr IntGetNextArgType ();

		public int GetRemainingCount ()
		{
			return num_args - next_arg;
		}
	}
}
