using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index(int sayfa = 1, string searchString = "")
        {
            var musteriList = db.TBLMUSTERILER.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                musteriList = musteriList.Where(m => m.MUSTERIAD.Contains(searchString) || m.MUSTERISOYAD.Contains(searchString)).ToList();
            }

            var model = musteriList.ToPagedList(sayfa, 5);

            ViewBag.CurrentFilter = searchString; // Filtre değerini saklarız
            ViewBag.CurrentPage = sayfa; // Geçerli sayfa numarasını saklarız

            return View(model);
        }



        [HttpGet]  //[HttpGet] ile sadece sayfayı görüntüleriz.
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult YeniMusteri(TBLMUSTERILER musteri)
        {
            if (!ModelState.IsValid) //Müşteri adının boş geçilip geçilmediğinin kontrolü
            {
                return View("YeniMusteri");
            }
            db.TBLMUSTERILER.Add(musteri);
            db.SaveChanges();
            return View();

        }

        public ActionResult MusteriSil(int id) //Seçilen kategorinin silinmesi
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult musteriGetir(int id) //Güncellenecek olan kategorinin ID' sini getirme
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            return View("musteriGetir", musteri);
        }


        public ActionResult musteriGuncelle(TBLMUSTERILER guncellenecekMusteri) //Kategori güncelleme işlemi
        {
            var musteri = db.TBLMUSTERILER.Find(guncellenecekMusteri.MUSTERIID);
            musteri.MUSTERIAD = guncellenecekMusteri.MUSTERIAD;
            musteri.MUSTERISOYAD = guncellenecekMusteri.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}