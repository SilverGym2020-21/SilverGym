namespace SilverGym.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using SilverGym.Web.ViewModels.EatingPlan;
    using SilverGym.Web.ViewModels.Profile;
    using SilverGym.Web.ViewModels.WorkoutPlan;

    public interface IProfileService
    {
        public Task<PersonalTrainerViewModel> GetTrainer(string userId);

        public Task<WorkoutPlanViewModel> GetWorkoutPlan(string userId);

        public Task<EatingPlanViewModel> GetEatingPlan(string userId);
    }
}
