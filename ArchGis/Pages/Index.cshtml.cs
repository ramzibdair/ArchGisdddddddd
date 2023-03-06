using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArchGis.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IEsriClient _esriClient;
        public string Token { get; set; } 
        public IndexModel(ILogger<IndexModel> logger, IEsriClient esriClient)
        {
            _logger = logger;
            _esriClient = esriClient;
        }

        public async Task OnGetAsync()
        {
            var result  = await _esriClient.GetToken();
            if (result != null)
            {
                Token = result.AccessToken;
            }
        }
    }
}