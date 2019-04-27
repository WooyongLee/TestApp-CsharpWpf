using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string srcFile = @"E:\TestApp\ThreadTestApp\TestFolder\Test.txt";

            Action<object> FileCopyAction = (object state) =>
            {
                string[] paths = (string[])state;
                File.Copy(paths[0], paths[1]);

                Console.WriteLine("TaskID : {0}, ThreadID : {1}, {2} was copied to {3}",
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId, paths[0], paths[1]);
            };

            Task t1 = new Task(
                FileCopyAction,
                new string[] { srcFile, srcFile + ".copy" });

            Task t2 = Task.Run(() =>
            {
                FileCopyAction(new string[] {
                    srcFile, srcFile + ".copy2"});
            });

            Task t3 = new Task(
                FileCopyAction,
                new string[] { srcFile, srcFile + ".copy3" });

            t3.RunSynchronously();

            t1.Wait(); t2.Wait(); t3.Wait();
        }
    }
}
