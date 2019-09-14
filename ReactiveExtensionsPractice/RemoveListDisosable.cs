using System;
using System.Collections.Generic;

namespace ReactiveExtensionsPractice
{
    //Disposeが呼ばれたらobserverを監視対象から削除する
    internal class RemoveListDisosable : IDisposable
    {
        private List<IObserver<int>> observers;
        private IObserver<int> observer;

        public RemoveListDisosable(List<IObserver<int>> observers, IObserver<int> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (this.observers == null)
            {
                return;
            }

            if (observers.IndexOf(observer) != -1)
            {
                this.observers.Remove(observer);
            }

            this.observers = null;
            this.observer = null;
        }
    }
}