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
                    foreach (var filme in resultado)
                    {
                        Dados.Add(filme);
                    }
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
        }
    }
}