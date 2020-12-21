namespace SilverGym.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SilverGym.Web.ViewModels;

    public interface ITrainersService
    {
        public Task AddClient(AddClientToTrainerInputModel client);
    }
}
