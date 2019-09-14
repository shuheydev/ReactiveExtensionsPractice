using System;

namespace ReactiveExtensionsPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            //監視される人を作成
            var source = new NumberObservable();

            //監視役を2つ登録
            var sbscriber1 = source.Subscribe(
                    //OnNext
                    value => Console.WriteLine($"OnNext({value}) called."),
                    //OnError
                    ex => Console.WriteLine($"OnError({ex.Message})"),
                    //OnCompleted
                    () => Console.WriteLine($"OnCompleted() called.")
                );
            var sbscriber2 = source.Subscribe(
                    //OnNext
                    value => Console.WriteLine($"OnNext({value}) called."),
                    //OnError
                    ex => Console.WriteLine($"OnError({ex.Message})"),
                    //OnCompleted
                    () => Console.WriteLine($"OnCompleted() called.")
                );

            //監視される人の処理を実行
            Console.WriteLine("## Execute(1)");
            source.Execute(1);

            //片方を監視する人から解雇
            Console.WriteLine("## Dispose");
            sbscriber2.Dispose();

            //再度処理を実行
            Console.WriteLine("## Execute(2)");
            source.Execute(2);

            //エラーを起こしてみる
            Console.WriteLine("## Execute(0)");
            source.Execute(0);

            //完了通知
            //もう1つ監視役を追加して完了通知を行う
            var sbscriber3 = source.Subscribe(
                    //OnNext
                    value => Console.WriteLine($"OnNext({value}) called."),
                    //OnError
                    ex => Console.WriteLine($"OnError({ex.Message})"),
                    //OnCompleted
                    () => Console.WriteLine($"OnCompleted() called.")
                );
            Console.WriteLine("## Completed");
            source.Completed();
        }
    }
}
