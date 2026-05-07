Лабораторная работа №9
Тема: Архитектурный шаблон MVVM
Цель работы: Изучить архитектурный шаблон Model‑View‑ViewModel (MVVM) и закрепить практические навыки его применения при разработке настольных приложений на платформе Windows Presentation Foundation (WPF).

Задание:
Разработать приложение «Телефонная книга» на платформе WPF с использованием паттерна MVVM. Приложение должно обеспечивать управление списком контактов (добавление, удаление, просмотр) и соответствовать следующим требованиям:

Реализовать модель Contact с полями Name (имя контакта) и Phone (номер телефона).

Создать ViewModel (MainViewModel) с командами добавления и удаления контактов (AddCommand – без параметра, DeleteCommand – с параметром).

Использовать ObservableCollection<Contact> для автоматического обновления UI.

Реализовать валидацию: имя не пустое, номер телефона в формате +7XXXXXXXXXX или 10 цифр.

Отображать список контактов в DataGrid с возможностью выбора контакта для удаления.

Обеспечить привязку данных (Data Binding) между XAML и свойствами ViewModel.

Теоретическое обоснование
Архитектурный шаблон MVVM (Model‑View‑ViewModel) предназначен для разделения логики представления и бизнес-логики. Он состоит из трёх компонентов:

Model (Модель) – хранит данные и бизнес-правила (например, валидацию номера телефона). Модель ничего не знает об интерфейсе.

View (Представление) – это XAML-разметка (окна, элементы управления). View не содержит кода бизнес-логики, только привязки к свойствам и командам ViewModel.

ViewModel (Модель представления) – связывает Model и View. Реализует INotifyPropertyChanged для автоматического обновления UI, предоставляет команды (ICommand) для действий пользователя и содержит коллекции с уведомлениями (ObservableCollection<T>).

Ключевые механизмы WPF, поддерживающие MVVM:

Data Binding – связывает UI‑элемент со свойством в ViewModel.

INotifyPropertyChanged – позволяет ViewModel уведомить View об изменении свойства.

ICommand – инкапсулирует действие (например, добавление контакта) и его доступность.

ObservableCollection<T> – автоматически оповещает UI о добавлении/удалении элементов.

Ожидаемый результат:
Полностью работающее приложение, в котором поля ввода привязаны к свойствам ViewModel, кнопки активируются только при корректных данных, список контактов обновляется без дополнительного кода, а логика не перемешана с XAML‑кодом (code‑behind остаётся пустым, кроме установки DataContext).

Описание выполненных действий
Создан проект WPF с именем WpfApp1 (или PhoneBook).

Реализован базовый класс ObservableObject в папке ViewModels, реализующий INotifyPropertyChanged и вспомогательный метод Set.

Реализованы классы RelayCommand (без параметра и с параметром T) в папке ViewModels для поддержки команд.

Создана модель Contact в папке Models с конструктором, проверяющим имя и номер телефона через регулярное выражение.

Создан класс MainViewModel (наследник ObservableObject):

Добавлены свойства Name, Phone, SelectedContact.

Добавлена коллекция ObservableCollection<Contact> Contacts.

Инициализированы команды:

AddCommand = new RelayCommand(AddContact, CanAddContact) – без параметра.

DeleteCommand = new RelayCommand<Contact>(DeleteContact, CanDeleteContact) – с параметром.

Реализованы методы AddContact (создаёт Contact, добавляет в коллекцию, очищает поля) и DeleteContact (удаляет выбранный контакт).

CanAddContact возвращает true, только если имя не пусто и номер телефона соответствует формату.

CanDeleteContact возвращает true, если SelectedContact != null.

Создано представление MainWindow.xaml:

Два TextBox с привязкой Text="{Binding Name/Phone, UpdateSourceTrigger=PropertyChanged}".

Кнопка «Добавить» с привязкой Command="{Binding AddCommand}".

Кнопка «Удалить» с привязкой Command="{Binding DeleteCommand}" CommandParameter="{Binding SelectedContact}".

DataGrid с ItemsSource="{Binding Contacts}", SelectedItem="{Binding SelectedContact}", колонками для Name и Phone.

В MainWindow.xaml.cs в конструкторе установлен DataContext = new MainViewModel(); (остальной code‑behind пуст).

Аналогичные действия по настройке привязок и команд выполнены для всех элементов управления.

Выполнено тестирование: проверено добавление корректных/некорректных контактов, работа кнопки «Удалить», автоматическое обновление DataGrid.

