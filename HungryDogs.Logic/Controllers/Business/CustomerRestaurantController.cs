using CommonBase.Extensions;
using HungryDogs.Logic.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HungryDogs.Contracts.Business;
using HungryDogs.Contracts.Modules.Common;
using HungryDogs.Contracts.Persistence;
using HungryDogs.Logic.Entities.Persistence;
using Microsoft.EntityFrameworkCore.Internal;
using TContract = HungryDogs.Contracts.Business.ICustomerRestaurant;
using TEntity = HungryDogs.Logic.Entities.Business.CustomerRestaurant;

namespace HungryDogs.Logic.Controllers.Business
{
	class CustomerRestaurantController : ControllerObject, Contracts.Client.ICustomerRestaurantController
	{
		public CustomerRestaurantController() 
			: base(new ProjectDbContext())
		{
		}
		public CustomerRestaurantController(ControllerObject controllerObject)
			: base(controllerObject)
		{
		}

        protected DbSet<Entities.Persistence.Restaurant> Set => Context.RestaurauntSet;
        protected DbSet<Entities.Persistence.SpecialOpeningHour> SpecialSet => Context.SpecialOpeningHourSet;

        internal async Task<TEntity> LoadCustomerRestaurantAsync(int id)
		{
            var result = default(TEntity);
			var qryResult = await Set.Include(e => e.OpeningHours)
							         .Include(e => e.SepcialOpeningHours)
							         .SingleOrDefaultAsync(e => e.Id == id)
							         .ConfigureAwait(false);

            if (qryResult != null)
			{
                result = new TEntity();
                result = CopyFromRestaurant(result, qryResult);
                SpecialOpeningHour specialOpening = qryResult.SepcialOpeningHours.Where(e => e.From < DateTime.Now && e.To > DateTime.Now).OrderBy(e => e.From).LastOrDefault();
                if (specialOpening != null)
                {
                    result = CopyFromSpecialOpeningTime(result, specialOpening);
                }
                else
                {
                    OpeningHour openingHour = qryResult.OpeningHours.SingleOrDefault(e => e.OpenFrom < DateTime.Now.TimeOfDay && e.OpenTo > DateTime.Now.TimeOfDay && e.Weekday == (int)DateTime.Now.DayOfWeek);
                    if(openingHour!=null) result = CopyFromOpeningTime(result, openingHour);
                    else
                    {
                        result.OpenState = SpecialOpenState.Closed;
                        AddNextOpenTime(result, qryResult);
                    }
                }
            }
            return result;
		}

        

        public Task<int> Count()
        {
            return Set.CountAsync();
        }
        public Task<TContract> CreateAsync()
        {
            throw new NotSupportedException();
        }

        public async Task<TContract> GetByIdAsync(int id)
        {
            var model = await LoadCustomerRestaurantAsync(id).ConfigureAwait(false);
            return model;
        }
        public async Task<TContract[]> GetAllAsync()
        {
            return (TContract[])await Set.ToArrayAsync().ConfigureAwait(false);
        }
        public Task<TContract> InsertAsync(TContract entity)
        {
            throw new NotSupportedException();
        }
        public Task<TContract> UpdateAsync(TContract entity)
        {
            throw new NotSupportedException();
        }
        public Task<TContract> DeleteAsync(int id)
        {
            throw new NotSupportedException();
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

            entity.NextOpen = contract.NextOpen;
            entity.NextClose = contract.NextClose;
            entity.OpenState = contract.OpenState;

            return entity;
        }

        protected virtual TEntity CopyFromRestaurant(TEntity entity, Restaurant restaurant)
        {
            entity.Id = restaurant.Id;
            entity.Name = restaurant.Name;
            entity.Email = restaurant.Email;
            entity.State = restaurant.State;
            entity.OwnerName = restaurant.OwnerName;
            entity.UniqueName = restaurant.UniqueName;
            return entity;
        }

        protected virtual TEntity CopyFromSpecialOpeningTime(TEntity entity, SpecialOpeningHour specialOpeningHour)
        {
            entity.SetOpenState(specialOpeningHour.State, 0);
            if (entity.OpenState == SpecialOpenState.Closed)
            {
                entity.NextOpen = specialOpeningHour.To;
            }
            else if (entity.OpenState == SpecialOpenState.Open)
            {
                entity.NextClose = specialOpeningHour.To;
            }
            else if(entity.OpenState == SpecialOpenState.IsBusy)
            {
                entity.NextClose = specialOpeningHour.To;
            }
            else if(entity.OpenState == SpecialOpenState.ClosedPermanent)
            {
                entity.NextClose = null;
            }
            return entity;
        }
        private TEntity CopyFromOpeningTime(TEntity entity, OpeningHour openingHour)
        {
            entity.OpenState = SpecialOpenState.Open;
            DateTime now = DateTime.Now;
            entity.NextClose = now.SetTimeSpan(openingHour.OpenTo);
            return entity;
        }

