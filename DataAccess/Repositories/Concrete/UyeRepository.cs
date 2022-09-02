using DataAccess.Context;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class UyeRepository : Repository<Uye>
    {
        private readonly ApplicationDbContext _context;
        public UyeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
