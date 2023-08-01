using MyApp.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyApp
{
    public class MyConsole
    {
        private  TasksManager _tasksManager = TasksManager.GetTasksManager();

        private static MyConsole _console = null!;
        private MyConsole() { }
        public static MyConsole GetConsole()
        {
            if (_console == null)
                _console = new MyConsole();
            return _console;
        }
        public void MainMenu()
        {
            Console.WriteLine("1 to create person in table");
            Console.WriteLine("2 to get uniq persons");
            Console.WriteLine("3 to gegenerate 1m persons");
            int a = Convert.ToInt32(Console.ReadLine());
            if (a == 1)
            {
                StartPage();
            }
            if (a == 2)
            {
                GetAllUniqPeople();
            }
            if (a == 3)
            {
                Generate();
            }
            if (a == 4)
            {
               // _tasksManager.CreatingFPersons();
                GetFPersons();
            }
        }
        public void StartPage()
        {
            Console.WriteLine("Console app 1 started");
            Console.WriteLine("ФИО датаРождения пол");
            string a = Console.ReadLine();
            string name = _tasksManager.CutString(a, 0);
            string date = _tasksManager.CutString(a, 1);
            string gender = _tasksManager.CutString(a, 2);
            DateTime time = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            _tasksManager.CreatePerson(name, time, gender);
            MainMenu();
        }
        public void GetAllUniqPeople()
        {
            _tasksManager.GetAllUniqPersons();
        }
        public void Generate()
        {
            _tasksManager.CreatingMPersons();
        }
        public void GetFPersons()
        {
            _tasksManager.GetTimePersons();
        }
    }
}
