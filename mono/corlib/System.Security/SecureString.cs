//
// System.Security.SecureString class
//
// Authors
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// Copyright (C) 2004-2005 Novell, Inc (http://www.novell.com)
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

#if NET_2_0

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Security.Permissions;

namespace System.Security {

	[MonoTODO ("work in progress - encryption is missing")]
	public sealed class SecureString : CriticalFinalizerObject, IDisposable {

		private const int BlockSize = 16;
		private const int MaxSize = 65536;

		private int length;
		private bool disposed;
		private bool read_only;
		private byte[] data;

		static SecureString ()
		{
			// ProtectedMemory has been moved to System.Security.dll
			// we use reflection to call it (if available) or we'll 
			// throw an exception
		}

		public SecureString ()
		{
			Alloc (BlockSize >> 1, false);
		}

		[CLSCompliant (false)]
		public unsafe SecureString (char* value, int length)
		{
			if (value == null)
				throw new ArgumentNullException ("value");
			if ((length < 0) || (length > MaxSize))
				throw new ArgumentOutOfRangeException ("length", "< 0 || > 65536");

			this.length = length; // real length
			Alloc (length, false);
			int n = 0;
			for (int i = 0; i < length; i++) {
				char c = *value++;
				data[n++] = (byte) (c >> 8);
				data[n++] = (byte) c;
			}
			Encrypt ();
		}

		// properties

		public int Length {
			get {
				if (disposed)
					throw new ObjectDisposedException ("SecureString");
				return length;
			}
		}

		public void AppendChar (char c)
		{
			if (disposed)
				throw new ObjectDisposedException ("SecureString");
			if (read_only) {
				throw new InvalidOperationException (Locale.GetText (
					"SecureString is read-only."));
			}
			if (length == MaxSize)
				throw new ArgumentOutOfRangeException ("length", "> 65536");

			try {
				Decrypt ();
				int n = length * 2;
				Alloc (++length, true);
				data[n++] = (byte) (c >> 8);
				data[n++] = (byte) c;
			}
			finally {
				Encrypt ();
			}
		}

		public void Clear ()
		{
			if (disposed)
				throw new ObjectDisposedException ("SecureString");
			if (read_only) {
				throw new InvalidOperationException (Locale.GetText (
					"SecureString is read-only."));
			}

			Array.Clear (data, 0, data.Length);
			length = 0;
		}

		public SecureString Copy () 
		{
			SecureString ss = new SecureString ();
			ss.data = (byte[]) data.Clone ();
			return ss;
		}

		public void Dispose ()
		{
			disposed = true;
			// don't call clear because we could be either in read-only 
			// or already disposed - but DO CLEAR the data
			if (data != null) {
				Array.Clear (data, 0, data.Length);
				data = null;
			}
			length = 0;
		}

		public void InsertAt (int index, char c)
		{
			if (disposed)
				throw new ObjectDisposedException ("SecureString");
			if (read_only) {
				throw new InvalidOperationException (Locale.GetText (
					"SecureString is read-only."));
			}
			if ((index < 0) || (index > length))
				throw new ArgumentOutOfRangeException ("index", "< 0 || > length");
			// insert increments length
			if (length >= MaxSize) {
				string msg = Locale.GetText ("Maximum string size is '{0}'.", MaxSize);
				throw new ArgumentOutOfRangeException ("index", msg);
			}

			try {
				Decrypt ();
				Alloc (++length, true);
				int n = index * 2;
				Buffer.BlockCopy (data, n, data, n + 2, data.Length - 2);
				data[n++] = (byte) (c >> 8);
				data[n] = (byte) c;
			}
			finally {
				Encrypt ();
			}
		}

		public bool IsReadOnly ()
		{
			if (disposed)
				throw new ObjectDisposedException ("SecureString");
			return read_only;
		}

		public void MakeReadOnly ()
		{
			read_only = true;
		}

		public void RemoveAt (int index)
		{
			if (disposed)
				throw new ObjectDisposedException ("SecureString");
			if (read_only) {
				throw new InvalidOperationException (Locale.GetText (
					"SecureString is read-only."));
			}
			if ((index < 0) || (index >= length))
				throw new ArgumentOutOfRangeException ("index", "< 0 || > length");

			try {
				Decrypt ();
				Buffer.BlockCopy (data, index + 1, data, index, data.Length - index - 1);
				Alloc (--length, true);
			}
			finally {
				Encrypt ();
			}
		}

		public void SetAt (int index, char c)
		{
			if (disposed)
				throw new ObjectDisposedException ("SecureString");
			if (read_only) {
				throw new InvalidOperationException (Locale.GetText (
					"SecureString is read-only."));
			}
			if ((index < 0) || (index >= length))
				throw new ArgumentOutOfRangeException ("index", "< 0 || > length");

			try {
				Decrypt ();
				int n = index * 2;
				data[n++] = (byte) (c >> 8);
				data[n] = (byte) c;
			}
			finally {
				Encrypt ();
			}
		}

		// internal/private stuff

		[MonoTODO ("ProtectedMemory is in System.Security.dll - move this into the runtime/icall")]
		private void Encrypt ()
		{
			if ((data != null) && (data.Length > 0)) {
//				ProtectedMemory.Protect (data, MemoryProtectionScope.SameProcess);
			}
		}

		[MonoTODO ("ProtectedMemory is in System.Security.dll - move this into the runtime/icall")]
		private void Decrypt ()
		{
			if ((data != null) && (data.Length > 0)) {
//				ProtectedMemory.Unprotect (data, MemoryProtectionScope.SameProcess);
			}
		}

		// note: realloc only work for bigger buffers. Clear will 
		// reset buffers to default (and small) size.
		private void Alloc (int length, bool realloc) 
		{
			if ((length < 0) || (length > MaxSize))
				throw new ArgumentOutOfRangeException ("length", "< 0 || > 65536");

			// (size / blocksize) + 1 * blocksize
			// where size = length * 2 (unicode) and blocksize == 16 (ProtectedMemory)
			// length * 2 (unicode) / 16 (blocksize)
			int size = (length >> 3) + (((length & 0x7) == 0) ? 0 : 1) << 4;

			// is re-allocation necessary ? (i.e. grow or shrink 
			// but do not re-allocate the same amount of memory)
			if (realloc && (data != null) && (size == data.Length))
				return;

			if (realloc) {
				// copy, then clear
				byte[] newdata = new byte[size];
				Array.Copy (data, 0, newdata, 0, Math.Min (data.Length, newdata.Length));
				Array.Clear (data, 0, data.Length);
				data = newdata;
			} else {
				data = new byte[size];
			}
		}

		// dangerous method (put a LinkDemand on it)
		internal byte[] GetBuffer ()
		{
			byte[] secret = new byte[length << 1];
			try {
				Decrypt ();
				Buffer.BlockCopy (data, 0, secret, 0, secret.Length);
			}
			finally {
				Encrypt ();
			}
			// NOTE: CALLER IS RESPONSIBLE TO ZEROIZE THE DATA
			return secret;
		}
	}
}

#endif
