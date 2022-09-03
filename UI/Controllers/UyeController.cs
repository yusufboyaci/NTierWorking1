using DataAccess.Context;
using DataAccess.Repositories.Concrete;
using Entities;
using Microsoft.AspNetCore.Mvc;
using UI.Models.ViewModels;

namespace UI.Controllers
{
    public class UyeController : Controller
    {
        UyeRepository _uyeRepository;
        public UyeController(ApplicationDbContext context)
        {
            _uyeRepository = new UyeRepository(context);
        }

        public IActionResult Index()
        {
            List<UyeVM> liste = new List<UyeVM>();
            UyeVM nesne = new UyeVM();
            List<Uye> uyeler = _uyeRepository.GetActive();
            if (uyeler != null)
            {
                foreach (Uye item in uyeler)
                {
                    nesne.Id = item.Id;
                    nesne.Ad = item.Ad;
                    nesne.Soyad = item.Soyad;
                    nesne.KullaniciAdi = item.KullaniciAdi;
                    nesne.KullaniciYorum = item.KullaniciYorum;
                    nesne.MailAdresi = item.MailAdresi;
                    nesne.KullaniciResimYolu = item.KullaniciResimYolu;
                    nesne.DogumGunu = item.DogumGunu;
                    nesne.OnayliMi = item.OnayliMi;
                    liste.Add(nesne);
                }
            }
            return View(liste);
        }

        //public IActionResult Index()
        //{

        //    return View(_uyeRepository.GetActive());
        //}
    }
}
