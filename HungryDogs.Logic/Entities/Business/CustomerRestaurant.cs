using HungryDogs.Contracts.Business;
using HungryDogs.Contracts.Modules.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HungryDogs.Logic.Entities.Business
{
	class CustomerRestaurant : IdentityEntity, Contracts.Business.ICustomerRestaurant
	{
		public DateTime? NextOpen { get; set; }
		public DateTime? NextClose { get; set; }
        public DateTime? BusyTo { get; set; }
		public SpecialOpenState OpenState { get; set; }
		public string Name { get; set; }
		public string OwnerName { get; set; }
		public string UniqueName { get; set; }
		public string Email { get; set; }
        public RestaurantState State { get; set; }

		public ICustomerRestaurant SetOpenState(SpecialOpenState openState, int value)
        {
            this.OpenState = openState;
            if (openState == SpecialOpenState.IsBusy)
            {
                this.BusyTo = DateTime.Now.AddMinutes(value);
			}
            return this;
		}
	}
}
