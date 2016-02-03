namespace UnityApiPoc.Values
{
    using System;
    using System.Runtime.InteropServices;

    public class DisposableValuesProvider : IDisposableValuesProvider
    {
        private IntPtr _unmanagerPtr;

        private bool _disposed;

        public DisposableValuesProvider()
        {
            _unmanagerPtr = Marshal.StringToCoTaskMemAuto("test");
        }

        public int GetValues()
        {
            return _unmanagerPtr.ToInt32();
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // todo release managed resources
                }

                if (_unmanagerPtr != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(_unmanagerPtr);
                    _unmanagerPtr = IntPtr.Zero;
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}