﻿/* Copyright (C) 2018 by John Cronin
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

namespace libsupcs
{
    public class CLRConfig
    {
        /* Implement as two separate functions to allow more advanced systems to
         * override GetConfigBoolValue with a non-weak linkage function to handle
         * config values it knows about, and then call the public GetConfigBool()
         * below for ones it doesn't */
        public static bool GetBoolValue(string name)
        {
            if (name == "System.Globalization.Invariant")
                return true;
            return false;
        }

        [WeakLinkage]
        [AlwaysCompile]
        [MethodAlias("_ZW6System9CLRConfig_18GetConfigBoolValue_Rb_P1u1S")]
        static bool GetConfigBoolValue(string name)
        {
            return GetBoolValue(name);
        }
    }
}
