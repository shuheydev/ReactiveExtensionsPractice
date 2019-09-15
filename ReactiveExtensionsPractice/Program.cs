using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace ReactiveExtensionsPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Observable.Range(1, 10)
                //3つずつの値に分ける
                .Buffer(3)
                .Subscribe(
                    list =>
                    {
                        //IList<int>の内容を出力
                        Console.WriteLine("-- Buffer start");
                        foreach (var i in list)
                        {
                            Console.WriteLine(i);
                        }
                    },
                    //完了
                    () => Console.WriteLine("OnCompleted")
                );

            Observable.Range(1, 10)
                //3つずつの値に分けて、値は2つ飛ばし
                .Buffer(3, 2)
                .Subscribe(
                    list =>
                    {
                        //IList<int>の内容を出力
                        Console.WriteLine("-- Buffer start");
                        foreach (var i in list)
                        {
                            Console.WriteLine(i);
                        }
                    },
                    //完了
                    () => Console.WriteLine("OnCompleted")
                );

            Observable.Range(1, 10)
                //3つずつの値に分けて、値は2つ飛ばし
                .Buffer(3, 5)
                .Subscribe(
                    list =>
                    {
                        //IList<int>の内容を出力
                        Console.WriteLine("-- Buffer start");
                        foreach (var i in list)
                        {
                            Console.WriteLine(i);
                        }
                    },
                    //完了
                    () => Console.WriteLine("OnCompleted")
                );

            var gate = new EventWaitHandle(false, EventResetMode.AutoReset);
            Observable
                //500msごとに値を発行
                .Interval(TimeSpan.FromMilliseconds(500))
                //3秒間溜める
                .Buffer(TimeSpan.FromSeconds(3))
                //最初の3つのかたまりを後続に流す
                .Take(3)
                .Subscribe(
                    l =>
                    {
                        // 値を表示
                        Console.WriteLine("--Buffer {0:HH:mm:ss}", DateTime.Now);
                        foreach (var i in l)
                        {
                            Console.WriteLine(i);
                        }
                    },
                    () =>
                    {
                        // 完了
                        Console.WriteLine("OnCompleted");
                        gate.Set();
                    });

            // OnCompleted待ち
            Console.WriteLine("WaitOne");
            gate.WaitOne();
            Console.WriteLine("WaitOne Completed");

            gate = new EventWaitHandle(false, EventResetMode.AutoReset);
            var clickEmuration = new Subject<Unit>();
            Observable
                //500ms間隔で値を発行
                .Interval(TimeSpan.FromMilliseconds(500))
                //任意の値で値をまとめるのをやめる（この場合3秒間隔）
                .Buffer(
                    //clickEmurationから通知が来たら
                    clickEmuration.AsObservable(),
                    //2秒間値を集める
                    _ => Observable.Interval(TimeSpan.FromSeconds(2))
                )
                .Take(2)
                .Subscribe(
                    list =>
                    {
                        //値を表示
                        Console.WriteLine($"--Buffer {DateTime.Now.ToString("0:HH:mm:ss")}");
                        foreach (var i in list)
                        {
                            Console.WriteLine(i);
                        }
                    },
                    () =>
                    {
                        //完了
                        Console.WriteLine("OnCompleted");
                        gate.Set();
                    }
                );

            Console.ReadLine();
            Console.WriteLine("{0:HH:mm:ss} Click emurate", DateTime.Now);
            clickEmuration.OnNext(Unit.Default);

            //Enterを押すとクリックを模したSubjectから通知を上げる
            Console.ReadLine();
            Console.WriteLine("{0:HH:mm:ss} Click emurate", DateTime.Now);
            clickEmuration.OnNext(Unit.Default);

            //OnCompleted待ち
            gate.WaitOne();
            Console.WriteLine("WaiteOne Completed");
        }
    }
}
