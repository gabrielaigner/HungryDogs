using System;

namespace HungryDogs.Logic
{
    public static class Factory
    {
        public static Contracts.Client.IController<Contracts.Persistence.IRestaurant> CreateRestaurant()
        {
            return new Controllers.Persistence.RestaurantController();
        }
        public static Contracts.Client.IController<Contracts.Persistence.IRestaurant> CreateRestaurant(Object controller)
        {
            return new Controllers.Persistence.RestaurantController(controller as Controllers.ControllerObject);
        }

        public static Contracts.Client.IController<Contracts.Persistence.IOpeningHour> CreateOpeningHour()
        {
            return new Controllers.Persistence.OpeningHourController();
        }
        public static Contracts.Client.IController<Contracts.Persistence.IOpeningHour> CreateOpeningHour(Object controller)
        {
            return new Controllers.Persistence.OpeningHourController(controller as Controllers.ControllerObject);
        }

        public static Contracts.Client.IController<Contracts.Persistence.ISpecialOpeningHour> CreateSpecialOpeningHour()
        {
            return new Controllers.Persistence.SpecialOpeningHourController();
        }
        public static Contracts.Client.IController<Contracts.Persistence.ISpecialOpeningHour> CreateSpecialOpeningHour(Object controller)
        {
            return new Controllers.Persistence.SpecialOpeningHourController(controller as Controllers.ControllerObject);
        }

        public static Contracts.Client.ICustomerRestaurantController CreateCustomerRestaurant()
        {
            return new Controllers.Business.CustomerRestaurantController();
        }
        public static Contracts.Client.ICustomerRestaurantController CreateCustomerRestaurant(Object controller)
        {
            return new Controllers.Business.CustomerRestaurantController(controller as Controllers.ControllerObject);
        }
    }
}