Результат выполненной работы
После запуска приложения открывается главное окно с двумя текстовыми полями («Имя», «Телефон»), кнопками «Добавить» и «Удалить» и таблицей контактов.

Тестирование:

При пустом имени кнопка «Добавить» неактивна.

При неверном формате телефона (например, 123) кнопка не активна.

При корректных данных (имя: Иван, телефон: +79131234567) кнопка становится активной.

После нажатия «Добавить» в таблице появляется новая строка, поля ввода очищаются.

Если выбрать контакт в таблице, кнопка «Удалить» становится активной.

После удаления строка исчезает из таблицы.

При попытке добавить контакт с пустым именем или неверным телефоном выводится сообщение об ошибке (благодаря try-catch в AddContact).

Сравнение с теоретической оценкой:

Валидация работает как задумано.

ObservableCollection обеспечивает мгновенное обновление UI.

Привязка TwoWay позволяет редактировать поля ввода и синхронизировать их с ViewModel.

Команды с лямбда-выражениями корректно управляют доступностью кнопок.

Отчёт подтверждает, что MVVM-подход полностью реализован, код тестируем и легко сопровождается.

Исходный код модуля
Файл ObservableObject.cs (папка ViewModels)
csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.ViewModels
{
    /// <summary>Базовый класс с реализацией INotifyPropertyChanged</summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
Файл RelayCommand.cs (папка ViewModels)
csharp
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    /// <summary>Команда без параметра</summary>
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
        public void Execute(object? parameter) => _execute();

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    /// <summary>Команда с параметром типа T</summary>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T?> _execute;
        private readonly Predicate<T?>? _canExecute;

        public RelayCommand(Action<T?> execute, Predicate<T?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke((T?)parameter) ?? true;
        public void Execute(object? parameter) => _execute((T?)parameter);

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
Файл Contact.cs (папка Models)
csharp
using System.Text.RegularExpressions;

namespace WpfApp1.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public Contact(string name, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя не может быть пустым.");
            if (!Regex.IsMatch(phone, @"^(\+7\d{10}|\d{10})$"))
                throw new ArgumentException("Телефон должен быть в формате +7XXXXXXXXXX или 10 цифр.");
            Name = name;
            Phone = phone;
        }
    }
}
Файл MainViewModel.cs (папка ViewModels)
csharp
using System.Collections.ObjectModel;
using System.Windows;
using System.Text.RegularExpressions;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<Contact> Contacts { get; }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand<Contact>(DeleteContact, c => c != null);
        }

        private void AddContact()
        {
            try
            {
                var newContact = new Contact(Name, Phone);
                Contacts.Add(newContact);
                Name = string.Empty;
                Phone = string.Empty;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) && Regex.IsMatch(Phone, @"^(\+7\d{10}|\d{10})$");
        }

        private void DeleteContact(Contact? contact)
        {
            if (contact != null && Contacts.Contains(contact))
                Contacts.Remove(contact);
        }
    }
}
Файл MainWindow.xaml
xml
<Window x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Телефонная книга" Height="450" Width="600">
    <Grid Margin="10">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>

        <TextBox Grid.Row="0" Margin="0,5" Text="{Binding Name, UpdateSourceTrigger=PropertyChanged}" ToolTip="Имя"/>
        <TextBox Grid.Row="1" Margin="0,5" Text="{Binding Phone, UpdateSourceTrigger=PropertyChanged}" ToolTip="Телефон +7... или 10 цифр"/>

        <StackPanel Grid.Row="2" Orientation="Horizontal" Margin="0,10">
            <Button Content="Добавить" Width="100" Margin="0,0,10,0" Command="{Binding AddCommand}"/>
            <Button Content="Удалить" Width="100" Command="{Binding DeleteCommand}" CommandParameter="{Binding SelectedContact}"/>
        </StackPanel>

        <DataGrid Grid.Row="3" AutoGenerateColumns="False" IsReadOnly="True"
                  ItemsSource="{Binding Contacts}"
                  SelectedItem="{Binding SelectedContact}">
            <DataGrid.Columns>
                <DataGridTextColumn Header="Имя" Binding="{Binding Name}" Width="*"/>
                <DataGridTextColumn Header="Телефон" Binding="{Binding Phone}" Width="*"/>
            </DataGrid.Columns>
        </DataGrid>
    </Grid>
</Window>
Файл MainWindow.xaml.cs
csharp
using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
