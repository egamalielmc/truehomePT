using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace TrueHomePT.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            //Propiedades pp = new Propiedades();

            //Console.WriteLine(pp.GetPropiedades());

            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}