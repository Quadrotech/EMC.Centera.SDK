/*****************************************************************************

Copyright © 2006 EMC Corporation. All Rights Reserved
 
This file is part of .NET wrapper for the Centera SDK.

.NET wrapper is free software; you can redistribute it and/or modify it under
the terms of the GNU General Public License as published by the Free Software
Foundation version 2.

In addition to the permissions granted in the GNU General Public License
version 2, EMC Corporation gives you unlimited permission to link the compiled
version of this file into combinations with other programs, and to distribute
those combinations without any restriction coming from the use of this file.
(The General Public License restrictions do apply in other respects; for
example, they cover modification of the file, and distribution when not linked
into a combined executable.)

.NET wrapper is distributed in the hope that it will be useful, but WITHOUT ANY
WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A
PARTICULAR PURPOSE. See the GNU General Public License version 2 for more
details.

You should have received a copy of the GNU General Public License version 2
along with .NET wrapper; see the file COPYING. If not, write to:

 EMC Corporation 
 Centera Open Source Intiative (COSI) 
 80 South Street
 1/W-1
 Hopkinton, MA 01748 
 USA

******************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace EMC.Centera.SDK.Native
{
    public class MarshalPtrToUtf8 : ICustomMarshaler
    {
        static readonly MarshalPtrToUtf8 marshaler = new MarshalPtrToUtf8();

        public void CleanUpManagedData(object ManagedObj)
        {

        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            Marshal.FreeHGlobal(pNativeData);
        }

        public int GetNativeDataSize()
        {
            return Marshal.SizeOf(typeof(byte));
        }

        public int GetNativeDataSize(IntPtr ptr)
        {
            int size = 0;
            for (size = 0; Marshal.ReadByte(ptr, size) > 0; size++);
            return size;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            if (ManagedObj == null)
            {
                return IntPtr.Zero;
            }

            if (!(ManagedObj is string))
            {
                throw new ArgumentException("CustomMarshal class MarshalPtrToUtf8 only works with System.String variables");
            }

            byte[] array = Encoding.UTF8.GetBytes((string)ManagedObj);
            int size = Marshal.SizeOf(array[0]) * array.Length + Marshal.SizeOf(array[0]);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(array, 0, ptr, array.Length);
            Marshal.WriteByte(ptr, size - 1, 0);
            return ptr;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
                return null;
            int size = GetNativeDataSize(pNativeData);
            byte[] array = new byte[size];
            Marshal.Copy(pNativeData, array, 0, size);
            return Encoding.UTF8.GetString(array);
        }

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return marshaler;
        }
    }
}