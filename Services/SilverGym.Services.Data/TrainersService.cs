namespace SilverGym.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SilverGym.Data;
    using SilverGym.Data.Models;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels;

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
    }
}
