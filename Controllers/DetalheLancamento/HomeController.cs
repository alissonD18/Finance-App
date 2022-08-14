using Microsoft.AspNetCore.Mvc;
using MVCSandBox.Repositories;

namespace MVCSandBox.Controllers.DetalheLancamento
{
    [ApiController]
    [Route("detalhe-lancamento")]
    public class HomeController : ControllerBase
    {
        public HomeController(ILogger<HomeController> logger,
            DetalheLancamentoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        private readonly ILogger<HomeController> _logger;
        private readonly DetalheLancamentoRepository _repository;

        [HttpGet("{resumoLancamentoId}")]
        public async Task<ActionResult> GetAsync(Guid resumoLancamentoId, CancellationToken cancellationToken)
        {
            var detalheLancamentos = await _repository.GetByResumoLancamentoAsync(resumoLancamentoId, cancellationToken);

            return new JsonResult(detalheLancamentos.Select(s => new
            {
                s.Id,
                s.DataMovimento,
                //TODO: ver como retornar da melhor forma
                s.Cartao,
                s.DiaLancamentoAutomatico,
                s.Fixo,
                s.Tipo,
                s.Valor
            }));
        }

        [HttpPost]
        public async Task<ActionResult> InsertAsync(ViewModel viewModel, CancellationToken cancellationToken)
        {
            // Pensar na validation service
            var model = new Models.DetalheLancamento(dataMovimento: viewModel.DataMovimento, valor: viewModel.Valor,
                tipo: viewModel.Tipo, status: viewModel.Status,
                fixo: viewModel.Fixo,
                diaLancamentoAutomatico: viewModel.DiaLancamentoAutomatico, diaVencimento: viewModel.DiaVencimento,
                cartaoId: viewModel.CartaoId);

            // Fazer recalcular os resumos interessados

            await _repository.InsertAsync(model, cancellationToken);

            return new OkResult();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, ViewModel viewModel, CancellationToken cancellationToken)
        {
            var detalheLancamento = await _repository.GetAsync(id, cancellationToken);

            // Pensar na validation service
            detalheLancamento.Update(dataMovimento: viewModel.DataMovimento, valor: viewModel.Valor,
                tipo: viewModel.Tipo, status: viewModel.Status,
                fixo: viewModel.Fixo,
                diaLancamentoAutomatico: viewModel.DiaLancamentoAutomatico, diaVencimento: viewModel.DiaVencimento,
                cartaoId: viewModel.CartaoId);

            // Fazer recalcular os resumos interessados
            await _repository.UpdateAsync(detalheLancamento, cancellationToken);

            return new OkResult();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            // Pensar na validation service
            // Fazer recalcular os resumos interessados
            await _repository.DeleteAsync(id, cancellationToken);

            return new OkResult();
        }
    }
}
