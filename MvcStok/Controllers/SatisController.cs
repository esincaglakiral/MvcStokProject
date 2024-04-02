using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis

        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SatisTablosu()
        {
            var satislar = db.TBLSATISLAR.ToList();
            return View(satislar);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(TBLSATISLAR satilacakUrun)
        {
            db.TBLSATISLAR.Add(satilacakUrun);
            db.SaveChanges();
            return View("Index");
        }
    }
}