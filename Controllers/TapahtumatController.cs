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
    public class TapahtumatController : Controller
    {
        private hoivaEntities db = new hoivaEntities();


        //GET tapahtumat hakusanalla

        public async Task <ActionResult> Index(string searching)
        {
            return View(await db.Tapahtumat.Where(x => x.Asiakkaat.Etunimi.Contains(searching) || searching == null).ToListAsync());
        }



        // GET: Tapahtumat kirjatun pvm ajan mukaan järjestettynä, ei id:n eli luontijärjestyksen

        public async Task<ActionResult> Aika()
        {

            var tapahtumat = (from t in db.Tapahtumat
                              orderby t.TapahtumaAika
                              select t);

            return View(await tapahtumat.ToListAsync());
        }


        // GET: Tapahtumat/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tapahtumat tapahtumat = await db.Tapahtumat.FindAsync(id);
            if (tapahtumat == null)
            {
                return HttpNotFound();
            }
            return View(tapahtumat);
        }

        // GET: Tapahtumat/Create
        public ActionResult Create()
        {
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi");
            return View();
        }

        // POST: Tapahtumat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TapahtumaID,Otsikko,Teksti,Kirjaaja,TapahtumaAika,AsiakasID")] Tapahtumat tapahtumat)
        {
            if (ModelState.IsValid)
            {
                db.Tapahtumat.Add(tapahtumat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi", tapahtumat.AsiakasID);
            return View(tapahtumat);
        }

        // GET: Tapahtumat/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tapahtumat tapahtumat = await db.Tapahtumat.FindAsync(id);
            if (tapahtumat == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi", tapahtumat.AsiakasID);
            return View(tapahtumat);
        }

        // POST: Tapahtumat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TapahtumaID,Otsikko,Teksti,Kirjaaja,TapahtumaAika,AsiakasID")] Tapahtumat tapahtumat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tapahtumat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi", tapahtumat.AsiakasID);
            return View(tapahtumat);
        }

        // GET: Tapahtumat/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tapahtumat tapahtumat = await db.Tapahtumat.FindAsync(id);
            if (tapahtumat == null)
            {
                return HttpNotFound();
            }
            return View(tapahtumat);
        }

        // POST: Tapahtumat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tapahtumat tapahtumat = await db.Tapahtumat.FindAsync(id);
            db.Tapahtumat.Remove(tapahtumat);
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
