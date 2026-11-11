using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EncostaSegura.Models.Enumeracoes;

namespace EncostaSegura.Models 
{
    public sealed class EstacaoEncosta : Estacao
    {
        public EstacaoEncosta()
            : base(
                "ENC-01",
                "Estação de Encosta",
                "Área de monitoramento da encosta")
        {
        }

        public override NivelRisco CalcularRisco(Leitura leitura)
        {
            if (leitura.Inclinacao >= 10 ||
                leitura.UmidadeSolo >= 85 ||
                leitura.Chuva72h >= 70)
            {
                return NivelRisco.Alto;
            }

            if (leitura.UmidadeSolo >= 60 ||
                leitura.Chuva72h >= 30 ||
                leitura.Inclinacao >= 5)
            {
                return NivelRisco.Médio;
            }

            return NivelRisco.Baixo;
        }

        public override string DescreverLeitura(Leitura leitura)
        {
            return
                $"Umidade: {leitura.UmidadeSolo}% | " +
                $"Chuva atual: {leitura.ChuvaAtual} mm | " +
                $"Chuva em 72h: {leitura.Chuva72h} mm | " +
                $"Inclinação: {leitura.Inclinacao}°";
        }
    }

}

