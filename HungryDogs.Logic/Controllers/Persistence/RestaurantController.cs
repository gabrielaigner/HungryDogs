using CommonBase.Extensions;
using HungryDogs.Logic.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TContract = HungryDogs.Contracts.Persistence.IRestaurant;
using TEntity = HungryDogs.Logic.Entities.Persistence.Restaurant;

namespace HungryDogs.Logic.Controllers.Persistence
{
    class RestaurantController : ControllerObject, Contracts.Client.IController<TContract>
    {
        public RestaurantController()
            : base(new ProjectDbContext())
        {

        }
        public RestaurantController(ControllerObject controllerObject)
            : base(controllerObject)
        {

        }

        protected DbSet<TEntity> Set => Context.RestaurauntSet;

        public Task<int> Count()
        {
            return Set.CountAsync();
        }
        public Task<TContract> CreateAsync()
        {
            return Task.Run<TContract>(() => new TEntity());
        }

        public async Task<TContract> GetByIdAsync(int id)
        {
            return (TContract)await Set.SingleOrDefaultAsync(i => i.Id == id).ConfigureAwait(false);
        }
        public async Task<TContract[]> GetAllAsync()
        {
            return (TContract[])await Set.ToArrayAsync().ConfigureAwait(false);
        }
        public async Task<TContract> InsertAsync(TContract entity)
        {
            entity.CheckArgument(nameof(entity));

            var result = await Set.AddAsync(ConvertTo(entity)).ConfigureAwait(false);
            return result.Entity;
        }
        public async Task<TContract> UpdateAsync(TContract entity)
        {
            entity.CheckArgument(nameof(entity));

            var updEntity = await Set.SingleOrDefaultAsync(i => i.Id == entity.Id).ConfigureAwait(false);

            CopyTo(updEntity, entity);
            Set.Update(updEntity);
            return (TContract)updEntity;
        }
        public async Task<TContract> DeleteAsync(int id)
        {
            var entity = await Set.SingleOrDefaultAsync(i => i.Id == id).ConfigureAwait(false);

            Set.Remove(entity);
            return (TContract)entity;
        }

        protected virtual TEntity ConvertTo(TContract contract)
        {
            return CopyTo(new TEntity(), contract);
        }
        protected virtual TEntity CopyTo(TEntity entity, TContract contract)
        {
            entity.CheckArgument(nameof(entity));
            contract.CheckArgument(nameof(contract));

            entity.Id = contract.Id;
            entity.Name = contract.Name;
            entity.UniqueName = contract.UniqueName;
            entity.OwnerName = contract.OwnerName;
            entity.Email = contract.Email;
            entity.State = contract.State;

            return entity;
        }
    }
}
