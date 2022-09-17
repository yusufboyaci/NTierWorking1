using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Makale : CoreEntity
    {
        public string? MakaleIcerigi { get; set; }
        public string? MakaleBasligi { get; set; }
        public string? ResimYolu { get; set; }
        public int? OkunmaSayisi { get; set; }
        public bool OnayliMi { get; set; }
        public Guid UyeId { get; set; } //FK
        public virtual Uye Uye { get; set; }
    }
}
