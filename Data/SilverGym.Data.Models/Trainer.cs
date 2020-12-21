namespace SilverGym.Data.Models
{
    using System.Collections.Generic;

    public class Trainer : ApplicationUser
    {
        public Trainer()
        {
            this.Clients = new HashSet<ApplicationUser>();
        }

        public virtual ICollection<ApplicationUser> Clients { get; set; }
    }
}
