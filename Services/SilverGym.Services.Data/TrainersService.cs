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
    using SilverGym.Web.ViewModels.EatingPlan;
    using SilverGym.Web.ViewModels.Trainer;
    using SilverGym.Web.ViewModels.WorkoutPlan;

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

        public async Task AddEatingPlantToClient(EatingPlanInputModel input)
        {
            var client = await this.db.Users.FirstOrDefaultAsync(u => u.Id == input.ClientId);

            if (client == null)
            {
                throw new ArgumentException("Невалиден клиент.");
            }

            var eatingPlan = new EatingPlan()
            {
                User = client,
                UserId = client.Id,
            };
            var mealPlans = new List<MealPlan>();

            foreach (var plan in input.MealPlans.Where(d => d.Meals != null).ToList())
            {
                var mealPlan = new MealPlan()
                {
                    Name = plan.Name,
                    Time = plan.Time,
                    EatingPlan = eatingPlan,
                    EatingPlanId = eatingPlan.EatingPlanId,
                };

                foreach (var inputMeal in plan.Meals)
                {
                    var meal = new Meal()
                    {
                        Name = inputMeal.Name,
                        GramsOrMil = inputMeal.GramsOrMil,
                        MealPlan = mealPlan,
                        MealPlanId = mealPlan.MealPlanId,
                    };
                    mealPlan.Meals.Add(meal);
                }

                mealPlans.Add(mealPlan);
            }

            eatingPlan.MealPlans = mealPlans;

            await this.db.EatingPlan.AddAsync(eatingPlan);
            await this.db.SaveChangesAsync();
        }

        public async Task AddWorkoutPlantToClient(WorkoutPlanInputModel input)
        {
            var client = await this.db.Users.FirstOrDefaultAsync(u => u.Id == input.ClientId);

            if (client == null)
            {
                throw new ArgumentException("Невалиден клиент.");
            }

            var workoutPlan = new WorkoutPlan()
            {
                User = client,
                UserId = client.Id,
            };
            var workoutDays = new List<WorkoutDay>();

            foreach (var day in input.WorkoutDays.Where(d => d.Exercises != null).ToList())
            {
                var workoutDay = new WorkoutDay()
                {
                    WorkDay = day.WorkDay,
                    WorkoutPlan = workoutPlan,
                    WorkoutPlanId = workoutPlan.WorkoutPlanId,
                };

                foreach (var inputExercise in day.Exercises)
                {
                    var exercise = new Exercise()
                    {
                        MuscleGroup = inputExercise.MuscleGroup,
                        Name = inputExercise.Name,
                        RepsOrTime = inputExercise.RepsOrTime,
                        WorkoutDay = workoutDay,
                        WorkoutDayId = workoutDay.WorkoutDayId,
                    };
                    workoutDay.Exercises.Add(exercise);
                }

                workoutDays.Add(workoutDay);
            }

            workoutPlan.WorkoutDays = workoutDays;

            await this.db.WorkoutPlans.AddAsync(workoutPlan);
            await this.db.SaveChangesAsync();
        }

        public async Task<ICollection<RemoveClientFromTrainerInputModel>> GetClients(string trainerId)
        {
            var trainer = await this.db.Trainers
                .Include(t => t.Clients)
                .ThenInclude(c => c.WorkoutPlans)
                .Include(t => t.Clients)
                .ThenInclude(e => e.EatingPlans)
                .FirstOrDefaultAsync(t => t.Id == trainerId);
            return trainer.Clients.Select(c => new RemoveClientFromTrainerInputModel()
            {
                ClientEmail = c.Email,
                ClientId = c.Id,
                TrainerId = trainer.Id,
                HasWokroutPlan = c.WorkoutPlans.Count > 0,
                WorkoutPlanId = c.WorkoutPlans.FirstOrDefault()?.WorkoutPlanId,
                HasEatingPlan = c.EatingPlans.Count > 0,
                EatingPlanId = c.EatingPlans.FirstOrDefault()?.EatingPlanId,
            }).ToList();
        }

        public async Task<TrainerViewModel> GetTrainer(string id)
        {
            var trainer = await this.db.Trainers.FirstOrDefaultAsync(t => t.Id == id);
            if (trainer == null)
            {
                throw new ArgumentException("Този треньор не съществува.");
            }

            return new TrainerViewModel()
            {
                TrainerId = trainer.Id,
                Name = trainer.FirstName + " " + trainer.LastName,
                ProfilePictureUrl = trainer.ProfilePictureUrl,
            };
        }

        public async Task<ICollection<TrainerViewModel>> GetTrainers()
        {
            return await this.db.Trainers.Select(t => new TrainerViewModel()
            {
                TrainerId = t.Id,
                Name = t.FirstName + " " + t.LastName,
                ProfilePictureUrl = t.ProfilePictureUrl,
            }).ToListAsync();
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

        public async Task RemoveEatingPlantFromClient(string id, string trainerId)
        {
            var client = await this.db.Users.FirstOrDefaultAsync(u => u.Id == id && u.TrainerId == trainerId);
            if (client == null)
            {
                throw new ArgumentException("Няма регистриран човек с този е-мейл.");
            }

            var eatingPlan = await this.db.EatingPlan
                .Include(e => e.MealPlans)
                .ThenInclude(m => m.Meals)
                .FirstOrDefaultAsync(w => w.UserId == client.Id);

            client.EatingPlans.Remove(eatingPlan);

            this.db.EatingPlan.Remove(eatingPlan);
            await this.db.SaveChangesAsync();

            this.db.MealPlan.RemoveRange(eatingPlan.MealPlans);
            foreach (var plan in eatingPlan.MealPlans)
            {
                this.db.Meals.RemoveRange(plan.Meals);
            }

            await this.db.SaveChangesAsync();
        }

        public async Task RemoveWorkoutPlantFromClient(string id, string traineriD)
        {
            var client = await this.db.Users.FirstOrDefaultAsync(u => u.Id == id && u.TrainerId == traineriD);
            if (client == null)
            {
                throw new ArgumentException("Няма регистриран човек с този е-мейл.");
            }

            var workoutPlan = await this.db.WorkoutPlans
                .Include(w => w.WorkoutDays)
                .ThenInclude(d => d.Exercises)
                .FirstOrDefaultAsync(w => w.UserId == client.Id);

            client.WorkoutPlans.Remove(workoutPlan);

            this.db.WorkoutPlans.Remove(workoutPlan);
            await this.db.SaveChangesAsync();

            this.db.WorkoutDays.RemoveRange(workoutPlan.WorkoutDays);
            foreach (var day in workoutPlan.WorkoutDays)
            {
                this.db.Exercises.RemoveRange(day.Exercises);
            }

            await this.db.SaveChangesAsync();
        }
    }
}
