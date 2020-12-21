namespace SilverGym.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SilverGym.Data;
    using SilverGym.Data.Models;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels;
    using SilverGym.Web.ViewModels.Trainer;

    public class TrainersService : ITrainersService
    {
        private readonly ApplicationDbContext db;

        public TrainersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddClient(AddClientToTrainerInputModel input)
        {
            var client = await this.db.Users.FirstOrDefaultAsync(u => u.Email == input.Email);
            if (client == null)
            {
                throw new ArgumentException("Няма регистриран човек с този е-мейл.");
            }

            if (client.Id == input.TrainerId)
            {
                throw new ArgumentException("Няма как сам да си си треньор :).");
            }

            Trainer trainer = (Trainer)await this.db.Users.FirstOrDefaultAsync(u => u.Id == input.TrainerId);
            if (trainer == null)
            {
                throw new ArgumentException("Невалиден опит. Опитай пак!");
            }

            if (trainer.Clients.Contains(client))
            {
                throw new InvalidOperationException("Този човек вече е твой клиент!");
            }

            trainer.Clients.Add(client);
            client.Trainer = trainer;
            client.TrainerId = trainer.Id;

            await this.db.SaveChangesAsync();
        }

        public async Task<ICollection<RemoveClientFromTrainerInputModel>> GetClients(string trainerId)
        {
            var trainer = await this.db.Trainers.Include(t => t.Clients).FirstOrDefaultAsync(t => t.Id == trainerId);
            return trainer.Clients.Select(c => new RemoveClientFromTrainerInputModel()
            {
                ClientEmail = c.Email,
                ClientId = c.Id,
                TrainerId = trainer.Id,
            }).ToList();
        }

        public async Task RemoveClient(RemoveClientFromTrainerInputModel input)
        {
            var client = await this.db.Users.FirstOrDefaultAsync(u => u.Email == input.ClientEmail && u.Id == input.ClientId);
            if (client == null)
            {
                throw new ArgumentException("Няма регистриран човек с този е-мейл.");
            }

            if (client.Id == input.TrainerId)
            {
                throw new ArgumentException("Няма как сам да си си треньор :).");
            }

            Trainer trainer = (Trainer)await this.db.Users.FirstOrDefaultAsync(u => u.Id == input.TrainerId);
            if (trainer == null)
            {
                throw new ArgumentException("Невалиден опит. Опитай пак!");
            }

            if (!trainer.Clients.Contains(client))
            {
                throw new InvalidOperationException("Този човек не е твой клиент!");
            }

            trainer.Clients.Remove(client);
            client.Trainer = null;
            client.TrainerId = null;

            await this.db.SaveChangesAsync();
        }
    }
}
