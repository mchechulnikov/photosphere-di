using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class ParallelEnumerableExtensions
    {
        public static void ParallelProceed<T>(this IEnumerable<T> array, int count, Action<T> action)
        {
            var tasks = new Task[count];
            var index = 0;
            foreach (var item in array)
            {
                var task = new Task(() => action(item));
                tasks[index] = task;
                task.Start();
                index++;
            }
            Task.WaitAll(tasks);
        }
    }
}