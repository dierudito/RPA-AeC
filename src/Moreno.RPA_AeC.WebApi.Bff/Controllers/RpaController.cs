using Microsoft.AspNetCore.Mvc;
using Moreno.RPA_AeC.Application.Interfaces;

namespace Moreno.RPA_AeC.WebApi.Bff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RpaController : ControllerBase
    {
        private readonly IPesquisaAppService _pesquisaAppService;

        public RpaController(IPesquisaAppService pesquisaAppService)
        {
            _pesquisaAppService = pesquisaAppService;
        }

        [HttpPost("/pesquisar/{termo}")]
        public async Task<IActionResult> PesquisarTermoAsync([FromRoute] string termo)
        {
            var relatorio = await _pesquisaAppService.PesquisarTermoAsync(termo);
            return Ok(relatorio);
        }

    }
}
