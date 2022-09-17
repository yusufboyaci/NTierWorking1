using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI.Models.ViewModels
{
    [NotMapped]
    public class MakaleVM
    {
        [HiddenInput]
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Makale İçeriği")]
        [Required(ErrorMessage ="Makale İçeriği gereklidir!")]
        [MaxLength(10000, ErrorMessage = "10000 karakterden fazla değer girmeyiniz")]
        public string? MakaleIcerigi { get; set; }
        [DisplayName("Makale Başlığı")]
        [Required(ErrorMessage = "Makale başlığı gereklidir!")]
        public string? MakaleBasligi { get; set; }
        [Display(Name = "Makale Resmi")]
        [DataType(DataType.ImageUrl)]
        public string? ResimYolu { get; set; }
        public IFormFile? Resim { get; set; }
        [Display(Name ="Okunma Sayısı")]
        [ReadOnly(true)]
        public int? OkunmaSayisi { get; set; }
        public bool OnayliMi { get; set; }
        [HiddenInput]
        public Guid UyeId { get; set; } //FK
    }
}
