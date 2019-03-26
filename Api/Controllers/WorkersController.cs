using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi;

namespace Api.Controllers
{
    /// <summary>
    /// Api Workers controller
    /// </summary>
    [RoutePrefix("API/Workers")]
    public class WorkersController : ApiController
    {
        private readonly DBase db = new DBase();

        // GET: api/Workers
        /// <summary>
        /// GetAllWorkers
        /// </summary>
        /// <returns>All Workers</returns>
        [HttpGet]
        [Route("AllWorkersInfo")]
        public IQueryable<Worker> GetWorkers()
        {
            return db.Workers;
        }

        // GET: api/Workers/5
        /// <summary>
        /// WorkerById
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Worker By Id</returns>
     
        [HttpGet]
        [ResponseType(typeof(Worker))]
        [Route("GetWorkerById/{id}") ]
        public IHttpActionResult GetWorker(int id)
        {
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }

        // PUT: api/Workers/5
        [HttpPut]
        [Route("UpdateWorker/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorker([FromUri]int id, [FromBody]Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != worker.Id)
            {
                return BadRequest();
            }

            db.Entry(worker).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
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

        // POST: api/Workers
        [HttpPost]
        [Route("CreateWorker")]
        [ResponseType(typeof(Worker))]
        public IHttpActionResult PostWorker(Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Workers.Add(worker);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = worker.Id }, worker);
        }

        // DELETE: api/Workers/5
        [ResponseType(typeof(Worker))]
        [HttpDelete]
        [Route("DeleteWorker/{id}")]
        public IHttpActionResult DeleteWorker(int id)
        {
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return NotFound();
            }

            db.Workers.Remove(worker);
            db.SaveChanges();

            return Ok(worker);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkerExists(int id)
        {
            return db.Workers.Count(e => e.Id == id) > 0;
        }
    }
}