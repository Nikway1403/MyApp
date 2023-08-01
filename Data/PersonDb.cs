using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Data
{
    public class PersonDb : DbContext
    {
        public PersonDb() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Persons;Trusted_Connection=True;");
        }
        public DbSet<Person> Persons { get; set; }
        public PersonDb(DbContextOptions<PersonDb> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
        
        public List<Person> GetFullList()
        {
            using (var context = new PersonDb())
            {
                List<Person> list = context.Persons.ToList();
                return list;
            }
        }
        public bool CheckPersonExist(Person person, List<Person> list)
        {
            foreach (var item in list) 
            {
                if(item.Id == person.Id)
                    return true;
            }
            return false;
        }
    }
}
