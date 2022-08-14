using MVCSandBox.Repositories;

namespace MVCSandBox.Controllers.DetalheLancamento
{
    public class Service
    {
        /*
         Esta service terá de ser acionada toda vez quem lançamento for inserido, atualizado, excluído. Futuramente usar FILA (apenas para conhecimento).
Esta service deve:
- Ao alterar os lançamentos, deve atualizar totais de entrada ou saída do mês referente, assim atualizando o saldoFinal.
- Após atualizar o saldoFinal do mês referente, deve-se atualizar o 
        saldoInicial e consequentemente os saldoFinal dos meses posteriores.
- Caso seja um lançamento relacionado a cartão e seja de crédito, verificar quantidade de parcelas e então adicionar uma 
        despesa em cada mês com o valor da parcela, e associada ao mesmo cartão.
- Caso o registro seja um investimento, deverá ser levado em consideração a regra de cada 
        investimento e o registro deve ser manipulado de acordo com o mesmo.
No processo, a controller fará a inserção/atualização/exclusão de registro normalmente, 
        mas a service precisará receber o registro para saber 
        se ele foi alterado ou atualizado de fato, para então fazer o processo que ela deve fazer.

Passo a passo inserção:
- Controller recebe registro.
- Controller passa o mesmo para a service.
- Esta service vai validar o registro.
- Se for valido, fazer regra isFixo, fazer atualização dos resumos, lançamentos de parcelas de cartão 
        de crédito, lançamento de investimentos.
- Se não for válido, deve ser devolvido algo como false e uma mensagem de erro.
         */
        public Service(DetalheLancamentoRepository repository,
            ResumoLancamentoRepository resumoLancamentoRepository,
            CartaoRepository cartaoRepository)
        {
            _repository = repository;
            _resumoLancamentoRepository = resumoLancamentoRepository;
            _cartaoRepository = cartaoRepository;
        }

        private readonly DetalheLancamentoRepository _repository;
        private readonly ResumoLancamentoRepository _resumoLancamentoRepository;
        private readonly CartaoRepository _cartaoRepository;

        /// <summary>
        /// Atualiza totais do mês relacionado.
        /// </summary>
        public async Task RecalcularMesAsync(Guid resumoLancamentoId, CancellationToken cancellationToken)
        {
            var resumoLancamento = await _resumoLancamentoRepository.GetAsync(resumoLancamentoId, cancellationToken);
            var detalheLancamentos = await _repository.GetByResumoLancamentoAsync(resumoLancamentoId, cancellationToken);

            var totalRecebimentos = detalheLancamentos
                .Where(w => w.Tipo == Models.DetalheLancamento.ETipo.Recebimento)
                .Sum(s => s.Valor);

            var totalDespesas = detalheLancamentos
                .Where(w => w.Tipo == Models.DetalheLancamento.ETipo.Despesa)
                .Sum(s => s.Valor);

            resumoLancamento.UpdateTotais(totalRecebimentos: totalRecebimentos, totalDespesas: totalDespesas);
        }

        /// <summary>
        /// Atualiza totais a partir de um mês de referência para frente.
        /// </summary>
        public async Task RecalcularSaldosIniciaisAsync(DateTime dataRefencia, CancellationToken cancellationToken)
        {
            var resumoLancamentos = await _resumoLancamentoRepository.GetRetotalizarAsync(mes: dataRefencia.Month,
                ano: dataRefencia.Year,
                cancellationToken: cancellationToken);

            var first = resumoLancamentos.First();

            decimal saldoFinalAnterior = 0M;

            foreach (var resumoLancamento in resumoLancamentos)
            {
                if (resumoLancamento != first)
                    resumoLancamento.UpdateSaldoInicial(saldoFinalAnterior);

                saldoFinalAnterior = resumoLancamento.SaldoFinal;
            }

            await _resumoLancamentoRepository.UpdateAsync(resumoLancamentos, cancellationToken);
        }

        /// <summary>
        /// Retotaliza faturas de acordo com cartão passado por parâmetro,
        /// retotalizando a fatura dos meses posteriores também.
        /// </summary>
        public async Task RecalcularFaturasAsync(Guid cartaoId, CancellationToken cancellationToken)
        {
            var cartao = await _cartaoRepository.GetAsync(cartaoId, cancellationToken);
            var detalheLancamentosFaturaEmAberto = await _repository.GetByCartaoAsync(cartaoId, cancellationToken);
        }
    }
}
