using MVCSandBox.Base;

namespace MVCSandBox.Models
{
    public class DetalheLancamento : BaseModel
    {
        public DetalheLancamento(DateTime dataMovimento, decimal valor, ETipo tipo, EStatus status, bool fixo,
            short? diaLancamentoAutomatico, short? diaVencimento, Guid? cartaoId)
        {
            DataMovimento = dataMovimento;
            Valor = valor;
            Tipo = tipo;
            Status = status;
            Fixo = fixo;
            DiaLancamentoAutomatico = diaLancamentoAutomatico;
            DiaVencimento = diaVencimento;

            CartaoId = cartaoId;
        }

        public enum ETipo : short
        {
            Recebimento = 1,
            Despesa = 2
        }

        public enum EStatus : short
        {
            Pago = 1,
            APagar = 2,
            Recebido = 3,
            AReceber = 4
        }

        public DateTime DataMovimento { get; private set; }
        public decimal Valor { get; private set; }
        public ETipo Tipo { get; set; }
        public EStatus Status { get; set; }

        #region Campos relacionados a lançamento futuros e automáticos

        public bool Fixo { get; set; }
        /// <summary>
        /// Em qual dia o lançamento será feito.
        /// </summary>
        public short? DiaLancamentoAutomatico { get; set; }
        /// <summary>
        /// Dia em que passará o lançamento para pago/recebido.
        /// </summary>
        public short? DiaVencimento { get; set; }

        #endregion


        public Guid ResumoLancamentoId { get; set; }
        public ResumoLancamento ResumoLancamento { get; set; }
        public Guid InvestimentoId { get; set; }
        public Investimento Investimento { get; set; }
        public Guid? CartaoId { get; private set; }
        public Cartao Cartao { get; private set; }

        public void Update(DateTime dataMovimento, decimal valor, ETipo tipo, EStatus status, bool fixo,
            short? diaLancamentoAutomatico, short? diaVencimento, Guid? cartaoId)
        {
            DataMovimento = dataMovimento;
            Valor = valor;
            Tipo = tipo;
            Status = status;
            Fixo = fixo;
            DiaLancamentoAutomatico = diaLancamentoAutomatico;
            DiaVencimento = diaVencimento;

            CartaoId = cartaoId;
        }
    }
}
