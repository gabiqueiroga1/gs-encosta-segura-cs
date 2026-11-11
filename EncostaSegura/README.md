# Encosta Segura

## Descrição

O Encosta Segura é uma aplicação desenvolvida em C# com Windows Forms
para demonstrar o monitoramento de riscos de deslizamentos e alagamentos.

O sistema representa duas estações de monitoramento:

- ENC-01: monitora as condições do solo e o risco de deslizamento.
- NIV-01: monitora o nível da água e o risco de alagamento.

A aplicação apresenta os níveis de risco, mantém um histórico de leituras,
gera alertas e permite cadastrar pontos de apoio e suas rotas seguras.

## Tecnologias

- C#
- .NET 8
- Windows Forms
- JSON para armazenamento local

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

1. Instale o Visual Studio 2022 com a carga de trabalho
   Desenvolvimento para desktop com .NET.
2. Abra a solução no Visual Studio.
3. Selecione o projeto EncostaSegura como projeto de inicialização.
4. Execute com F5 ou Ctrl + F5.

## Organização do projeto

- Models: classes e modelos de domínio.
- Models/Enumeracoes: enumerações do sistema.
- Services: regras e operações da aplicação.
- Data: persistência local das configurações.

## Limitações

Esta versão utiliza dados simulados. Não existe integração com sensores
físicos, inteligência artificial, serviços de notificação ou API externa.

As regras de risco são demonstrativas e não devem ser utilizadas como
critério técnico real de evacuação ou prevenção de desastres.

O histórico de leituras e os alertas permanecem em memória durante
a execução. Os pontos de apoio são armazenados localmente em JSON.

## Alunos
- Davi Correa Paião | RM: 560438
- Gabriela Queiroga | RM: 560035
- Maria Eduarda Ferrés | RM: 560418