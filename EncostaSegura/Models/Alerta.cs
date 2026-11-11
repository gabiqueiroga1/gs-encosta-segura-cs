using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncostaSegura.Models.Enumeracoes;

namespace EncostaSegura.Models 
{

    public sealed class Alerta
    {
        private readonly HashSet<string> _confirmacoes = new();

        public Guid Id { get; private set; }
        public string CodigoEstacao { get; private set; }
        public TipoAlerta Tipo { get; private set; }
        public NivelRisco Nivel { get; private set; }
        public string Mensagem { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public int MoradoresNotificados { get; private set; }
        public int TotalConfirmacoes => _confirmacoes.Count;
        public bool Encerrado { get; private set; }

        public Alerta(
            string codigoEstacao,
            TipoAlerta tipo,
            NivelRisco nivel,
            string mensagem,
            int moradoresNotificados)
        {
            if (string.IsNullOrWhiteSpace(codigoEstacao))
                throw new ArgumentException("Código da estação inválido.");

            if (string.IsNullOrWhiteSpace(mensagem))
                throw new ArgumentException("Mensagem inválida.");

            if (moradoresNotificados < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(moradoresNotificados));

            Id = Guid.NewGuid();
            CodigoEstacao = codigoEstacao;
            Tipo = tipo;
            Nivel = nivel;
            Mensagem = mensagem;
            MoradoresNotificados = moradoresNotificados;
            DataCriacao = DateTime.Now;
        }

        public bool RegistrarEstouSeguro(string identificadorMorador)
        {
            if (Encerrado)
                return false;

            if (string.IsNullOrWhiteSpace(identificadorMorador))
                return false;

            return _confirmacoes.Add(identificadorMorador.Trim());
        }

        public void Atualizar(
            NivelRisco novoNivel,
            string novaMensagem)
        {
            if (Encerrado)
                return;

            Nivel = novoNivel;
            Mensagem = novaMensagem;
        }

        public void Encerrar()
        {
            Encerrado = true;
        }
    }
}

