namespace FundClear.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Fund : DbContext
    {
        public Fund()
            : base("name=Fund")
        {
        }

        public virtual DbSet<Batch_Status> Batch_Status { get; set; }
        public virtual DbSet<Borrower> Borrower { get; set; }
        public virtual DbSet<Contract_Status> Contract_Status { get; set; }
        public virtual DbSet<Fix_Batch_Running_Summary> Fix_Batch_Running_Summary { get; set; }
        public virtual DbSet<Fix_Contract> Fix_Contract { get; set; }
        public virtual DbSet<Fix_Prod_Batch> Fix_Prod_Batch { get; set; }
        public virtual DbSet<Fix_Prod_Batch_running> Fix_Prod_Batch_running { get; set; }
        public virtual DbSet<Fix_Product> Fix_Product { get; set; }
        public virtual DbSet<Fix_Running_Summary> Fix_Running_Summary { get; set; }
        public virtual DbSet<FOF_Contract> FOF_Contract { get; set; }
        public virtual DbSet<FOF_Product> FOF_Product { get; set; }
        public virtual DbSet<Investor> Investor { get; set; }
        public virtual DbSet<Sales_Branch> Sales_Branch { get; set; }
        public virtual DbSet<Sales_Person> Sales_Person { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<System_Role> System_Role { get; set; }
        public virtual DbSet<System_User> System_User { get; set; }
        public virtual DbSet<Fix_running_tab> Fix_running_tab { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Borrower>()
                .HasMany(e => e.Fix_Product)
                .WithRequired(e => e.Borrower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract_Status>()
                .HasMany(e => e.FOF_Contract)
                .WithOptional(e => e.Contract_Status)
                .HasForeignKey(e => e.Contract_status_id);

            modelBuilder.Entity<Fix_Contract>()
                .Property(e => e.已付收益)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Fix_Prod_Batch>()
                .HasMany(e => e.Fix_Contract)
                .WithRequired(e => e.Fix_Prod_Batch)
                .WillCascadeOnDelete(false);          

            modelBuilder.Entity<Fix_Product>()
                .HasMany(e => e.Fix_Contract)
                .WithRequired(e => e.Fix_Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FOF_Product>()
                .Property(e => e.募集规模)
                .HasPrecision(18, 0);

            modelBuilder.Entity<FOF_Product>()
                .Property(e => e.最低认购金额)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Investor>()
                .HasMany(e => e.Fix_Contract)
                .WithRequired(e => e.Investor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sales_Person>()
                .HasMany(e => e.Fix_Contract)
                .WithOptional(e => e.Sales_Person)
                .HasForeignKey(e => e.Salesperson_id);

            modelBuilder.Entity<Sales_Person>()
                .HasMany(e => e.FOF_Contract)
                .WithOptional(e => e.Sales_Person)
                .HasForeignKey(e => e.SalesPerson_id);

            modelBuilder.Entity<Fix_running_tab>()
                .Property(e => e.金额)
                .HasPrecision(18, 4);
        }
    }
}
