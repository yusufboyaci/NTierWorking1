using Core.Entity.Concrete;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Uye : CoreEntity
    {
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? KullaniciAdi { get; set; }
        public string? KullaniciYorum { get; set; }
        public string? MailAdresi { get; set; }
        public string? KullaniciResimYolu { get; set; }
        public Role? Role { get; set; }
        public DateTime? DogumGunu { get; set; }
        public bool OnayliMi { get; set; }
        public virtual List<Makale> Makaleler { get; set; }
    }
}
