using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ASPA007_1.Pages
{
    public class CelebritiesModel : PageModel
    {
        private readonly IRepository _repo;
        private readonly CelebritiesConfig _cfg;

        public List<Celebrity> AllCelebrities { get; private set; } = new();
        public string PhotosRequestPath { get; }

        public CelebritiesModel(IRepository repo, IOptions<CelebritiesConfig> cfg)
        {
            _repo = repo;
            _cfg = cfg.Value;
            PhotosRequestPath = _cfg.PhotosRequestPath?.TrimEnd('/') ?? "";
        }


        public void OnGet()
        {
            AllCelebrities = _repo.GetAllCelebrities();
        }
    }
}
