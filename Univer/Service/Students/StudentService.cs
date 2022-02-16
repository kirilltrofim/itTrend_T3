using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Univer.Data;
using Univer.Models;

namespace Univer.Service.Students
{
    public class StudentService : IStudentService
    {
        private UniverContext _context;
        IWebHostEnvironment _appEnvironment;

        public StudentService(UniverContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public List<Student> List()
        {
                var univerContext = _context.Students.Include(s => s.Course).Include(s => s.Group);
                var students = from m in univerContext select m;


            return students.ToList();
        }

        public List<Course> CourseList()
        {
            var courseList = _context.Courses.ToList();
            return courseList;
        }

        public List<Group> GroupList()
        {
            var groupList = _context.Groups.ToList();
            return groupList;
        }

        public Student GetById(int id)
        {
            var student = _context.Students
                .Include(s => s.Course)
                .Include(s => s.Group)
                .FirstOrDefault(m => m.Id == id);
            return student;
        }

        public void Create([Bind("Id,FullName,PhoneNumber, CourseId,GroupId")] Student student, IFormFile uploadFile)
        {
            _context.Add(student);
            if (uploadFile != null)
            {
                string path = "/Files/Students/" + uploadFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadFile.CopyTo(fileStream);
                }
                student.Photo = path;
            }        
            _context.SaveChanges();
        }

        public void Update(int id, [Bind("Id,FullName,PhoneNumber,CourseId,GroupId")] Student student, IFormFile uploadFile)
        {
            _context.Update(student);

            if (uploadFile != null)
            {
                string path = "/Files/Students/" + uploadFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadFile.CopyTo(fileStream);
                }
                student.Photo = path;
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = GetById(id);
            string path = _appEnvironment.WebRootPath + student.Photo;
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }

            _context.Remove(student);
            _context.SaveChanges();
        }

        public bool StudentExists(int id)
        {
            return _context.Students.Any(m => m.Id ==id);                                                       
        }
    }
}
