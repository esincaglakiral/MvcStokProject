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
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index(int sayfa = 1)
        {
            
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa, 5); //Yukarıdaki satırdan farkı, verilerin sayfalanmış bir şekilde gelmesidir. Sondaki (1,4) ise 1 sayfada 4 veri olması demektir.

            return View(degerler);
        }

        [HttpGet]  //[HttpGet] ile sadece sayfayı görüntüleriz.
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost] // sayfaya herhangi bir post işlemi yapıldğı zaman yani mesela ben butona bastığım zaman burdaki işlemi gerçekleştir aksi durumda [HttpGet] ile sadece sayfayı görüntüleriz.
        public ActionResult YeniKategori(TBLKATEGORILER kategori)
        {
            if (!ModelState.IsValid) //Kategori adının boş geçilip geçilmediğinin kontrolü
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(kategori);
            db.SaveChanges();
            return View();

        }

        public ActionResult kategoriSil(int id) //Seçilen kategorinin silinmesi
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // her actionresult sayfası post edildiğinde ya da refresh yapıldığında yeni ekleme işlemi gerçekleşiyor. o zaman benim ben butona basana kadar ekleme
        // işlemi gerçekleştirmemesi gerek. 


        public ActionResult kategoriGetir(int id) //Güncellenecek olan kategorinin ID' sini getirme
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            return View("kategoriGetir", kategori);
        }


        public ActionResult kategoriGuncelle(TBLKATEGORILER guncellenecekKategori) //Kategori güncelleme işlemi
        {
            var kategori = db.TBLKATEGORILER.Find(guncellenecekKategori.KATEGORIID);
            kategori.KATEGORIAD = guncellenecekKategori.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}