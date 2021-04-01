using System;
using System.Linq;
using System.Collections.Generic;

namespace BastAlbum
{
    // 베스트엘범 문제풀이. ㅅㅂ "속한 노래가 많이 재생된 장르를 먼저 수록합니다." 뜻이 genres 별 합산해서 정렬하란 뜻이었구나
    // https://programmers.co.kr/learn/courses/30/lessons/42579?language=csharp
    public class Program
    {
        static void Main(string[] args)
        {

        }

        public class Album
        {
            public int idx { get; set; }
            public string genres { get; set; }
            public int plays { get; set; }
        }

        public static int[] solution(string[] genres, int[] plays)
        {
            var answer = new List<int>();
            var albums = new List<Album>();
            albums.AddRange(
                genres.Select((g, i) => new Album()
            {
                genres = g,
                idx = i,
                plays = plays[i]
            }));
            foreach(var order in albums.GroupBy(a => a.genres).OrderByDescending(a => a.Sum(b => b.plays))) 
            {
                answer.AddRange(order.OrderByDescending(a => a.plays).Take(2).Select(a => a.idx));
            }
            return answer.ToArray();
        }
    }
}
