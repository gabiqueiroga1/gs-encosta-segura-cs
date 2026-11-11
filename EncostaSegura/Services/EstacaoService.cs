using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncostaSegura.Models;

namespace EncostaSegura.Services 
{
    public sealed class EstacaoService
    {
        private readonly List<Estacao> _estacoes = new();

        public EstacaoService()
        {
            _estacoes.Add(new EstacaoEncosta());
            _estacoes.Add(new EstacaoCorrego());
        }

        public IReadOnlyList<Estacao> ObterTodas()
        {
            return _estacoes.AsReadOnly();
        }

        public Estacao? BuscarPorCodigo(string codigo)
        {
            foreach (Estacao estacao in _estacoes)
            {
                if (estacao.Codigo == codigo)
                    return estacao;
            }

            return null;
        }

        public void AdicionarLeitura(string codigo, Leitura leitura)
        {
            Estacao? estacao = BuscarPorCodigo(codigo);

            if (estacao is null)
                throw new InvalidOperationException(
                    "Estação não encontrada.");

            estacao.RegistrarLeitura(leitura);
        }
    }
}

