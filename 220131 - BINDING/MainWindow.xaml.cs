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
using System.Text.RegularExpressions; 

namespace _220131___BINDING
{ // ZÁPIS DO SOUBORU!!!!!!!
    public partial class MainWindow : Window
    {
        Zamestnanec z;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = z = new Zamestnanec() { Narozeni = DateTime.Now };
        }
        private void BtShow_Click(object sender, RoutedEventArgs e)
        {
            if (Prijmeni.Text != "" && Jmeno.Text != "" && HrubaMzda.Text != "" && NejvyssiVzdelani.SelectedItem != null && PracovniPozice.Text != "")
            {
                BindingExpression expr = Prijmeni.GetBindingExpression(TextBox.TextProperty);
                expr?.UpdateSource();

                MessageBox.Show("Uloženo:\n" + z.ToString());
            }
            else
                MessageBox.Show("Zadejte všechny údaje!!!");
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
                if (Regex.IsMatch(value, @"^([A-Z, {ŠČŘŽÁÍÉĎŤŇÓ}]\S+ *)+$") && value != null)
                    _Jmeno = value;
                else
                    MessageBox.Show("Neplatný vstup pro jméno!!!");
                OnPropertyChanged("Jmeno");
                OnPropertyChanged("Status");
            }
        }
        public string Prijmeni
        {
            get => _Prijmeni;
            set
            {

                if (Regex.IsMatch(value, @"^([A-Z, {ŠČŘŽÁÍÉĎŤŇÓ}]\S+ *)+$") && value != null)
                    _Prijmeni = value;
                else
                    MessageBox.Show("Neplatný vstup pro jméno!!!");
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
            return $"Jméno: {Jmeno}\nPříjmení: {Prijmeni}\nDatum Narození: {Narozeni.ToShortDateString()}\n";
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
        private string _HrubaMzda;
        private string _PracovniPozice, _Vzdelani;
        public string PracovniPozice
        {
            get => _PracovniPozice;
            set
            {
                _PracovniPozice = value.ToString();
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
        public string HrubaMzda
        {
            get => _HrubaMzda;
            set
            {
                if (Regex.IsMatch(value, @"^(\d *)+$"))
                    _HrubaMzda = value;
                else
                    MessageBox.Show("Neplatný vstup pro hrubou mzdu!!!");
                OnPropertyChanged("HrubaMzda");
                OnPropertyChanged("Status");
            }
        }
        public override string ToString()
        {
            return base.ToString() + $"Nejvyšší vzdělání: {Vzdelani}\nHrubá mzda: {HrubaMzda}\nPracovní pozice: {PracovniPozice}";
        }
    }

}
