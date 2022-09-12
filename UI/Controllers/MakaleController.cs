﻿using Core.Enum;
using DataAccess.Context;
using DataAccess.Repositories.Concrete;
using Entities;
using Microsoft.AspNetCore.Mvc;
using UI.Models.ViewModels;

namespace UI.Controllers
{
    public class MakaleController : Controller
    {
        MakaleRepository _makaleRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MakaleController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _makaleRepository = new MakaleRepository(context);
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
        public IActionResult Create(MakaleVM makaleVM)
        {
            if (ModelState.IsValid && makaleVM != null)
            {
                Makale makale = new Makale();
                makale.Id = makaleVM.Id;
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
                return RedirectToAction("Error", "Shared");
            }
            return RedirectToAction("Index", "Uye");
        }

        public IActionResult MakaleGoster(Guid id)
        {
            Makale makale = _makaleRepository.GetByDefault(x => x.UyeId == id);
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
                return RedirectToAction("Error", "Shared");
            }
        }
    }
}
