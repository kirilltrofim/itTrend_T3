using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Data;
using Univer.Models;

namespace Univer.Service.Groups
{
    public class GroupService : IGroupService
    {
        private UniverContext _context;

        public GroupService(UniverContext context)
        {
            _context = context;
        }

        public List<Group> List()
        {
            var list = _context.Groups
                .Include(c => c.Students)
                .Include(c => c.Course)
                .Include(c => c.Curator)
                .Include(c => c.Special)
                .ToList();
            return list;
        }

        public List<Student> StudentList(int? id)
        {
            var studentContext = _context.Students.Include(s => s.Group).ToList();
            var studentList = new List<Student>();

            foreach (var i in studentContext)
            {
                if(i.Group is null || i.Group is not null && i.GroupId == id)
                {
                    studentList.Add(i);
                }
            }

            return studentList;
        }

        public List<Course> CourseList()
        {
            var courseList = _context.Courses.ToList();
            return courseList;
        }

        public List<Special> SpecialList()
        {
            var specialList = _context.Specials.ToList();
            return specialList;
        }

        public List<Instructor> CuratorList(int? id)
        {
            var curatorContext = _context.Instructors.Include(c => c.Group).ToList();
            var copyCuratorContext = new List<Instructor>();

            for (int i = 0; i < curatorContext.Count; i++)
            {
                Instructor r = curatorContext[i];
                if (r.Group is null || r.Group is not null && r.Group.Id == id)
                {
                    copyCuratorContext.Add(r);
                }
            }
            return copyCuratorContext.ToList();
        }

        public Group GetById(int id)
        {
            var group = _context.Groups
                .Include(c => c.Students)
                .Include(c => c.Curator)
                .Include(c => c.Special)
                .Include(c => c.Course)
                .FirstOrDefault(m => m.Id == id);
            return group;
        }

        public void Create([Bind("Title,Year,CourseId,SpecialId, CuratorId")] Group group, List<Int32> list)
        {
            _context.Groups.Add(group);
            var studentChoice = new List<Student>();
            foreach(var i in list)
            {
                studentChoice.Add(_context.Students.FirstOrDefault(m => m.Id == i));
            }
            //var group1 = _context.Groups.Include(m => m.Students).FirstOrDefault(m => m.Id == group.Id);
            group.Students = studentChoice;

            _context.SaveChanges();
        }

        public void Update(int id, [Bind("Id, Title,Year,CourseId,SpecialId, CuratorId")] Group group, List<Int32> list)
        {
            var group1 = _context.Groups.Include(m => m.Students).FirstOrDefault(m => m.Id == group.Id);
            _context.Entry(group1).CurrentValues.SetValues(group);

            var studentChoice = new List<Student>();
            foreach (var i in list)
            {
                studentChoice.Add(_context.Students.FirstOrDefault(m => m.Id == i));
            }
            group1.Students = studentChoice;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Remove(GetById(id));
            _context.SaveChanges();
        }

        public bool GroupExists(int id)
        {
            return _context.Groups.Any(m => m.Id == id);
        }
    }
}
