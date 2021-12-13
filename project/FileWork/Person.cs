using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileWork
{
    public class Person : INotifyPropertyChanged
    {
        private string firstname;   //Имя
        private string surname;     //Фамилия
        private string middlename;  //Отчество
        private string birthday;    //День рождения
        private string address;     //Адресс
        private string departament; //Отдел
        private string personal;    //О себе


        //Конструктор
        public Person(string firstname, string surname, string middlename, string birthday,
                      string address, string departament, string personal)
        {
            Firstname   = firstname;
            Surname     = surname;
            Middlename  = middlename;
            Birthday    = birthday;
            Address     = address;
            Departament = departament;
            Personal    = personal;
        }


        //Конструктор
        public Person(string[] mas)
        {
            Firstname = mas[0];
            Surname = mas[1];
            Middlename = mas[2];
            Birthday = mas[3];
            Address = mas[4];
            Departament = mas[5];
            Personal = mas[6];
        }

        
        //Метод сохранения изменений 
        public void SaveModific(Person person,
                                string firstname, string surname, string middlename, string birthday,
                                string address, string departament, string personal)
        {
            person.Firstname    = firstname;
            person.Surname      =  surname;
            person.Middlename   = middlename;
            person.Birthday     = birthday;
            person.Address      = address;
            person.Departament  = departament;
            person.Personal     = personal;
        }


        
        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged("firstname");
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("surname");
            }
        }
        public string Middlename
        {
            get { return middlename; }
            set
            {
                middlename = value;
                OnPropertyChanged("middlename");
            }
        }

        public string Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged("birthday");
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("address");
            }
        }
        public string Departament
        {
            get { return departament; }
            set
            {
                departament = value;
                OnPropertyChanged("departament");
            }
        }
        public string Personal
        {
            get { return personal; }
            set
            {
                personal = value;
                OnPropertyChanged("personal");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
