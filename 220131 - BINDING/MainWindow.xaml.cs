using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace _220131___BINDING
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }


    public interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
    class Person : INotifyPropertyChanged
    {
        private string _Jmeno, _Prijmeni;
        private DateTime _Narozeni;

        public event PropertyChangedEventHandler PropertyChanged;

        // Při každé změně dat chceme akutalizovat status bar
        public string Jmeno
        {
            get => _Jmeno;
            set
            {
                _Jmeno = value;
                OnPropertyChanged("Jmeno");
                OnPropertyChanged("Status");
            }
        }
        public string Prijmeni
        {
            get => _Prijmeni;
            set
            {
                _Prijmeni = value;
                OnPropertyChanged("Prijmeni");
                OnPropertyChanged("Status");
            }
        }
        public DateTime Narozeni
        {
            get => _Narozeni;
            set
            {
                _Narozeni = value;
                OnPropertyChanged("Narozeni");
                OnPropertyChanged("Status");
            }
        }

        public string Status
        {
            get => Jmeno + " " + Prijmeni + " " + Narozeni.ToString();
        }

        public override string ToString()
        {
            return Jmeno + " " + Prijmeni + " " + Narozeni.ToShortDateString();
        }
        // pomocná metoda pro informaci o změně v datech
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) // jestli někdo poslouchá ...
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
    class Zamestnanec :Person, INotifyPropertyChanged
    {
        private int _HrubaMzda;
        private string _PracovniPozice, _Vzdelani;
        public string PracovniPozice
        {
            get => _PracovniPozice;
            set
            {
                _PracovniPozice = value;
                OnPropertyChanged("PracovniPozice");
                OnPropertyChanged("Status");
            }
        }
        public string Vzdelani
        {
            get => _Vzdelani;
            set
            {
                _Vzdelani = value;
                OnPropertyChanged("Vzdelani");
                OnPropertyChanged("Status");
            }
        }
        public int HrubaMzda
        {
            get => _HrubaMzda;
            set
            {
                _HrubaMzda = value;
                OnPropertyChanged("HrubaMzda");
                OnPropertyChanged("Status");
            }
        }
    }

}
