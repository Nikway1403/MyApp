using MyApp.Data;
using MyApp.Tasks;
using System.Globalization;



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
            Console.WriteLine("1 to create  table");
            Console.WriteLine("2 to create person in table");
            Console.WriteLine("3 to get all uniq persons");
            Console.WriteLine("4 to create 1000000 notes and 100 Persons with lastname on F");
            Console.WriteLine("5 to find output time");
            int a = Convert.ToInt32(Console.ReadLine());
            if (a == 1)
            {
                FirstTask();
                MainMenu();
            }
            if (a == 2)
            {
                PersonCreating();
            }
            if (a == 3)
            {
                _tasksManager.GetAllUniqPersons();
                MainMenu();
            }
            if (a == 4)
            {

                _tasksManager.CreatingMPersons();
                _tasksManager.CreatingFPersons();
                MainMenu();

            }
            if (a == 5)
            {
                _tasksManager.GetTimePersons();
                MainMenu();
            }
        }
        public void FirstTask()
        {
            using (var context = new PersonDb())
            {
                context.Database.EnsureCreated();
            }
        }
        public void PersonCreating()
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
        
    }
}
