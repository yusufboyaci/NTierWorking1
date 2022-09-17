using Core.Enum;
using DataAccess.Context;
using DataAccess.Repositories.Concrete;
using Entities;
using Entities.Mapping;
using Microsoft.AspNetCore.Mvc;
using UI.Models.ViewModels;

namespace UI.Controllers
{
    public class MakaleController : Controller
    {
        MakaleRepository _makaleRepository;
        UyeRepository _uyeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MakaleController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _makaleRepository = new MakaleRepository(context);
            _uyeRepository = new UyeRepository(context);
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(Guid id)
        {
            List<MakaleVM> liste = new List<MakaleVM>();
            List<Makale> makaleler = _makaleRepository.GetDefault(x => x.UyeId == id && x.Status == Status.Active);
            if (makaleler != null)
            {
                foreach (var item in makaleler)
                {
                    MakaleVM makaleVM = new MakaleVM();
                    makaleVM.Id = item.Id;
                    makaleVM.UyeId = item.UyeId;
                    makaleVM.MakaleBasligi = item.MakaleBasligi;
                    makaleVM.MakaleIcerigi = item.MakaleIcerigi;
                    makaleVM.ResimYolu = item.ResimYolu;
                    makaleVM.OkunmaSayisi = item.OkunmaSayisi;
                    makaleVM.OnayliMi = item.OnayliMi;
                    liste.Add(makaleVM);
                }
            }
            return View(liste);
        }
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(MakaleVM makaleVM, Guid id)
        {
            if (ModelState.IsValid && makaleVM != null)
            {
               // makaleVM.OkunmaSayisi = 
                Makale makale = new Makale();
                makale.UyeId = id;
                makale.MakaleBasligi = makaleVM.MakaleBasligi;
                makale.MakaleIcerigi = makaleVM.MakaleIcerigi;
                makale.OkunmaSayisi = makaleVM.OkunmaSayisi;
                makale.OnayliMi = makaleVM.OnayliMi;
                if (makaleVM.Resim != null)
                {
                    string resim = Path.Combine(_webHostEnvironment.WebRootPath, "resimler");
                    if (makaleVM.Resim.Length > 0)
                    {
                        using (FileStream file = new FileStream(Path.Combine(resim, makaleVM.Resim.FileName), FileMode.Create))
                        {
                            makaleVM.Resim.CopyTo(file);
                        }
                        makaleVM.ResimYolu = makaleVM.Resim.FileName;
                        makale.ResimYolu = makaleVM.ResimYolu;
                    }
                    _makaleRepository.Add(makale);
                    _makaleRepository.Activate(makale.Id);
                }
                else
                {
                    _makaleRepository.Add(makale);
                    _makaleRepository.Activate(makale.Id);
                }
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index", "Makale", new { id = id });
        }
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            Makale makale = _makaleRepository.GetById(id);
            if (makale != null)
            {
                MakaleVM makaleVM = new MakaleVM();
                makaleVM.Id = makale.Id;
                makaleVM.UyeId = makale.UyeId;
                makaleVM.MakaleIcerigi = makale.MakaleIcerigi;
                makaleVM.MakaleBasligi = makale.MakaleBasligi;
                makaleVM.ResimYolu = makale.ResimYolu;
                makaleVM.OkunmaSayisi = makale.OkunmaSayisi;
                makaleVM.OnayliMi = makale.OnayliMi;
                return View(makaleVM);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public IActionResult Update(MakaleVM makaleVM)
        {
            if (ModelState.IsValid && makaleVM != null)
            {
                Makale makale = new Makale();
                makale.Id = makaleVM.Id;
                makale.UyeId = makaleVM.UyeId;
                makale.MakaleBasligi = makaleVM.MakaleBasligi;
                makale.MakaleIcerigi = makaleVM.MakaleIcerigi;
                makale.OkunmaSayisi = makaleVM.OkunmaSayisi;
                makale.OnayliMi = makaleVM.OnayliMi;
                if (makaleVM.Resim != null)
                {
                    string resim = Path.Combine(_webHostEnvironment.WebRootPath, "resimler");
                    if (makaleVM.Resim.Length > 0)
                    {
                        using (FileStream file = new FileStream(Path.Combine(resim, makaleVM.Resim.FileName), FileMode.Create))
                        {
                            makaleVM.Resim.CopyTo(file);
                        }
                        makaleVM.ResimYolu = makaleVM.Resim.FileName;
                        makale.ResimYolu = makaleVM.ResimYolu;
                    }
                    _makaleRepository.Update(makale);
                    _makaleRepository.Activate(makale.Id);
                }
                else
                {
                    _makaleRepository.Update(makale);
                    _makaleRepository.Activate(makale.Id);
                }
                return RedirectToAction("Index", "Makale", new { id = makaleVM.UyeId });
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Makale makale = _makaleRepository.GetById(id);
            if (makale != null)
            {
                MakaleVM makaleVM = new MakaleVM();
                makaleVM.Id = makale.Id;
                makaleVM.UyeId = makale.UyeId;
                makaleVM.MakaleIcerigi = makale.MakaleIcerigi;
                makaleVM.MakaleBasligi = makale.MakaleBasligi;
                makaleVM.ResimYolu = makale.ResimYolu;
                makaleVM.OkunmaSayisi = makale.OkunmaSayisi;
                makaleVM.OnayliMi = makale.OnayliMi;
                return View(makaleVM);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public IActionResult Delete(MakaleVM makaleVM)
        {
            if (makaleVM != null)
            {
                _makaleRepository.Remove(makaleVM.Id);
                return RedirectToAction("Index", "Makale", new { id = makaleVM.UyeId });
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public IActionResult MakaleGoster(Guid id)
        {
            Makale makale = _makaleRepository.GetById(id);
            makale.OkunmaSayisi++;
            _makaleRepository.Update(makale);
            if (makale != null)
            {
                MakaleVM makaleVM = new MakaleVM();
                makaleVM.Id = makale.Id;
                makaleVM.UyeId = makale.UyeId;
                makaleVM.MakaleIcerigi = makale.MakaleIcerigi;
                makaleVM.MakaleBasligi = makale.MakaleBasligi;
                makaleVM.ResimYolu = makale.ResimYolu;
                makaleVM.OkunmaSayisi = makale.OkunmaSayisi;
                makaleVM.OnayliMi = makale.OnayliMi;
                return View(makaleVM);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
