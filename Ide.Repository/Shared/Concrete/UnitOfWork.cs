﻿using Ide.Data;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Repository.Shared.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IRepository<AppUser> Users { get; private set; }

        public IRepository<Order> Orders { get; private set; }

        public IRepository<Product> Products { get; private set; }

        public IRepository<ShoppingBasket> ShoppingBaskets { get; private set; }

        public IRepository<UserType> UserTypes { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new Repository<AppUser>(_context);
            Orders=new Repository<Order>(_context);
            Products = new Repository<Product>(_context);
            ShoppingBaskets= new Repository<ShoppingBasket>(_context);
            UserTypes=new Repository<UserType>(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
