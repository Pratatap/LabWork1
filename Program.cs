using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LabWork1
{
    public class Program
    {
        public static Dictionary<int, TelephoneProfile> ContactsDict = new Dictionary<int, TelephoneProfile>();
        public static void Main(string[] args)
        {
            //пробные контакты
            ContactsDict.Add(1, new TelephoneProfile(ContactsDict.Count, "Ivan", "Ivanov", "Ivanovich", 89991335533, "Country", "10.02.2000", "", "", "homeless"));
            ContactsDict.Add(2, new TelephoneProfile(ContactsDict.Count, "Pepe", "The", "Frog", 88005553535, "St.Petersburg", "00.00.0000", "IT company as well", "The director of CEO's", "personification of hell"));
            ContactsDict.Add(3, new TelephoneProfile(ContactsDict.Count, "Подоплелов", "Роман", "", 899999999, "Роися", "30.08.2000", "IT company probably", "CEO of CEO's", "dats a me"));
            MainMenu();
        }
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\nТелефонная книжка");
            Console.WriteLine("Напишите номер команды");
            Console.WriteLine("\t1 - Создать новый контакт");
            Console.WriteLine("\t2 - Редактировать контакты");
            Console.WriteLine("\t3 - Удалить контакт");
            Console.WriteLine("\t4 - Информация о контакте");
            Console.WriteLine("\t5 - Подробный список контактов");
            //берем инпут, запускаем соответствующую функцию
            bool invalidinput = true;
            while (invalidinput)
            {
                invalidinput = false;
                if (!int.TryParse(Console.ReadLine(), out int result))
                {
                    Console.WriteLine("Введите число с номером команды\n");
                    continue;
                }
                else
                {
                    switch (result)
                    {
                        case 1:
                            CreateNewNote();
                            break;
                        case 2:
                            EditNotes();
                            break;
                        case 3:
                            RemoveNotes();
                            break;
                        case 4:
                            ViewExpandedContactList();
                            Console.WriteLine("Какой контакт вы хотите открыть?");
                            ChooseContact("profileinfo");
                            Console.WriteLine("Для возврата в меню нажмите любую клавишу");
                            Console.ReadKey();
                            break;
                        case 5:
                            ViewExpandedContactList();
                            Console.WriteLine("Для возврата в меню нажмите любую клавишу");
                            Console.ReadKey();
                            break;
                        default:
                            invalidinput = true;
                            Console.WriteLine("Такой команды не найдено");
                            break;
                    }
                }
            }
            MainMenu();
        }
        public static void CreateNewNote()
        {
            //берем переменные для создания экземпляра класса с соблюдением условий
            string surname, name, patronymic, country, workplace, workposition, notes, date;
            long phonenumber;
            Console.WriteLine("Необязательные поля можно оставить пустыми");
            Console.WriteLine("Фамилия");
            surname = GetSurname();
            Console.WriteLine("Имя");
            name = GetName();
            Console.WriteLine("Отчество (необязательно)");
            patronymic = GetPatronymic();
            Console.WriteLine("Номер телефона");
            phonenumber = GetPhoneNumber();
            Console.WriteLine("Страна");
            country = GetCountry();
            Console.WriteLine("Дата рождения (необязательно)");
            date = GetBirthDate();
            Console.WriteLine("Место работы (необязательно)");
            workplace = GetWorkPlace();
            Console.WriteLine("Должность (необязательно)");
            workposition = GetWorkPosition();
            Console.WriteLine("Заметки (необязательно)");
            notes = GetNotes();
            //создаем entry в словаре dict 
            int lowestkey = 1;
            foreach(KeyValuePair<int, TelephoneProfile> item in ContactsDict)
            {
                if(item.Key == lowestkey)
                {
                    lowestkey++;
                }
            }
            ContactsDict.Add(lowestkey, new TelephoneProfile(lowestkey, surname, name, patronymic, phonenumber, country, date, workplace, workposition, notes));
            Console.WriteLine("Контакт добавлен \n");
            MainMenu();
        }
        public static void EditNotes()
        {
            //выводим список номеров
            ViewExpandedContactList();
            Console.WriteLine("Выберите контакт для редактирования");
            //берем инпут и пытаемся запарсить его
            int id = ChooseContact("profileinfo");
            ChooseAndEditField(id);
            Console.WriteLine("Поле успешно изменено!");
            Console.WriteLine("Хотите ли вы поменять еще какие-то поля? \n\t1 - Да\n\t2 - Нет, вернуться");
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out int result))
                {
                    Console.WriteLine("Введите номер 1 или 2!");
                    continue;
                }
                else
                {
                    switch (result)
                    {
                        case 1:
                            EditNotes();
                            break;
                        case 2:
                            MainMenu();
                            break;
                    }
                }
            }
        }
        public static void RemoveNotes()
        {
            if (!(ContactsDict.Count == 0))
            {
                while (true)
                {
                    Console.WriteLine("Точно хотите удалить контакт? \n\t0 - Точно\n\tЛюбое число для выхода");
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        if (result == 0)
                        {
                            ViewContactList();
                            Console.WriteLine("Выберите контакт для удаления");
                            int id = ChooseContact("none");
                            ContactsDict.Remove(id);
                            Console.WriteLine("Контакт успешно удален!");
                            Console.WriteLine("Для возврата в меню нажмите любую клавишу");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            MainMenu();
                        }

                    }
                    else
                    {
                        Console.WriteLine("Число не буква, введи число");
                        continue;
                    }
                }

            }
            else
            {
                Console.WriteLine("Нечего удалять, создай что-нибудь");
                Console.WriteLine("Для возврата в меню нажми любую клавишу");
                Console.ReadKey();
            }
            MainMenu();

        }
        public static void ViewExpandedContactList()
        {
            if (ContactsDict.Count > 0)
            {
                ViewContactList();
            }
            else
            {
                Console.WriteLine("Нет контактов, создайте новый!");
                Console.WriteLine("Для возврата в меню нажмите любую клавишу");
                Console.ReadKey();
                MainMenu();
            }
        }
        public static void ViewContactList()
        {
            foreach (KeyValuePair<int, TelephoneProfile> item in ContactsDict)
            {
                Console.WriteLine($"{item.Key} - {item.Value.DetailedToString()}");
            }
        }
        public static int ChooseContact(string flag)
        {
            int id;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    //выводим полную информацию о контакте
                    if (flag == "profileinfo" && ContactsDict.ContainsKey(id))
                    {
                        ShowFullContact(id, out bool invalidinput);
                        break;
                    }
                    if (id > ContactsDict.Count || id < 1 || !ContactsDict.ContainsKey(id))
                    {
                        Console.WriteLine("Введите номер существующего контакта!");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Введите номер контакта!");
                    continue;
                }
            }
            return id;
        }
        public static void ChooseAndEditField(int id)
        {
            bool localinput = true;
            while (localinput)
            {
                Console.WriteLine("Выберите поле для редактирования \n1-Фамилия\n2-Имя\n3-Отчество\n4-Номер Телефона\n5-Страна\n6-Дата рождения\n7-Организация\n8-Должность\n9-Заметки");
                if (!int.TryParse(Console.ReadLine(), out int result))
                {
                    Console.WriteLine("Введите номер поля!");
                    continue;
                }
                else
                {
                    switch (result)
                    {
                        case 1:
                            localinput = false;
                            Console.WriteLine("Введите фамилию");
                            ContactsDict[id].Surname = GetSurname();
                            break;
                        case 2:
                            localinput = false;
                            Console.WriteLine("Введите имя");
                            ContactsDict[id].Name = GetName();
                            break;
                        case 3:
                            localinput = false;
                            Console.WriteLine("Введите отчество");
                            ContactsDict[id].Patronymic = GetPatronymic();
                            break;
                        case 4:
                            localinput = false;
                            Console.WriteLine("Введите номер телефона");
                            ContactsDict[id].PhoneNumber = GetPhoneNumber();
                            break;
                        case 5:
                            localinput = false;
                            Console.WriteLine("Введите страна");
                            ContactsDict[id].Country = GetCountry();
                            break;
                        case 6:
                            localinput = false;
                            Console.WriteLine("Введите дату рождения");
                            ContactsDict[id].DateOfBirth = GetBirthDate();
                            break;
                        case 7:
                            localinput = false;
                            Console.WriteLine("Введите организацию");
                            ContactsDict[id].Workplace = GetWorkPlace();
                            break;
                        case 8:
                            localinput = false;
                            Console.WriteLine("Введите должность");
                            ContactsDict[id].WorkPosition = GetWorkPosition();
                            break;
                        case 9:
                            localinput = false;
                            Console.WriteLine("Введите заметки");
                            ContactsDict[id].Notes = GetNotes();
                            break;
                        default:
                            Console.WriteLine("Такой команды не найдено");
                            break;
                    }
                }
            }
        }
        public static void ShowFullContact(int id, out bool invalidinput)
        {
            invalidinput = true;
            while (invalidinput)
            {
                if (id <= ContactsDict.Count && id > 0)
                {
                    invalidinput = false;
                    ContactsDict.TryGetValue(id, out TelephoneProfile value);
                    Console.WriteLine($"Фамилия: \t{value.Surname} \nИмя: \t{value.Name}\nОтчество: \t{value.Patronymic}\nНомер телефона: \t{value.PhoneNumber}\nСтрана: \t{value.Country}\nДата рождения: \t{value.DateOfBirth}\nОрганизация: \t{value.Workplace}\nДолжность: \t{value.WorkPosition}\nЗаметки: \t{value.Notes}");

                }
                else
                {
                    break;
                }
            }
        }
        public static string GetSurname()
        {
            string surname;
            while (true)
            {
                surname = Console.ReadLine();
                if (String.IsNullOrEmpty(surname))
                {
                    Console.WriteLine("Фамилия не может быть пустой, это обязательная строка!");
                }
                else if (!surname.All(Char.IsLetter))
                {
                    Console.WriteLine("Должно состоять только из букв!");
                }
                else
                {
                    break;
                }
            }
            return surname;
        }
        public static string GetName()
        {
            string name;
            while (true)
            {
                name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Имя не может быть пустым, это обязательная строка!");
                }
                else if (!name.All(Char.IsLetter))
                {
                    Console.WriteLine("Должно состоять только из букв!");
                }
                else
                {
                    break;
                }
            }
            return name;
        }
        public static string GetPatronymic()
        {
            string patronymic;
            while (true)
            {
                patronymic = Console.ReadLine();
                if (String.IsNullOrEmpty(patronymic))
                {
                    return "";
                }
                else if (!patronymic.All(Char.IsLetter))
                {
                    Console.WriteLine("Должно состоять только из букв! Не думаю, что твоего отца зовут X Æ A-12");
                }
                else
                {
                    break;
                }
            }
            return patronymic;
        }
        public static long GetPhoneNumber()
        {
            long phonenumber;
            while (true)
            {
                string input = Console.ReadLine();
                if (!long.TryParse(input, out phonenumber) || (input[0] == '-'))
                {
                    Console.WriteLine("Номер телефона должен состоять из цифр");
                }
                else if (String.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Номер телефона не может быть пустым, это обязательная строка!");
                }
                else
                {
                    break;
                }
            }
            return phonenumber;
        }
        public static string GetCountry()
        {
            string country = "";
            while (true)
            {
                country = Console.ReadLine();
                if (String.IsNullOrEmpty(country))
                {
                    Console.WriteLine("Страна не может быть пустой, каждый где-то родился!");
                }
                else if (!country.All(Char.IsLetter))
                {
                    Console.WriteLine("Должно состоять только из букв! Стран с цифрами в названии не существует");
                }
                else
                {
                    break;
                }
            }
            return country;
        }
        public static string GetBirthDate()
        {
            string date;
            date = Console.ReadLine();
            if (String.IsNullOrEmpty(date))
            {
                return "";
            }
            return date;
        }
        public static string GetWorkPlace()
        {
            string workplace;
            workplace = Console.ReadLine();
            if (String.IsNullOrEmpty(workplace))
            {
                return "";
            }
            return workplace;
        }
        public static string GetWorkPosition()
        {
            string workposition;
            workposition = Console.ReadLine();
            if (String.IsNullOrEmpty(workposition))
            {
                return "";
            }
            return workposition;
        }
        public static string GetNotes()
        {
            string notes;
            notes = Console.ReadLine();
            if (String.IsNullOrEmpty(notes))
            {
                return "";
            }
            return notes;
        }
    }
    public class TelephoneProfile
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public long PhoneNumber { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }
        public string Workplace { get; set; }
        public string WorkPosition { get; set; }
        public string Notes { get; set; }
        public override string ToString()
        {
            return Surname + " " + Name;
        }
        public string DetailedToString()
        {
            return $"{Surname} {Name} \n {PhoneNumber}";
        }
        public TelephoneProfile(int id, string surname, string name, string patronymic, long phonenumber, string country, string date, string workplace, string workposition, string notes)
        {
            this.Surname = surname;
            this.Name = name;
            this.PhoneNumber = phonenumber;
            this.Id = id;
            this.Patronymic = patronymic;
            this.PhoneNumber = phonenumber;
            this.Country = country;
            this.DateOfBirth = date;
            this.Workplace = workplace;
            this.WorkPosition = workposition;
            this.Notes = notes;
        }

    }
}

