# 📊 Painel de Análise Disney & Auditoria Firebase

Este projeto foi desenvolvido como parte prática do meu aprendizado em Desenvolvimento de Sistemas. A proposta principal foi construir um ecossistema completo que consome dados de uma API, aplica regras de negócio para extrair estatísticas de mídias e armazena um histórico de consultas (auditoria) em um banco de dados em nuvem.

O projeto utiliza a tecnologia **WPF (Windows Presentation Foundation)** com **.NET 8** e adota o padrão de arquitetura **MVVM**, garantindo a separação de responsabilidades e uma interface de usuário responsiva.

---

## 🚀 Funcionalidades da Aplicação

- **Consumo de API Externa:** Conecta-se de forma assíncrona ao serviço web da Disney para listar personagens e suas mídias.
- **Processamento de Insights em Tempo Real:** Varre os registros recebidos no momento da busca para calcular e exibir um painel estatístico com:
  - O total de personagens carregados na listagem.
  - O volume de personagens que possuem forte presença em programas de TV.
  - O destaque do painel (identificação do personagem com maior número de filmes gravados).
- **Módulo de Auditoria (Firebase):** Cada personagem consultado dispara uma rotina de persistência para salvar o estado e o horário da busca no banco de dados NoSQL do Firebase.
- **Interface Fluida:** Controle de concorrência que impede o travamento da tela enquanto as requisições de rede e banco estão acontecendo.

---

## 📁 Estrutura do Projeto

Abaixo está o mapeamento dos diretórios e arquivos que organizam a lógica da aplicação:

```text
PainelAnaliseDisney/
│
├── 📁 Models/
│   ├── 📄 Filme.cs            # Modelo que reflete a estrutura de dados original da API
│   └── 📄 LogFilme.cs          # Modelo estruturado para o formato de gravação dos logs no Firebase
│
├── 📁 ViewModels/
│   ├── 📄 BaseViewModel.cs     # Implementação do mecanismo de notificação da interface (INotifyPropertyChanged)
│   └── 📄 MainViewModel.cs     # Concentra o estado da tela, controle de carregamento, filtros e lógica dos insights
│
├── 📁 Views/
│   ├── 📄 MainWindow.xaml      # Desenho da interface gráfica (estilização dos cartões, botões e sumário estatístico)
│   └── 📄 MainWindow.xaml.cs   # Inicializador da janela e acoplamento do DataContext
│
├── 📁 Repositories/
│   └── 📄 FilmeRepository.cs   # Orquestrador que une a busca de dados ao fluxo de persistência
│
├── 📁 Services/
│   └── 📄 FirebaseService.cs   # Cliente HTTP responsável pelas chamadas e envio de payloads para a nuvem
│
├── 📄 App.xaml                 # Declaração de recursos globais do WPF
├── 📄 App.xaml.cs              # Inicialização customizada do ciclo de vida do app
└── 📄 PainelAnaliseDisney.csproj # Gerenciador de pacotes e versão do framework (.NET 8)
