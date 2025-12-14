using CvMvc.Models.Entity;
using CvMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CvMvc.Controllers
{
    public class HakkimdaController : Controller
    {
        // GET: Hakkimda
        GenericRepository<TblHakkimda> repo = new GenericRepository<TblHakkimda>();
        public ActionResult Index()
        {
            var hakkimda = repo.List();
            return View(hakkimda);
        }
        public ActionResult HakkimdaSil(int id)
        {
            var hakkimda = repo.Find(x => x.ID == id);
            repo.TDelete(hakkimda);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult HakkimdaDuzenle(int id)
        {
            var hakkimda = repo.Find(x => x.ID == id);
            return View(hakkimda);
        }
        [HttpPost]
        public ActionResult HakkimdaDuzenle(TblHakkimda t)
        {
            var hakkimda = repo.Find(x => x.ID == t.ID);
            hakkimda.Aciklama1 = t.Aciklama1;
            hakkimda.Aciklama2 = t.Aciklama2;
            repo.TUpdate(hakkimda);
            return RedirectToAction("Index");
        }
    }
}