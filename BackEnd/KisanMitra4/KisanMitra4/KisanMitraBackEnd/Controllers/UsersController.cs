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
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private KisanMitra1Entities db = new KisanMitra1Entities();


        [Route("Login")]

        /* [ResponseType(typeof(User))]*/
        [HttpPost]
        public IHttpActionResult Login([FromBody] User user)
        {
            var UserBy = (from users in db.Users
                          where users.email == user.email && users.password == user.password
                          select users).FirstOrDefault();

            if (UserBy != null)
            {
                return Ok(UserBy);
            }
            else
            {
                return NotFound();
            }


        }




        
        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.userId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        [HttpPost]
        [Route("Registration")]

        /*public string Registration([FromBody]User usr)
        {
            if (!ModelState.IsValid)
            {
                return "Invalid Data";
            }

            db.Users.Add(usr);
            db.SaveChanges();
            return "User SAVED";



        }*/
        // POST: api/Users
        [ResponseType(typeof(User))]
        public String Registration([FromBody] User user)
                            {
            if (!ModelState.IsValid)
            {
                return "Invalid Credential";
            }

            db.Users.Add(user);
            if (db.SaveChanges() != 0)
                // return Ok(user);
                return "Registration Successfully";
            else
                return "Invalid Credential";
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.userId == id) > 0;
        }
    }
}