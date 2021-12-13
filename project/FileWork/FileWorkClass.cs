using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace FileWork
{
    class FileWorkClass 
    {
        private string filePath; //Путь к фалу
        const string ValidationFile = "*listPerson.by.Kozitskiy*"; //Строка, по которой проверяется конфигурация выбранного файла



        //----------------------Метод-открытия-файла----------------------
        public string[] OpenReadTextFile(out bool flagOpen)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.DefaultExt = ".txt"; // Default file extension
            openFileDlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension


            string filePath; //Пусть к файлу

            //Если файл был выбран
            if (openFileDlg.ShowDialog() == true)
            {
                flagOpen = true; //Файл был открыт (результат наружу)

                filePath = openFileDlg.FileName; //Путь к фалй

                //Подсчёт количества строк
                int n = 0; //Счётчик количества строк в файле
                using (StreamReader reader = new StreamReader(filePath))
                {
                    //Если первая строка файла не равна строке конфигурации
                    if (reader.ReadLine() != ValidationFile)
                        return null;

                    //Пока не достигнут конец файла
                    while (reader.Peek() != -1)
                    {
                        reader.ReadLine();
                        n++;
                    }
                }

                //Чтение строк файла в массив 
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string[] arrayPersons = new string[n]; //создать массив

                    reader.ReadLine(); //Прочитать первую строку (т.к. это строка конфигурации, то не нужно её заносить в массив)

                    //Прочитать остальные строки файла и записать их в массив
                    for (int i = 0; i < arrayPersons.Length; i++)
                    {
                        arrayPersons[i] = reader.ReadLine();
                    }
                    return arrayPersons;
                }
            }
            //Если файл не был открыт
            else
                flagOpen = false; //Файл не был открыт

            return null; //Если файл не был открыт
        }




        //----------------------Метод-сохранения-файла----------------------
        public void SaveTextFile(ObservableCollection<Person> Persons)
        {
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            //Создание фильтра для поиска файла
            saveFileDlg.DefaultExt = ".txt"; // Default file extension
            saveFileDlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            saveFileDlg.CreatePrompt = true;

            //Выбор пути файла
            if (saveFileDlg.ShowDialog() == true)
            {
                filePath = saveFileDlg.FileName;

                //Запись информации в файл
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine(ValidationFile); //Записать строку конфигурации

                    //Записать информация в файл о каждом сотруднике
                    foreach (var item in Persons)
                    {
                        writer.WriteLine($"{item.Firstname};"    +
                                         $"{item.Surname};"      +
                                         $"{item.Middlename};"   +
                                         $"{item.Birthday};"     +
                                         $"{item.Address};"      +
                                         $"{item.Departament};"  +
                                         $"{item.Personal};");
                    }
                }
                
            }
        }

    }
}
