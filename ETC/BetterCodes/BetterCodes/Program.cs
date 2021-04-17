using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BetterCodes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Substring_기존 방식".Substring(0, 9));
            Console.WriteLine("Substring_C# 8부터 사용가능한 방식"[0..9]); // 범위연산자. C# 8.0부터 추가
            Console.WriteLine("Substring_C# 8부터 사용가능한 방식"[..9]); // 0..9와 같음

            // https://docs.microsoft.com/ko-kr/dotnet/csharp/whats-new/csharp-8#indices-and-ranges

        }
    }
}
