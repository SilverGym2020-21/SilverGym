namespace SilverGym.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SilverGym.Web.ViewModels;
    using SilverGym.Web.ViewModels.EatingPlan;
    using SilverGym.Web.ViewModels.Trainer;
    using SilverGym.Web.ViewModels.WorkoutPlan;

    public interface ITrainersService
    {
        public Task AddClient(AddClientToTrainerInputModel client);

        public Task RemoveClient(RemoveClientFromTrainerInputModel input);

        public Task<ICollection<RemoveClientFromTrainerInputModel>> GetClients(string trainerId);

        public Task<ICollection<TrainerViewModel>> GetTrainers();

        public Task<TrainerViewModel> GetTrainer(string id);

        public Task AddWorkoutPlantToClient(WorkoutPlanInputModel input);

        public Task RemoveWorkoutPlantFromClient(string id, string trainerId);

        public Task AddEatingPlantToClient(EatingPlanInputModel input);

        public Task RemoveEatingPlantFromClient(string id, string trainerId);
    }
}
