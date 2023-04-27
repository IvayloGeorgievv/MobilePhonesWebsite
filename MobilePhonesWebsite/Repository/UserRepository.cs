using MobilePhonesWebsite.Models;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Collections.Generic;
using MobilePhonesWebsite.Data;
using System.Data.Entity;
using System.Security.Principal;
using MobilePhonesWebsite.ViewModels.UserVM;
using Scrypt;

namespace MobilePhonesWebsite.Repository
{
    public class UserRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        private ScryptEncoder scryptEncoder = new ScryptEncoder();

        public static List<User> Items { get; set; }
        static UserRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.Users.ToList();
        }
        public bool UserExisting(int id)
        {
            foreach (var item in applicationDbContext.Users)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddUser(User item)
        {
            User user = new User();

            user.Username = item.Username;
            user.Email = item.Email;
            user.Password = item.Password;
            user.IsAdmin = item.IsAdmin;
            applicationDbContext.Users.Add(user);

            applicationDbContext.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            User user = applicationDbContext.Users.Find(id);

            applicationDbContext.Users.Remove(user);
            applicationDbContext.SaveChanges();
        }

        public void UpdateYourUserAcc(EditUserVM item)
        {
            User user = applicationDbContext.Users.Find(item.Id);

            if (user != null)
            {
                user.Id = item.Id;
                user.Username = item.Username;
                user.Email = item.Email;
                user.Password = scryptEncoder.Encode(item.Password);
                user.IsAdmin = user.IsAdmin;

                applicationDbContext.Entry(user).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }
        public void UpdateUser(EditUserVM item)
        {
            User user = applicationDbContext.Users.Find(item.Id);

            if (user != null)
            {
                user.Id = item.Id;
                user.Username = item.Username;
                user.Email = item.Email;
                user.Password = scryptEncoder.Encode(item.Password);
                user.IsAdmin = item.IsAdmin;

                applicationDbContext.Entry(user).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }
        public List<User> GetAllUsers()
        {
            IQueryable<User> query = applicationDbContext.Users;
           
            return query.OrderBy(x => x.Id).ToList();
        }
        public User GetByEmailAndPassword(string email, string password)
        {
            ScryptEncoder scryptEncoder = new ScryptEncoder();
            foreach (var user in applicationDbContext.Users)
            {
                if (user.Email == email && scryptEncoder.Compare(password, user.Password))
                {
                    return user;
                }
            }
            return null;
        }
        public int UsersCount(Expression<Func<User, bool>> filter = null)
        {
            IQueryable<User> query = applicationDbContext.Users;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }

    }
}
