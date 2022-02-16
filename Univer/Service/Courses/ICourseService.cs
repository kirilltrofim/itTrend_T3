using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Models;

namespace Univer.Service.Courses
{
    public interface ICourseService
    {
        List<Course> List();
        void Create(Course course, List<Int32> list, List<Int32> list1, List<Int32> list2);
        Course GetById(int id);
        void Update(int id, Course course, List<Int32> list, List<Int32> list1, List<Int32> list2);
        void Delete(int id);
        bool CourseExists(int id);
        List<Group> GroupList();
        List<Instructor> InstructorList();
        List<Student> StudentList();
    }
}
