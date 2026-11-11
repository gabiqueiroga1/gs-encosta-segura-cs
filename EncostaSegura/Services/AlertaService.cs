using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncostaSegura.Models;
using EncostaSegura.Models.Enumeracoes;

namespace EncostaSegura.Services 
{
    public sealed class AlertaService
    {
        private readonly List<Alerta> _alertas = new();

        public IReadOnlyList<Alerta> ObterTodos()
        {
            return _alertas.AsReadOnly();
        }

        public IReadOnlyList<Alerta> ObterAtivos()
        {
            List<Alerta> ativos = new();

            foreach (Alerta alerta in _alertas)
            {
                if (!alerta.Encerrado)
                    ativos.Add(alerta);
            }

            return ativos.AsReadOnly();
        }

        public void SincronizarComEstacao(Estacao estacao)
        {
            ArgumentNullException.ThrowIfNull(estacao);

            Alerta? alertaAtivo = null;

            foreach (Alerta alerta in _alertas)
            {
                if (alerta.CodigoEstacao == estacao.Codigo &&
                    !alerta.Encerrado)
                {
                    alertaAtivo = alerta;
                    break;
                }
            }

            Leitura? leitura = estacao.UltimaLeitura;

            if (leitura is null)
                return;

            TipoAlerta tipo;

            if (estacao is EstacaoEncosta)
            {
                tipo = TipoAlerta.Deslizamento;
            }
            else if (estacao is EstacaoCorrego)
            {
                tipo = TipoAlerta.Alagamento;
            }
            else
            {
                throw new InvalidOperationException(
                    "Tipo de estação não reconhecido.");
            }

            string mensagem = estacao.DescreverLeitura(leitura);

            if (alertaAtivo is not null)
            {
                alertaAtivo.Atualizar(
                    estacao.RiscoAtual,
                    mensagem);

                return;
            }

            int moradoresNotificados =
                estacao is EstacaoEncosta ? 48 : 35;

            Alerta novoAlerta = new(
                estacao.Codigo,
                tipo,
                estacao.RiscoAtual,
                mensagem,
                moradoresNotificados);

            _alertas.Add(novoAlerta);
        }
    }
}

