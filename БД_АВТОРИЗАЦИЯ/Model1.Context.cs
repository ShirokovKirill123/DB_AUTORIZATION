﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace БД_АВТОРИЗАЦИЯ
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BakeryEntities2 : DbContext
    {
        public BakeryEntities2()
            : base("name=BakeryEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<OrderedProducts> OrderedProducts { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductСomposition> ProductСomposition { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<suppliedIngredients> suppliedIngredients { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
