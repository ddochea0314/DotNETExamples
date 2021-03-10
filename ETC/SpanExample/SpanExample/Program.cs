using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace SpanExample
{
    // https://m.blog.naver.com/PostView.nhn?blogId=oidoman&logNo=221827481700&navType=tl
    // https://www.sysnet.pe.kr/2/0/11547
    public class SpanTest
    {
        public static int ArraySum(byte[] data)
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            return sum;
        }

        public static int SpanSum(Span<byte> data)
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            return sum;
        }

        public static int ListSum(List<byte> data)
        {
            int sum = 0;
            for (int i = 0; i < data.Count; i++)
            {
                sum += data[i];
            }
            return sum;
        }
    }
    
    public class BenchmarkForSpanTest
    {
        [Benchmark]
        public int BenchmarkArraySum()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7 };
            return SpanTest.ArraySum(data);
        }

        [Benchmark]
        public int BenchmarkSpanSum()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7 };
            return SpanTest.SpanSum(data);
        }

        [Benchmark]
        public int BenchmarkListSum()
        {
            var data = new List<byte> { 1, 2, 3, 4, 5, 6, 7 };
            return SpanTest.ListSum(data);
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkForSpanTest>();
        }
    }
}
