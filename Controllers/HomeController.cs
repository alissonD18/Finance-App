using Microsoft.AspNetCore.Mvc;
using MVCSandBox.ViewModels;

namespace MVCSandBox.Controllers.ResumoLancamento
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger = logger;
        }

        private readonly ILogger<HomeController> _logger;
        
        [HttpPost("plano-rendimento")]
        public JsonResult PlanoRendimento(PlanoRendimentoRequestViewModel viewModel)
        {
            var dataInical = DateTime.Today;
            if(viewModel.DataInicial.HasValue)
                dataInical = viewModel.DataInicial.Value;

            var valorInvestimentoMes = viewModel.ValorAtualCota * viewModel.QuantidadeCotasCompradasMes;

            var response = new List<PlanoRendimentoResponseViewModel>()
            {
                new PlanoRendimentoResponseViewModel()
                {
                    Data = dataInical,
                    PercentualRendimento = viewModel.PercentualRendimento,
                    QuantidadeCotasAcumuldas = viewModel.QuantidadeCotasCompradasMes,
                    ValorInvestimentoMes = valorInvestimentoMes,
                    ValorIvestidoAcumulado = valorInvestimentoMes,
                    ValorAReceberDividendo = valorInvestimentoMes * viewModel.PercentualRendimento
                }
            };

            while(response.Select(s => s.ValorAReceberDividendo).Last() < viewModel.Meta)
            {
                var atual = response.Last();
                var data = atual.Data.AddMonths(1);

                var quantidadeCotasCompradasMes = viewModel.QuantidadeCotasCompradasMes;

                if (atual.ValorAReceberDividendo >= viewModel.ValorAtualCota)
                {
                    quantidadeCotasCompradasMes += Convert.ToInt32(Math.Truncate(atual.ValorAReceberDividendo / viewModel.ValorAtualCota));
                    valorInvestimentoMes = viewModel.ValorAtualCota * quantidadeCotasCompradasMes;
                }

                response.Add(new PlanoRendimentoResponseViewModel
                {
                    Data = data,
                    PercentualRendimento = viewModel.PercentualRendimento,
                    QuantidadeCotasAcumuldas = atual.QuantidadeCotasAcumuldas + quantidadeCotasCompradasMes,
                    ValorInvestimentoMes = valorInvestimentoMes,
                    ValorIvestidoAcumulado = atual.ValorIvestidoAcumulado + valorInvestimentoMes,
                    ValorAReceberDividendo = (atual.ValorIvestidoAcumulado + valorInvestimentoMes) * viewModel.PercentualRendimento
                });
            }

            JsonResult jsonResult = new JsonResult(new
            {
                Planejamentos = response,
            })
            {
                StatusCode = 200
            };

            return jsonResult;
        }
    }
}