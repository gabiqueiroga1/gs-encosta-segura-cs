using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncostaSegura.Models.Enumeracoes;

namespace EncostaSegura.Models {

    public abstract class Estacao
    {
        private readonly List<Leitura> _historico = new();

        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Localizacao { get; private set; }

        public NivelRisco RiscoAtual { get; private set; }

        public IReadOnlyList<Leitura> Historico => _historico.AsReadOnly();

        public Leitura? UltimaLeitura =>
            _historico.Count > 0 ? _historico[^1] : null;

        protected Estacao(
            string codigo,
            string nome,
            string localizacao)
        {
            Codigo = codigo;
            Nome = nome;
            Localizacao = localizacao;
            RiscoAtual = NivelRisco.Baixo;
        }

        public void RegistrarLeitura(Leitura leitura)
        {
            ArgumentNullException.ThrowIfNull(leitura);

            _historico.Add(leitura);
            RiscoAtual = CalcularRisco(leitura);
        }

        public abstract NivelRisco CalcularRisco(Leitura leitura);

        public abstract string DescreverLeitura(Leitura leitura);
    }

}
