using CvMvc.Models.Entity;
using CvMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CvMvc.Controllers
{
    public class ProjeController : Controller
    {
        // GET: Proje
        GenericRepository<TblProjelerim> repo = new GenericRepository<TblProjelerim>();
        public ActionResult Index()
        {
            var sertifika = repo.List();
            return View(sertifika);
        }
        [HttpGet]
        public ActionResult ProjeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProjeEkle(TblProjelerim p)
        {
            if (!ModelState.IsValid)
            {
                return View("ProjeEkle");
            }
            repo.TAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult ProjeSil(int id)
        {
            TblProjelerim proje = repo.Find(x => x.ID == id);
            repo.TDelete(proje);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ProjeDuzenle(int id)
        {
            TblProjelerim proje = repo.Find(x => x.ID == id);
            ViewBag.d = id;
            return View(proje);
        }
        [HttpPost]
        public ActionResult ProjeDuzenle(TblProjelerim t)
        {
            var y = repo.Find(x => x.ID == t.ID);
            y.Kategori = t.Kategori;
            y.ProjeAdi = t.ProjeAdi;
            y.Teknolojiler = t.Teknolojiler;
            y.Yil = t.Yil;
            repo.TUpdate(t);
            return RedirectToAction("Index");
        }
    }
}