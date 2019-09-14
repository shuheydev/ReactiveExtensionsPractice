
using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveExtensionsPractice
{
    class PrintObserver : IObserver<int>
    {
        //完了通知が来た時の処理
        public void OnCompleted()
        {
            Console.WriteLine("OnCompleted called");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"OnError({error.Message}) called.");
        }

        //監視対象から通知が来た時の処理
        public void OnNext(int value)
        {
            Console.WriteLine($"OnNext({value}) called.");
        }
    }
}
