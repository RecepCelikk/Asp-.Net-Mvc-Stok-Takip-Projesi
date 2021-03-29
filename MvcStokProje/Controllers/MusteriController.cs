using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokProje.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcStokProje.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLMUSTERILER.ToList();
            var degerler = db.TBLMUSTERILER.ToList().ToPagedList(sayfa,5);//sayfalama(paging) kullanılmıştır.
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YenıMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YenıMusteri(TBLMUSTERILER p1)
        {
            if(!ModelState.IsValid)
            {
                return View("YenıMusteri");
            }
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var musterı = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musterı);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir", mus);
        }
        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var mus = db.TBLMUSTERILER.Find(p1.MUSTERIID);
            mus.MUSTERIAD = p1.MUSTERIAD;
            mus.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}