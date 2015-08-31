using System;
using System.Diagnostics;

namespace PerformanceTest
{
    internal class Helper
    {
        internal static double AverageRuntime(Action act, int runs) {
            var watch = new Stopwatch();

            watch.Start();
            for (var i = 0; i < runs; i++) {
                act();
            }
            watch.Stop();

            return watch.ElapsedMilliseconds / (double)runs;
        }

        internal static Foo GetFoo() {
            return new Foo
            {
                Id = 1,
                Name = "John",
                Age = 30,
                Date = DateTime.Now,
                Sex = true,
                Xuid = Guid.NewGuid(),
                UserStatus = UserStatus.Approved,
                Lastip = new byte[] { 192, 168, 0, 254 },
                Inner = new InnerFoo { InnerNumeric = 999.99912m }
            };
        }
    }
}