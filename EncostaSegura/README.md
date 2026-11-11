# Encosta Segura

O Encosta Segura é uma aplicação desenvolvida em C# com Windows Forms para demonstrar o monitoramento de riscos de deslizamentos e alagamentos.


## Sobre o Projeto

Sistema desenvolvido com o objetivo de auxiliar no monitoramento de áreas suscetíveis a desastres naturais, contribuindo para a prevenção de acidentes e a proteção de pessoas que vivem em regiões de risco.

O sistema representa duas estações de monitoramento:

- ENC-01: monitora as condições do solo e o risco de deslizamento.
- NIV-01: monitora o nível da água e o risco de alagamento.

A aplicação apresenta os níveis de risco, mantém um histórico de leituras, gera alertas e permite cadastrar pontos de apoio e suas rotas seguras.

## Problema escolhido e beneficiário

O problema escolhido é a vulnerabilidade de comunidades que vivem próximas a encostas e córregos, estando sujeitas a deslizamentos de terra e alagamentos.

Beneficiário real: moradores de regiões de risco, especialmente aqueles que vivem próximos a encostas e córregos e que podem ser diretamente afetados por esses desastres.

O sistema busca contribuir para a segurança dessas pessoas, oferecendo uma ferramenta de monitoramento e identificação de situações de risco.

## Tecnologias

- C#: linguagem de programação
- .NET 8: plataforma de desenvolvimento
- Windows: Forms para construção da interface gráfica.
- Visual: Studio ambiente de desenvolvimento.
- Git: ferramenta de controle de versão.
- GitHub

## Funcionalidades

- Monitoramento de duas estações.
- Classificação de risco em baixo, atenção e alto.
- Simulação de diferentes cenários.
- Histórico de leituras.
- Geração e atualização de alertas.
- Registro simulado de confirmação de segurança dos moradores.
- Cadastro e remoção de pontos de apoio.
- Salvamento local dos pontos de apoio.

## Regras demonstrativas de classificação

### ENC-01 - Deslizamento

Risco alto:
- Inclinação igual ou superior a 10 graus;
- Umidade igual ou superior a 85%;
- Chuva acumulada em 72 horas igual ou superior a 70 mm.

Risco de atenção:
- Umidade igual ou superior a 60%;
- Chuva acumulada em 72 horas igual ou superior a 30 mm;
- Inclinação igual ou superior a 5 graus.

Risco baixo:
- Valores abaixo dos limites anteriores.

### NIV-01 - Alagamento

Risco alto:
- Nível da água igual ou superior a 70 cm;
- Velocidade de subida igual ou superior a 30 cm/h.

Risco de atenção:
- Nível da água igual ou superior a 40 cm;
- Velocidade de subida igual ou superior a 15 cm/h.

Risco baixo:
- Valores abaixo dos limites anteriores.

## Como executar

1. Windows 10 ou superior;
2. Visual Studio com suporte ao desenvolvimento desktop com .NET (Windows Form);
3. .NET compatível com a versão utilizada no projeto;
4. Git.

## Passo a Passo

1. Clone o repositório: git clone URL_DO_REPOSITORIO
2. Abra a pasta do projeto;
3. Localize e abra o arquivo da solução (.sln) no Visual Studio;
4. Aguarde o carregamento do projeto e a restauração das dependências;
5. Clique em Iniciar (F5);
6. A interface do sistema Windows Forms será iniciada. 

## Organização do projeto

- Models: classes e modelos de domínio.
- Models/Enumeracoes: enumerações do sistema.
- Services: regras e operações da aplicação.
- Data: persistência local das configurações.

## Limitações

Esta versão utiliza dados simulados. Não existe integração com sensores físicos, inteligência artificial, serviços de notificação ou API externa.

As regras de risco são demonstrativas e não devem ser utilizadas como critério técnico real de evacuação ou prevenção de desastres.

O histórico de leituras e os alertas permanecem em memória durante a execução. Os pontos de apoio são armazenados localmente em JSON.

## Equipe
- Davi Correa Paião | RM: 560438
- Gabriela Queiroga | RM: 560035
- Maria Eduarda Ferrés | RM: 560418