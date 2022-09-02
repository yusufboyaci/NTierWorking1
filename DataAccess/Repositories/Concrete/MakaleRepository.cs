using DataAccess.Context;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class MakaleRepository : Repository<Makale>
    {
        private readonly ApplicationDbContext _context;
        public MakaleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
