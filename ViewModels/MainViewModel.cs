using WpfApp1.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PhoneBook.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<Contact> Contacts { get; }
        private string _name = "";
        public string Name { get => _name; set => Set(ref _name, value); }
        private string _phone = "";
        public string Phone { get => _phone; set => Set(ref _phone, value); }
        private Contact? _selected;
        public Contact? SelectedContact { get => _selected; set => Set(ref _selected, value); }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            Contacts = new ObservableCollection<Contact>();
            AddCommand = new RelayCommand(AddContact, () => !string.IsNullOrWhiteSpace(Name) && IsPhoneValid(Phone));
            DeleteCommand = new RelayCommand<Contact>(c => { if (c != null) Contacts.Remove(c); }, c => c != null);
        }

        private void AddContact()
        {
            try { Contacts.Add(new Contact(Name, Phone)); Name = Phone = ""; }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка"); }
        }

        private bool IsPhoneValid(string p) => System.Text.RegularExpressions.Regex.IsMatch(p, @"^(\+7\d{10}|\d{10})$");
    }
}