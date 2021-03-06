//
// System.Enum.cs
//
// Authors:
//   Miguel de Icaza (miguel@ximian.com)
//   Nick Drochak (ndrochak@gol.com)
//   Gonzalo Paniagua Javier (gonzalo@ximian.com)
//
// (C) Ximian, Inc.  http://www.ximian.com
//
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

using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	internal struct MonoEnumInfo
	{
		internal Type utype;
		internal Array values;
		internal string[] names;
		static Hashtable cache;
		
		[MethodImplAttribute (MethodImplOptions.InternalCall)]
		private static extern void get_enum_info (Type enumType, out MonoEnumInfo info);

		static MonoEnumInfo ()
		{
		    	cache = new Hashtable ();
		}
		
		private MonoEnumInfo (MonoEnumInfo other)
		{
			utype = other.utype;
			values = other.values;
			names = other.names;
		}

		internal static void GetInfo (Type enumType, out MonoEnumInfo info)
		{
			lock (cache) {
				if (cache.ContainsKey (enumType)) {
					info = (MonoEnumInfo) cache [enumType];
					return;
				}
				get_enum_info (enumType, out info);
				Array.Sort (info.values, info.names);
				cache.Add (enumType, new MonoEnumInfo (info));
			}
		}
	};

	[Serializable]
#if NET_2_0
	[ComVisible (true)]
