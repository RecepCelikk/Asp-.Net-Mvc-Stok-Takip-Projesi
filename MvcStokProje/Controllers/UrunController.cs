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
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLURUNLER.ToList();
            var degerler = db.TBLURUNLER.ToList().ToPagedList(sayfa, 5);//paging(sayfalama) kullanılmıştır.
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YenıUrun()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()

                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YenıUrun(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()

                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urun = db.TBLURUNLER.Find(p1.URUNID);
            urun.URUNAD = p1.URUNAD;
            //urun.URUNKATEGORİ = p1.URUNKATEGORİ;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORİ = ktg.KATEGORIID;
            urun.MARKA = p1.MARKA;
            urun.FİYAT = p1.FİYAT;
            urun.STOK = p1.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}