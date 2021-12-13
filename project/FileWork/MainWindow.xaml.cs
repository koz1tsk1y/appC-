using System.Windows;
using System.Collections.ObjectModel;

namespace FileWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Person> Persons { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            AddFiealdInCollection(); //метод добавления полей редактирования в коллекцию

            Persons = new ObservableCollection<Person>();
            DataContext = this;
        }




        //=====================События=кнопок=управления=====================

        //-------------------Нажатие-на-главную-кнопку-"Создать-запись"-------------------
        private void CreateNewRecord_Click(object sender, RoutedEventArgs e)
        {
            MainButtons.Visibility = Visibility.Collapsed; //Спрятать основные кнопки
            AdditionalButtonsCreateRecord.Visibility = Visibility.Visible; //Отобразить дополнительные кнопки

            EditField.Visibility = Visibility.Visible;     //Отобразить поля ввода

            ReadOnlyEditFields(IsReadOnlyEditFields: false); //Разрешить редактирование полей
            ClearEditFields(); //Очистить поля ввода
        }





        //-------------------Нажатие-на-кнопку-"Прочитать-запись"-------------------
        private void ReadRecord_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на выбор записи
            if (!SelectionCheck(personsList.SelectedIndex)) 
                return;

            EditField.Visibility = Visibility.Visible; //Отобразить поля ввода
            ReadOnlyEditFields(IsReadOnlyEditFields: true);     //Запретить редактирование полей

            ReadEmployeeInformation(personsList.SelectedIndex); //Прочитать содержимое выбранной записи

            MainButtons.Visibility = Visibility.Collapsed;               //Спрятать основные кнопки
            AdditionalButtonsReadRecord.Visibility = Visibility.Visible; //Отобразить дополнительные кнопки
        }





        //-------------------Нажатие-на-кнопку-"Редактировать-запись"-------------------
        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на выбор записи
            if (!SelectionCheck(personsList.SelectedIndex))
                return;

            EditField.Visibility = Visibility.Visible;       //Отобразить поля ввода
            ReadOnlyEditFields(IsReadOnlyEditFields: false);    //Разрешить редактирование полей

            ReadEmployeeInformation(personsList.SelectedIndex); //Прочитать содрежимое выбранной записи

            MainButtons.Visibility = Visibility.Collapsed; //Спрятать основные кнопки
            AdditionalButtonsEditRecord.Visibility = Visibility.Visible; //Отобразить дополнительные кнопки
            
        }





        //-------------------Нажатие-на-кнопку-"Удалить-запись"-------------------
        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на выбор записи
            if (!SelectionCheck(personsList.SelectedIndex))
                return;
            
            Persons.RemoveAt(personsList.SelectedIndex); //Удалить выбранную запись
        }





        //----------------Нажатие-на-дополнительную-кнопку-"Создать-запись"----------------
        private void CreateRecord_Click(object sender, RoutedEventArgs e)
        {

            if (CheckingEditFields()) //Проверка полей на пустой ввод
            {
                Person person = new Person
                (
                    FirstnameBox.Text,
                    SurnameBox.Text,
                    MiddlenameBox.Text,
                    BirthdayBox.Text,
                    AddressBox.Text,
                    DepartmentBox.Text,
                    PersonalBox.Text
                );
                Persons.Add(person);

                AdditionalButtonsCreateRecord.Visibility = Visibility.Collapsed;   //Спрятать дополнительные кнопки
                EditField.Visibility = Visibility.Collapsed;   //Спрятать поля ввода
                MainButtons.Visibility = Visibility.Visible;     //Отобразить основные кнопки
                
            }
        }





        //------------------------Нажатие-на-дополнительную-кнопку-"Сохранть-изменения"------------------------
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {

            if (CheckingEditFields()) //Проверка полей на пустой ввод 
            {
                int index = personsList.SelectedIndex;
                Persons[index].SaveModific(Persons[index],
                                           FirstnameBox.Text, SurnameBox.Text, MiddlenameBox.Text,
                                           BirthdayBox.Text, AddressBox.Text, DepartmentBox.Text, PersonalBox.Text);


                AdditionalButtonsEditRecord.Visibility = Visibility.Collapsed;   //Спрятать дополнительные кнопки
                EditField.Visibility = Visibility.Collapsed;   //Спрятать поля ввода
                MainButtons.Visibility = Visibility.Visible;     //Отобразить основные кнопки
            } 
        }





        //------------------------Нажатие-на-кнопку-"Отмена"------------------------
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainButtons.Visibility      = Visibility.Visible;     //Отобразить основные кнопки
            EditField.Visibility        = Visibility.Collapsed;   //Спрятать поля ввода

            //Спрятать дополнительные кнопки
            if (sender == CancelRead)
                AdditionalButtonsReadRecord.Visibility = Visibility.Collapsed;
            else
            if (sender == CancelEdit)
                AdditionalButtonsEditRecord.Visibility = Visibility.Collapsed;
            else
            if (sender == CancelCreate)
                AdditionalButtonsCreateRecord.Visibility = Visibility.Collapsed;
        }







        //=====================События=кнопок=дополнительного=меню=====================
        FileWorkClass file = new FileWorkClass();

        //---------------------Нажатие-на-кнопку-"File"->"Open-list"---------------------
        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {

            bool flagOpenFile; //Переменная для отслеживания характера ошибки (был ли открыт файл)

            string[] masPerson = file.OpenReadTextFile(out flagOpenFile); //записать в массив результат выполнения метода по чтению файла


            //Если файл не был открыт, вывести сообщение об ошибке и завершить работу
            if (flagOpenFile == false)
            {
                MessageBox.Show("Файл не выбран",
                                "Внивание",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }
            
            
            //Если у файла верная конфигурация
            if (masPerson != null)
            {
                Persons.Clear(); //Очистить коллекцию

                //Разбить каждую строку массива на соответствующие поля и добавить "человека" в коллекцию
                for (int i = 0; i < masPerson.Length; i++)
                {
                    string[] mas = masPerson[i].Split(';');

                    Persons.Add(new Person(mas));
                }
            }
            //Если у файла неверная конфигурация
            else
            {
                MessageBox.Show("Неверная конфигурация файла!",
                                "Внивание",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }




        //---------------------Нажатие-на-кнопку-"File"->"Save-list"---------------------
        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            file.SaveTextFile(Persons);
        }



        //---------------------Нажатие-на-кнопку-"Exit"---------------------
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void MenuItemNew_Click(object sender, RoutedEventArgs e)
        {
            Persons.Clear();

            MainButtons.Visibility = Visibility.Visible;     //Отобразить основные кнопки
            EditField.Visibility = Visibility.Collapsed;   //Спрятать поля ввода

            //Спрятать дополнительные кнопки
            AdditionalButtonsReadRecord.Visibility   = Visibility.Collapsed;
            AdditionalButtonsEditRecord.Visibility   = Visibility.Collapsed;
            AdditionalButtonsCreateRecord.Visibility = Visibility.Collapsed;

        }
    }
}
