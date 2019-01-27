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
    public class MyTopsController : Controller
    {
        private ClothingDatabaseEntities db = new ClothingDatabaseEntities();

        // GET: MyTops
        public ActionResult Index()
        {
            var myTops = db.MyTops.Include(m => m.TopType);
            return View(myTops.ToList());
        }

        // GET: MyTops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyTop myTop = db.MyTops.Find(id);
            if (myTop == null)
            {
                return HttpNotFound();
            }
            return View(myTop);
        }

        // GET: MyTops/Create
        public ActionResult Create()
        {
            ViewBag.TopID = new SelectList(db.TopTypes, "TopID", "TopType1");
            return View();
        }

        // POST: MyTops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MyTopID,Name,Photo,TopID,Color,Season,Occasion")] MyTop myTop)
        {
            if (ModelState.IsValid)
            {
                db.MyTops.Add(myTop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TopID = new SelectList(db.TopTypes, "TopID", "TopType1", myTop.TopID);
            return View(myTop);
        }

        // GET: MyTops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyTop myTop = db.MyTops.Find(id);
            if (myTop == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopID = new SelectList(db.TopTypes, "TopID", "TopType1", myTop.TopID);
            return View(myTop);
        }

        // POST: MyTops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MyTopID,Name,Photo,TopID,Color,Season,Occasion")] MyTop myTop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myTop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopID = new SelectList(db.TopTypes, "TopID", "TopType1", myTop.TopID);
            return View(myTop);
        }

        // GET: MyTops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyTop myTop = db.MyTops.Find(id);
            if (myTop == null)
            {
                return HttpNotFound();
            }
            return View(myTop);
        }

        // POST: MyTops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyTop myTop = db.MyTops.Find(id);
            db.MyTops.Remove(myTop);
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
