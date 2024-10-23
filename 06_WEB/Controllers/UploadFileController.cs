using _06_WEB.Models.InputModels;
using _06_WEB.Data; // namespace pro přístup k databázi a contextu
using _06_WEB.Models; // namespace pro modely
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace _06_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Dependency Injection pro databázový kontext

        public UploadFileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] InputForm model)
        {
            var fileId = Guid.NewGuid().ToString();

            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            // Vytvoření cesty k souboru
            var filePath = Path.Combine("Upload", fileId + model.File.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            // Vytvoření nového záznamu v databázi
            var uploadedFile = new ImageFolder
            {
                Id = Guid.NewGuid(),
                FileName = model.File.FileName,
                MediaType = model.File.ContentType,
                FilePath = filePath, // Relativní nebo absolutní cesta podle potřeby
                UploadedAt = DateTime.Now
            };

            _context.ImageFolders.Add(uploadedFile); // Přidání záznamu do databáze
            await _context.SaveChangesAsync(); // Uložení změn do databáze

            return Ok(new { message = "File uploaded successfully.", filePath = uploadedFile.FilePath });
        }
    }
}
