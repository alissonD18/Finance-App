using MVCSandBox.Base;

namespace MVCSandBox.Models
{
    public class ResumoLancamento : BaseModel
    {
        public ResumoLancamento(short mes, short ano,
            decimal saldoInicial, decimal totalDespesas, decimal totalRecebimentos, decimal saldoFinal)
        {
            Mes = mes;
            Ano = ano;
            SaldoInicial = saldoInicial;
            TotalDespesas = totalDespesas;
            TotalRecebimentos = totalRecebimentos;
            SaldoFinal = saldoFinal;
        }

        public int Mes { get; private set; }
        public string MesNome { get => new DateTime(1900, Mes, 1).ToString("MMMM"); }
        public int Ano { get; set; }
        public decimal SaldoInicial { get; private set; }
        public decimal TotalDespesas { get; private set; }
        public decimal TotalRecebimentos { get; private set; }
        public decimal SaldoFinal { get; private set; }

        public List<DetalheLancamento> DetalheLancamentos { get; } = new();

        public void UpdateTotais(decimal totalDespesas, decimal totalRecebimentos)
        {
            TotalDespesas = totalDespesas;
            TotalRecebimentos = totalRecebimentos;

            RecalcularSaldoFinal();
        }

        public void UpdateSaldoInicial(decimal saldoInicial)
        {
            SaldoInicial = saldoInicial;
            RecalcularSaldoFinal();
        }

        private void RecalcularSaldoFinal()
        {
            SaldoFinal = SaldoInicial + TotalRecebimentos - TotalDespesas;
        }
    }
}
