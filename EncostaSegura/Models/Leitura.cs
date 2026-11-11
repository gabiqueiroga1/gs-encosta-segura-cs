using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncostaSegura.Models
{
    public sealed class Leitura
    {
        public DateTime DataHora { get; private set; }
        public double UmidadeSolo { get; private set; }
        public double ChuvaAtual { get; private set; }
        public double Chuva72h { get; private set; }
        public double Inclinacao { get; private set; }
        public double NivelAgua { get; private set; }
        public double VelocidadeSubida { get; private set; }

        public Leitura(
            double umidadeSolo,
            double chuvaAtual,
            double chuva72h,
            double inclinacao,
            double nivelAgua,
            double velocidadeSubida)
        {
            ValidarNaoNegativo(umidadeSolo, nameof(umidadeSolo));
            ValidarNaoNegativo(chuvaAtual, nameof(chuvaAtual));
            ValidarNaoNegativo(chuva72h, nameof(chuva72h));
            ValidarNaoNegativo(inclinacao, nameof(inclinacao));
            ValidarNaoNegativo(nivelAgua, nameof(nivelAgua));
            ValidarNaoNegativo(velocidadeSubida, nameof(velocidadeSubida));

            if (umidadeSolo > 100)
                throw new ArgumentOutOfRangeException(
                    nameof(umidadeSolo),
                    "A umidade deve estar entre 0 e 100.");

            if (inclinacao > 90)
                throw new ArgumentOutOfRangeException(
                    nameof(inclinacao),
                    "A inclinação deve estar entre 0 e 90 graus.");

            UmidadeSolo = umidadeSolo;
            ChuvaAtual = chuvaAtual;
            Chuva72h = chuva72h;
            Inclinacao = inclinacao;
            NivelAgua = nivelAgua;
            VelocidadeSubida = velocidadeSubida;
            DataHora = DateTime.Now;
        }

        private static void ValidarNaoNegativo(double valor, string nome)
        {
            if (double.IsNaN(valor) ||
                double.IsInfinity(valor) ||
                valor < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nome,
                    "O valor deve ser um número válido e não negativo.");
            }
        }
    }

}


