using System;
using System.Linq;
using System.Collections.Generic;

namespace Camouflage
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }

        public static int solution(string[,] clothes)
        {
            int answer = 1;
            var dic = new Dictionary<string, List<string>>();
            for (int i = 0; i < (clothes.Length / clothes.Rank); i++)
            {
                for(int r = 0; r < clothes.Rank / 2; r++)
                {
                    var key = clothes[i, r + 1];
                    if (!dic.ContainsKey(key))
                    {
                        dic.Add(key, new List<string>());
                    }
                    dic[key].Add(clothes[i, r]);
                }
            }
            
            foreach (var item in dic)
            {
                answer *= item.Value.Count + 1;
            }

            return answer - 1;
        }
    }
}
