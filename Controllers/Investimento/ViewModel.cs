namespace MVCSandBox.Controllers.Investimento
{
    public class ViewModel
    {
        public string Descricao { get; set; }
        public decimal PercentualMensalProvento { get; set; }
        public decimal ValorAtualLote { get; set; }
        public int QuantidadeLotes { get; set; }
        public Models.Investimento.ETipoProvento TipoProvento { get; set; }
        public Models.Investimento.ETipoInvestimento TipoInvestimento { get; set; }
        public bool Isento { get; set; }
        public int DiaRecebimento { get; set; }
        public DateTime? DataRecebimentoAPrazo { get; set; }
        public bool Vendido { get; set; }
        public decimal? ValorRecebidoVenda { get; set; }
    }
}
