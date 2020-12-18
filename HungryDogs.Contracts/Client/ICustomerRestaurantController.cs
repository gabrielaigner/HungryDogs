using System;
using System.Collections.Generic;
using System.Text;
using HungryDogs.Contracts.Business;

namespace HungryDogs.Contracts.Client
{
    public interface ICustomerRestaurantController : IController<ICustomerRestaurant>, ITimeController
    {
    }
}