#endif
	public abstract class Enum : ValueType, IComparable, IConvertible, IFormattable
	{
		protected Enum ()
		{
		}

		// IConvertible methods Start -->
		public TypeCode GetTypeCode ()
		{
			MonoEnumInfo info;
			MonoEnumInfo.GetInfo (this.GetType (), out info);
			return Type.GetTypeCode (info.utype);
		}

		bool IConvertible.ToBoolean (IFormatProvider provider)
		{
			return Convert.ToBoolean (get_value (), provider);
		}

		byte IConvertible.ToByte (IFormatProvider provider)
		{
			return Convert.ToByte (get_value (), provider);
		}

		char IConvertible.ToChar (IFormatProvider provider)
		{
			return Convert.ToChar (get_value (), provider);
		}

		DateTime IConvertible.ToDateTime (IFormatProvider provider)
		{
			return Convert.ToDateTime (get_value (), provider);
		}

		decimal IConvertible.ToDecimal (IFormatProvider provider)
		{	
			return Convert.ToDecimal (get_value (), provider);
		}

		double IConvertible.ToDouble (IFormatProvider provider)
		{	
			return Convert.ToDouble (get_value (), provider);
		}

		short IConvertible.ToInt16 (IFormatProvider provider)
		{
			return Convert.ToInt16 (get_value (), provider);
		}

		int IConvertible.ToInt32 (IFormatProvider provider)
		{
			return Convert.ToInt32 (get_value (), provider);
		}

		long IConvertible.ToInt64 (IFormatProvider provider)
		{
			return Convert.ToInt64 (get_value (), provider);
		}

		sbyte IConvertible.ToSByte (IFormatProvider provider)
		{
			return Convert.ToSByte (get_value (), provider);
		}

		float IConvertible.ToSingle (IFormatProvider provider)
		{
			return Convert.ToSingle (get_value (), provider);
		}

		object IConvertible.ToType (Type conversionType, IFormatProvider provider)
		{
			return Convert.ToType (get_value (), conversionType, provider);
		}

		ushort IConvertible.ToUInt16 (IFormatProvider provider)
		{
			return Convert.ToUInt16 (get_value (), provider);
		}

		uint IConvertible.ToUInt32 (IFormatProvider provider)
		{
			return Convert.ToUInt32 (get_value (), provider);
		}

		ulong IConvertible.ToUInt64 (IFormatProvider provider)
		{
			return Convert.ToUInt64 (get_value (), provider);
		}
		// <-- End IConvertible methods

		[MethodImplAttribute (MethodImplOptions.InternalCall)]
		private extern object get_value ();

		public static Array GetValues (Type enumType)
		{
			if (enumType == null)
				throw new ArgumentNullException ("enumType");

			if (!enumType.IsEnum)
				throw new ArgumentException ("enumType is not an Enum type.");

			MonoEnumInfo info;
			MonoEnumInfo.GetInfo (enumType, out info);
			return (Array) info.values.Clone ();
		}

		public static string[] GetNames (Type enumType)
		{
			if (enumType == null)
				throw new ArgumentNullException ("enumType");

			if (!enumType.IsEnum)
				throw new ArgumentException ("enumType is not an Enum type.");

			MonoEnumInfo info;
			MonoEnumInfo.GetInfo (enumType, out info);
			return (string []) info.names.Clone ();
		}

		public static string GetName (Type enumType, object value)
		{
			if (enumType == null)
				throw new ArgumentNullException ("enumType");
			if (value == null)
				throw new ArgumentNullException ("value");

			if (!enumType.IsEnum)
				throw new ArgumentException ("enumType is not an Enum type.");

			MonoEnumInfo info;
			int i;
			value = ToObject (enumType, value);
			MonoEnumInfo.GetInfo (enumType, out info);
			for (i = 0; i < info.values.Length; ++i) {
				if (value.Equals (info.values.GetValue (i)))
					return info.names [i];
			}
			return null;
		}

		public static bool IsDefined (Type enumType, object value)
		{
			if (enumType == null)
				throw new ArgumentNullException ("enumType");
			if (value == null)
				throw new ArgumentNullException ("value");

			if (!enumType.IsEnum)
				throw new ArgumentException ("enumType is not an Enum type.");

			MonoEnumInfo info;
			MonoEnumInfo.GetInfo (enumType, out info);

			Type vType = value.GetType ();
			if (vType == typeof(String)) {
				return ((IList)(info.names)).Contains (value);
			} else if ((vType == info.utype) || (vType == enumType)) {
				int i;
				value = ToObject (enumType, value);
				MonoEnumInfo.GetInfo (enumType, out info);
				for (i = 0; i < info.values.Length; ++i) {
					if (value.Equals (info.values.GetValue (i)))
						return true;
				}
				return false;
			} else {
				throw new ArgumentException("The value parameter is not the correct type."
					+ "It must be type String or the same type as the underlying type"
					+ "of the Enum.");
			}
		}

		public static Type GetUnderlyingType (Type enumType)
		{
			if (enumType == null)
				throw new ArgumentNullException ("enumType");

			if (!enumType.IsEnum)
				throw new ArgumentException ("enumType is not an Enum type.");

			MonoEnumInfo info;
			MonoEnumInfo.GetInfo (enumType, out info);
			return info.utype;
		}

		public static object Parse (Type enumType, string value)
		{
			// Note: Parameters are checked in the other overload
			return Parse (enumType, value, false);
		}

		private static int FindName (string [] names, string name,  bool ignoreCase)
		{
			for (int i = 0; i < names.Length; ++i) {
				if (String.Compare (name, names [i], ignoreCase, CultureInfo.InvariantCulture) == 0)
					return i;
			}
			return -1;
		}

		// Helper function for dealing with [Flags]-style enums.
		private static ulong GetValue (object value, TypeCode typeCode)
		{
			switch (typeCode) {
			case TypeCode.Byte:
				return (byte) value;
			case TypeCode.SByte:
				return (byte) ((sbyte) value);
			case TypeCode.Int16:
				return (ushort) ((short) value);
			case TypeCode.Int32:
				return (uint) ((int) value);
			case TypeCode.Int64:
				return (ulong) ((long) value);
			case TypeCode.UInt16:
				return (ushort) value;
			case TypeCode.UInt32:
				return (uint) value;
			case TypeCode.UInt64:
				return (ulong) value;
			}
			throw new ArgumentException ("typeCode is not a valid type code for an Enum");
		}

		private static char [] split_char = { ',' };

		public static object Parse (Type enumType, string value, bool ignoreCase)
		{
			if (enumType == null)
				throw new ArgumentNullException ("enumType");

			if (value == null)
				throw new ArgumentNullException ("value");

			if (!enumType.IsEnum)
				throw new ArgumentException ("not an Enum type", "enumType");

			value = value.Trim ();
			if (String.Empty == value)
				throw new ArgumentException ("cannot be an empty string", "value");

			MonoEnumInfo info;
			MonoEnumInfo.GetInfo (enumType, out info);

			// is 'value' a named constant?
			int loc = FindName (info.names, value, ignoreCase);
			if (loc >= 0)
				return info.values.GetValue (loc);

			TypeCode typeCode = ((Enum) info.values.GetValue (0)).GetTypeCode ();

			// is 'value' a list of named constants?
			if (value.IndexOf (',') != -1) {
				string [] names = value.Split (split_char);
				ulong retVal = 0;
				for (int i = 0; i < names.Length; ++i) {
					loc = FindName (info.names, names [i].Trim (), ignoreCase);
					if (loc < 0)
						throw new ArgumentException ("The requested value was not found.");
					retVal |= GetValue (info.values.GetValue (loc), typeCode);
				}
				return ToObject (enumType, retVal);
			}

			// is 'value' a number?
			try {
				return ToObject (enumType, Convert.ChangeType (value, typeCode));
			} catch (Exception e) {
				throw new ArgumentException (String.Format ("The requested value `{0}' was not found", value), e);
			}
		}

		/// <summary>
		///   Compares the enum value with another enum value of the same type.
		/// </summary>
		///
		/// <remarks/>
		public int CompareTo (object obj)
		{
			Type thisType;

			if (obj == null)
				return 1;

			thisType = this.GetType ();
			if (obj.GetType() != thisType) {
				throw new ArgumentException (
					"Object must be the same type as the "
					+ "enum. The type passed in was " 
					+ obj.GetType().ToString ()
					+ "; the enum type was " 
					+ thisType.ToString () + ".");
			}

			object value1, value2;

			value1 = this.get_value ();
			value2 = ((Enum)obj).get_value ();

			return ((IComparable)value1).CompareTo (value2);
		}

		public override string ToString ()
		{
			return ToString ("G");
		}

#if NET_2_0
	[Obsolete("Provider is ignored, just use ToString")]
#endif
		public string ToString (IFormatProvider provider)
		{
			return ToString ("G", provider);
		}

		public string ToString (String format)
		{
			if (format == String.Empty || format == null)
				format = "G";
			
			return Format (this.GetType (), this.get_value (), format);
		}

#if NET_2_0
		[Obsolete("Provider is ignored, just use ToString")]
#endif
		public string ToString (String format, IFormatProvider provider)
		{
			// provider is not used for Enums

			if (format == String.Empty || format == null) {
				format = "G";
			}
			return Format (this.GetType(), this.get_value (), format);
		}

		public static object ToObject (Type enumType, byte value)
		{
			return ToObject (enumType, (object)value);
		}

		public static object ToObject (Type enumType, short value)
		{
			return ToObject (enumType, (object)value);
		}

		public static object ToObject (Type enumType, int value)
		{
			return ToObject (enumType, (object)value);
		}

		public static object ToObject (Type enumType, long value)
		{
			return ToObject (enumType, (object)value);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		public static extern object ToObject (Type enumType, object value);

		[CLSCompliant (false)]
		public static object ToObject (Type enumType, sbyte value)
		{
			return ToObject (enumType, (object)value);
		}

		[CLSCompliant (false)]
		public static object ToObject (Type enumType, ushort value)
		{
			return ToObject (enumType, (object)value);
		}

		[CLSCompliant (false)]
		public static object ToObject (Type enumType, uint value)
		{
			return ToObject (enumType, (object)value);
		}

		[CLSCompliant (false)]
		public static object ToObject (Type enumType, ulong value)
		{
			return ToObject (enumType, (object)value);
		}

		public override bool Equals (object obj)
		{
			return DefaultEquals (this, obj);
		}

		public override int GetHashCode ()
		{
			object v = this.get_value ();
			return v.GetHashCode ();
		}

		private static string FormatSpecifier_X (Type enumType, object value, bool upper)
		{
			switch (Type.GetTypeCode (enumType)) {
				case TypeCode.SByte:
					return ((sbyte)value).ToString (upper ? "X2" : "x2");
				case TypeCode.Byte:
					return ((byte)value).ToString (upper ? "X2" : "x2");
				case TypeCode.Int16:
					return ((short)value).ToString (upper ? "X4" : "x4");
				case TypeCode.UInt16:
					return ((ushort)value).ToString (upper ? "X4" : "x4");
				case TypeCode.Int32:
					return ((int)value).ToString (upper ? "X8" : "x8");
				case TypeCode.UInt32:
					return ((uint)value).ToString (upper ? "X8" : "x8");
				case TypeCode.Int64:
					return ((long)value).ToString (upper ? "X16" : "x16");
				case TypeCode.UInt64:
					return ((ulong)value).ToString (upper ? "X16" : "x16");
				default:
					throw new Exception ("Invalid type code for enumeration.");
			}
		}

		static string FormatFlags (Type enumType, object value)
		{
			string retVal = String.Empty;
			MonoEnumInfo info;
			MonoEnumInfo.GetInfo (enumType, out info);
			string asString = value.ToString ();
			if (asString == "0") {
				retVal = GetName (enumType, value);
				if (retVal == null)
					retVal = asString;
				return retVal;
			}
			// This is ugly, yes.  We need to handle the different integer
			// types for enums.  If someone else has a better idea, be my guest.
			switch (((Enum)info.values.GetValue (0)).GetTypeCode ()) {
			case TypeCode.SByte: {
				sbyte flags = (sbyte) value;
				sbyte enumValue;
				for (int i = info.values.Length - 1; i >= 0; i--) {
					enumValue = (sbyte) info.values.GetValue (i);
					if (enumValue == 0)
						continue;

					if ((flags & enumValue) == enumValue) {
						retVal = info.names[i] + (retVal == String.Empty ? String.Empty : ", ") + retVal;
						flags -= enumValue;
					}
				}
				if (flags != 0) return asString;
				}
				break;
			case TypeCode.Byte: {
				byte flags = (byte) value;
				byte enumValue;
				for (int i = info.values.Length - 1; i >= 0; i--) {
					enumValue = (byte) info.values.GetValue (i);
					if (enumValue == 0)
						continue;

					if ((flags & enumValue) == enumValue) {
						retVal = info.names[i] + (retVal == String.Empty ? String.Empty : ", ") + retVal;
						flags -= enumValue;
					}
				}
				if (flags != 0) return asString;
				}
				break;
			case TypeCode.Int16: {
				short flags = (short) value;
				short enumValue;
				for (int i = info.values.Length - 1; i >= 0; i--) {
					enumValue = (short) info.values.GetValue (i);
					if (enumValue == 0)
						continue;

					if ((flags & enumValue) == enumValue) {
						retVal = info.names[i] + (retVal == String.Empty ? String.Empty : ", ") + retVal;
						flags -= enumValue;
					}
				}
				if (flags != 0) return asString;
				}
				break;
			case TypeCode.Int32: {
				int flags = (int) value;
				int enumValue;
				for (int i = info.values.Length - 1; i >= 0; i--) {
					enumValue = (int) info.values.GetValue (i);
					if (enumValue == 0)
						continue;

					if ((flags & enumValue) == enumValue) {
						retVal = info.names[i] + (retVal == String.Empty ? String.Empty : ", ") + retVal;
						flags -= enumValue;
					}
				}
				if (flags != 0) return asString;
				}
				break;
			case TypeCode.UInt16: {
				ushort flags = (ushort) value;
				ushort enumValue;
				for (int i = info.values.Length - 1; i >= 0; i--) {
					enumValue = (ushort) info.values.GetValue (i);
					if (enumValue == 0)
						continue;

					if ((flags & enumValue) == enumValue) {
						retVal = info.names[i] + (retVal == String.Empty ? String.Empty : ", ") + retVal;
						flags -= enumValue;
					}
				}
				if (flags != 0) return asString;
				}
				break;
			case TypeCode.UInt32: {
				uint flags = (uint) value;
				uint enumValue;
				for (int i = info.values.Length - 1; i >= 0; i--) {
					enumValue = (uint) info.values.GetValue (i);
					if (enumValue == 0)
						continue;

					if ((flags & enumValue) == enumValue) {
						retVal = info.names[i] + (retVal == String.Empty ? String.Empty : ", ") + retVal;
						flags -= enumValue;
					}
				}
				if (flags != 0) return asString;
				}
				break;
			case TypeCode.Int64: {
				long flags = (long) value;
				long enumValue;
				for (int i = info.values.Length - 1; i >= 0; i--) {
					enumValue = (long) info.values.GetValue (i);
					if (enumValue == 0)
						continue;

					if ((flags & enumValue) == enumValue) {
						retVal = info.names[i] + (retVal == String.Empty ? String.Empty : ", ") + retVal;
						flags -= enumValue;
					}
				}
				if (flags != 0) return asString;
				}
				break;
			case TypeCode.UInt64: {
				ulong flags = (ulong) value;
				ulong enumValue;
				for (int i = info.values.Length - 1; i >= 0; i--) {
					enumValue = (ulong) info.values.GetValue (i);
					if (enumValue == 0)
						continue;

					if ((flags & enumValue) == enumValue) {
						retVal = info.names[i] + (retVal == String.Empty ? String.Empty : ", ") + retVal;
						flags -= enumValue;
					}
				}
				if (flags != 0) return asString;
				}
				break;
			}

			if (retVal == String.Empty)
				return asString;

			return retVal;
		}

		public static string Format (Type enumType, object value, string format)
		{
			if (enumType == null)
				throw new ArgumentNullException ("enumType");
			if (value == null)
				throw new ArgumentNullException ("value");
			if (format == null)
				throw new ArgumentNullException ("format");

			if (!enumType.IsEnum)
				throw new ArgumentException ("Type provided must be an Enum.");
			
			Type vType = value.GetType();
			Type underlyingType = Enum.GetUnderlyingType (enumType);
			if (vType.IsEnum) {
				if (vType != enumType)
					throw new ArgumentException (string.Format(CultureInfo.InvariantCulture,
						"Object must be the same type as the enum. The type" +
						" passed in was {0}; the enum type was {1}.",
						vType.FullName, enumType.FullName));
			} else if (vType != underlyingType) {
                System.Diagnostics.Debugger.Break();
				throw new ArgumentException (string.Format (CultureInfo.InvariantCulture,
					"Enum underlying type and the object must be the same type" +
					" or object. Type passed in was {0}; the enum underlying" +
					" type was {1}.", vType.FullName, underlyingType.FullName));
			}

			if (format.Length != 1)
				throw new FormatException ("Format String can be only \"G\",\"g\",\"X\"," + 
					"\"x\",\"F\",\"f\",\"D\" or \"d\".");

			char formatChar = format [0];
			string retVal;
			if ((formatChar == 'G' || formatChar == 'g')) {
				if (!Attribute.IsDefined (enumType, typeof(FlagsAttribute))) {
					retVal = GetName (enumType, value);
					if (retVal == null)
						retVal = value.ToString();

					return retVal;
				}

				formatChar = 'f';
			}
			
			if ((formatChar == 'f' || formatChar == 'F'))
				return FormatFlags (enumType, value);

			retVal = String.Empty;
			switch (formatChar) {
			case 'X':
				retVal = FormatSpecifier_X (enumType, value, true);
				break;
			case 'x':
				retVal = FormatSpecifier_X (enumType, value, false);
				break;
			case 'D':
			case 'd':
				if (underlyingType == typeof (ulong)) {
					ulong ulongValue = Convert.ToUInt64 (value);
					retVal = ulongValue.ToString ();
				} else {
					long longValue = Convert.ToInt64 (value);
					retVal = longValue.ToString ();
				}
				break;
			default:
				throw new FormatException ("Format String can be only \"G\",\"g\",\"X\"," + 
					"\"x\",\"F\",\"f\",\"D\" or \"d\".");
			}
			return retVal;
		}
	}
}
