using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth.Server
{
    public static class Constants
    {
        /// <summary>
        /// JWT 토큰 발급자(현재 예제는 토큰발급자 = 사용자서버가 같음)
        /// </summary>
        public const string Issuer = Audiance;
        /// <summary>
        /// JWT 토큰 대상자
        /// </summary>
        public const string Audiance = "https://localhost:44300/"; // 디버깅시 열리는 웹서비스 포트 확인
        /// <summary>
        /// 암호화 키
        /// </summary>
        public const string Secret = "not_too_short_secret_otherwise_it_might_error"; 
    }
}
