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
using KisanMitraBackEnd.Models;

namespace KisanMitraBackEnd.Controllers
{
    public class ProductsController : ApiController
    {
        private KisanMitra1Entities db = new KisanMitra1Entities();

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }


      



        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IQueryable<Product> GetProduct(int id)
        {
            /*User u = db.Users.Find(id);*/


            var p = (from a in db.Products
                     where  a.userId== id
                     select a);

            

            return p;
    
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.productId)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

       

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public Product PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            db.Products.Add(product);
            db.SaveChanges();

            return product;
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.productId == id) > 0;
        }
    }
}