namespace MVCSandBox.Controllers.DetalheLancamento
{
    public class ViewModel
    {
        //public Guid Id { get; set; }
        public DateTime DataMovimento { get; set; }
        public Guid? CartaoId { get; set; }
        public bool Fixo { get; set; }
        public short? DiaLancamentoAutomatico { get; set; }
        public short? DiaVencimento { get; set; }
        public Models.DetalheLancamento.ETipo Tipo { get; set; }
        public Models.DetalheLancamento.EStatus Status { get; set; }
        public decimal Valor { get; set; }
    }
}
