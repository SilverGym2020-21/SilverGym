namespace SilverGym.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SilverGym.Common;
    using SilverGym.Data;
    using SilverGym.Data.Models;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels.Administration;

    public class AdministrationService : IAdministrationService
    {
        private readonly ApplicationDbContext db;

        public AdministrationService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddTrainer(TrainerInputModel input)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(c => c.Email == input.Email);

            if (user == null)
            {
                throw new ArgumentException("Няма регистриран човек с този е-мейл.");
            }

            var asTrainer = user as Trainer;
            if (asTrainer != null)
            {
                throw new ArgumentException("Този човек вече е треньор.");
            }

            this.db.Users.Remove(user);

            var trainer = new Trainer()
            {
                UserName = user.UserName,
                Roles = user.Roles,
                Claims = user.Claims,
                TwoFactorEnabled = user.TwoFactorEnabled,
                PhoneNumber = user.PhoneNumber,
                ConcurrencyStamp = user.ConcurrencyStamp,
                NormalizedEmail = user.NormalizedEmail,
                Email = user.Email,
                NormalizedUserName = user.NormalizedUserName,
                EmailConfirmed = user.EmailConfirmed,
                SecurityStamp = user.SecurityStamp,
                PasswordHash = user.PasswordHash,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                Logins = user.Logins,
            };

            var trainerRoleId = (await this.db.Roles.FirstOrDefaultAsync(r => r.Name == GlobalConstants.TrainerRoleName)).Id;

            await this.db.Trainers.AddAsync(trainer);
            await this.db.SaveChangesAsync();

            await this.db.UserRoles.AddAsync(new IdentityUserRole<string>()
            {
                UserId = trainer.Id,
                RoleId = trainerRoleId,
            });
            await this.db.SaveChangesAsync();
        }

        public async Task RemoveTrainer(TrainerInputModel input)
        {
            var trainer = await this.db.Users.FirstOrDefaultAsync(u => u.Email == input.Email);

            if (trainer == null)
            {
                throw new ArgumentException("Няма регистриран човек с този е-мейл.");
            }

            var asTrainer = trainer as Trainer;
            if (asTrainer == null)
            {
                throw new ArgumentException("Този човек не е треньор.");
            }

            var userRole = await this.db.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == trainer.Id);

            if (userRole == null)
            {
                throw new ArgumentException("Този човек не притежава роля треньор.");
            }

            var usersWhosTrainer = this.db.Users.Where(u => u.TrainerId == asTrainer.Id).ToList();
            foreach (var client in usersWhosTrainer)
            {
                client.Trainer = null;
                client.TrainerId = null;
            }

            this.db.UserRoles.Remove(userRole);
            await this.db.SaveChangesAsync();

            var user = new ApplicationUser()
            {
                UserName = trainer.UserName,
                Roles = trainer.Roles,
                Claims = trainer.Claims,
                TwoFactorEnabled = trainer.TwoFactorEnabled,
                PhoneNumber = trainer.PhoneNumber,
                ConcurrencyStamp = trainer.ConcurrencyStamp,
                NormalizedEmail = trainer.NormalizedEmail,
                Email = trainer.Email,
                NormalizedUserName = trainer.NormalizedUserName,
                EmailConfirmed = trainer.EmailConfirmed,
                SecurityStamp = trainer.SecurityStamp,
                PasswordHash = trainer.PasswordHash,
                PhoneNumberConfirmed = trainer.PhoneNumberConfirmed,
                Logins = trainer.Logins,
            };

            this.db.Users.Remove(trainer);

            await this.db.Users.AddAsync(user);
            await this.db.SaveChangesAsync();
        }
    }
}
