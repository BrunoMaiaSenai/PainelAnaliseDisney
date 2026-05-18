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

```
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
```

## 🛠️ Desafios Enfrentados & Soluções Práticas

Desenvolver esta integração trouxe alguns problemas reais de arquitetura e lógica de programação que exigiram investigação detalhada para serem resolvidos:

### 1. Incompatibilidade de Nomenclatura no JSON
* **Problema:** O formato dos dados fornecidos pela API utilizava padrão de escrita e nomes em inglês (`name`, `imageUrl`, `tvShows`), enquanto os atributos das minhas classes e componentes visuais estavam modelados em português. Isso fez com que os dados chegassem, mas os campos ficassem em branco na tela.
* **Solução:** Ajustei a serialização de dados mapeando explicitamente o nome de cada propriedade recebida com o atributo correspondente no C#, corrigindo a conversão do JSON e garantindo a exibição das fotos e descrições dos personagens.

### 2. Interface de Usuário Congelada
* **Problema:** Nas primeiras tentativas, ao efetuar a busca, a janela do Windows simplesmente travava por alguns segundos e o mouse exibia o ícone de carregamento do sistema. Isso acontecia porque a chamada da API e a gravação individual no Firebase rodavam na mesma linha de execução da interface gráfica (UI Thread).
* **Solução:** Refatorei o fluxo de chamadas nos repositórios utilizando programação assíncrona (`async`/`await`). Dessa forma, a busca passou a rodar em segundo plano e consegui implementar uma propriedade booleana ligada a um aviso visual na tela, notificando o progresso ao usuário sem interromper a fluidez do sistema.

### 3. Falha de Comunicação por Erro de Vínculo (*Binding*)
* **Problema:** Ao implementar o novo contêiner visual de estatísticas no XAML, os botões principais de controle pararam de responder aos cliques. Ao depurar o Output do Visual Studio, notei erros do tipo `BindingExpression path error: 'BuscarDadosCommand' property not found`.
* **Solução:** Identifiquei que o botão na camada de visão procurava por uma propriedade de comando com nomenclatura incorreta na camada de controle (ViewModel). Corrigi a tag do XAML para apontar exatamente para o nome do comando exposto no código C#, reestabelecendo a comunicação da tela.

### 4. Validação das Gravações na Nuvem
* **Problema:** No início, não havia certeza se as chaves e valores enviados estavam estruturando o banco corretamente ou se as requisições estavam sendo rejeitadas de forma silenciosa.
* **Solução:** Criei blocos de tratamento de exceção (`try-catch`) nos métodos de gravação para capturar possíveis erros de rede e passei a monitorar o comportamento e as árvores de nós JSON em tempo real diretamente pelo console de gerenciamento web do Firebase.

---

## 🔧 Como Executar a Aplicação

1. Baixe ou clone o repositório em seu ambiente:
   [https://github.com/BrunoMaiaSenai/PainelAnaliseDisney.git](https://github.com/BrunoMaiaSenai/PainelAnaliseDisney.git)
