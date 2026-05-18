using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PainelAnaliseDisney.Commands;
using PainelAnaliseDisney.Models;
using PainelAnaliseDisney.Respositories;

namespace PainelAnaliseDisney.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly FilmeRepository _repository;
        private ObservableCollection<Filme> _dados;
        private bool _isCarregando;

        private int _totalPersonagens;
        public int TotalPersonagens
        {
            get => _totalPersonagens;
            set { _totalPersonagens = value; OnPropertyChanged(nameof(TotalPersonagens)); }
        }

        private int _personagensNaTv;
        public int PersonagensNaTv
        {
            get => _personagensNaTv;
            set { _personagensNaTv = value; OnPropertyChanged(nameof(PersonagensNaTv)); }
        }

        private string _personagemDestaque = "Nenhum";
        public string PersonagemDestaque
        {
            get => _personagemDestaque;
            set { _personagemDestaque = value; OnPropertyChanged(nameof(PersonagemDestaque)); }
        }

        public ObservableCollection<Filme> Dados
        {
            get => _dados;
            set
            {
                _dados = value;
                OnPropertyChanged(nameof(Dados));
            }
        }

        public bool IsCarregando
        {
            get => _isCarregando;
            set
            {
                _isCarregando = value;
                OnPropertyChanged(nameof(IsCarregando));
            }
        }

        public ICommand BuscarCommand { get; }
        public ICommand LimparCommand { get; }

        public MainViewModel()
        {
            _repository = new FilmeRepository();
            _dados = new ObservableCollection<Filme>();

            // Vinculando comandos com a classe RelayCommand fornecida
            BuscarCommand = new RelayCommand(async () => await ExecutarBuscaAsync());
            LimparCommand = new RelayCommand(() => ExecutarLimpeza());
        }

        private async Task ExecutarBuscaAsync()
        {
            if (IsCarregando) return;

            IsCarregando = true;
            Dados.Clear();

            try
            {
                var resultado = await _repository.ObterEGravarFilmesAsync();

                if (resultado == null)
                {
                    System.Windows.MessageBox.Show(
                        "O repositório retornou um valor NULO (Null). Verifique se o método ObterEGravarFilmesAsync está repassando os dados corretamente do serviço.",
                        "Diagnóstico Técnico",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Warning);
                }
                else if (resultado.Count == 0)
                {
                    System.Windows.MessageBox.Show(
                        "Conexão com sucesso, mas o servidor retornou uma lista com ZERO registros. O banco de dados pode estar limpo no momento.",
                        "Diagnóstico Técnico",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    // Criamos variáveis simples para acumular as estatísticas durante a leitura
                    int contadorTv = 0;
                    string nomeMaisFilmes = "Nenhum";
                    int maiorQuantidadeFilmes = -1;

                    // Percorremos a lista de resultados vinda da API
                    foreach (var filme in resultado)
                    {
                        // Adiciona o elemento na lista que aparece na interface (DataGrid/ListView)
                        Dados.Add(filme);

                        // Cenário de Insight 1: Conta se o personagem possui programas de TV cadastrados
                        if (filme.TvShows != null && filme.TvShows.Count > 0)
                        {
                            contadorTv++;
                        }

                        // Cenário de Insight 2: Descobre qual personagem tem a maior lista de filmes gravados
                        if (filme.Films != null && filme.Films.Count > maiorQuantidadeFilmes)
                        {
                            maiorQuantidadeFilmes = filme.Films.Count;
                            nomeMaisFilmes = filme.Nome;
                        }
                    }

                    // Após ler todos, alimentamos as propriedades da ViewModel que estão ligadas ao XAML
                    TotalPersonagens = resultado.Count;
                    PersonagensNaTv = contadorTv;
                    PersonagemDestaque = $"{nomeMaisFilmes} ({maiorQuantidadeFilmes} filme(s))";
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Ocorreu um erro inesperado na ViewModel: {ex.Message}",
                    "Erro Crítico",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }

            IsCarregando = false;
        }

        private void ExecutarLimpeza()
        {
            Dados.Clear();

            // Opcional: Zera também os contadores visuais ao limpar a tela
            TotalPersonagens = 0;
            PersonagensNaTv = 0;
            PersonagemDestaque = "Nenhum";
        }
    }
}