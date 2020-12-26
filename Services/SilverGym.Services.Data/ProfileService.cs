namespace SilverGym.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SilverGym.Data;
    using SilverGym.Data.Models.Enums;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels.EatingPlan;
    using SilverGym.Web.ViewModels.Exercises;
    using SilverGym.Web.ViewModels.MealPlan;
    using SilverGym.Web.ViewModels.Meals;
    using SilverGym.Web.ViewModels.Profile;
    using SilverGym.Web.ViewModels.WorkDays;
    using SilverGym.Web.ViewModels.WorkoutPlan;

    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext db;

        public ProfileService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<EatingPlanViewModel> GetEatingPlan(string userId)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Моля влезте си в акаунта!");
            }

            var eatingPlan = await this.db.EatingPlan
                .Include(e => e.MealPlans)
                .ThenInclude(m => m.Meals)
                .FirstOrDefaultAsync(e => e.UserId == user.Id);

            if (eatingPlan == null)
            {
                return null;
            }

            eatingPlan.MealPlans = eatingPlan.MealPlans.OrderBy(mp => mp.Name).ToList();

            var mealPlanModel = new List<MealPlanViewModel>();
            foreach (var mealPlan in eatingPlan.MealPlans)
            {
                var mealsModel = new List<MealViewModel>();
                var plan = new MealPlanViewModel()
                {
                    Name = mealPlan.Name,
                    Time = mealPlan.Time,
                };
                foreach (var meal in mealPlan.Meals)
                {
                    var model = new MealViewModel()
                    {
                        Name = meal.Name,
                        GramsOrMil = meal.GramsOrMil,
                    };
                    mealsModel.Add(model);
                }

                plan.Meals = mealsModel;
                mealPlanModel.Add(plan);
            }

            return new EatingPlanViewModel()
            {
                MealPlans = mealPlanModel,
            };
        }

        public async Task<PersonalTrainerViewModel> GetTrainer(string userId)
        {
            var user = await this.db.Users.Include(u => u.Trainer).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Моля влезте си в акаунта!");
            }

            var trainer = user.Trainer;

            if (trainer == null)
            {
                return new PersonalTrainerViewModel()
                {
                    TrainerEmail = "Нямаш личен треньор",
                };
            }

            return new PersonalTrainerViewModel()
            {
                TrainerId = trainer.Id,
                TrainerEmail = trainer.Email,
                Name = trainer.FirstName + " " + trainer.LastName,
            };
        }

        public async Task<WorkoutPlanViewModel> GetWorkoutPlan(string userId)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Моля влезте си в акаунта!");
            }

            var workoutPlan = await this.db.WorkoutPlans
                .Include(w => w.WorkoutDays)
                .ThenInclude(d => d.Exercises)
                .FirstOrDefaultAsync(w => w.UserId == user.Id);

            if (workoutPlan == null)
            {
                return null;
            }

            workoutPlan.WorkoutDays = workoutPlan.WorkoutDays.OrderBy(d => d.WorkDay).ToList();

            var viewWorkoutDays = new List<WorkoutDayViewModel>();
            foreach (var day in workoutPlan.WorkoutDays)
            {
                var exercises = new List<ExerciseViewModel>();
                foreach (var exercise in day.Exercises)
                {
                    exercises.Add(new ExerciseViewModel()
                    {
                        MuscleGroup = exercise.MuscleGroup,
                        Name = exercise.Name,
                        RepsOrTime = exercise.RepsOrTime,
                    });
                }

                int value = (int)day.WorkDay;
                var enumDisplayStatus = (WorkDay)value;
                string stringValue = this.TranslateToBg(enumDisplayStatus.ToString());

                viewWorkoutDays.Add(new WorkoutDayViewModel()
                {
                    Exercises = exercises,
                    WorkDay = stringValue,
                });
            }

            return new WorkoutPlanViewModel()
            {
                WorkoutDays = viewWorkoutDays,
            };
        }

        private string TranslateToBg(string day)
        {
            switch (day)
            {
                case "Monday":
                    return "Понеделник";
                case "Tuesday":
                    return "Вторник";
                case "Wednesday":
                    return "Сряда";
                case "Thursday":
                    return "Четвъртък";
                case "Friday":
                    return "Петък";
                case "Saturday":
                    return "Събота";
                case "Sunday":
                    return "Неделя";
                default:
                    return "Неопределен";
            }
        }
    }
}
