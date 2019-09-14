using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveExtensionsPractice
{
    class NumberObservable : IObservable<int>
    {
        //自分を監視している人を管理するリスト
        public List<IObserver<int>> observers = new List<IObserver<int>>();

        public void Execute(int value)
        {
            if (value == 0)
            {
                foreach (var obs in observers)
                {
                    obs.OnError(new Exception("value is 0"));
                }

                //エラーが起きたので処理は終了
                this.observers.Clear();

                return;
            }

            foreach (var obs in observers)
            {
                obs.OnNext(value);
            }
        }

        //完了通知
        public void Completed()
        {
            foreach (var obs in observers)
            {
                obs.OnCompleted();
            }

            //完了したので監視している人たちをクリア
            this.observers.Clear();
        }

       　//監視する人を追加する
        //戻り値のIDisposableをDisposeすると監視から外れる
        public IDisposable Subscribe(IObserver<int> observer)
        {
            this.observers.Add(observer);
            return new RemoveListDisosable(observers, observer);
        }
    }
}
