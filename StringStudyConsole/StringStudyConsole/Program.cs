using System;
using System.Globalization;
using System.Text;
using static System.Console;
namespace StringStudyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string greeting = "Good Morning";

            WriteLine(greeting);

            // A.1

            // Contains()
            WriteLine("Contains 'Evening' : {0}", greeting.Contains("Evening"));
            WriteLine("Contains 'Morning' : {0}", greeting.Contains("Morning"));

            // replace()
            WriteLine("Replaced 'Moring' with 'Evening : {0}", greeting.Replace("Morning", "Evening"));

            // A.2
            // Remove(), Trim()
            WriteLine("Remove() : '{0}'", "I Don't Love You".Remove(2, 6));
            WriteLine("Trim() : '{0}'", " No Soaces ".Trim());

            // A.3
            // Split(), SubString()
            WriteLine(greeting.Substring(0, 5));
            WriteLine(greeting.Substring(5));
            WriteLine();
            string[] arr = greeting.Split(new string[] {" "}, StringSplitOptions.None);
            // string을 해당 string을 통해 구분해 주고 이를 string배열(string[])에 넣는다.

            // A.4-1
            // Format, 서식화
            // 날짜와 시간 서식화
            DateTime dt = new DateTime(2019, 2, 7, 21, 27, 01);
            WriteLine("작성한 시각 Default :: {0}", dt);
            
            WriteLine("12시간 형식 : {0:yyyy-MM-dd tt hh:mm:ss (ddd)}", dt); // 오전, 오후 구분
            WriteLine("24시간 형식 : {0:yyyy-MM-dd hh:mm:ss (dddd)}", dt); // 군대식, dddd는 "요일" 문구까지 붙여줌

            // System.Globalization.CultureInfo 클래스를 이용, 해당 문화권에 맞는 요일 이름을 얻을 수 있음
            CultureInfo ciKo = new CultureInfo("ko-KR");
            WriteLine(dt.ToString("yyyy-MM-dd tt hh:mm:ss (ddd)"), ciKo);

            CultureInfo ciEn = new CultureInfo("en-US");
            WriteLine(dt.ToString("yyyy-MM-dd tt hh:mm:ss (ddd)"), ciEn);

            // A.4-2 문자열 보간
            // // $ "텍스트{<보간식>[,길이] [:서식]}텍스트{...}..."
            string name = "김튼튼";
            int age = 23;
            WriteLine($"{name,-10}, {age:D3}");

            name = "박날씬";
            age = 30;
            WriteLine($"{name}, {age, -10:D3}");

            name = "이비실";
            age = 17;
            WriteLine($"{name}, {(age > 20 ? "성인" : "미성년자")}");

            // String-StringBuilder 성능 비교, 각각 100000번 Append하기
            const int loop_count = 100000;
            long start, end;
            DateTime DT;

            // String
            start = DateTime.Now.Ticks;
            String test = null;
            for (int i = 0; i < loop_count; i++)
            {
                test += "Data\n";
            }
            end = DateTime.Now.Ticks;
            DT = new DateTime(end - start);
            WriteLine("String의 소요시간 :: {0}초.", DT.Second);

            // StringBuilder
            start = DateTime.Now.Ticks;
            StringBuilder test2 = new StringBuilder();
            for (int i = 0; i < loop_count; i++)
            {
                test2.Append("Data\n");
            }
            end = DateTime.Now.Ticks;
            DT = new DateTime(end - start);
            WriteLine("String Buider의 소요시간 :: {0}초.", DT.Second);


            // Stringbuilder 사용 예제
            // 최대 용량 지정 및 최대 개체 길이 지정
            StringBuilder myStringBuilder = new StringBuilder("Hello World!", 25);
            myStringBuilder.Capacity = 25;

            // Append
            myStringBuilder = new StringBuilder("Hello World!");
            myStringBuilder.Append(" What a beautiful day.");
            WriteLine(myStringBuilder);

            // AppendFormat, 서식 지정
            int MyInt = 25;
            myStringBuilder = new StringBuilder("Your total is ");
            myStringBuilder.AppendFormat("{0:C} ", MyInt);
            WriteLine(myStringBuilder);

            // Insert, remove, replace
            myStringBuilder = new StringBuilder("Hello World!");
            myStringBuilder.Insert(6, "Beautiful "); // 6 번째 부터 해당 문자열 삽입
            myStringBuilder.Remove(5, 7); // 5 번째 문자열 부터 시작해서 7개의 문자 삭제
            myStringBuilder.Replace('!', '?'); // 문자열 교체
            WriteLine(myStringBuilder);

            // Stringbuilder -> string ::  StringBuilder.ToString 메서드를 호출
            myStringBuilder.Append("String Type으로 변환 후 ::");
            string myString = myStringBuilder.ToString();

        }
    }
}
