using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TaskTest.FileCopyTask();
            // TaskTest.LamdaTest();
            TaskTest.ActionDelegateTest();
        }

        public void FileCopyTask()
        {
            string srcFile = @"E:\TestApp\ThreadTestApp\TestFolder\Test.txt";

            // File Copty하는 작업 정의
            Action<object> FileCopyAction = (object state) =>
            {
                string[] paths = (string[])state;
                File.Copy(paths[0], paths[1]);

                Console.WriteLine("TaskID : {0}, ThreadID : {1}, {2} was copied to {3}",
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId, paths[0], paths[1]);
            };

            Task t1 = new Task(
                FileCopyAction,
                new string[] { srcFile, srcFile + ".copy" }
                );

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

    // Task를 Test하기 위한 클래스
    public static class TaskTest
    {
        // 여러가지 델리게이트 생성
        delegate void CompareDelegate(int a, int b);
        delegate void StringDelegate();

        // 람다식 만드는 이유 ::
        // 익명 메소드를 만드는 경우
        // Func와 Action으로 더 간편하게 무명 함수 만드는 경우
        // LINQ를 이용한 방법으로 표현하는 경우
        // 람다식을 테스트 하는 함수
        public static void LamdaTest()
        {
            // 두 수 a, b를 비교하는 람다식
            CompareDelegate Compare = (a, b) =>
            {
                if (a > b) Console.WriteLine("{0}보다 {1}가 크다", b, a);
                else if (a > b) Console.WriteLine("{0}보다 {1}가 크다", a, b);
                else  Console.WriteLine("{0}과 {1}은 같다", a, b);
            };

            StringDelegate ConsoleWriteSampleText = () =>
            {
                ShowMessage("Compare Delegate Test");
                //Console.WriteLine("픽소니어");
                //Console.WriteLine("이우용");
                //Console.WriteLine("입니다.");
            };
        }

        // Func<T, TResult> 델리게이트 이용 :: 결과를 반환하는 메소드를 참조하기
        //  
        public static void FuncDelegateTest()
        {
            // 1. Func<T, TResult> 델리게이트를 인스턴스화 해서 코드를 간소화시키기
            Func<string, string> convertMethodInstance = UpperCaster;

            // 2. Func<T, TResult> 델리게이트를 Anonymous method와 함께 사용
            Func<string, string> convertMethodAnonymous = delegate (string s)
            {
                return s.ToUpper();
            };

            // 3. Func<T, TResult> 델리게이트를 람다식을 이용하여 할당
            Func<string, string> convertMethodLamda = (string s) => s.ToUpper();

            // Test
            string name = "Korea Forever !!!";

            Console.WriteLine("Before Convert string : {0}", name);
            Console.WriteLine("After Convert string : {0}", convertMethodAnonymous(name));
        }

        // 대문자로 캐스팅하기
        public static string UpperCaster(string inputString)
        {
            return inputString.ToUpper();
        }

        // Action<T> 델리게이트를 이용하기
        public static void ActionDelegateTest()
        {
            // 1. Action<T> 델리게이트를 인스턴스화하여 이 코드를 간소화
            Action<string> msgTarget;

            // 명령줄 인수가 지정되어 있을 경우에 메시지 표시
            if (Environment.GetCommandLineArgs().Length > 1)
                msgTarget = ShowMessage;
            else
                msgTarget = Console.WriteLine;


            // 2. Action<T> 델리게이트를 anonymous method와 함께 사용
            Action<string> msgTargetAnony;

            if (Environment.GetCommandLineArgs().Length > 1)
                msgTargetAnony = delegate (string s) { ShowMessage(s); };
            else
                msgTargetAnony = delegate (string s) { Console.WriteLine(s); };

            // 3. Action<T> 델리게이트 인스턴스에 람다식을 할당
            Action<string> msgTargetLamda;

            if (Environment.GetCommandLineArgs().Length > 1)
                msgTargetLamda = (string s) => { ShowMessage(s); }; // 매개변수 Type을 지정한 람다
            else
                msgTargetLamda = (s) => { Console.WriteLine(s); }; // 알아서 컴파일러가 Type을 지정해 주는 람다
        }

        // 본인이 정의한 메세지를 콘솔에 뿌리기
        public static void ShowMessage(string msg)
        {
            Console.WriteLine(msg);

            Console.WriteLine("픽소니어");
            Console.WriteLine("이우용");
            Console.WriteLine("입니다.");
        }

        // 파일을 복사하는 Task 예제
        public static void FileCopyTask()
        {
            string srcFile = @"E:\TestApp\ThreadTestApp\TestFolder\Test.txt";

            // File Copty하는 작업 정의
            Action<object> FileCopyAction = (object state) =>
            {
                string[] paths = (string[])state;
                File.Copy(paths[0], paths[1]);

                Console.WriteLine("TaskID : {0}, ThreadID : {1}, {2} was copied to {3}",
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId, paths[0], paths[1]);
            };

            // Task를 new로 생성, Target 함수와 매개변수로 작업을 실행할 대리자 설정 
            Task t1 = new Task(
                FileCopyAction,
                new string[] { srcFile, srcFile + ".copy" }
                );

            // Action의 매개변수를 지정하여 바로 Task를 Run 시키기
            Task t2 = Task.Run(() =>
            {
                FileCopyAction(new string[] {
                    srcFile, srcFile + ".copy2"});
            });

            Task t3 = new Task(
                FileCopyAction,
                new string[] { srcFile, srcFile + ".copy3" }
                );

            t3.RunSynchronously();

            t1.Wait(); t2.Wait(); t3.Wait();
        }
    }
}
