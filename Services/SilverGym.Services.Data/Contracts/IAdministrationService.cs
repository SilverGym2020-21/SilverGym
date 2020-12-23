namespace SilverGym.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using SilverGym.Web.ViewModels.Administration;

    public interface IAdministrationService
    {
        public Task AddTrainer(TrainerInputModel input);

        public Task RemoveTrainer(TrainerRemoveInputModel input);
    }
}
