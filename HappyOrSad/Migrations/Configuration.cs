using HappyOrSad.Models;
namespace HappyOrSad.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HappyOrSad.Models.HappyOrSadContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HappyOrSad.Models.HappyOrSadContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Villages.AddOrUpdate(
                v => v.VillageID,
                new Village { VillageCode = "SUP", VillageName = "Support Office" },
                new Village { VillageCode = "AVG", VillageName = "AVG VLG" },
                new Village { VillageCode = "BAY", VillageName = "BAY VLG" },
                new Village { VillageCode = "COS", VillageName = "COS VLG" },
                new Village { VillageCode = "CWD", VillageName = "CWD VLG" },
                new Village { VillageCode = "DMG", VillageName = "DMG VLG" },
                new Village { VillageCode = "FLG", VillageName = "FLG VLG" },
                new Village { VillageCode = "GWP", VillageName = "GWP VLG" },
                new Village { VillageCode = "HCV", VillageName = "HCV VLG" },
                new Village { VillageCode = "HHV", VillageName = "HHV VLG" },
                new Village { VillageCode = "HLD", VillageName = "HLD VLG" },
                new Village { VillageCode = "LPV", VillageName = "LPV VLG" },
                new Village { VillageCode = "ORV", VillageName = "ORV VLG" },
                new Village { VillageCode = "PAK", VillageName = "PAK VLG" },
                new Village { VillageCode = "PAN", VillageName = "PAN VLG" },
                new Village { VillageCode = "PBV", VillageName = "PBC VLG" },
                new Village { VillageCode = "POW", VillageName = "POW VLG" },
                new Village { VillageCode = "POY", VillageName = "POY VLG" },
                new Village { VillageCode = "PSG", VillageName = "PSG VLG" },
                new Village { VillageCode = "SOM", VillageName = "SOM VLG" },
                new Village { VillageCode = "SSV", VillageName = "SSV VLG" },
                new Village { VillageCode = "WAI", VillageName = "WAI VLG" },
                new Village { VillageCode = "WTG", VillageName = "WTG VLG" }
            );
        }
    }
}
