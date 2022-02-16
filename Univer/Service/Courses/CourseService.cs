using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Data;
using Univer.Models;

namespace Univer.Service.Courses
{
    public class CourseService : ICourseService
    {
        private readonly UniverContext _context;

        public CourseService(UniverContext context)
        {
            _context = context;
        }

        public List<Course> List()
        {
            var list = _context.Courses.Include(c => c.Students).Include(c => c.Instructors).Include(c => c.Groups).ToList();
            return list;
        }

        public List<Group> GroupList()
        {
            var groupList = _context.Groups.ToList();
            return groupList;
        }

        public List<Instructor> InstructorList()
        {
            var instructorList = _context.Instructors.ToList();
            return instructorList;
        }

        public List<Student> StudentList()
        {
            var studentList = _context.Students.ToList();
            return studentList;
        }

        public void Create([Bind("Id,Title")] Course course, List<Int32> list, List<Int32> list1, List<Int32> list2)
        {
            _context.Add(course);

            var groupChoice = new List<Group>();
            var instructorChoice = new List<Instructor>();
            var studentChoice = new List<Student>();

            foreach (var i in list)
            {
                groupChoice.Add(_context.Groups.FirstOrDefault(m => m.Id == i));
            }
            foreach (var i in list1)
            {
                instructorChoice.Add(_context.Instructors.FirstOrDefault(m => m.Id == i));
            }
            foreach (var i in list2)
            {
                studentChoice.Add(_context.Students.FirstOrDefault(m => m.Id == i));
            }

            course.Groups = groupChoice;
            course.Instructors = instructorChoice;
            course.Students = studentChoice;

            _context.SaveChanges();
        }

        public Course GetById(int id)
        {
            var course = _context.Courses
                .Include(c => c.Students)
                .Include(c => c.Instructors)
                .Include(c => c.Groups)
                .FirstOrDefault(m => m.Id == id);
            return course;
        }

        public void Update(int id, [Bind("Id,Title")] Course course, List<Int32> list, List<Int32> list1, List<Int32> list2)
        {
            var course1 = _context.Courses
                .Include(m => m.Groups).Include(m => m.Instructors).Include(m => m.Students).FirstOrDefault(m => m.Id == course.Id);
            _context.Entry(course1).CurrentValues.SetValues(course);

            var groupChoice = new List<Group>();
            var instructorChoice = new List<Instructor>();
            var studentChoice = new List<Student>();

            foreach (var i in list)
            {
                groupChoice.Add(_context.Groups.FirstOrDefault(m => m.Id == i));
            }
            foreach (var i in list1)
            {
                instructorChoice.Add(_context.Instructors.FirstOrDefault(m => m.Id == i));
            }
            foreach (var i in list2)
            {
                studentChoice.Add(_context.Students.FirstOrDefault(m => m.Id == i));
            }

            course1.Groups = groupChoice;
            course1.Instructors = instructorChoice;
            course1.Students = studentChoice;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Courses.Remove(GetById(id));
            _context.SaveChanges();
        }

        public bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

    }
}
