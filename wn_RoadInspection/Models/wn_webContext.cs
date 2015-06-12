using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using wn_RoadInspection.Models.RoadInspection;
using wn_web.Models.Reclaimation;
using wn_web.Models.Reclaimation.Report;

namespace wn_web.Models
{
    public class wn_webContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public wn_webContext()
            : base("wn_webContext")
        {
        }

        public DbSet<Kml> Kmls { get; set; }
        public DbSet<FieldData> FieldDatas { get; set; }
        public DbSet<SearchType> SearchTypes { get; set; }
        public DbSet<DesktopReview> DesktopReviews { get; set; }
        public DbSet<FacilityType> FacilityTypes { get; set; }
        public DbSet<Aspect> Aspects { get; set; }
        public DbSet<FMAHolder> FMAHolders { get; set; }
        public DbSet<OperatingArea> OperatingAreas { get; set; }
        public DbSet<ProvincialArea> ProvincialAreas { get; set; }
        public DbSet<ProvincialAreaType> ProvincialAreaTypes { get; set; }
        public DbSet<County> Countys { get; set; }
        public DbSet<NaturalRegion> NaturalRegions { get; set; }
        public DbSet<NaturalSubRegion> NaturalSubRegions { get; set; }
        public DbSet<RelevantCriteria> RelevantCriterias { get; set; }
        public DbSet<Landscape> Landscapes { get; set; }
        public DbSet<Soil> Soils { get; set; }
        public DbSet<Vegetation> Vegetations { get; set; }
        public DbSet<ReviewSite> ReviewSites { get; set; }

        public DbSet<SiteVisitReport> SiteVisitReports { get; set; }
        public DbSet<FormType> FormTypes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<RoadInspection> RoadInspections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<DesktopReview>().HasKey(x => x.SiteID).Property(p => p.SiteID).HasColumnOrder(1);
            modelBuilder.Entity<Aspect>().HasKey(x => x.AspectName);
            modelBuilder.Entity<County>().HasKey(x => x.CountyName);

            modelBuilder.Entity<FacilityType>().HasKey(x => x.FacilityTypeName);
            modelBuilder.Entity<FMAHolder>().HasKey(x => x.FMAHolderName);
            modelBuilder.Entity<Landscape>().HasKey(x => x.LandscapeName);
            modelBuilder.Entity<NaturalRegion>().HasKey(x => x.NaturalRegionName);
            modelBuilder.Entity<NaturalSubRegion>().HasKey(x => x.NaturalSubRegionName);
            modelBuilder.Entity<OperatingArea>().HasKey(x => x.OperatingAreaName);
            modelBuilder.Entity<ProvincialArea>().HasKey(x => x.ProvincialAreaName);
            modelBuilder.Entity<ProvincialAreaType>().HasKey(x => x.ProvincialAreaTypeName);
            modelBuilder.Entity<RelevantCriteria>().HasKey(x => x.RelevantCriteriaName);
            modelBuilder.Entity<Soil>().HasKey(x => x.SoilName);
            modelBuilder.Entity<Vegetation>().HasKey(x => x.VegetationName);

            modelBuilder.Entity<FormType>().HasKey(x => x.FormTypeName);
            


        }

    }
}
