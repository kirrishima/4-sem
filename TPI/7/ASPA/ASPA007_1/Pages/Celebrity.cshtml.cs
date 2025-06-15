using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ASPA007_1.Pages
{
    public class CelebrityModel : PageModel
    {
        private readonly IRepository _repo;
        private readonly CelebritiesConfig _config;


        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Celebrity? Celebrity { get; private set; }

        public string PhotosRequestPath => _config.PhotosRequestPath;

        public CelebrityModel(IRepository repo, IOptions<CelebritiesConfig> cfg)
        {
            _repo = repo;
            _config = cfg.Value;
        }

        public IActionResult OnGet()
        {
            Celebrity = _repo.GetCelebrityById(Id);
            if (Celebrity == null)
                return NotFound();
            return Page();
        }
    }
}
