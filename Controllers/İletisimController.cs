using CvMvc.Models.Entity;
using CvMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CvMvc.Controllers
{
    public class İletisimController : Controller
    {
        // GET: İletisim
        GenericRepository<Tblİletisim> repo = new GenericRepository<Tblİletisim>();
        public ActionResult Index()
        {
            var mesaj = repo.List();
            return View(mesaj);
        }
        public ActionResult MesajSil(int id)
        {
            var mesaj = repo.Find(x => x.ID == id);
            repo.TDelete(mesaj);
            return RedirectToAction("Index");
        }
    }
}