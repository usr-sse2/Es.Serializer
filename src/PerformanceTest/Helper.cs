using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTest
{
    class Helper
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

        internal static IEnumerable<int[]> Permutate(int upTo) {
            var asArr = Enumerable.Range(0, upTo);

            return Permutate(asArr.ToArray());
        }

        internal static IEnumerable<T[]> Permutate<T>(T[] array) {
            if (array == null || array.Length == 0) {
                yield return new T[0];
            }
            else {
                for (int pick = 0; pick < array.Length; ++pick) {
                    T item = array[pick];
                    int i = -1;
                    T[] rest = Array.FindAll<T>(
                        array, p => ++i != pick
                    );
                    foreach (T[] restPermuted in Permutate(rest)) {
                        i = -1;
                        yield return Array.ConvertAll<T, T>(
                            array, p => ++i == 0 ? item : restPermuted[i - 1]
                        );
                    }
                }
            }
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
