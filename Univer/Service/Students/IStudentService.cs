using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Models;

namespace Univer.Service.Students
{
    public interface IStudentService
    {
        List<Student> List();
        void Create(Student student, IFormFile uploadFile);
        Student GetById(int id);
        void Update(int id, Student student, IFormFile uploadFile);
        void Delete(int id);
        bool StudentExists(int id);
        List<Course> CourseList();
        List<Group> GroupList();
    }
}
