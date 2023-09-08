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
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace KisanMitraBackEnd.Controllers
{
    public class CartsController : ApiController
    {
        

        private KisanMitra1Entities db = new KisanMitra1Entities();

        // GET: api/Carts
        public IQueryable<Cart> GetCarts()
        {
            return db.Carts;
        }

        // GET: api/Carts/5
        [ResponseType(typeof(Cart))]
        public IQueryable<Product> GetCart(int id)//where id =userId
        {
            /*  var jsonSerializerSettings = new JsonSerializerSettings
              {
                  ReferenceLoopHandling = ReferenceLoopHandling.Ignore
              };


              var c = (from a in db.Carts
                       where a.userId == id
                       select a).ToList(); // Fetch the user's cart items into a list








              var productIdsInCart = c.Select(item => item.productId).ToList(); // Extract product IDs from the cart items

              var p = (from product in db.Products
                       where productIdsInCart.Contains(product.productId)
                       select product); // Fetch the products based on the product IDs from the cart

              // Now 'p' contains a list of products that are in the user's cart
              var json = JsonConvert.SerializeObject(p, Formatting.Indented, jsonSerializerSettings);
              //Console.WriteLine(p);
  */

            var query = from product in db.Products
                        where (from cart in db.Carts
                               where cart.userId == id
                               select cart.productId)
                              .Contains(product.productId)
                        select product;


            return query;
        }

        // PUT: api/Carts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCart(int id, Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cart.cartId)
            {
                return BadRequest();
            }

            db.Entry(cart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        [ResponseType(typeof(Cart))]
        public IHttpActionResult PostCart(Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Carts.Add(cart);
            db.SaveChanges();

            return Ok(cart);
        }

        // DELETE: api/Carts/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult DeleteCart(int id)
        {
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return NotFound();
            }

            db.Carts.Remove(cart);
            db.SaveChanges();

            return Ok(cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartExists(int id)
        {
            return db.Carts.Count(e => e.cartId == id) > 0;
        }
    }
}