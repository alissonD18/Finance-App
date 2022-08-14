using MVCSandBox.Base;

namespace MVCSandBox.Models
{
    public class Cartao : BaseModel
    {
        public Cartao(string nome, decimal limite, short diaVencimentoFatura, short diaFechamentoFatura)
        {
            Nome = nome;
            Limite = limite;
            DiaVencimentoFatura = diaVencimentoFatura;
            DiaFechamentoFatura = diaFechamentoFatura;
        }

        public string Nome { get; private set; }
        public decimal Limite { get; private set; }
        public short DiaVencimentoFatura { get; private set; }
        public short DiaFechamentoFatura { get; private set; }

        public void Update(string nome, decimal limite, short diaVencimentoFatura, short diaFechamentoFatura)
        {
            Nome = nome;
            Limite = limite;
            DiaVencimentoFatura = diaVencimentoFatura;
            DiaFechamentoFatura = diaFechamentoFatura;
        }
    }
}
