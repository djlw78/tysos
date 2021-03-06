//
// BinaryFormatterTest.cs - Unit tests for 
//	System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
//
// Author:
//      Sebastien Pouliot  <sebastien@ximian.com>
//
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
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

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

using NUnit.Framework;

namespace MonoTests.System.Runtime.Serialization.Formatters.Binary {

	[Serializable]
	public class SerializationTest {
		private int integer;
		[NonSerialized]
		private bool boolean;

		public SerializationTest (bool b, int i)
		{
			boolean = b;
			integer = i;
		}

		public bool Boolean {
			get { return boolean; }
			set { boolean = value; }
		}

		public int Integer {
			get { return integer; }
			set { integer = value; }
		}
	}

	class SurrogateSelector: ISurrogateSelector {
		public void ChainSelector (ISurrogateSelector selector)
		{
		}

		public ISurrogateSelector GetNextSelector ()
		{
			return null;
		}

		public ISerializationSurrogate GetSurrogate (Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			selector = null;
			return null;
		}
	}

	[Serializable]
	sealed class ThisObjectReference : IObjectReference
	{
		internal static int Count;

		internal ThisObjectReference()
		{
		}

		public object GetRealObject(StreamingContext context)
		{
			Count++;
			return this;
		}
	}

	[Serializable]
	sealed class NewObjectReference : IObjectReference
	{
		internal static int Count;

		internal NewObjectReference()
		{
		}

		public object GetRealObject(StreamingContext context)
		{
			Count++;
			return new NewObjectReference();
		}
	}

	[TestFixture]
	public class BinaryFormatterTest {

		[Test]
		public void Constructor_Default ()
		{
			BinaryFormatter bf = new BinaryFormatter ();
#if NET_2_0
			Assert.AreEqual (FormatterAssemblyStyle.Simple, bf.AssemblyFormat, "AssemblyFormat");
#else
			Assert.AreEqual (FormatterAssemblyStyle.Full, bf.AssemblyFormat, "AssemblyFormat");
#endif
			Assert.IsNull (bf.Binder, "Binder");
			Assert.AreEqual (StreamingContextStates.All, bf.Context.State, "Context");
			Assert.AreEqual (TypeFilterLevel.Full, bf.FilterLevel, "FilterLevel");
			Assert.IsNull (bf.SurrogateSelector, "SurrogateSelector");
			Assert.AreEqual (FormatterTypeStyle.TypesAlways, bf.TypeFormat, "TypeFormat");
		}

		[Test]
		public void Constructor ()
		{
			SurrogateSelector ss = new SurrogateSelector ();
			BinaryFormatter bf = new BinaryFormatter (ss, new StreamingContext (StreamingContextStates.CrossMachine));
#if NET_2_0
			Assert.AreEqual (FormatterAssemblyStyle.Simple, bf.AssemblyFormat, "AssemblyFormat");
#else
			Assert.AreEqual (FormatterAssemblyStyle.Full, bf.AssemblyFormat, "AssemblyFormat");
#endif
			Assert.IsNull (bf.Binder, "Binder");
			Assert.AreEqual (StreamingContextStates.CrossMachine, bf.Context.State, "Context");
			Assert.AreEqual (TypeFilterLevel.Full, bf.FilterLevel, "FilterLevel");
			Assert.AreSame (ss, bf.SurrogateSelector, "SurrogateSelector");
			Assert.AreEqual (FormatterTypeStyle.TypesAlways, bf.TypeFormat, "TypeFormat");
		}

		public Stream GetSerializedStream ()
		{
			SerializationTest test = new SerializationTest (true, Int32.MinValue);
			BinaryFormatter bf = new BinaryFormatter ();
			MemoryStream ms = new MemoryStream ();
			bf.Serialize (ms, test);
			ms.Position = 0;
			return ms;
		}

		[Test]
		public void SerializationRoundtrip ()
		{
			Stream s = GetSerializedStream ();
			BinaryFormatter bf = new BinaryFormatter ();
			SerializationTest clone = (SerializationTest) bf.Deserialize (s);
			Assert.AreEqual (Int32.MinValue, clone.Integer, "Integer");
			Assert.IsFalse (clone.Boolean, "Boolean");
		}

		[Test]
		public void SerializationUnsafeRoundtrip ()
		{
			Stream s = GetSerializedStream ();
			BinaryFormatter bf = new BinaryFormatter ();
			SerializationTest clone = (SerializationTest) bf.UnsafeDeserialize (s, null);
			Assert.AreEqual (Int32.MinValue, clone.Integer, "Integer");
			Assert.IsFalse (clone.Boolean, "Boolean");
		}
		
		[Test]
		public void NestedObjectReference ()
		{
			MemoryStream ms = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();

			bf.Serialize(ms, new ThisObjectReference());
			bf.Serialize(ms, new NewObjectReference());
			ms.Position = 0;
			Assert.AreEqual (0, ThisObjectReference.Count, "#1");

			bf.Deserialize(ms);
			Assert.AreEqual (2, ThisObjectReference.Count, "#2");
			Assert.AreEqual (0, NewObjectReference.Count, "#3");
			try
			{
				bf.Deserialize(ms);
			}
			catch (SerializationException e)
			{
			}
			Assert.AreEqual (101, NewObjectReference.Count, "#4");
		}

		[Test]
		public void DateTimeArray ()
		{
			DateTime [] e = new DateTime [6];
			string [] names = new string [6];

			names [0] = "Today";  e [0] = DateTime.Today;
			names [1] = "Min";    e [1] = DateTime.MinValue;
			names [2] = "Max";    e [2] = DateTime.MaxValue;
			names [3] = "BiCent"; e [3] = new DateTime (1976, 07, 04);
			names [4] = "Now";    e [4] = DateTime.Now;
			names [5] = "UtcNow"; e [5] = DateTime.UtcNow;

			BinaryFormatter bf = new BinaryFormatter ();
			MemoryStream ms = new MemoryStream ();

			bf.Serialize (ms, e);

			ms.Position = 0;
			DateTime [] a = (DateTime []) bf.Deserialize (ms);

			Assert.AreEqual (e.Length, a.Length);
			for (int i = 0; i < e.Length; ++i)
				Assert.AreEqual (e [i], a [i], names [i]);
		}
	}
}
