using CvMvc.Models.Entity;
using CvMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CvMvc.Controllers
{
    public class DeneyimController : Controller
    {
        // GET: Deneyim
        GenericRepository<TblDeneyimlerim> repo = new GenericRepository<TblDeneyimlerim>();
        public ActionResult Index()
        {
            var egitim = repo.List();
            return View(egitim);
        }
        [HttpGet]
        public ActionResult DeneyimEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeneyimEkle(TblDeneyimlerim p)
        {
            repo.TAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult DeneyimSil(int id)
        {
            var deneyim = repo.Find(x => x.ID == id);
            repo.TDelete(deneyim);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DeneyimDuzenle(int id)
        {
            var deneyim = repo.Find(x => x.ID == id);
            return View(deneyim);
        }
        [HttpPost]
        public ActionResult DeneyimDuzenle(TblDeneyimlerim t)
        {
            var deneyim = repo.Find(x => x.ID == t.ID);
            deneyim.Baslik = t.Baslik;
            deneyim.AltBaslik = t.AltBaslik;
            deneyim.Aciklama = t.Aciklama;
            deneyim.Tarih = t.Tarih;
            repo.TUpdate(deneyim);
            return RedirectToAction("Index");
        }
    }
}