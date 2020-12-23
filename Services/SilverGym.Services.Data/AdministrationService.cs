namespace SilverGym.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment hostingEnvironment;

        public AdministrationService(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
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

            var newRoles = new List<IdentityUserRole<string>>();
            var userRoles = await this.db.UserRoles.Where(ur => ur.UserId == user.Id).ToListAsync();

            newRoles.AddRange(userRoles);

            this.db.UserRoles.RemoveRange(userRoles);
            await this.db.SaveChangesAsync();
            this.db.Users.Remove(user);

            string fileName = this.UploadFile(input);

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
                FirstName = input.FirstName,
                LastName = input.LastName,
                ProfilePictureUrl = fileName,
            };

            var trainerRoleId = (await this.db.Roles.FirstOrDefaultAsync(r => r.Name == GlobalConstants.TrainerRoleName)).Id;

            await this.db.Trainers.AddAsync(trainer);
            await this.db.SaveChangesAsync();

            newRoles.ForEach(r => r.UserId = trainer.Id);
            newRoles.Add(new IdentityUserRole<string>()
            {
                UserId = trainer.Id,
                RoleId = trainerRoleId,
            });

            await this.db.UserRoles.AddRangeAsync(newRoles);
            await this.db.SaveChangesAsync();
        }

        public async Task RemoveTrainer(TrainerRemoveInputModel input)
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

            var oldRoles = new List<IdentityUserRole<string>>();
            var userRoles = await this.db.UserRoles.Where(ur => ur.UserId == trainer.Id).ToListAsync();

            var trainerRoleId = (await this.db.Roles.FirstOrDefaultAsync(r => r.Name == GlobalConstants.TrainerRoleName)).Id;

            oldRoles.AddRange(userRoles.Where(ur => ur.RoleId != trainerRoleId).ToList());

            var usersWhosTrainer = this.db.Users.Where(u => u.TrainerId == asTrainer.Id).ToList();
            foreach (var client in usersWhosTrainer)
            {
                client.Trainer = null;
                client.TrainerId = null;
            }

            this.db.UserRoles.RemoveRange(userRoles);
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

            oldRoles.ForEach(r => r.UserId = user.Id);

            await this.db.UserRoles.AddRangeAsync(oldRoles);
            await this.db.SaveChangesAsync();
        }

        private string UploadFile(TrainerInputModel input)
        {
            string fileName = null;
            if (input.ProfileImage != null)
            {
                string uploadDir = Path.Combine(this.hostingEnvironment.WebRootPath, "trainerImages");
                fileName = Guid.NewGuid().ToString() + "-" + input.ProfileImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    input.ProfileImage.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
