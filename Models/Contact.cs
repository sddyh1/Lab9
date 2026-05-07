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
                throw new ArgumentException("Неверный формат телефона.");

            Name = name;
            Phone = phone;
        }
    }
}