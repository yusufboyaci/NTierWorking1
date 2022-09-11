using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI.Models.ViewModels
{
    public enum Role
    {
        None = 1,
        Member = 2,
        Admin = 3
    }
    [NotMapped]
    public class UyeVM
    {
        [HiddenInput]
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Adınız")]
        [Required(ErrorMessage ="Lütfen bu alanı boş bırakmayanız!")]
        [MaxLength(50,ErrorMessage ="50 karakterden fazla değer girmeyiniz")]
        public string? Ad { get; set; }
        [Display(Name = "Soyadınız")]
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayanız!")]
        [MaxLength(50, ErrorMessage = "50 karakterden fazla değer girmeyiniz")]
        public string? Soyad { get; set; }
        [Display(Name ="Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayanız!")]
        [MaxLength(50, ErrorMessage = "50 karakterden fazla değer girmeyiniz")]
        public string? KullaniciAdi { get; set; }
        [Display(Name = "Yorumunuz")]
        [MaxLength(50, ErrorMessage = "150 karakterden fazla değer girmeyiniz")]
        public string? KullaniciYorum { get; set; }
        [Display(Name = "Mail Adresiniz")]
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayanız!")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "250 karakterden fazla değer girmeyiniz")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli mail adresi giriniz.")]
        public string? MailAdresi { get; set; }
        [Display(Name = "Resminiz")]
        [DataType(DataType.ImageUrl)]
        public string? KullaniciResimYolu { get; set; }
        public IFormFile? KullaniciResim { get; set; }
        [Display(Name = "Rolünüz")]
        public Role? Role { get; set; }
        [Display(Name ="Doğum Tarihi")]
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayanız!")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]//önemli tarih gösterme yöntemi
        public DateTime? DogumGunu { get; set; }
        [Display(Name ="Onaylı")]
        public bool OnayliMi { get; set; }
    }
}
