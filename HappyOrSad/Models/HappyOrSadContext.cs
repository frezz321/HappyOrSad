using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Collections.Generic;

namespace HappyOrSad.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int VillageID { get; set; }
        public virtual Village Village { get; set; }
        public bool isActivated { get; set; }
        public virtual List<QuestionResponse> QuestionReponses { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class HappyOrSadContext : IdentityDbContext<ApplicationUser>
    {
        
        public HappyOrSadContext() : base("HappyOrSad_ConnectionString", throwIfV1Schema: false)
        {

        }

        public static HappyOrSadContext Create()
        {
            return new HappyOrSadContext();
        }

        public DbSet<HappyOrSad.Models.Question> Question { get; set; }
        public DbSet<HappyOrSad.Models.QuestionResponse> QuestionResponse { get; set; }

        public DbSet<HappyOrSad.Models.Village> Villages { get; set; }
        public DbSet<HappyOrSad.Models.TimeInterval> TimeInterval { get; set; }
    }
}