        private TEntity AddNextOpenTime(TEntity entity, Restaurant restaurant)
        {
            SpecialOpeningHour specialOpeningHour =
                restaurant.SepcialOpeningHours.FirstOrDefault(e => DateTime.Now < e.From);
            OpeningHour openingHour = restaurant.OpeningHours.FirstOrDefault(e =>
                e.Weekday == (int) DateTime.Now.DayOfWeek && e.OpenFrom > DateTime.Now.TimeOfDay);
            if(openingHour == null && restaurant.OpeningHours.Any()) openingHour=getNextOpeningHourNextDay(restaurant, (int) DateTime.Now.DayOfWeek + 1);
            if (SpecialCloser(specialOpeningHour, openingHour))
            {
                if (specialOpeningHour != null)
                {
                    entity.NextOpen = specialOpeningHour.From;
                    entity.NextClose = specialOpeningHour.To;
                }
            }
            else
            {
                DateTime now = DateTime.Now;
                if (openingHour != null)
                {
                    entity.NextOpen = now.SetTimeSpan(openingHour.OpenFrom).AddDays(GetDayDifference((int)DateTime.Now.DayOfWeek, openingHour.Weekday));
                    entity.NextClose = now.SetTimeSpan(openingHour.OpenTo).AddDays(GetDayDifference((int)DateTime.Now.DayOfWeek, openingHour.Weekday));
                }
            }
            return entity;
        }

        private OpeningHour getNextOpeningHourNextDay(Restaurant restaurant, int plusDays)
        {
            return restaurant.OpeningHours.FirstOrDefault(e =>
                       e.Weekday == (int)(DateTime.Now.DayOfWeek + plusDays) % 7) 
                   ?? getNextOpeningHourNextDay(restaurant, (plusDays + 1));
        }

        private bool SpecialCloser(SpecialOpeningHour specialOpeningHour, OpeningHour openingHour)
        {
            if (specialOpeningHour == null)
            {
                return false;
            }
            else if ((specialOpeningHour.From - DateTime.Now).Value.Days <
                     GetDayDifference((int) DateTime.Now.DayOfWeek, openingHour.Weekday))
            {
                return true;
            }
            else if((specialOpeningHour.From - DateTime.Now).Value.Days >
                    GetDayDifference((int)DateTime.Now.DayOfWeek, openingHour.Weekday))
            {
                return false;
            }
            else if (specialOpeningHour.From.Value.TimeOfDay > openingHour.OpenFrom)
            {
                return false;
            }
            return true;
        }

        private int GetDayDifference(int today, int nextDay)
        {
            return today > nextDay ? 7-today + nextDay : nextDay - today;
        }
        public async Task SetOpen(int restaurantId)
        {
            var customerRestaurant = await LoadCustomerRestaurantAsync(restaurantId);
            if (customerRestaurant.OpenState == SpecialOpenState.Closed)
            {
                await SpecialSet.AddAsync(new SpecialOpeningHour(restaurantId, DateTime.Now, customerRestaurant.NextClose,
                    "SetOpenManual", SpecialOpenState.Open)).ConfigureAwait(false);
            }
        }

        public async Task SetClosed(int restaurantId)
        {
            var customerRestaurant = await LoadCustomerRestaurantAsync(restaurantId);
            if (customerRestaurant.OpenState == SpecialOpenState.Open || customerRestaurant.OpenState == SpecialOpenState.IsBusy)
            {
                await SpecialSet.AddAsync(new SpecialOpeningHour(restaurantId, DateTime.Now, customerRestaurant.NextClose,
                    "SetClosedManual", SpecialOpenState.Closed)).ConfigureAwait(false);
            }
        }

        public async Task SetBusy(int restaurantId, int value)
        {
            var customerRestaurant = await LoadCustomerRestaurantAsync(restaurantId);
            if (customerRestaurant.OpenState == SpecialOpenState.Open)
            {
                await SpecialSet.AddAsync(new SpecialOpeningHour(restaurantId, DateTime.Now, DateTime.Now.AddMinutes(value), 
                    "SetBusyManual", SpecialOpenState.IsBusy));
            }
        }

        public async Task SetClosedIndefinitely(int restaurantId)
        {
            await SpecialSet.AddAsync(new SpecialOpeningHour(restaurantId, DateTime.Now, null,
                "SetClosedIndefinitelyManual", SpecialOpenState.ClosedPermanent));
        }

        protected virtual Restaurant ConvertTo(IRestaurant contract)
        {
            return CopyTo(new Restaurant(), contract);
        }
        protected virtual Restaurant CopyTo(Restaurant entity, IRestaurant contract)
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
