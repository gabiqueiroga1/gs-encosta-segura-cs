using EncostaSegura.Data;
using EncostaSegura.Models;
using EncostaSegura.Models.Enumeracoes;
using EncostaSegura.Services;

namespace EncostaSegura 
{
    public partial class Form1 : Form
    {
        private readonly EstacaoService _estacaoService = new();
        private readonly AlertaService _alertaService = new();
        private readonly PontoDeApoioService _pontoService = new();
        private readonly ConfiguracaoRepository _repository = new();

        private readonly DataGridView _gridEstacoes = CriarGrade();
        private readonly DataGridView _gridAlertas = CriarGrade();
        private readonly DataGridView _gridPontos = CriarGrade();

        private readonly TextBox _txtMorador = new();
        private readonly TextBox _txtNomePonto = new();
        private readonly TextBox _txtEndereco = new();
        private readonly TextBox _txtRota = new();

        public Form1()
        {
            Text = "Encosta Segura - Monitoramento de Riscos";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1100, 700);
            MinimumSize = new Size(900, 600);

            _repository.Carregar(_pontoService);

            ConstruirInterface();

            // Leituras iniciais demonstrativas.
            CriarLeiturasIniciais();

            AtualizarEstacoes();
            AtualizarAlertas();
            AtualizarPontos();
        }

        private void ConstruirInterface()
        {
            TabControl abas = new()
            {
                Dock = DockStyle.Fill
            };

            TabPage abaEstacoes = new("Estações");
            TabPage abaAlertas = new("Alertas");
            TabPage abaPontos = new("Pontos de apoio");

            abas.TabPages.Add(abaEstacoes);
            abas.TabPages.Add(abaAlertas);
            abas.TabPages.Add(abaPontos);

            ConstruirAbaEstacoes(abaEstacoes);
            ConstruirAbaAlertas(abaAlertas);
            ConstruirAbaPontos(abaPontos);

            Controls.Add(abas);
        }

        private void ConstruirAbaEstacoes(TabPage aba)
        {
            Panel painel = CriarPainel();

            Label titulo = CriarTitulo(
                "Monitoramento das estações");

            Label descricao = new()
            {
                Text = "Acompanhe as leituras e os níveis de risco " +
                       "das estações ENC-01 e NIV-01.",
                AutoSize = true,
                Location = new Point(20, 65)
            };

            _gridEstacoes.Location = new Point(20, 105);
            _gridEstacoes.Size = new Size(1020, 390);

            Button simular = CriarBotao(
                "Simular cenário",
                20,
                520,
                170);

            simular.Click += (_, _) => AbrirSimulacao();

            Button historico = CriarBotao(
                "Ver histórico",
                205,
                520,
                170);

            historico.Click += (_, _) => AbrirHistorico();

            painel.Controls.Add(titulo);
            painel.Controls.Add(descricao);
            painel.Controls.Add(_gridEstacoes);
            painel.Controls.Add(simular);
            painel.Controls.Add(historico);

            aba.Controls.Add(painel);
        }

        private void ConstruirAbaAlertas(TabPage aba)
        {
            Panel painel = CriarPainel();

            Label titulo = CriarTitulo("Alertas ativos");

            Label descricao = new()
            {
                Text = "Os alertas são atualizados de acordo com " +
                       "o risco identificado nas estações.",
                AutoSize = true,
                Location = new Point(20, 65)
            };

            _gridAlertas.Location = new Point(20, 105);
            _gridAlertas.Size = new Size(1020, 330);

            Label labelMorador = new()
            {
                Text = "Identificador do morador:",
                AutoSize = true,
                Location = new Point(20, 465)
            };

            _txtMorador.Location = new Point(210, 460);
            _txtMorador.Size = new Size(220, 30);

            Button confirmar = CriarBotao(
                "Estou seguro",
                450,
                455,
                160);

            confirmar.Click += (_, _) => RegistrarConfirmacao();

            Button atualizar = CriarBotao(
                "Atualizar alertas",
                620,
                455,
                180);

            atualizar.Click += (_, _) =>
            {
                SincronizarAlertas();
                AtualizarAlertas();
            };

            painel.Controls.Add(titulo);
            painel.Controls.Add(descricao);
            painel.Controls.Add(_gridAlertas);
            painel.Controls.Add(labelMorador);
            painel.Controls.Add(_txtMorador);
            painel.Controls.Add(confirmar);
            painel.Controls.Add(atualizar);

            aba.Controls.Add(painel);
        }

