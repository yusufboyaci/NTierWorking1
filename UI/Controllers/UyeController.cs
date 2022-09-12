using DataAccess.Context;
using DataAccess.Repositories.Concrete;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UI.Models.ViewModels;

namespace UI.Controllers
{
    public class UyeController : Controller
    {
        UyeRepository _uyeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UyeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _uyeRepository = new UyeRepository(context);
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<UyeVM> liste = new List<UyeVM>();
            List<Uye> uyeler = _uyeRepository.GetActive();
            if (uyeler != null)
            {
                foreach (Uye item in uyeler)
                {
                    UyeVM nesne = new UyeVM();
                    nesne.Id = item.Id;
                    nesne.Ad = item.Ad;
                    nesne.Soyad = item.Soyad;
                    nesne.KullaniciAdi = item.KullaniciAdi;
                    nesne.KullaniciYorum = item.KullaniciYorum;
                    nesne.MailAdresi = item.MailAdresi;
                    nesne.KullaniciResimYolu = item.KullaniciResimYolu;
                    nesne.Role = (Role?)item.Role;
                    nesne.DogumGunu = item.DogumGunu;
                    nesne.OnayliMi = item.OnayliMi;
                    liste.Add(nesne);
                }
            }
            return View(liste);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UyeVM uyeVM)
        {
            if (ModelState.IsValid)
            {
                Uye uye = new Uye();
                if (uyeVM != null)
                {
                    uye.Ad = uyeVM.Ad;
                    uye.Soyad = uyeVM.Soyad;
                    uye.KullaniciAdi = uyeVM.KullaniciAdi;
                    uye.KullaniciYorum = uyeVM.KullaniciYorum;
                    uye.MailAdresi = uyeVM.MailAdresi;
                    uye.DogumGunu = uyeVM.DogumGunu;
                    uye.Role = (Core.Enum.Role?)uyeVM.Role;
                    uye.OnayliMi = uyeVM.OnayliMi;
                    if (uyeVM.KullaniciResim != null)
                    {
                        string resim = Path.Combine(_webHostEnvironment.WebRootPath, "resimler");
                        if (uyeVM.KullaniciResim.Length > 0)
                        {
                            using (FileStream file = new FileStream(Path.Combine(resim, uyeVM.KullaniciResim.FileName), FileMode.Create))
                            {
                                uyeVM.KullaniciResim.CopyTo(file);
                            }
                            uyeVM.KullaniciResimYolu = uyeVM.KullaniciResim.FileName;
                            uye.KullaniciResimYolu = uyeVM.KullaniciResimYolu;
                        }
                        _uyeRepository.Add(uye);
                        _uyeRepository.Activate(uye.Id);
                    }
                    else
                    {
                        _uyeRepository.Add(uye);
                        _uyeRepository.Activate(uye.Id);
                    }
                }
            }
            else
            {
                return RedirectToAction("Error", "Shared");
            }
            return RedirectToAction("Index", "Uye");
        }
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            Uye uye = _uyeRepository.GetById(id);
            if (uye != null)
            {
                UyeVM uyeVM = new UyeVM();
                uyeVM.Id = uye.Id;
                uyeVM.Ad = uye.Ad;
                uyeVM.Soyad = uye.Soyad;
                uyeVM.KullaniciAdi = uye.KullaniciAdi;
                uyeVM.KullaniciYorum = uye.KullaniciYorum;
                uyeVM.MailAdresi = uye.MailAdresi;
                uyeVM.KullaniciResimYolu = uye.KullaniciResimYolu;
                uyeVM.Role = (Role?)uye.Role;
                uyeVM.DogumGunu = uye.DogumGunu;
                uyeVM.OnayliMi = uye.OnayliMi;
                return View(uyeVM);
            }
            else
            {
                return RedirectToAction("Error", "Shared");
            }
        }
        [HttpPost]
        public IActionResult Update(UyeVM uyeVM)
        {
            if (ModelState.IsValid)
            {
                Uye uye = new Uye();
                uye.Id = uyeVM.Id;
                uye.Ad = uyeVM.Ad;
                uye.Soyad = uyeVM.Soyad;
                uye.KullaniciAdi = uyeVM.KullaniciAdi;
                uye.KullaniciYorum = uyeVM.KullaniciYorum;
                uye.MailAdresi = uyeVM.MailAdresi;
                uye.DogumGunu = uyeVM.DogumGunu;
                uye.Role = (Core.Enum.Role?)uyeVM.Role;
                uye.OnayliMi = uyeVM.OnayliMi;
                if (uyeVM.KullaniciResim != null)
                {
                    string resim = Path.Combine(_webHostEnvironment.WebRootPath, "resimler");
                    if (uyeVM.KullaniciResim.Length > 0)
                    {
                        using (FileStream file = new FileStream(Path.Combine(resim, uyeVM.KullaniciResim.FileName), FileMode.Create))
                        {
                            uyeVM.KullaniciResim.CopyTo(file);
                        }
                        uyeVM.KullaniciResimYolu = uyeVM.KullaniciResim.FileName;
                        uye.KullaniciResimYolu = uyeVM.KullaniciResimYolu;
                    }
                    _uyeRepository.Update(uye);
                    _uyeRepository.Activate(uye.Id);
                }
                else
                {
                    _uyeRepository.Update(uye);
                    _uyeRepository.Activate(uye.Id);
                }
                return RedirectToAction("Index", "Uye");
            }
            return RedirectToAction("Error", "Shared");
        }
        public IActionResult Delete(Guid id)
        {
            Uye uye = _uyeRepository.GetById(id);

            _uyeRepository.Remove(uye);

            return RedirectToAction("Index", "Uye");
        }
        public IActionResult UyeInfo(Guid id)
        {
            Uye uye = _uyeRepository.GetById(id);
            if (uye != null)
            {
                UyeVM uyeVM = new UyeVM();
                uyeVM.Id = uye.Id;
                uyeVM.Ad = uye.Ad;
                uyeVM.Soyad = uye.Soyad;
                uyeVM.KullaniciAdi = uye.KullaniciAdi;
                uyeVM.KullaniciYorum = uye.KullaniciYorum;
                uyeVM.MailAdresi = uye.MailAdresi;
                uyeVM.KullaniciResimYolu = uye.KullaniciResimYolu;
                uyeVM.Role = (Role?)uye.Role;
                uyeVM.DogumGunu = uye.DogumGunu;
                uyeVM.OnayliMi = uye.OnayliMi;
                return View(uyeVM);
            }
            else
            {
                return RedirectToAction("Error", "Shared");
            }
        }
    }
}
