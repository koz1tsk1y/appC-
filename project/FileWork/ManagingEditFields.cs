using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FileWork
{
    partial class MainWindow 
    {
        public List<TextBox> editFields = new List<TextBox>();



        //Добавление в коллекцию всех полей редактирования
        void AddFiealdInCollection()
        {
            editFields.Add(FirstnameBox);
            editFields.Add(SurnameBox);
            editFields.Add(MiddlenameBox);
            editFields.Add(BirthdayBox);
            editFields.Add(AddressBox);
            editFields.Add(DepartmentBox);
            editFields.Add(PersonalBox);
        }



        //Проверка на пустой ввод полей редактирования
        public bool CheckingEditFields()
        {
            foreach (var item in editFields)
            {
                if (item.Text == "")
                {
                    item.Background = Brushes.Red;
                    item.ToolTip = "Поле ввода не может быть пустым!";
                    return false;
                }
                else
                {
                    item.Background = Brushes.White;
                    item.ToolTip = null;
                }
            }
            return true;
        }



        //Очистка полей редактирования
        public void ClearEditFields()
        {
            foreach (var item in editFields)
            {
                item.Text = "";
                item.Background = Brushes.White;
                item.ToolTip = null;
            }
                
        }



        //Разрешить\Запретить редактирование полей
        public void ReadOnlyEditFields(bool IsReadOnlyEditFields)
        {
            foreach (var item in editFields)
                item.IsReadOnly = IsReadOnlyEditFields;
        }


        
        //Проверка на выбор записи
        public bool SelectionCheck(int index)
        {
            if (index == -1)
            {
                MessageBox.Show("Запись не выбрана",
                                "Внимание",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return false;
            }
            return true;
        }



        //Прочитать информацию о сотруднике
        public void ReadEmployeeInformation(int index)
        {
            FirstnameBox.Text   = Persons[index].Firstname;
            SurnameBox.Text     = Persons[index].Surname;
            MiddlenameBox.Text  = Persons[index].Middlename;
            BirthdayBox.Text    = Persons[index].Birthday;
            AddressBox.Text     = Persons[index].Address;
            DepartmentBox.Text  = Persons[index].Departament;
            PersonalBox.Text    = Persons[index].Personal;
        }
    }
}
