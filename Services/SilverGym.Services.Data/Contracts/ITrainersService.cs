namespace SilverGym.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SilverGym.Web.ViewModels;
    using SilverGym.Web.ViewModels.Trainer;

    public interface ITrainersService
    {
        public Task AddClient(AddClientToTrainerInputModel client);

        public Task RemoveClient(RemoveClientFromTrainerInputModel input);

        public Task<ICollection<RemoveClientFromTrainerInputModel>> GetClients(string trainerId);

        public Task<ICollection<TrainerViewModel>> GetTrainers();

        public Task<TrainerViewModel> GetTrainer(string id);
    }
}
