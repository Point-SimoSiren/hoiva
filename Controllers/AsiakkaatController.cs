using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hoivasovellus.Models;

namespace Hoivasovellus.Controllers
{
    public class AsiakkaatController : Controller
    {
        private hoivadbEntities2 db = new hoivadbEntities2();

        // GET: Asiakkaat
        public async Task<ActionResult> Index()
        {
            return View(await db.Asiakkaat.ToListAsync());
        }

        // GET: Asiakkaat/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaat asiakkaat = await db.Asiakkaat.FindAsync(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaat);
        }

        // GET: Asiakkaat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asiakkaat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AsiakasID,Etunimi,Sukunimi,Puhelin,Lääkitykset,Sotu")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                db.Asiakkaat.Add(asiakkaat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(asiakkaat);
        }

        // GET: Asiakkaat/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaat asiakkaat = await db.Asiakkaat.FindAsync(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaat);
        }

        // POST: Asiakkaat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AsiakasID,Etunimi,Sukunimi,Puhelin,Lääkitykset,Sotu")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asiakkaat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(asiakkaat);
        }

        // GET: Asiakkaat/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaat asiakkaat = await db.Asiakkaat.FindAsync(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaat);
        }

        // POST: Asiakkaat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Asiakkaat asiakkaat = await db.Asiakkaat.FindAsync(id);
            db.Asiakkaat.Remove(asiakkaat);
            await db.SaveChangesAsync();
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
