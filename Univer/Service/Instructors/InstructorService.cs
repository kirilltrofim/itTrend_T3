using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Data;
using Univer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace Univer.Service.Instructors
{
    public class InstructorService : IInstructorService
    {
        private UniverContext _context;
        IWebHostEnvironment _appEnvironment;

        public InstructorService(UniverContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public List<Instructor> List()
        {
            var list = _context.Instructors
                .Include(s => s.Courses)
                .Include(s => s.Group)
                .ToList();
            return list;
        }

        public List<Course> CourseList()
        {
            var courseList = _context.Courses.ToList();
            return courseList;
        }

        public List<Group> GroupList(int? id)
        {
            var groupContextList = _context.Groups.Include(s => s.Curator).ToList();
            var groupList = new List<Group>();

            foreach(Group g in groupContextList)
            {
                if(g.Curator == null || g.Curator != null && g.CuratorId == id)
                {
                    groupList.Add(g);
                }
            }

            return groupList;
        }

        public Instructor GetById(int id)
        {
            var instructor = _context.Instructors
                .Include(c => c.Courses)
                .Include(c => c.Group)
                .FirstOrDefault(m => m.Id == id);
            return instructor;
        }

        public void Create([Bind("Id,FullName,PhoneNumber")] Instructor instructor, IFormFile uploadFile, int? name, List<Int32> list)
        {
            _context.Add(instructor);

            if (uploadFile != null)
            {
                string path = "/Files/Students/" + uploadFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadFile.CopyTo(fileStream);
                }
                instructor.Photo = path;
            }

            instructor.Group = _context.Groups.FirstOrDefault(s => s.Id == name);
            var courseChoice = new List<Course>();
            foreach(int i in list)
            {
                courseChoice.Add(_context.Courses.FirstOrDefault(m => m.Id == i));
            }
            instructor.Courses = courseChoice;
            
            _context.SaveChanges();
        }

        public void Update(int id, [Bind("Id,FullName,PhoneNumber")] Instructor instructor, IFormFile uploadFile, int? name, List<Int32> list)
        {
            _context.Update(instructor);

            if (uploadFile != null)
            {
                string path = "/Files/Students/" + uploadFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadFile.CopyTo(fileStream);
                }
                instructor.Photo = path;
            }

            var instructor1 = _context.Instructors
                .Include(s => s.Courses).Include(s => s.Group).Include(s => s.Courses).FirstOrDefault(s => s.Id ==id);
            _context.Entry(instructor1).CurrentValues.SetValues(instructor);
            instructor1.Group = _context.Groups.FirstOrDefault(s => s.Id == name);

            var courseChoice = new List<Course>();
            foreach (int i in list)
            {
                courseChoice.Add(_context.Courses.FirstOrDefault(m => m.Id == i));

            }
            instructor1.Courses = courseChoice;

            

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Instructors.Remove(GetById(id));
            _context.SaveChanges();
        }

        public bool InstructorExists(int id)
        {
            return _context.Instructors.Any(s => s.Id == id);
        }
    }
}
