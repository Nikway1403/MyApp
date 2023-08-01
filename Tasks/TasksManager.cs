using MyApp.Data;
using MyApp.Models;
using Faker;
using System.Diagnostics;


namespace MyApp.Tasks
{
    public class TasksManager
    {
        private static TasksManager _taskManager;
        private TasksManager() { }

        public static TasksManager GetTasksManager()
        {
            if (_taskManager == null)
                _taskManager = new TasksManager();
            return _taskManager;
        }
        public string CutString(string str, int id)
        {
            string[] words = str.Split();
            return words[id];
        }

        public void CreatePerson(string fullname, DateTime date, string gender)
        {
            using (var context = new PersonDb())
            {
                var newPerson = new Person();
                newPerson.Gender = gender;
                newPerson.FullName = fullname;
                newPerson.DateOfBith = date;
                context.Add(newPerson);
                context.SaveChanges();
            }

        }
        public void GetAllUniqPersons()
        {
            using (var context = new PersonDb())
            {
                List<Person> persons = context.GetFullList();
                var uniqPersons = persons.GroupBy(p => new { p.FullName, p.DateOfBith }).Where(g => g.Count() == 1 || g.Count() == 2)
                    .Select(g => g.First());
                var sortedPersons = from p in uniqPersons orderby p.FullName select p;
                foreach (var person in sortedPersons)
                {
                    DateTime now = DateTime.Today;
                    int age = now.Year - person.DateOfBith.Year;
                    if (person.DateOfBith.Month > now.Month)
                        age--;
                    Console.WriteLine($"{person.FullName} {person.DateOfBith.ToShortDateString()} {person.Gender} " + age);
                }
            }
        }
        public void CreatingMPersons()
        {
            using (var context = new PersonDb())
            {
                var random = new Random();
                for (int i = 0; i < 1000000; i++)
                {

                    var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    var letterIndex = random.Next(0, 26);
                    string name = alphabet[letterIndex].ToString();
                    string lastname = Name.Last();
                    string firstname = Name.First();
                    lastname = name + lastname + firstname;
                    int birthDay = random.Next(1, 28);
                    int birthMonth = random.Next(1, 13);
                    int birthYear = random.Next(1920, 2024);
                    DateTime dateOfBirth = new DateTime(birthYear, birthMonth, birthDay);
                    string gender = random.NextDouble() < 0.5 ? "M" : "F";
                    CreatePerson(lastname, dateOfBirth, gender);
                }
            }
        }
        public void CreatingFPersons()
        {
           
            using (var context = new PersonDb())
            {
                var random = new Random();
                for (int i = 0; i < 100; i++)
                {
                    string lastName = Faker.Name.Last();
                    string firstName = Faker.Name.First();
                    lastName.ToLower();
                    lastName = "F" + lastName + firstName;
                    int birthDay = random.Next(1, 28);
                    int birthMonth = random.Next(1, 13);
                    int birthYear = random.Next(1920, 2024);
                    DateTime dateOfBirth = new DateTime(birthYear, birthMonth, birthDay);
                    string gender = "M";
                    CreatePerson(lastName, dateOfBirth, gender);
                }
            }
        }
        public void GetTimePersons()
        {
            using (var context = new PersonDb())
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var query = from p in context.Persons where p.Gender == "M" && p.FullName.StartsWith("F")
                            select p;
                foreach (var person in query)
                {
                    Console.WriteLine(person.FullName + " " + person.Gender + " " + person.DateOfBith.ToShortDateString());

                }
                stopwatch.Stop();
                Console.WriteLine("Time: {0} ms", stopwatch.ElapsedMilliseconds);
            }
        }
        public void CreatingIndex()
        {
            
        }
    }
}
