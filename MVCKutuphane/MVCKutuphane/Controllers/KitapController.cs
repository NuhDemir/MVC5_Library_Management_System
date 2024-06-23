using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index(string p)//Listeleme yapıyoruz.
        {
            var kitaplar = from k in db.TBLKITAP select k;
            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m => m.AD.Contains(p));

            }
            return View(kitaplar.ToList());
        }

        [HttpGet] // HTTP Get isteği almak için Action'ı belirtir.
        public ActionResult KitapEkle() // KitapEkle Action'ı
        {
            // Veritabanından TBLKATEGORI tablosundaki verileri liste olarak alır.
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                               // Her bir öğe için SelectListItem oluşturur.
                                           select new SelectListItem
                                           {
                                               Text = i.AD, // Öğenin metin değerini ayarlar.
                                               Value = i.ID.ToString() // Öğenin değerini ayarlar.
                                           }).ToList(); // Sonuçları liste olarak döndürür.

            ViewBag.dgr1 = deger1; // View tarafından kullanılmak üzere deger1'i ViewBag üzerinden taşır.


            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + " " + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            return View(); // KitapEkle view'ını döndürür.
        }

        [HttpPost]
        public ActionResult KitapEkle(TBLKITAP p)
        {
            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(k => k.ID == p.ID).FirstOrDefault();
            p.TBLKATEGORI = ktg;
            p.TBLYAZAR = yzr;
            db.TBLKITAP.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapSil(int id)
        {
            var kitap = db.TBLKITAP.Find(id);
            db.TBLKITAP.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapGetir(int id)
        {
            var ktp = db.TBLKITAP.Find(id);

            //Kategori DropDown İşlemi
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                               // Her bir öğe için SelectListItem oluşturur.
                                           select new SelectListItem
                                           {
                                               Text = i.AD, // Öğenin metin değerini ayarlar.
                                               Value = i.ID.ToString() // Öğenin değerini ayarlar.
                                           }).ToList(); // Sonuçları liste olarak döndürür.

            ViewBag.dgr1 = deger1; // View tarafından kullanılmak üzere deger1'i ViewBag üzerinden taşır.

            //Yazar DropDown İşlemi
            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + " " + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            return View("KitapGetir", ktp);
        }

        public ActionResult KitapGuncelle(TBLKITAP p)
        {
            var kitap = db.TBLKITAP.Find(p.ID);
            kitap.AD = p.AD;
            kitap.BASIMYIL = p.BASIMYIL;
            kitap.SAYFA = p.SAYFA;
            kitap.YAYINEVI = p.YAYINEVI;

            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
            kitap.KATEGORI = ktg.ID;
            kitap.YAZAR = yzr.ID;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}