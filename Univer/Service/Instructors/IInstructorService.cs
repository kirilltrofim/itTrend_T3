using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Univer.Service.Instructors
{
    public interface IInstructorService
    {
        List<Instructor> List();
        void Create(Instructor instructor, IFormFile uploadFile, int? name, List<Int32> list);
        Instructor GetById(int id);
        void Update(int id, Instructor instructor, IFormFile uploadFile ,int? name, List<Int32> list);
        void Delete(int id);
        bool InstructorExists(int id);
        List<Group> GroupList(int? id);
        List<Course> CourseList();

    }
}
