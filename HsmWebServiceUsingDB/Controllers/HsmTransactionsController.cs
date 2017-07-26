using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HsmWebServiceUsingDB.Models;

namespace HsmWebServiceUsingDB.Controllers
{
    public class HsmTransactionsController : ApiController
    {
        private HsmServiceDBContext db = new HsmServiceDBContext();

        // GET: api/HsmTransactions
        public IQueryable<HsmTransaction> GetHsmTransactions()
        {
            return db.HsmTransactions;
        }

        // GET: api/HsmTransactions/5
        [ResponseType(typeof(HsmTransaction))]
        public async Task<IHttpActionResult> GetHsmTransaction(Guid id)
        {
            HsmTransaction hsmTransaction = await db.HsmTransactions.FindAsync(id);
            if (hsmTransaction == null)
            {
                return NotFound();
            }

            return Ok(hsmTransaction);
        }

        // PUT: api/HsmTransactions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHsmTransaction(Guid id, HsmTransaction hsmTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hsmTransaction.TransactionSeq)
            {
                return BadRequest();
            }

            db.Entry(hsmTransaction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HsmTransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HsmTransactions
        [ResponseType(typeof(HsmTransaction))]
        public async Task<IHttpActionResult> PostHsmTransaction(HsmTransaction hsmTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HsmTransactions.Add(hsmTransaction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HsmTransactionExists(hsmTransaction.TransactionSeq))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hsmTransaction.TransactionSeq }, hsmTransaction);
        }

        // DELETE: api/HsmTransactions/5
        [ResponseType(typeof(HsmTransaction))]
        public async Task<IHttpActionResult> DeleteHsmTransaction(Guid id)
        {
            HsmTransaction hsmTransaction = await db.HsmTransactions.FindAsync(id);
            if (hsmTransaction == null)
            {
                return NotFound();
            }

            db.HsmTransactions.Remove(hsmTransaction);
            await db.SaveChangesAsync();

            return Ok(hsmTransaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HsmTransactionExists(Guid id)
        {
            return db.HsmTransactions.Count(e => e.TransactionSeq == id) > 0;
        }
    }
}