using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Models;

namespace Univer.Service.Groups
{
    public interface IGroupService
    {
        List<Group> List();
        void Create(Group group, List<Int32> list);
        Group GetById(int id);
        void Update(int id, Group group, List<Int32> list);
        void Delete(int id);
        bool GroupExists(int id);
        List<Instructor> CuratorList(int? id);
        List<Special> SpecialList();
        List<Course> CourseList();
        List<Student> StudentList(int? id);
    }
}
