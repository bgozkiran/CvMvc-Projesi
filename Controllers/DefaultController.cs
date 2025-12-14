using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CvMvc.Models.Entity;

namespace CvMvc.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        // GET: Default
        CvDbEntities db = new CvDbEntities();
        public ActionResult Index()
        {
            var anasayfa = db.TblAnasayfa.ToList();
            return View(anasayfa);
        }
        public ActionResult Hakkimda()
        {
            var degerler = db.TblHakkimda.ToList();
            return PartialView(degerler);
        }
        public ActionResult SosyalMedya()
        {
            var sosyalmedya = db.TblSosyalMedya.Where(x => x.Durum == true).ToList();
            return PartialView(sosyalmedya);
        }
       
        public ActionResult Egitimlerim()
        {
            var egitim = db.TblEgitimlerim.ToList();
            return PartialView(egitim);
        }
        public ActionResult Deneyimlerim()
        {
            var deneyim = db.TblDeneyimlerim.ToList();
            return PartialView(deneyim);
        }
       
        public ActionResult Yeteneklerim()
        {
            var yetenek = db.TblYeteneklerim.ToList();
            return PartialView(yetenek);
        }
        public ActionResult Sertifikalarim()
        {
            var sertifika = db.TblSertifikalarim.ToList();
            return PartialView(sertifika);
        }
        public ActionResult Projelerim()
        {
            var projeler = db.TblProjelerim.OrderByDescending(x => x.ID).ToList();
            return PartialView(projeler);
        }
        [HttpGet]
        public PartialViewResult İletisim()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult İletisim(Tblİletisim t)
        {
            try
            {
                t.Tarih = DateTime.Now; // Parse uğraşma, direkt DateTime ver
                db.Tblİletisim.Add(t);
                db.SaveChanges();

                // Kayıt başarılı olursa anasayfaya yönlendir
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                // Hangi alan patlıyor, konsola / debug output'a yazalım
                foreach (var eve in ex.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName,
                            ve.ErrorMessage);
                    }
                }

                // Kullanıcıya genel bir mesaj göstermek istersen:
                ViewBag.Hata = "Mesajınız kaydedilirken bir hata oluştu. Lütfen tüm alanları doldurduğunuzdan emin olun.";

                // Aynı view'e modeli ile geri dön
                return View("İletisim", t);
            }
        }
    }
}