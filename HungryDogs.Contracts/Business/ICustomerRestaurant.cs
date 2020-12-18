using HungryDogs.Contracts.Modules.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HungryDogs.Contracts.Business
{
	public interface ICustomerRestaurant : IIdentifiable, ICommonRestaurant
	{
		DateTime? NextOpen { get; set; }
		DateTime? NextClose { get; set; }
		SpecialOpenState OpenState { get; set; }
		ICustomerRestaurant SetOpenState(SpecialOpenState openState, int value);
    }
}
