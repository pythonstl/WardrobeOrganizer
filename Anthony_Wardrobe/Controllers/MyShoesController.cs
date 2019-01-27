using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Anthony_Wardrobe.Models;

namespace Anthony_Wardrobe.Controllers
{
    public class MyShoesController : Controller
    {
        private ClothingDatabaseEntities db = new ClothingDatabaseEntities();

        // GET: MyShoes
        public ActionResult Index()
        {
            var myShoes = db.MyShoes.Include(m => m.ShoeType);
            return View(myShoes.ToList());
        }

        // GET: MyShoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySho mySho = db.MyShoes.Find(id);
            if (mySho == null)
            {
                return HttpNotFound();
            }
            return View(mySho);
        }

        // GET: MyShoes/Create
        public ActionResult Create()
        {
            ViewBag.ShoeID = new SelectList(db.ShoeTypes, "ShoeID", "ShoeType1");
            return View();
        }

        // POST: MyShoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MyShoeID,Name,Photo,ShoeID,Color,Season,Occasion")] MySho mySho)
        {
            if (ModelState.IsValid)
            {
                db.MyShoes.Add(mySho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShoeID = new SelectList(db.ShoeTypes, "ShoeID", "ShoeType1", mySho.ShoeID);
            return View(mySho);
        }

        // GET: MyShoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySho mySho = db.MyShoes.Find(id);
            if (mySho == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShoeID = new SelectList(db.ShoeTypes, "ShoeID", "ShoeType1", mySho.ShoeID);
            return View(mySho);
        }

        // POST: MyShoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MyShoeID,Name,Photo,ShoeID,Color,Season,Occasion")] MySho mySho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mySho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShoeID = new SelectList(db.ShoeTypes, "ShoeID", "ShoeType1", mySho.ShoeID);
            return View(mySho);
        }

        // GET: MyShoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySho mySho = db.MyShoes.Find(id);
            if (mySho == null)
            {
                return HttpNotFound();
            }
            return View(mySho);
        }

        // POST: MyShoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MySho mySho = db.MyShoes.Find(id);
            db.MyShoes.Remove(mySho);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
