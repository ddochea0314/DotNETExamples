using System;
using Xunit;

namespace BastAlbum.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Program.solution(new string[] { "classic", "pop", "classic", "classic", "pop" }, new int[] { 500, 600, 150, 800, 2500 });
        }
    }
}
