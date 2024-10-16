using _06_WEB.Models.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _06_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm]/*určuje co nás zajímá*/ InputForm model)
        {
            var fileId = Guid.NewGuid().ToString();

            if (model.File == null || model.File.Length == 0) 
            { 
                return BadRequest("No file uploaded");
            }

            var filePath = Path.Combine("Upload", fileId+model.File.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            return Ok(new { filePath }); //návratová metoda z ControllerBase - vrací status code: 200
        }
    }
}
