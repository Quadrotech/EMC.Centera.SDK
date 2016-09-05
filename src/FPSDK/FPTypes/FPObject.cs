using System;
using System.Collections;

namespace EMC.Centera.SDK.FPTypes
{
  /// <summary> 
///Abstract disposable base class for FP objects.
///@author Graham Stuart
///@version
 /// </summary>
    public abstract class FPObject : IDisposable
    {
        protected bool Disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The "disposing" boolean allows derived objects to add an additional step to dispose
        // of any IDisposable objects they own dependent on who is calling it i.e. diectly or via the dtor.
        // We don't have any others so it's effectively not utilised.
        public void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                Disposed = true;
                Close();
            }
        }

        ~FPObject()
        {
            Dispose(false);
        }

        public abstract void Close();

        internal static Hashtable SDKObjects = Hashtable.Synchronized(new Hashtable());

        protected void AddObject(object key, FPObject obj)
        {
            SDKObjects.Add(key, obj);
        }

        protected void RemoveObject(object key)
        {
            SDKObjects.Remove(key);
        }
    }
}