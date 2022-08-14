using System.ComponentModel;
using MVCSandBox.Base;

namespace MVCSandBox.Models
{
    public class Investimento : BaseModel
    {
        public Investimento(string descricao, decimal percentualMensalProvento,
            decimal valorAtualLote, int quantidadeLotes, ETipoProvento tipoProvento, ETipoInvestimento tipoInvestimento,
            bool isento, int diaRecebimento, DateTime? dataRecebimentoAPrazo)
        {
            Descricao = descricao;
            PercentualMensalProvento = percentualMensalProvento;
            ValorAtualLote = valorAtualLote;
            QuantidadeLotes = quantidadeLotes;
            TipoProvento = tipoProvento;
            TipoInvestimento = tipoInvestimento;
            Isento = isento;
            DiaRecebimento = diaRecebimento;
            DataRecebimentoAPrazo = dataRecebimentoAPrazo;
        }

        public enum ETipoProvento : short
        {
            [Description("Diário")]
            Diario = 1,
            [Description("Semanal")]
            Semanal = 2,
            [Description("Mensal")]
            Mensal = 3,
            [Description("Trimestral")]
            Trimestral = 4,
            [Description("Semestral")]
            Semestral = 5,
            [Description("Anual")]
            Anual = 6,
            [Description("A longo prazo")]
            LongoPrazo = 7
        }

        public enum ETipoInvestimento : short
        {
            [Description("Fundo imobiliário")]
            FundoImobiliario = 1,
            [Description("Ação na bolsa")]
            Acao = 2,
            [Description("Renda fixa")]
            RendaFixa = 3,
            // Colocar mais tipos a medida da necessidade
        }

        public string Descricao { get; private set; }
        public decimal PercentualMensalProvento { get; private set; }
        /// <summary>
        /// Cota em caso de FII, papeis em caso de ações na bolsa e etc...
        /// </summary>
        public decimal ValorAtualLote { get; private set; }
        public int QuantidadeLotes { get; private set; }
        public ETipoProvento TipoProvento { get; private set; }
        public ETipoInvestimento TipoInvestimento { get; private set; }
        /// <summary>
        /// Se é isento de imposto de renda.
        /// </summary>
        public bool Isento { get; private set; }
        public int DiaRecebimento { get; private set; }
        /// <summary>
        /// Em caso de investimento a longo prazo, esta data deve ser informada.
        /// </summary>
        public DateTime? DataRecebimentoAPrazo { get; private set; }
        /// <summary>
        /// Campos para informar dados de investimentos que podem ser vendidos e
        /// devem ser informados apenas quando foram vendidos.
        /// </summary>
        public bool Vendido { get; private set; }
        public decimal? ValorRecebidoVenda { get; private set; }

        /// <summary>
        /// Um investimento pode estar associado a vários lançamentos, simulando assim os aportes.
        /// </summary>
        public List<DetalheLancamento> DetalheLancamentos { get; private set; }

        public void Update(string descricao, decimal percentualMensalProvento,
            decimal valorAtualLote, ETipoProvento tipoProvento, ETipoInvestimento tipoInvestimento,
            bool isento, int diaRecebimento, DateTime? dataRecebimentoAPrazo)
        {
            if (Vendido)
                throw new Exception("Uma vez vendido, o investimento não deve ser alterado.");

            Descricao = descricao;
            PercentualMensalProvento = percentualMensalProvento;
            ValorAtualLote = valorAtualLote;
            TipoProvento = tipoProvento;
            TipoInvestimento = tipoInvestimento;
            Isento = isento;
            DiaRecebimento = diaRecebimento;
            DataRecebimentoAPrazo = dataRecebimentoAPrazo;
        }

        /// <summary>
        /// Método deverá ser chamado quando for dado um lançamento indicando a venda do mesmo.
        /// </summary>
        public void UpdateVenda()
        {
            if (Vendido)
                throw new Exception("Uma vez vendido, o investimento não deve ser alterado.");

            Vendido = true;
            ValorRecebidoVenda = QuantidadeLotes * ValorAtualLote;
        }

        public void UpdateQuantidadeLote(int quantidadeLotes)
        {
            if (Vendido)
                throw new Exception("Uma vez vendido, o investimento não deve ser alterado.");

            QuantidadeLotes = quantidadeLotes;
        }
    }
}
