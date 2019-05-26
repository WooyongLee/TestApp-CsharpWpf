using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTest
{
    class Program
    {
        static void Main(string[] args)
        {

            LinqExample.GetElementAtCollection();

        }

    }

    public static class LinqExample
    {
        // Enumerable.Request 메서드 연습
        public static void RepeatTest()
        {
            var numbers = Enumerable.Repeat(-1, 20).ToList(); // -1로 채워, List<int>로 변환한다.

            Console.Write("numbers 배열 초기화 :");
            foreach (var number in numbers)
            {
                Console.Write("{0}, ", number);
            }
            Console.WriteLine();

            var strings = Enumerable.Repeat("(unknown", 12).ToArray(); // "(unknown)"으로 채운다.
            Console.Write("strings 배열 초기화 :");
            foreach (var str in strings)
            {
                Console.Write("{0}, ", str);
            }
            Console.WriteLine();
        }

        // Enumerable.Range 메서드 연습
        public static void RangeTest()
        {
            var array = Enumerable.Range(1, 20).ToArray(); // 1~20을 index 0~19 배열에 할당한다.
            Console.Write("array 1부터 20까지 할당 :");
            foreach (var item in array)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();
        }

        // 컬렉션을 집계하기
        public static void CompileAtCollection()
        {
            var numbers = new List<int> { 9, 7, 3, 2, 1, 5, 6, 2, 8, 6, 4 };
            var average = numbers.Average(); // 평균
            var sum = numbers.Sum(); // 합
            var min = numbers.Where(n => n > 0).Min(); // 0보다 큰 값들 중에서 최소값
            var max = numbers.Max(x => x); // 최대값(x가 클래스 형태일 경우 x. (값 참조)를 이용하여 필드를 호출하기
            var count = numbers.Count(n => n == 2); // 요소 중에서 특정 값의 갯수를 세기

            Console.Write("Source :");
            foreach (var item in numbers)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();
            Console.Write("average = {0}, sum = {1}, min = {2}, max = {3}, count = {4}", average, sum, min, max, count);
            Console.WriteLine();
        }

        // 컬렉션 판정하기
        public static void JudgeAtCollection()
        {
            var numbers = new List<int> { 92, 71, 34, 23, 17, 56, 62, 29, 86, 61, 40 };
            bool exists = numbers.Any(n => n % 8 == 0); // 조건에 일치하는 요소가 존재하는 지 여부를 조사
            exists = numbers.Any(x => x >= 50); // (x가 클래스 형태일 경우 x. (값 참조)를 이용하여 필드를 호출하기
            bool isAllPositive = numbers.All(n => n > 0); // 모든 요소가 조건을 만족하는 지 조사

            var numbers2 = new List<int> { 92, 71, 34, 23, 17, 56, 62, 29, 86, 61, 40 };
            bool equal = numbers.SequenceEqual(numbers2); // 두 컬렉션이 같은 지 조사
        }
        
        // 컬렉션 내에서 요소를 구하기(단일, 복수요소)
        public static void GetElementAtCollection()
        {
            // 조건을 만족하는 단일 요소 구하기
            var text = "I am Lee Woo Yong. I am in home";
            var words = text.Split(' ');
            var wordFirst = words.FirstOrDefault(x => x.Length == 5); // 조건에 일치하는 첫 요소를 구하기
            var wordLast = words.LastOrDefault(x => x.Length == 2); // 조건에 일치하는 마지막 요소를 구하기

            var numbers = new List<int> { 92, 71, 34, 23, 17, 56, 62, 29, 86, 61, 40 };
            var index = numbers.FindIndex(n => n > 50); // 조건에 일치하는 첫 인덱스 구하기

            Console.WriteLine(text);
            Console.WriteLine(wordFirst);
            Console.WriteLine(wordLast);

            // 조건을 만족하는 복수 요소 구하기
            var results = numbers.Where(n => n >= 40).Take(5); // 지정된 개수만큼 배열에서 구하기
            var selected = numbers.TakeWhile(x => x <= 100); // 지정한 조건을 만족하는 동안에만 요소 구하기
            // 여기서는 100 초과한 값이 발견되면 열거 작업을 끝낸다.
            var selectedSkip = numbers.SkipWhile(n => n >= 50).ToList(); // 조건을 만족하는 동안에 값을 건너뛰기 
            // + 건너띈 값으로 리스트를 생성
            // 여기서는 50을 미만의 값이 발견되면, 이후 값들로 리스트를 만든다 (<->TakeWhile())

            Console.Write("results :");
            foreach (var item in results)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();

            Console.Write("selected :");
            foreach (var item in selected)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();

            Console.Write("selected Skip:");
            foreach (var item in selectedSkip)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();

        }

    }
}
