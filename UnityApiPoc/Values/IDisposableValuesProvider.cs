namespace UnityApiPoc.Values
{
    using System;

    public interface IDisposableValuesProvider : IDisposable
    {
        int GetValues();

        void Dispose(bool disposing);
    }
}