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
    public class MyAccessoriesController : Controller
    {
        private ClothingDatabaseEntities db = new ClothingDatabaseEntities();

        // GET: MyAccessories
        public ActionResult Index()
        {
            var myAccessories = db.MyAccessories.Include(m => m.AccessoryType);
            return View(myAccessories.ToList());
        }

        // GET: MyAccessories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAccessory myAccessory = db.MyAccessories.Find(id);
            if (myAccessory == null)
            {
                return HttpNotFound();
            }
            return View(myAccessory);
        }

        // GET: MyAccessories/Create
        public ActionResult Create()
        {
            ViewBag.AccessoryID = new SelectList(db.AccessoryTypes, "AccessoryID", "AccessoryType1");
            return View();
        }

        // POST: MyAccessories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MyAccessoryID,Name,Photo,AccessoryID,Color,Season,Occasion")] MyAccessory myAccessory)
        {
            if (ModelState.IsValid)
            {
                db.MyAccessories.Add(myAccessory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccessoryID = new SelectList(db.AccessoryTypes, "AccessoryID", "AccessoryType1", myAccessory.AccessoryID);
            return View(myAccessory);
        }

        // GET: MyAccessories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAccessory myAccessory = db.MyAccessories.Find(id);
            if (myAccessory == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccessoryID = new SelectList(db.AccessoryTypes, "AccessoryID", "AccessoryType1", myAccessory.AccessoryID);
            return View(myAccessory);
        }

        // POST: MyAccessories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MyAccessoryID,Name,Photo,AccessoryID,Color,Season,Occasion")] MyAccessory myAccessory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myAccessory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccessoryID = new SelectList(db.AccessoryTypes, "AccessoryID", "AccessoryType1", myAccessory.AccessoryID);
            return View(myAccessory);
        }

        // GET: MyAccessories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAccessory myAccessory = db.MyAccessories.Find(id);
            if (myAccessory == null)
            {
                return HttpNotFound();
            }
            return View(myAccessory);
        }

        // POST: MyAccessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyAccessory myAccessory = db.MyAccessories.Find(id);
            db.MyAccessories.Remove(myAccessory);
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
