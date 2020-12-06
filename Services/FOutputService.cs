using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    class FOutputService //описание класса-сервиса, который отвечает за загрузку данных из файла и дальнейшее их сохранение
    {
        private readonly string PATH;

        public FOutputService(string path)
        {
            PATH = path;
        }

        public BindingList<ToDoModel> LoadData()//описание метода загрузки данных из файла
        {
            var fileExists = File.Exists(PATH); // проверяем существование файла
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();//если его нет, то создаем его 
                return new BindingList<ToDoModel>();//и возвращаем пустой список
            }
            using( var reader = File.OpenText(PATH)) 
            {
                var fileText = reader.ReadToEnd();// или же считываем содержимое файла
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);// и конвертируем из json-формата в подходящий для нашей программы
            }
        }

        public void SaveData(object toDoData)//метод сохранения записей в файл
        {
            using (StreamWriter writer = File.CreateText(PATH))// открываем файл
            {
                string output = JsonConvert.SerializeObject(toDoData);//и записываем туда данные, предварительно конвертированные в json-формат для удобства использования
                writer.Write(output);
            }
        }
    }
}
