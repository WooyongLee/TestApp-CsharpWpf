using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTestApp
{
    public class ThreadTestApp
    {
        // Thread Pool에서 돌아가는 Thread Delegate
        delegate int del(int n);
        private static int Func(int n)
        {
            int sum = 0; for (int i = 1; i <= n; i++) { sum += i; Thread.Sleep(100); }
            Console.WriteLine(sum);
            return sum;
        }

        private static void CallBackMethod(IAsyncResult ar)
        {
            del temp = ((System.Runtime.Remoting.Messaging.AsyncResult)ar).AsyncDelegate as del;
            int result = temp.EndInvoke(ar);
            Console.WriteLine("1부터 {0}까지의 합은 {1}입니다.", ar.AsyncState, result);
        }

        public ThreadTestApp()
        {
            del a = Func;
            // a.BeginInvoke(null, null);
            IAsyncResult ar = a.BeginInvoke(10, null, null);
            // Block Main Thread, ~Result
            int result = a.EndInvoke(ar); // 비동기 스레드의 리턴값 반환

            IAsyncResult arCallBack = a.BeginInvoke(10, new AsyncCallback(CallBackMethod), 10);
            // Thread 수행 대기
            arCallBack.AsyncWaitHandle.WaitOne();
        }
    }
}
