using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _06_WEB.Models.DataModels;
using _06_WEB.Data;

namespace _06_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageFolderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImageFolderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Soubor nebyl poskytnut.");

            var uploadsFolder = Path.Combine("uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageFolder = new ImageFolder
            {
                FileName = fileName,
                MediaType = file.ContentType,
                FilePath = filePath
            };

            _context.ImageFolders.Add(imageFolder);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Soubor byl úspěšně nahrán.", imageFolder });
        }
    }
}
