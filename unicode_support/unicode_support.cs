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
using System.IO;

namespace unicode_support
{
    class Program
    {
        static Dictionary<int, string[]> unicode_db = new Dictionary<int, string[]>();

        static void Main(string[] args)
        {
            string input = "UnicodeData.txt";
            string output = "UnicodeData.cs";

            if (args.Length >= 1)
            {
                input = args[0];

                if (args.Length >= 2)
                    output = args[1];
            }

            StreamReader r = new StreamReader(input);
            while (!r.EndOfStream)
            {
                string line = r.ReadLine();
                string[] vals = line.Split(new char[] { ';' }, StringSplitOptions.None);
                int index = Int32.Parse(vals[0], System.Globalization.NumberStyles.HexNumber);
                unicode_db.Add(index, vals);
            }
            r.Close();

            StreamWriter w = new StreamWriter(output);
            w.WriteLine("/* Automatically generated by unicode_support */");
            w.WriteLine();
            w.WriteLine("namespace unicode_support_output {");
            w.WriteLine("    class DataTables {");

            w.WriteLine("        internal static readonly ushort[] ToLowerDataLow = new ushort[] {");
            for (int i = 0; i <= 0x24cf; i++)
            {
                string val = "";
                if (unicode_db.ContainsKey(i))
                {
                    if (unicode_db[i][13] != "")
                        val = Int32.Parse(unicode_db[i][13], System.Globalization.NumberStyles.HexNumber).ToString();
                }
                if (val == "")
                    val = i.ToString();

                w.Write("            ");
                w.Write(val);
                if (i != 0x24cf)
                    w.WriteLine(",");
                else
                    w.WriteLine();
            }
            w.WriteLine("        };");
            w.WriteLine();

            w.WriteLine("        internal static readonly ushort[] ToLowerDataHigh = new ushort[] {");
            for (int i = 0xff21; i <= 0xffff; i++)
            {
                string val = "";
                if (unicode_db.ContainsKey(i))
                {
                    if (unicode_db[i][13] != "")
                        val = Int32.Parse(unicode_db[i][13], System.Globalization.NumberStyles.HexNumber).ToString();
                }
                if (val == "")
                    val = i.ToString();

                w.Write("            ");
                w.Write(val);
                if (i != 0xffff)
                    w.WriteLine(",");
                else
                    w.WriteLine();
            }
            w.WriteLine("        };");
            w.WriteLine();

            w.WriteLine("        internal static readonly ushort[] ToUpperDataLow = new ushort[] {");
            for (int i = 0; i <= 0x24e9; i++)
            {
                string val = "";
                if (unicode_db.ContainsKey(i))
                {
                    if (unicode_db[i][12] != "")
                        val = Int32.Parse(unicode_db[i][12], System.Globalization.NumberStyles.HexNumber).ToString();
                }
                if (val == "")
                    val = i.ToString();

                w.Write("            ");
                w.Write(val);
                if (i != 0x24e9)
                    w.WriteLine(",");
                else
                    w.WriteLine();
            }
            w.WriteLine("        };");
            w.WriteLine();

            w.WriteLine("        internal static readonly ushort[] ToUpperDataHigh = new ushort[] {");
            for (int i = 0xff21; i <= 0xffff; i++)
            {
                string val = "";
                if (unicode_db.ContainsKey(i))
                {
                    if (unicode_db[i][12] != "")
                        val = Int32.Parse(unicode_db[i][12], System.Globalization.NumberStyles.HexNumber).ToString();
                }
                if (val == "")
                    val = i.ToString();

                w.Write("            ");
                w.Write(val);
                if (i != 0xffff)
                    w.WriteLine(",");
                else
                    w.WriteLine();
            }
            w.WriteLine("        };");
            w.WriteLine();

            /* numeric_data_values is an array of doubles.  numeric_data[c] is an array of bytes that points
             * into this table */
            List<string> numeric_data_values = new List<string>();
            Dictionary<string, int> reverse_map = new Dictionary<string, int>();
            List<int> numeric_data = new List<int>();

            for (int i = 0; i <= 0x3289; i++)
            {
                string s = "-1";
                if (unicode_db.ContainsKey(i))
                    s = get_numeric_val(unicode_db[i]);
                int nd_idx = add_val(s, numeric_data_values, reverse_map);
                numeric_data.Add(nd_idx);
            }

            w.WriteLine("        internal static readonly byte[] NumericData = new byte[] {");
            for (int i = 0; i <= 0x3289; i++)
            {
                string s = numeric_data[i].ToString();
                w.Write("            ");
                w.Write(s);
                if (i != 0x3289)
                    w.WriteLine(",");
                else
                    w.WriteLine();
            }
            w.WriteLine("        };");
            w.WriteLine();

            w.WriteLine("        internal static readonly double[] NumericDataValues = new double[] {");
            for (int i = 0; i < numeric_data_values.Count; i++)
            {
                w.Write("            (double)");
                w.Write(numeric_data_values[i]);
                if (i != numeric_data_values.Count)
                    w.WriteLine(",");
                else
                    w.WriteLine();
            }
            w.WriteLine("        };");

            w.WriteLine("    }");
            w.WriteLine("}");
            w.WriteLine();

            w.Close();
        }

        private static int add_val(string s, List<string> numeric_data_values, Dictionary<string, int> reverse_map)
        {
            if (reverse_map.ContainsKey(s))
                return reverse_map[s];

            int ndv_idx = numeric_data_values.Count;
            reverse_map.Add(s, ndv_idx);
            numeric_data_values.Add(s);
            return ndv_idx;
        }

        private static string get_numeric_val(string[] p)
        {
            if (p[6] != "")
                return p[6];
            if (p[7] != "")
                return p[7];
            if (p[8] != "")
                return p[8];
            return "-1";
        }
    }
}
