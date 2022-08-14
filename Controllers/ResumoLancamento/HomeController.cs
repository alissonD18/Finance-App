using Microsoft.AspNetCore.Mvc;
using MVCSandBox.Context;
using MVCSandBox.Repositories;

namespace MVCSandBox.Controllers.ResumoLancDia
{
    [ApiController]
    [Route("resumo-lancamento")]
    public class HomeController : ControllerBase
    {
        public HomeController(ILogger<HomeController> logger,
            ResumoLancamentoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        private readonly ILogger<HomeController> _logger;
        private readonly ResumoLancamentoRepository _repository;

        [HttpGet("{ano}")]
        public async Task<ActionResult> GetAsync(short ano, CancellationToken cancellationToken)
        {
            var resumoLancamentos = await _repository.GetByAnoAsync(ano, cancellationToken);

            return new JsonResult(resumoLancamentos.Select(s => new
            {
                s.Id,
                s.MesNome,
                s.SaldoInicial,
                s.TotalRecebimentos,
                s.TotalDespesas,
                s.SaldoFinal
            }).ToList());
        }
    }
}