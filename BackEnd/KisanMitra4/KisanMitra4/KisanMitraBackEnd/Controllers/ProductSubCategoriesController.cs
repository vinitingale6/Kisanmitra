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
    public class ProductSubCategoriesController : ApiController
    {
        private KisanMitra1Entities db = new KisanMitra1Entities();

        // GET: api/ProductSubCategories
        public IQueryable<ProductSubCategory> GetProductSubCategories()
        {
            return db.ProductSubCategories;
        }

        // GET: api/ProductSubCategories/5
        [ResponseType(typeof(ProductSubCategory))]
        public IHttpActionResult GetProductSubCategory(int id)
        {
            ProductSubCategory productSubCategory = db.ProductSubCategories.Find(id);
            if (productSubCategory == null)
            {
                return NotFound();
            }

            return Ok(productSubCategory);
        }

        // PUT: api/ProductSubCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductSubCategory(int id, ProductSubCategory productSubCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productSubCategory.prodSubCatId)
            {
                return BadRequest();
            }

            db.Entry(productSubCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSubCategoryExists(id))
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

        // POST: api/ProductSubCategories
        [ResponseType(typeof(ProductSubCategory))]
        public IHttpActionResult PostProductSubCategory(ProductSubCategory productSubCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductSubCategories.Add(productSubCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productSubCategory.prodSubCatId }, productSubCategory);
        }

        // DELETE: api/ProductSubCategories/5
        [ResponseType(typeof(ProductSubCategory))]
        public IHttpActionResult DeleteProductSubCategory(int id)
        {
            ProductSubCategory productSubCategory = db.ProductSubCategories.Find(id);
            if (productSubCategory == null)
            {
                return NotFound();
            }

            db.ProductSubCategories.Remove(productSubCategory);
            db.SaveChanges();

            return Ok(productSubCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductSubCategoryExists(int id)
        {
            return db.ProductSubCategories.Count(e => e.prodSubCatId == id) > 0;
        }
    }
}