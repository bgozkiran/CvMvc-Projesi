using CvMvc.Models.Entity;
using CvMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace CvMvc.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        AnasayfaRepository repo = new AnasayfaRepository();
        public ActionResult Index()
        {
            var hero = repo.List();
            return View(hero);
        }
        [HttpGet]
        public ActionResult AnasayfaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AnasayfaEkle(TblAnasayfa p)
        {
            repo.TAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult AnasayfaSil(int id)
        {
            TblAnasayfa t = repo.Find(x => x.ID == id);
            repo.TDelete(t);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AnasayfaDuzenle(int id)
        {
            TblAnasayfa t = repo.Find(x => x.ID == id);
            return View(t);
        }
        [HttpPost]
        public ActionResult AnasayfaDuzenle(TblAnasayfa p)
        {
            TblAnasayfa t = repo.Find(x => x.ID == p.ID);
            t.Resim = p.Resim;
            t.AdSoyad = p.AdSoyad;
            t.Aciklama = p.Aciklama;
            repo.TUpdate(t);
            return RedirectToAction("Index");
        }

    }
}