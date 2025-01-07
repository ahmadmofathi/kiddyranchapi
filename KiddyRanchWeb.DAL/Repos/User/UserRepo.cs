﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }
        public List<User> GetUsers()
        {
            return _context.Set<User>().ToList();
        }
        public User? GetById(string id)
        {
            return _context.Set<User>().Find(id);
        }
        public User? GetByUsername(string username)
        {
            return (User?)_context.Set<User>().Where(u => u.UserName == username);
        }
        public void Add(User user)
        {
            _context.Set<User>().Add(user);
        }

        public void Delete(User user)
        {
            _context.Set<User>().Remove(user);
        }
        public void Update(User user)
        {
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
