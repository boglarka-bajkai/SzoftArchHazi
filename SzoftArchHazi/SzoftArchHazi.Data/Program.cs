using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SzoftArchHf_DB_New
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SzoftArchAddPrint();
            ProjectAddPrint();
        }

        /*
         Project adatainak kiírása
         */
        static void ProjectAddPrint()
        {
            using(var db=new SzoftArchContext())
            {
                var project1 =new Project { Id = 0, Name = "Project1", Description = "Ez egy minta project leírás!", StartDate =  new DateOnly(2023,11,12), EndDate = new DateOnly(2023,12,10) };
                db.Projects.Add(project1);
                var project2 = new Project { Id = 0, Name = "Project2", Description = "Ez másik minta project leírás!", StartDate = new DateOnly(2024, 11, 12), EndDate = new DateOnly(2025, 12, 10) };
                db.Projects.Add(project2);
                db.SaveChanges();

                foreach (var item in db.Projects)
                {
                    Console.WriteLine(item.Name + " "+item.Description);
                }
                Console.ReadKey();
            }
        }
        /*
         Dolgozó adatainak hozzáadása és a nevének hozzáadása
         */
        static void SzoftArchAddPrint()
        {
            using(var db=new SzoftArchContext())
            {
                //új dolgozó felvétele
                Console.Write("Dolgozó adatai (Név, Email, Szerep, Jelszó): ");
                var employeeLineData = Console.ReadLine();
                string[] employeeData = employeeLineData.Split(' ');
                var employee=new Employee {id=0, Name = employeeData[0], Email= employeeData[1], Role= employeeData[2], Password= employeeData[3] };
                db.Employees.Add(employee);
                db.SaveChanges();

                //dolgozók kilistázása
                var query= from b in db.Employees orderby b.Name select b;
                Console.WriteLine("Dolgozók:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

/*
        Database Access
 */
        public class SzoftArchContext:DbContext
        {
            public DbSet<Employee> Employees { get; set;}
            public DbSet<OnDuty>onDuties { get; set;}
            public DbSet<OnDutyDates> onDutyDates { get; set;}
            public DbSet<Project> Projects { get; set;}
        }

        /*
            Database Tables
         */

        public class Employee
        {
           public int id { get; set; }
           public string Name { get; set; }
           public string Email { get; set; }
            public String Role { get; set; }
            public String Password { get; set; }
        }

        public class OnDuty
        {
            [Key]
            public int EmployeeId { get; set; }
            public int ProjectId { get; set; }
        }
        public class OnDutyDates
        {
            [Key]
            public int OnDutyId { get; set; }
            public DateOnly DutyDay { get; set; }
        }
        public class Project
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Description { get; set; }
            public DateOnly StartDate { get; set; }
            public DateOnly EndDate { get; set; }
        }
    }
}
