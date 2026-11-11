using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncostaSegura.Models.Enumeracoes;

namespace EncostaSegura.Models 
{
    public sealed class EstacaoCorrego : Estacao
    {
        public EstacaoCorrego()
            : base(
                "NIV-01",
                "Estação de Córrego",
                "Ponte sobre o córrego")
        {
        }

        public override NivelRisco CalcularRisco(Leitura leitura)
        {
            if (leitura.NivelAgua >= 70 ||
                leitura.VelocidadeSubida >= 30)
            {
                return NivelRisco.Alto;
            }

            if (leitura.NivelAgua >= 40 ||
                leitura.VelocidadeSubida >= 15)
            {
                return NivelRisco.Médio;
            }

            return NivelRisco.Baixo;
        }

        public override string DescreverLeitura(Leitura leitura)
        {
            return
                $"Nível da água: {leitura.NivelAgua} cm | " +
                $"Velocidade de subida: {leitura.VelocidadeSubida} cm/h";
        }
    }
}

