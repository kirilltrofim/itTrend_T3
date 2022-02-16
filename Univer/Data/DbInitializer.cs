using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Models;

namespace Univer.Data
{
    public class DbInitializer
    {
        public static void Initialize(UniverContext context)
        {
            context.Database.EnsureCreated();
            if(context.Students.Any())
            {
                return;
            }

            var instructors = new Instructor[]
            {
                new Instructor{FullName="Кирилл", PhoneNumber="077862455", Photo="Files\\Students\\1.jpg",},
                new Instructor{FullName="Артем", PhoneNumber="077862455", Photo="Files\\Students\\1.jpg",},
                new Instructor{FullName="Яна", PhoneNumber="077862455", Photo="Files\\Students\\1.jpg",}
            };
            foreach(Instructor i in instructors)
            {
                context.Instructors.Add(i);
            }
            context.SaveChanges();

            var specials = new Special[]
            {
                new Special{Title="ООП"},
                new Special{Title="Высшая математика"},
                new Special{Title="Основы алгоритмов"}
            };
            foreach(Special s in specials)
            {
                context.Specials.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{ Title="1 курс", Instructors=new List<Instructor>(){ instructors[0], instructors[1], instructors[2] } },
                new Course{ Title="2 курс", Instructors=new List<Instructor>(){ instructors[0], instructors[2] } },
                new Course{ Title="3 курс", Instructors=new List<Instructor>(){ instructors[2] } }
            };
            foreach(Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var groups = new Group[]
            {
                new Group{Title="19ИВ", Year=new DateTime(2019,12,21), CourseId=1, SpecialId=1},
                new Group{Title="19ИС", Year=new DateTime(2019,12,21), CuratorId=3, CourseId=2, SpecialId=1},
                new Group{Title="19ПИ", Year=new DateTime(2019,12,21), CuratorId=2, CourseId=1, SpecialId=2}
            };
            foreach (Group g in groups)
            {
                context.Groups.Add(g);
            }
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{FullName="Трофим Кирилл", PhoneNumber="077862455", Photo="Files\\Students\\1.jpg", CourseId=1, GroupId=1},
                new Student { FullName = "Барановский Александр", PhoneNumber = "077862654", Photo = "Files\\Students\\1.jpg", CourseId = 2, GroupId = 2 },
                new Student { FullName = "Безрук Сергей", PhoneNumber = "077862194", Photo = "Files\\Students\\1.jpg", CourseId = 1, GroupId = 1 }
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();
        }
    }
}
