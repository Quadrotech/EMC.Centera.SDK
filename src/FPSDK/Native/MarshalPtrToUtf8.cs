using System;
using System.Runtime.InteropServices;
using System.Text;

namespace EMC.Centera.SDK.Native
{
    public class MarshalPtrToUtf8 : ICustomMarshaler
    {
        static MarshalPtrToUtf8 marshaler = new MarshalPtrToUtf8();

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