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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UyeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _uyeRepository = new UyeRepository(context);
            _webHostEnvironment = webHostEnvironment;
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
    }
}
