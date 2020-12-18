using System;
using System.Collections.Generic;
using System.Text;

namespace HungryDogs.Contracts.Modules.Common
{
	public interface ICommonRestaurant
	{
        string Name { get; set; }
        string OwnerName { get; set; }
        string UniqueName { get; set; }
        string Email { get; set; }
        RestaurantState State { get; set; }
    }
}
