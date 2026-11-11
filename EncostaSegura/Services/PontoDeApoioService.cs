using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncostaSegura.Models;

namespace EncostaSegura.Services 
{
    public sealed class PontoDeApoioService
    {
        private readonly List<PontoDeApoio> _pontos = new();

        public PontoDeApoioService()
        {
            RestaurarPadroes();
        }

        public IReadOnlyList<PontoDeApoio> ObterTodos()
        {
            return _pontos.AsReadOnly();
        }

        public void Adicionar(PontoDeApoio ponto)
        {
            ArgumentNullException.ThrowIfNull(ponto);
            _pontos.Add(ponto);
        }

        public void Remover(PontoDeApoio ponto)
        {
            _pontos.Remove(ponto);
        }

        public void Limpar()
        {
            _pontos.Clear();
        }

        public void RestaurarPadroes()
        {
            _pontos.Clear();

            _pontos.Add(new PontoDeApoio(
                "Escola Municipal",
                "Região externa à encosta",
                "Rota verde - escadaria"));

            _pontos.Add(new PontoDeApoio(
                "Centro Comunitário",
                "Região elevada do bairro",
                "Rota azul"));
        }
    }
}



