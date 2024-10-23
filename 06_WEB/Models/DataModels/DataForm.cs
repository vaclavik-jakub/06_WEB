using Microsoft.EntityFrameworkCore;
using _06_WEB.Models.DataModels;


namespace _06_WEB.Models.DataModels
{
    public class DataForm
    {
        public Guid Id { get; set; } // Primární klíč
        public string FileName { get; set; } // Název souboru
        public string MediaType { get; set; } // Typ média (např. obrázek, video atd.)
        public DateTime UploadedAt { get; set; } // Datum a čas nahrání souboru
        public string FilePath { get; set; } // Cesta k souboru

        // Konstruktor pro automatické nastavení času nahrání a vytvoření ID
        public ImageFolder()
        {
            Id = Guid.NewGuid();
            UploadedAt = DateTime.Now;
        }
    }
}
