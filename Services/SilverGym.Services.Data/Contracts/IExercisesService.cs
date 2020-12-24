namespace SilverGym.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SilverGym.Web.ViewModels.Exercises;

    public interface IExercisesService
    {
        public Task AddExercise(ExerciseInputModel input);
    }
}
