namespace MVCSandBox.ViewModels
{
    /*
     - Valor Objetivo
     - Aporte mensal planejado (ou número de cotas compradas, em caso de FII's)
     - Valor pago (na cota, em caso de FII's)
     - Percentual de rendimento (dividendo)
     */
    public class PlanoRendimentoResponseViewModel
    {
        public DateTime Data { get; set; }
        public decimal ValorIvestidoAcumulado { get; set; }
        public int QuantidadeCotasAcumuldas { get; set; }
        public decimal ValorInvestimentoMes { get; set; }
        public decimal PercentualRendimento { get; set; }
        public decimal ValorAReceberDividendo { get; set; }
    }
}
