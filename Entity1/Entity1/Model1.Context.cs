﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UserDB : DbContext
    {
        public UserDB()
            : base("name=UserDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Adress> Adress { get; set; }
        public virtual DbSet<AdressType> AdressType { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<BillType> BillType { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Street> Street { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<AllUsers> AllUsers { get; set; }
    }
}
