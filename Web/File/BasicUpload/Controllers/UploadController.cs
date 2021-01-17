using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BasicUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {      
            if(file.Length > 0) // 파일사이즈가 0이면 처리하지 않는다.
            {
                var path = Path.Combine($"Upload");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path); // 웹 서비스 내 업로드폴더가 없을 경우 자동생성을 위한 처리
                }
                var filename = DateTime.Now.ToString("yyyyMMddHHmmss_") + file.FileName; // 동일한 파일명이 있으면 덮어쓰거나, 오류가 날 수 있으므로 파일명을 바꾼다.
                path = Path.Combine(path, filename);
                using (var stream = System.IO.File.Create(path))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return Ok();
        }
    }
}
