using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShopOnline_ProjectSem3.Models;

namespace BookShopOnline_ProjectSem3.Models
{
    public class UserFunction
    {
        BookDB_DemoEntities db = new BookDB_DemoEntities();

        // Change status client
        public bool ChangeStatuss(int id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        //Change status book
        public bool ChangeStatusBook(int id)
        {
            var book = db.Books.Find(id);
            book.Status = !book.Status;
            db.SaveChanges();
            return book.Status;
        }

        // Delete record Employee
        public bool Delete(int id)
        {
            try
            {
                var emp = db.Employees.Find(id);
                db.Employees.Remove(emp);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        // Delete record Commet
        public bool DeleteComment(int id)
        {
            try
            {
                var cmt = db.Comments.Find(id);
                db.Comments.Remove(cmt);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        // Delete record Book
        public bool DeleteBook(int id)
        {
            try
            {
                //find an genre I wanted to delete.
                Book c = db.Books.Where(a => a.BookID == id).SingleOrDefault();
                // find all the books that related to the genre
                var goods = db.FILES.Where(b => b.BookID == c.BookID).AsEnumerable();
                foreach (var bk in goods)
                {
                    var b = bk;
                    c.FILES.Remove(b);
                }
                // delete the genre
                db.Books.Remove(c);
                db.SaveChanges(); //a SQL DELETE command is generated
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        // Delete record Customer
        public bool DeleteClient(int id)
        {
            try
            {
                var cus = db.Users.Find(id);
                db.Users.Remove(cus);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //Delete record Articles
        public bool DeleteNews(int id)
        {
            try
            {
                var news = db.News.Find(id);
                db.News.Remove(news);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //Delete record FAQ
        public bool DeleteFAQ(int id)
        {
            try
            {
                var faq = db.FAQs.Find(id);
                db.FAQs.Remove(faq);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        // Delete record Author
        public bool DeleteAuthor(int id)
        {
            try
            {
                //find an Author I wanted to delete.
                Author auth = db.Authors.Where(a => a.AuthorID == id).SingleOrDefault();
                // find all the books that related to the author
                var goods = db.Books.Where(b => b.AuthorID == auth.AuthorID).AsEnumerable();
                foreach (var bk in goods)
                {
                    var b = bk;
                    auth.Books.Remove(b);
                }
                // delete the author
                db.Authors.Remove(auth);
                db.SaveChanges(); //a SQL DELETE command is generated
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        // Delete record Genre
        public bool DeleteGenre(int id)
        {
            try
            {
                //find an genre I wanted to delete.
                Genre catagories = db.Genres.Where(a => a.GenreID == id).SingleOrDefault();
                // find all the books that related to the genre
                var goods = db.Books.Where(b => b.GenreID == catagories.GenreID).AsEnumerable();
                foreach (var bk in goods)
                {
                    var b = bk;
                    catagories.Books.Remove(b); 
                }
                // delete the genre
                db.Genres.Remove(catagories);
                db.SaveChanges(); //a SQL DELETE command is generated
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<string> ListName(string keyword)
        {
            return db.Books.Where(x => x.BookName.Contains(keyword)).Select(x => x.BookName).ToList();
        }


        public List<Book> Search(string keyword, ref int totalRecord)
        {
            totalRecord = db.Books.Where(x => x.BookName == keyword).Count();
            var model = (from a in db.Books                       
                         where a.BookName.Contains(keyword)
                         select new
                         {
                             CreatedDate = a.CreatedDate,
                             ID = a.BookID,                            
                             Name = a.BookName,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new Book()
                         {

                             CreatedDate = x.CreatedDate,
                             BookName = x.Name,
                             BookID = x.ID,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate);
            return model.ToList();
        }
        //public User ViewDetail( int id)
        //{
        //    return db.Users.Find(id);
        //}

        //// update userprofile
        //public bool UpdateUser(User entity)
        //{
        //    try
        //    {
        //        var client = db.Users.Find(entity.UserID);
        //        client.FirstName = entity.FirstName;
        //        client.LastName = entity.LastName;
        //        client.Address = entity.Address;
        //        client.Email = entity.Email;
        //        client.Phone = entity.Phone;
        //        return true;


        //    }catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}

        // Delete record feedback
        public bool DeleteFeedback(int id)
        {
            try
            {
                var fb = db.Feedbacks.Find(id);
                db.Feedbacks.Remove(fb);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }




    }
}