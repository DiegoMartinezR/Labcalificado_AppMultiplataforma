using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace LabCalificado
{
    public enum Operacion
    {
        Suma,
        Resta,
        Multiplicacion,
        Division
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Operacion? _operacion;
        private int _numero1;
        private int _numero2;

        private int? _visor;

        public int? Visor
        {
            get { return _visor; }
            set
            {
                _visor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visor)));
            }
        }

        public ICommand RealizarOperacionCommand { get; }
        public ICommand InsertarNumeroCommand { get; }
        public ICommand LimpiarTodoCommand { get; }
        public ICommand InsertarOperacionCommand { get; }

        public ViewModel()
        {
            _numero1 = 0;
            _numero2 = 0;

            InsereNumeroCommand = new Command<string>(InsereNumero);
            LimpaTudoCommand = new Command(LimpaTudo);
            InsereOperacaoCommand = new Command<string>(InsereOperacao);
            RealizaOperacaoCommand = new Command(RealizaOperacao);
        }

        private void InsertarOperacion(string numeroInserido)
        {
            if (_operacao == null)
            {
                _numero1 = _numero1 * 10 + Convert.ToInt32(numeroInserido);
                Visor = _numero1;
            }
            else
            {
                _numero2 = _numero2 * 10 + Convert.ToInt32(numeroInserido);
                Visor = _numero2;
            }
        }

        private void Limpiar()
        {
            _numero1 = 0;
            _numero2 = 0;
            _operacao = null;
            Visor = null;
        }

        private void InsertarOperacion(string operacao)
        {
            if (operacion == "+")
                _operacao = Operacao.Suma;
            if (operacion == "-")
                _operacao = Operacao.Resta;
            if (operacion == "*")
                _operacao = Operacao.Multiplicacion;
            if (operacion == "/")
                _operacao = Operacao.Division;
        }

        private void RealizarOperacion()
        {
            switch (_operacao)
            {
                case Operacao.Suma:
                    _numero1 = _numero1 + _numero2;
                    break;
                case Operacao.Resta:
                    _numero1 = _numero1 - _numero2;
                    break;
                case Operacao.Multiplicacion:
                    _numero1 = _numero1 * _numero2;
                    break;
                case Operacao.Division:
                    try
                    {
                        _numero1 = _numero1 / _numero2;
                    }
                    catch
                    {
                        _numero1 = 0;
                    }
                    break;
                case null:
                    return;
            }

            Visor = _numero1;
            _numero2 = 0;
            _operacao = null;
        }
    }
}
