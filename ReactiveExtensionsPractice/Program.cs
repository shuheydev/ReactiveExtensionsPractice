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
            //Subject
            var source = new Subject<int>();

            Console.WriteLine("# Subscribe1");
            source.Subscribe(
                   i => Console.WriteLine($"Subscribe1#OnNext:{i}"),
                   ex => Console.WriteLine($"Subscribe1#OnError: {ex}"),
                   () => Console.WriteLine($"Subscribe1#OnCompleted")
                );

            Console.WriteLine("OnNext(1)");
            source.OnNext(1);

            Console.WriteLine("OnNext(2)");
            source.OnNext(2);

            Console.WriteLine("# Subscribe2");
            source.Subscribe(
                    i => Console.WriteLine($"Subscribe2#OnNext:{i}"),
                    ex => Console.WriteLine($"Subscribe2#OnError:{ex}"),
                    () => Console.WriteLine($"Subscribe2#OnCompleted")
                );

            Console.WriteLine("OnNext(3)");
            source.OnNext(3);

            Console.WriteLine("OnNext(3)");
            source.OnNext(3);
        }
    }
}
