using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1//логика работы основного окна программы, включающая в себя описание основных полей таких как: 
{
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\toDoData.json"; // имя считываемого файла
        private BindingList<ToDoModel> toDoData; // список с обьектами дел
        private FOutputService fOutputService; // обьект, отвечающий за ввод-вывод информации из файла

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)//обработчик событий при загрузке программы
        {
            fOutputService = new FOutputService(PATH); // переменная ввода-вывода

            try//чтобы программа не вылелатала изза ошибки чтения, например такой как нарушение прав доступа
            {
                toDoData = fOutputService.LoadData();//загрузка данных 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

           
            dgToDoList.ItemsSource = toDoData;
            toDoData.ListChanged += ToDoData_ListChanged; 
        }

        private void ToDoData_ListChanged(object sender, ListChangedEventArgs e) // событие, реагирующее на изменения в списке дел
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try//чтобы программа не вылелатала изза ошибки записи, например такой как нарушение прав доступа
                {
                    fOutputService.SaveData(sender);//при любом изменении (удалении элемента, добавлении элемента, редактировании элемента) весь список сохраняется в файл
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            } 
        }
    }
}
