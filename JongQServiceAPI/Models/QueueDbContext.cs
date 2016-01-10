using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace JongQServiceAPI.Models
{
    public class QueueDbContext
        : DbContext
    {
        public QueueDbContext()
            : base("JongQDB")
        {
            Database.SetInitializer<QueueDbContext>(new ResStatusInitializer()); 
        }
        public DbSet<AfterYouTable> AfterYouTableEntity { get; set; }
        public DbSet<AfterYouTableHistory> AfterYouTableHistoryEntity { get; set; }
        public DbSet<BonChonTable> BonChonTableEntity { get; set; }
        public DbSet<BonChonTableHistory> BonChonTableHistoryEntity { get; set; }
        public DbSet<BarBQPlazaTable> BarBQPlazaTableEntity { get; set; }
        public DbSet<ResStatus> ResStatusEntity { get; set; }
        public DbSet<EatAmAreTable> EatAmAreTableEntity { get; set; }

        
    }
    
}