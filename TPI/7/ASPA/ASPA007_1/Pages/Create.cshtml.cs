using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace ASPA007_1.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IRepository _repo;
        private readonly CelebritiesConfig _cfg;
        public string PhotosRequestPath { get; set; }
        public string PhotosFolder { get; set; }
        public Celebrity? Celebrity { get; set; }

        public CreateModel(IRepository repo, IOptions<CelebritiesConfig> cfg)
        {
            _repo = repo;
            _cfg = cfg.Value;
            PhotosFolder = cfg.Value.PhotosFolder;
            PhotosRequestPath = cfg.Value.PhotosRequestPath;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(
                [FromForm] string fullname,
                [FromForm] string nationality,
                IFormFile upload,
                string? press,
                string? filename)
        {
            if (string.IsNullOrEmpty(press))
            {
                var origName = Path.GetFileName(upload.FileName);
                var baseName = Path.GetFileNameWithoutExtension(origName);
                var ext = Path.GetExtension(origName);

                var targetName = origName;
                var targetPath = Path.Combine(PhotosFolder, targetName);
                if (System.IO.File.Exists(targetPath))
                {
                    targetName = $"{baseName}_{Guid.NewGuid():N}{ext}";
                    targetPath = Path.Combine(PhotosFolder, targetName);
                }

                using (var fs = new FileStream(targetPath, FileMode.CreateNew))
                {
                    upload.CopyTo(fs);
                }

                return RedirectToPage(
                    "Create",
                    "Confirm",
                    new { fullname, nationality, filename = targetName }
                );
            }

            if (press.Equals("Confirm", StringComparison.OrdinalIgnoreCase))
            {
                _repo.AddCelebrity(new Celebrity
                {
                    FullName = fullname,
                    Nationality = nationality.Substring(0, 2),
                    ReqPhotoPath = filename!
                });
                return RedirectToPage("Celebrities");
            }

            if (!string.IsNullOrEmpty(filename))
            {
                var fileToDelete = Path.Combine(PhotosFolder, filename);
                if (System.IO.File.Exists(fileToDelete))
                {
                    System.IO.File.Delete(fileToDelete);
                }
            }
            return RedirectToPage("Create");
        }

        public IActionResult OnGetConfirm(string fullname, string nationality, string filename)
        {
            ViewData["Confirm"] = true;
            Celebrity = new() { FullName = fullname, Nationality = nationality, ReqPhotoPath = filename };

            return Page();
        }
    }
}
