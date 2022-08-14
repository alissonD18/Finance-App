namespace MVCSandBox.ViewModels
{
    public class PlanoRendimentoRequestViewModel
    {
        public DateTime? DataInicial { get; set; }
        public decimal Meta { get; set; }
        public int QuantidadeCotasCompradasMes { get; set; }
        public decimal ValorAtualCota { get; set; }
        public decimal PercentualRendimento { get; set; }
    }
}
