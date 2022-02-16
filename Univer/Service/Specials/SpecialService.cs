using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Data;
using Univer.Models;

namespace Univer.Service.Specials
{
    public class SpecialService : ISpecialService
    {
        private UniverContext _context;

        public SpecialService(UniverContext context)
        {
            _context = context;
        }

        public List<Special> List()
        {
            var list = _context.Specials.Include(c => c.Groups).ToList();
            return list;
        }

        public List<Group> GroupList()
        {
            var groupList = _context.Groups.ToList();
            return groupList;
        }

        public Special GetById(int id)
        {
            var special = _context.Specials
                .Include(m => m.Groups)
                .FirstOrDefault(m => m.Id == id);
            return special;
        }

        public void Create([Bind("Id,Title")] Special special, List<Int32> list)
        {
            _context.Specials.Add(special);

            var groupChoice = new List<Group>();
            foreach (var i in list)
            {
                groupChoice.Add(_context.Groups.FirstOrDefault(m => m.Id == i));
            }

            special.Groups = groupChoice;

            _context.SaveChanges();
        }

        public void Update(int id, [Bind("Id,Title")] Special special, List<Int32> list)
        {
            var special1 = _context.Specials.Include(m => m.Groups).FirstOrDefault(m => m.Id == special.Id);
            _context.Entry(special1).CurrentValues.SetValues(special);

            var groupChoice = new List<Group>();
            foreach (var i in list)
            {
                groupChoice.Add(_context.Groups.FirstOrDefault(m => m.Id == i));
            }

            special1.Groups = groupChoice;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Specials.Remove(GetById(id));
            _context.SaveChanges();
        }

        public bool SpecialExists(int id)
        {
            return _context.Specials.Any(m => m.Id == id);
        }
    }
}
