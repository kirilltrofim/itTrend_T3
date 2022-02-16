using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Models;

namespace Univer.Service.Specials
{
    public interface ISpecialService
    {
        List<Special> List();
        void Create(Special special, List<Int32> list);
        Special GetById(int id);
        void Update(int id, Special special, List<Int32> list);
        void Delete(int id);
        bool SpecialExists(int id);
        List<Group> GroupList();
    }
}