        private void ConstruirAbaPontos(TabPage aba)
        {
            Panel painel = CriarPainel();

            Label titulo = CriarTitulo("Pontos de apoio");

            Label descricao = new()
            {
                Text = "Locais de referência para acolhimento " +
                       "e rotas seguras.",
                AutoSize = true,
                Location = new Point(20, 65)
            };

            _gridPontos.Location = new Point(20, 105);
            _gridPontos.Size = new Size(1020, 280);

            Label nome = CriarLabel("Nome:", 20, 410);
            _txtNomePonto.Location = new Point(100, 405);
            _txtNomePonto.Width = 250;

            Label endereco = CriarLabel("Endereço:", 370, 410);
            _txtEndereco.Location = new Point(460, 405);
            _txtEndereco.Width = 300;

            Label rota = CriarLabel("Rota:", 20, 455);
            _txtRota.Location = new Point(100, 450);
            _txtRota.Width = 250;

            Button adicionar = CriarBotao(
                "Adicionar ponto",
                370,
                445,
                170);

            adicionar.Click += (_, _) => AdicionarPonto();

            Button remover = CriarBotao(
                "Remover selecionado",
                550,
                445,
                200);

            remover.Click += (_, _) => RemoverPonto();

            painel.Controls.Add(titulo);
            painel.Controls.Add(descricao);
            painel.Controls.Add(_gridPontos);
            painel.Controls.Add(nome);
            painel.Controls.Add(_txtNomePonto);
            painel.Controls.Add(endereco);
            painel.Controls.Add(_txtEndereco);
            painel.Controls.Add(rota);
            painel.Controls.Add(_txtRota);
            painel.Controls.Add(adicionar);
            painel.Controls.Add(remover);

            aba.Controls.Add(painel);
        }

        private void CriarLeiturasIniciais()
        {
            // ENC-01: estação de monitoramento de deslizamentos.
            _estacaoService.AdicionarLeitura(
                "ENC-01",
                new Leitura(
                    96, 25, 85, 10.2,
                    0, 0));

            // NIV-01: estação de monitoramento de alagamentos.
            _estacaoService.AdicionarLeitura(
                "NIV-01",
                new Leitura(
                    0, 0, 0, 0,
                    77, 42));

            SincronizarAlertas();
        }

