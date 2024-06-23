using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;


namespace MVCKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            //Listeleme işlemi ->ToList() metodu
            var degerler = db.TBLPERSONEL.ToList();

            return View(degerler);
        }
    }
}