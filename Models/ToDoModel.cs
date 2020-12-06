using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    class ToDoModel : INotifyPropertyChanged//класс, описывающий поведение модели записи дела
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;//ниже описаны 3 основных поля записи, длч этого поля автоматически создается информация о текущей дате

        private bool _isDone;//выполнено/невыполнено

        public bool isDone//при каждом изменении поля, вызывается этот метод
        {
            get { return _isDone; }
            set
            {
                if (_isDone == value) return;//сравнение с текущими данными в окне
                _isDone = value;//присвоение если не совпадают
                OnPropertyChanged("isDone");//вызов события изменения
            }

        }

        private string _text;//описание задачи

        

        public string text
        {
            get { return _text; }
            set
            {
                if (_text == value) return;
                _text = value;
                OnPropertyChanged("text");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName = "")
        {
            if (PropertyChanged != null)
            {
            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