        private void AbrirSimulacao()
        {
            using Form dialogo = new()
            {
                Text = "Simular cenário",
                Size = new Size(430, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label labelEstacao = CriarLabel(
                "Selecione a estação:",
                20,
                25);

            ComboBox comboEstacao = new()
            {
                Location = new Point(20, 55),
                Width = 360,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            comboEstacao.Items.Add("ENC-01 - Deslizamento");
            comboEstacao.Items.Add("NIV-01 - Alagamento");
            comboEstacao.SelectedIndex = 0;

            Label labelRisco = CriarLabel(
                "Selecione o nível de risco:",
                20,
                105);

            ComboBox comboRisco = new()
            {
                Location = new Point(20, 135),
                Width = 360,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            comboRisco.Items.Add("BAIXO");
            comboRisco.Items.Add("ATENÇÃO");
            comboRisco.Items.Add("ALTO");
            comboRisco.SelectedIndex = 0;

            Button executar = CriarBotao(
                "Aplicar simulação",
                20,
                190,
                180);

            executar.Click += (_, _) =>
            {
                try
                {
                    AplicarSimulacao(
                        comboEstacao.SelectedIndex,
                        comboRisco.SelectedIndex);

                    dialogo.DialogResult = DialogResult.OK;
                    dialogo.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Erro na simulação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            };

            dialogo.Controls.Add(labelEstacao);
            dialogo.Controls.Add(comboEstacao);
            dialogo.Controls.Add(labelRisco);
            dialogo.Controls.Add(comboRisco);
            dialogo.Controls.Add(executar);

            dialogo.ShowDialog(this);

            AtualizarEstacoes();
            AtualizarAlertas();
        }

        private void AplicarSimulacao(
            int indiceEstacao,
            int indiceRisco)
        {
            string codigo = indiceEstacao switch
            {
                0 => "ENC-01",
                1 => "NIV-01",
                _ => throw new ArgumentOutOfRangeException(
                    nameof(indiceEstacao))
            };

            Leitura leitura;

            if (codigo == "ENC-01")
            {
                leitura = indiceRisco switch
                {
                    0 => new Leitura(35, 2, 10, 1.5, 0, 0),
                    1 => new Leitura(70, 10, 45, 6, 0, 0),
                    2 => new Leitura(96, 25, 85, 10.2, 0, 0),
                    _ => throw new ArgumentOutOfRangeException(
                        nameof(indiceRisco))
                };
            }
            else
            {
                leitura = indiceRisco switch
                {
                    0 => new Leitura(0, 0, 0, 0, 20, 5),
                    1 => new Leitura(0, 0, 0, 0, 50, 20),
                    2 => new Leitura(0, 0, 0, 0, 77, 42),
                    _ => throw new ArgumentOutOfRangeException(
                        nameof(indiceRisco))
                };
            }

            _estacaoService.AdicionarLeitura(codigo, leitura);

            Estacao? estacao = _estacaoService.BuscarPorCodigo(codigo);

            if (estacao is not null)
                _alertaService.SincronizarComEstacao(estacao);
        }

        private void SincronizarAlertas()
        {
            foreach (Estacao estacao in _estacaoService.ObterTodas())
            {
                _alertaService.SincronizarComEstacao(estacao);
            }
        }

        private void AtualizarEstacoes()
        {
            _gridEstacoes.Columns.Clear();
            _gridEstacoes.Rows.Clear();

            _gridEstacoes.Columns.Add("Codigo", "Código");
            _gridEstacoes.Columns.Add("Nome", "Estação");
            _gridEstacoes.Columns.Add("Localizacao", "Localização");
            _gridEstacoes.Columns.Add("Risco", "Nível de risco");
            _gridEstacoes.Columns.Add("Leitura", "Última leitura");

            foreach (Estacao estacao in _estacaoService.ObterTodas())
            {
                string leitura = estacao.UltimaLeitura is null
                    ? "Sem leituras"
                    : estacao.DescreverLeitura(estacao.UltimaLeitura);

                int indice = _gridEstacoes.Rows.Add(
                    estacao.Codigo,
                    estacao.Nome,
                    estacao.Localizacao,
                    FormatarRisco(estacao.RiscoAtual),
                    leitura);

                ColorirLinha(
                    _gridEstacoes.Rows[indice],
                    estacao.RiscoAtual);
            }

            AjustarColunas(_gridEstacoes);
        }

        private void AtualizarAlertas()
        {
            _gridAlertas.Columns.Clear();
            _gridAlertas.Rows.Clear();

            _gridAlertas.Columns.Add("Estacao", "Estação");
            _gridAlertas.Columns.Add("Tipo", "Tipo");
            _gridAlertas.Columns.Add("Nivel", "Nível");
            _gridAlertas.Columns.Add("Mensagem", "Leitura");
            _gridAlertas.Columns.Add("Notificados", "Notificados");
            _gridAlertas.Columns.Add("Confirmacoes", "Confirmações");
            _gridAlertas.Columns.Add("Criacao", "Criado em");

            foreach (Alerta alerta in _alertaService.ObterAtivos())
            {
                int indice = _gridAlertas.Rows.Add(
                    alerta.CodigoEstacao,
                    alerta.Tipo,
                    FormatarRisco(alerta.Nivel),
                    alerta.Mensagem,
                    alerta.MoradoresNotificados,
                    alerta.TotalConfirmacoes,
                    alerta.DataCriacao.ToString("dd/MM/yyyy HH:mm"));

                ColorirLinha(
                    _gridAlertas.Rows[indice],
                    alerta.Nivel);
            }

            AjustarColunas(_gridAlertas);
        }

        private void RegistrarConfirmacao()
        {
            if (_gridAlertas.CurrentRow is null)
            {
                MessageBox.Show(
                    "Selecione um alerta primeiro.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string codigo = _gridAlertas.CurrentRow
                .Cells["Estacao"].Value?.ToString() ?? "";

            Alerta? alerta = _alertaService.ObterAtivos()
                .FirstOrDefault(
                    item => item.CodigoEstacao == codigo);

            if (alerta is null)
                return;

            bool registrado = alerta.RegistrarEstouSeguro(
                _txtMorador.Text);

            if (!registrado)
            {
                MessageBox.Show(
                    "Informe um identificador válido e confira se " +
                    "o morador ainda não confirmou o recebimento.",
                    "Confirmação não registrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            _txtMorador.Clear();
            AtualizarAlertas();

            MessageBox.Show(
                "Confirmação registrada com sucesso.",
                "Encosta Segura",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void AbrirHistorico()
        {
            if (_gridEstacoes.CurrentRow is null)
            {
                MessageBox.Show("Selecione uma estação.");
                return;
            }

            string codigo = _gridEstacoes.CurrentRow
                .Cells["Codigo"].Value?.ToString() ?? "";

            Estacao? estacao =
                _estacaoService.BuscarPorCodigo(codigo);

            if (estacao is null)
                return;

            using Form janela = new()
            {
                Text = $"Histórico - {estacao.Codigo}",
                Size = new Size(850, 450),
                StartPosition = FormStartPosition.CenterParent
            };

            DataGridView grade = CriarGrade();
            grade.Dock = DockStyle.Fill;

            grade.Columns.Add("Data", "Data e hora");
            grade.Columns.Add("Risco", "Nível de risco");
            grade.Columns.Add("Leitura", "Dados da leitura");

            foreach (Leitura leitura in estacao.Historico)
            {
                NivelRisco risco = estacao.CalcularRisco(leitura);

                int indice = grade.Rows.Add(
                    leitura.DataHora.ToString("dd/MM/yyyy HH:mm:ss"),
                    FormatarRisco(risco),
                    estacao.DescreverLeitura(leitura));

                ColorirLinha(grade.Rows[indice], risco);
            }

            AjustarColunas(grade);

            janela.Controls.Add(grade);
            janela.ShowDialog(this);
        }

        private void AtualizarPontos()
        {
            _gridPontos.Columns.Clear();
            _gridPontos.Rows.Clear();

            _gridPontos.Columns.Add("Nome", "Nome");
            _gridPontos.Columns.Add("Endereco", "Endereço");
            _gridPontos.Columns.Add("Rota", "Rota segura");
            _gridPontos.Columns.Add("Ativo", "Ativo");

            foreach (PontoDeApoio ponto in _pontoService.ObterTodos())
            {
                _gridPontos.Rows.Add(
                    ponto.Nome,
                    ponto.Endereco,
                    ponto.RotaSegura,
                    ponto.Ativo ? "Sim" : "Não");
            }

            AjustarColunas(_gridPontos);
        }

        private void AdicionarPonto()
        {
            try
            {
                PontoDeApoio ponto = new(
                    _txtNomePonto.Text,
                    _txtEndereco.Text,
                    _txtRota.Text);

                _pontoService.Adicionar(ponto);
                _repository.Salvar(_pontoService);

                _txtNomePonto.Clear();
                _txtEndereco.Clear();
                _txtRota.Clear();

                AtualizarPontos();

                MessageBox.Show(
                    "Ponto de apoio cadastrado com sucesso.",
                    "Encosta Segura",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Dados inválidos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "O ponto foi cadastrado, mas não foi possível " +
                    "salvar as configurações no computador.",
                    "Erro ao salvar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RemoverPonto()
        {
            if (_gridPontos.CurrentRow is null)
            {
                MessageBox.Show("Selecione um ponto de apoio.");
                return;
            }

            int indice = _gridPontos.CurrentRow.Index;

            if (indice < 0 ||
                indice >= _pontoService.ObterTodos().Count)
            {
                return;
            }

            PontoDeApoio ponto =
                _pontoService.ObterTodos()[indice];

            DialogResult resposta = MessageBox.Show(
                $"Deseja remover o ponto {ponto.Nome}?",
                "Confirmar remoção",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resposta != DialogResult.Yes)
                return;

            _pontoService.Remover(ponto);

            try
            {
                _repository.Salvar(_pontoService);
                AtualizarPontos();
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "O ponto foi removido da sessão, mas não foi " +
                    "possível salvar a alteração no computador.",
                    "Erro ao salvar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static string FormatarRisco(NivelRisco risco)
        {
            return risco switch
            {
                NivelRisco.Baixo => "BAIXO",
                NivelRisco.Médio => "ATENÇÃO",
                NivelRisco.Alto => "ALTO",
                _ => "DESCONHECIDO"
            };
        }

        private static void ColorirLinha(
            DataGridViewRow linha,
            NivelRisco risco)
        {
            switch (risco)
            {
                case NivelRisco.Baixo:
                    linha.DefaultCellStyle.BackColor =
                        Color.LightGreen;
                    break;

                case NivelRisco.Médio:
                    linha.DefaultCellStyle.BackColor =
                        Color.LightYellow;
                    break;

                case NivelRisco.Alto:
                    linha.DefaultCellStyle.BackColor =
                        Color.LightCoral;
                    break;
            }
        }

        private static DataGridView CriarGrade()
        {
            return new DataGridView
            {
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D
            };
        }

        private static void AjustarColunas(DataGridView grade)
        {
            grade.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCells);
        }

        private static Panel CriarPainel()
        {
            return new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };
        }

        private static Label CriarTitulo(string texto)
        {
            return new Label
            {
                Text = texto,
                AutoSize = true,
                Font = new Font(
                    "Segoe UI",
                    18,
                    FontStyle.Bold),
                Location = new Point(20, 20)
            };
        }

        private static Label CriarLabel(
            string texto,
            int x,
            int y)
        {
            return new Label
            {
                Text = texto,
                AutoSize = true,
                Location = new Point(x, y)
            };
        }

        private static Button CriarBotao(
            string texto,
            int x,
            int y,
            int largura)
        {
            return new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(largura, 35),
                BackColor = Color.FromArgb(35, 95, 75),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
        }
    }

}

