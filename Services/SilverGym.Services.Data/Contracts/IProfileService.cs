namespace SilverGym.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SilverGym.Web.ViewModels.Profile;

    public interface IProfileService
    {
        public Task<PersonalTrainerViewModel> GetTrainer(string userId);
    }
}
