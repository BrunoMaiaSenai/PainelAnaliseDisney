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

            var resultado = await _repository.ObterEGravarFilmesAsync();

            foreach (var filme in resultado)
            {
                Dados.Add(filme);
            }

            IsCarregando = false;
        }

        private void ExecutarLimpeza()
        {
            Dados.Clear();
        }
    }
}