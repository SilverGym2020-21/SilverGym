namespace SilverGym.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using SilverGym.Web.ViewModels.Administration;
    using SilverGym.Web.ViewModels.Products;

    public interface IAdministrationService
    {
        public Task AddTrainer(TrainerInputModel input);

        public Task RemoveTrainer(TrainerRemoveInputModel input);

        public Task AddProduct(ProductInputModel input);

        public Task RemoveProduct(ProductRemoveInputModel input);
    }
}
