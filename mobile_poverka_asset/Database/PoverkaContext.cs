using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace mobile_poverka_asset.Database
{
    public class PoverkaContext : DbContext
    {
        public DbSet<dbSpisok> Spisoks {get;set;}
        public DbSet<dbPribor> Pribors { get; set; }

        /*public PoverkaContext(DbContextOptions<PoverkaContext> options)
            : base(options)
        {
        }*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connection.getSettingsMS());
                //@"Server=192.168.1.9;Database=test;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
    public class dbSpisok {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Count { get; set; }
        public string Complete { get; set; }
        public string Comment { get; set; }
        public List<dbPribor> pribors {get;set;}
    }

    public class dbPribor
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string idchannel { get; set; }
        public string spisok_id { get; set; }

        public dbSpisok spisok { get; set; }
    }
}
