using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncostaSegura.Models 
{
    public sealed class PontoDeApoio
    {
        public string Nome { get; private set; }
        public string Endereco { get; private set; }
        public string RotaSegura { get; private set; }
        public bool Ativo { get; private set; }

        public PontoDeApoio(
            string nome,
            string endereco,
            string rotaSegura,
            bool ativo = true)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Informe o nome do ponto.");

            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("Informe o endereço.");

            if (string.IsNullOrWhiteSpace(rotaSegura))
                throw new ArgumentException("Informe a rota segura.");

            Nome = nome.Trim();
            Endereco = endereco.Trim();
            RotaSegura = rotaSegura.Trim();
            Ativo = ativo;
        }

        public void AtualizarStatus(bool ativo)
        {
            Ativo = ativo;
        }
    }

}

