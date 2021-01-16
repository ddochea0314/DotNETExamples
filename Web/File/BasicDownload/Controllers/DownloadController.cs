using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BasicDownload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        // GET 방식으로 다운로드를 받는다. 파일명에 특수문자가 있다면 URLEncoding을 통해 파일명을 인코딩하여 호출해야한다. 
        // 구축환경에 IIS인 경우, URLEncoding 했음에도, + 기호와 공란이 제대로 구분 못할 수 있다. 해결방법은 사이트 참조. (https://ddochea.tistory.com/75)
        [HttpGet("{filename}")]
        public async Task<IActionResult> GetDownloadResult(string filename)
        {
            var path = Path.Join("Download", filename);
            if (System.IO.File.Exists(path))
            {
                byte[] bytes;
                using (FileStream file = new FileStream(path: path, mode: FileMode.Open)) // 배포환경에선 다운로드폴더에 대한 권한설정작업이 필요할 수 있다.
                {
                    try
                    {
                        bytes = new byte[file.Length];
                        await file.ReadAsync(bytes);
                        return File(bytes, "application/octet-stream");
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }


            }
            else
            {
                return NotFound();
            }
        }
    }
}
