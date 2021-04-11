using System;
using Xunit;

namespace Camouflage.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Program.solution(new string[,] { { "yellow_hat", "headgear" }, { "blue_sunglasses", "eyewear" }, { "green_turban", "headgear" } });
        }
    }
}
