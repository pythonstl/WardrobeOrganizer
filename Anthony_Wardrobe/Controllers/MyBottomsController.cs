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
    public class MyBottomsController : Controller
    {
        private ClothingDatabaseEntities db = new ClothingDatabaseEntities();

        // GET: MyBottoms
        public ActionResult Index()
        {
            var myBottoms = db.MyBottoms.Include(m => m.BottomType);
            return View(myBottoms.ToList());
        }

        // GET: MyBottoms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyBottom myBottom = db.MyBottoms.Find(id);
            if (myBottom == null)
            {
                return HttpNotFound();
            }
            return View(myBottom);
        }

        // GET: MyBottoms/Create
        public ActionResult Create()
        {
            ViewBag.BottomID = new SelectList(db.BottomTypes, "BottomID", "BottomType1");
            return View();
        }

        // POST: MyBottoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MyBottomID,Name,Photo,BottomID,Color,Season,Occasion")] MyBottom myBottom)
        {
            if (ModelState.IsValid)
            {
                db.MyBottoms.Add(myBottom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BottomID = new SelectList(db.BottomTypes, "BottomID", "BottomType1", myBottom.BottomID);
            return View(myBottom);
        }

        // GET: MyBottoms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyBottom myBottom = db.MyBottoms.Find(id);
            if (myBottom == null)
            {
                return HttpNotFound();
            }
            ViewBag.BottomID = new SelectList(db.BottomTypes, "BottomID", "BottomType1", myBottom.BottomID);
            return View(myBottom);
        }

        // POST: MyBottoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MyBottomID,Name,Photo,BottomID,Color,Season,Occasion")] MyBottom myBottom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myBottom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BottomID = new SelectList(db.BottomTypes, "BottomID", "BottomType1", myBottom.BottomID);
            return View(myBottom);
        }

        // GET: MyBottoms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyBottom myBottom = db.MyBottoms.Find(id);
            if (myBottom == null)
            {
                return HttpNotFound();
            }
            return View(myBottom);
        }

        // POST: MyBottoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyBottom myBottom = db.MyBottoms.Find(id);
            db.MyBottoms.Remove(myBottom);
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
