using KisanMitraBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KisanMitraBackEnd.Controllers
{
    [RoutePrefix("api/Admin")]

    public class AdminController : ApiController
    {

        private KisanMitra1Entities db = new KisanMitra1Entities();


        /*  public IQueryable<Product> GetProducts([FromBody] int user)
          {



              var p = (from a in db.Products
                       where user == a.userId
                       select a);



              return p;
          }
  */
        /*
                [ResponseType(typeof(Product))]
                public IHttpActionResult GetProduct(int id)
                {
                    User u=db.Users.Find(id);


                    var p = (from a in db.Products
                             where u.userId == a.userId
                             select a);

                    if (p == null)
                    {
                        return NotFound();
                    }

                    return Ok(p);



                }
        */
        [HttpGet]
        [Route("GetUsers")]
        public IQueryable<User> GetUsers()
        {


            var p = (from a in db.Users
                     where a.roleType == "Seller"
                     select a);



            return p;
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]

        /*    [ResponseType(typeof(User))]*/
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }


    }
}
