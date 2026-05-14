using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PainelAnaliseDisney.Commands
{
    /// <summary>
    /// Implementação de ICommand que suporta ações síncronas e assíncronas.
    /// Usado para vincular botões da View a métodos do ViewModel.
    /// </summary>
    public class RelayCommand : ICommand
    {
        // Campo para ação assíncrona (ex: buscar dados na API)
        private readonly Func<Task>? _executeAsync;

        // Campo para ação síncrona (ex: limpar lista)
        private readonly Action? _execute;

        // Função opcional que determina se o comando pode ser executado
        private readonly Func<bool>? _canExecute;

        // ── Construtor ASSÍNCRONO ──────────────────────────────────────────
        /// <summary>
        /// Use este construtor para métodos que fazem await (Firebase, HTTP).
        /// Exemplo: new RelayCommand(async () => await BuscarAsync())
        /// </summary>
        public RelayCommand(Func<Task> executeAsync, Func<bool>? canExecute = null)
        {
            _executeAsync = executeAsync;
            _canExecute = canExecute;
        }

        // ── Construtor SÍNCRONO ───────────────────────────────────────────
        /// <summary>
        /// Use este construtor para métodos simples sem await.
        /// Exemplo: new RelayCommand(() => Limpar())
        /// </summary>
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// O WPF chama este método para saber se o botão deve estar habilitado.
        /// Se _canExecute for null, o botão sempre fica habilitado.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }

        /// <summary>
        /// Chamado quando o botão é clicado. Executa async ou sync conforme o construtor usado.
        /// </summary>
        public async void Execute(object? parameter)
        {
            if (_executeAsync != null)
                await _executeAsync();
            else
                _execute?.Invoke();
        }

        /// <summary>
        /// Delega ao CommandManager do WPF para que os botões
        /// atualizem seu estado (habilitado/desabilitado) automaticamente.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

