using Microsoft.AspNetCore.Mvc;
using MVCSandBox.Repositories;

namespace MVCSandBox.Controllers.Cartao
{
    [ApiController]
    [Route("cartao")]
    public class HomeController : ControllerBase
    {
        public HomeController(ILogger<HomeController> logger,
            CartaoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        private ILogger<HomeController> _logger;
        private readonly CartaoRepository _repository;

        [HttpGet]
        public async Task<ActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAsync(cancellationToken);

            return new JsonResult(entities.Select(s => new
            {
                s.Id,
                s.Nome,
                s.DiaFechamentoFatura,
                s.DiaVencimentoFatura,
                s.Limite
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync(id, cancellationToken);

            return new JsonResult(new
            {
                entity.Id,
                entity.Nome,
                entity.DiaFechamentoFatura,
                entity.DiaVencimentoFatura,
                entity.Limite
            });
        }

        [HttpPost]
        public async Task<ActionResult> InsertAsync(ViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = new Models.Cartao(nome: viewModel.Nome,
                limite: viewModel.Limite,
                diaVencimentoFatura: viewModel.DiaVencimentoFatura,
                diaFechamentoFatura: viewModel.DiaFechamentoFatura);

            await _repository.InsertAsync(model, cancellationToken);

            return new OkResult();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, ViewModel viewModel, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync(id, cancellationToken);

            entity.Update(nome: viewModel.Nome,
                limite: viewModel.Limite,
                diaVencimentoFatura: viewModel.DiaVencimentoFatura,
                diaFechamentoFatura: viewModel.DiaFechamentoFatura);

            await _repository.UpdateAsync(entity, cancellationToken);

            return new OkResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
            return new OkResult();
        }
    }
}
