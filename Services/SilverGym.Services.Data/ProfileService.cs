namespace SilverGym.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SilverGym.Data;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels.Profile;

    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext db;

        public ProfileService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<PersonalTrainerViewModel> GetTrainer(string userId)
        {
            var user = await this.db.Users.Include(u => u.Trainer).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Моля влезте си в акаунта!");
            }

            var trainer = user.Trainer;

            if (trainer == null)
            {
                return new PersonalTrainerViewModel()
                {
                    TrainerEmail = "Нямаш личен треньор",
                };
            }

            return new PersonalTrainerViewModel()
            {
                TrainerEmail = trainer.Email,
            };
        }
    }
}
