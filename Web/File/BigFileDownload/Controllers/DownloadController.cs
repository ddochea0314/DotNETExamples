using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BigFileDownload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDownloadResult()
        {
            // 파일 다운로드 링크 : speedtest-tor1.digitalocean.com. 1gb.test 파일을 다운로드 받은 후 Download 폴더에 붙여넣고, "출력 디렉토리 복사"에서 "새 버전이면 복사"를 선택한다.
            var path = Path.Join("Download", "1gb.test");
            if (System.IO.File.Exists(path))
            {
                var file = new FileStream(path: path, mode: FileMode.Open, FileAccess.Read);
                return new FileStreamResult(file, "application/octet-stream") { FileDownloadName = "1gb.test", EnableRangeProcessing = true }; // 단순 파일 다운로드의 경우는 EnableRangeProcessing 링크를 따로 설정하지 않아도됨.
            }
            else
            {
                return NotFound();
            }
        }
    }
}
