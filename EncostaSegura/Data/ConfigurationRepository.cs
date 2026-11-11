using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EncostaSegura.Models;
using EncostaSegura.Services;

namespace EncostaSegura.Data 
{
    public sealed class ConfiguracaoRepository
    {
        private readonly string _caminhoArquivo;

        public ConfiguracaoRepository()
        {
            string pasta = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "EncostaSegura");

            Directory.CreateDirectory(pasta);

            _caminhoArquivo = Path.Combine(
                pasta,
                "pontos-de-apoio.json");
        }

        public void Salvar(PontoDeApoioService service)
        {
            List<PontoDeApoioDto> dados = new();

            foreach (PontoDeApoio ponto in service.ObterTodos())
            {
                dados.Add(new PontoDeApoioDto
                {
                    Nome = ponto.Nome,
                    Endereco = ponto.Endereco,
                    RotaSegura = ponto.RotaSegura,
                    Ativo = ponto.Ativo
                });
            }

            string json = JsonSerializer.Serialize(
                dados,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_caminhoArquivo, json);
        }

        public void Carregar(PontoDeApoioService service)
        {
            if (!File.Exists(_caminhoArquivo))
                return;

            try
            {
                string json = File.ReadAllText(_caminhoArquivo);

                List<PontoDeApoioDto>? dados =
                    JsonSerializer.Deserialize<List<PontoDeApoioDto>>(json);

                if (dados is null)
                    return;

                service.Limpar();

                foreach (PontoDeApoioDto dado in dados)
                {
                    PontoDeApoio ponto = new(
                        dado.Nome,
                        dado.Endereco,
                        dado.RotaSegura,
                        dado.Ativo);

                    service.Adicionar(ponto);
                }
            }
            catch (JsonException)
            {
                MessageBox.Show(
                    "O arquivo de configurações está inválido. " +
                    "Os pontos padrão serão utilizados.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "Não foi possível acessar o arquivo de configurações.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private sealed class PontoDeApoioDto
        {
            public string Nome { get; set; } = "";
            public string Endereco { get; set; } = "";
            public string RotaSegura { get; set; } = "";
            public bool Ativo { get; set; }
        }
    }

}


