using Microsoft.AspNetCore.Mvc;
using MVCSandBox.Repositories;

namespace MVCSandBox.Controllers.Investimento
{
    [ApiController]
    [Route("investimento")]
    public class HomeController : ControllerBase
    {
        public HomeController(ILogger<HomeController> logger,
            InvestimentoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        private ILogger<HomeController> _logger;
        private readonly InvestimentoRepository _repository;

        [HttpGet]
        public async Task<ActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAsync(cancellationToken);

            return new JsonResult(entities.Select(s => new
            {
                s.Id,
                s.Descricao,
                s.DiaRecebimento,
                //TODO: fazer getDescription
                TipoProvento = s.TipoProvento.ToString(),
                s.Isento,
                s.TipoInvestimento,
                s.DataRecebimentoAPrazo,
                s.PercentualMensalProvento,
                s.ValorAtualLote,
                s.ValorRecebidoVenda,
                s.Vendido
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync(id, cancellationToken);

            return new JsonResult(new
            {
                entity.Id,
                entity.Descricao,
                entity.DiaRecebimento,
                //TODO: fazer getDescription
                TipoProvento = entity.TipoProvento.ToString(),
                entity.Isento,
                entity.TipoInvestimento,
                entity.DataRecebimentoAPrazo,
                entity.PercentualMensalProvento,
                entity.ValorAtualLote,
                entity.ValorRecebidoVenda,
                entity.Vendido
            });
        }

        [HttpPost]
        public async Task<ActionResult> InsertAsync(ViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = new Models.Investimento(descricao: viewModel.Descricao,
                percentualMensalProvento: viewModel.PercentualMensalProvento,
                valorAtualLote: viewModel.ValorAtualLote, quantidadeLotes: viewModel.QuantidadeLotes,
                tipoProvento: viewModel.TipoProvento, tipoInvestimento: viewModel.TipoInvestimento,
                isento: viewModel.Isento, diaRecebimento: viewModel.DiaRecebimento,
                dataRecebimentoAPrazo: viewModel.DataRecebimentoAPrazo);

            await _repository.InsertAsync(model, cancellationToken);

            return new OkResult();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, ViewModel viewModel, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync(id, cancellationToken);

            entity.Update(descricao: viewModel.Descricao,
                percentualMensalProvento: viewModel.PercentualMensalProvento,
                valorAtualLote: viewModel.ValorAtualLote,
                tipoProvento: viewModel.TipoProvento, tipoInvestimento: viewModel.TipoInvestimento,
                isento: viewModel.Isento, diaRecebimento: viewModel.DiaRecebimento,
                dataRecebimentoAPrazo: viewModel.DataRecebimentoAPrazo);

            await _repository.UpdateAsync(entity, cancellationToken);

            return new OkResult();
        }

        //TODO: fazer os demais updates necessários
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
            return new OkResult();
        }
    }
}
