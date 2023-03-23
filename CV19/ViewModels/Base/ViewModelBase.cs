
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CV19.ViewModels.Base
{
    internal abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
       public event PropertyChangedEventHandler? PropertyChanged;
       protected virtual void OnPropertyChanged([CallerMemberName]string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field,value))
                return false;

            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
        //~Viewmodelbase()
        //{
        //    Dispose(false);
        //}
        public void Dispose()
        {
            Dispose(true);
        }
        private bool _Disposed;
        protected virtual void Dispose(bool Disposing)
        {
            if (!_Disposed || !Disposing) return; // Процесс освобождения неуправляемых ресурсов, возможно описать перед прерыванием.

            _Disposed = true;
        }
    }
}
