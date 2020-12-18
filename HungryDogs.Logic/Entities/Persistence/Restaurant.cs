using HungryDogs.Contracts.Modules.Common;
using System.Collections.Generic;

namespace HungryDogs.Logic.Entities.Persistence
{
    internal class Restaurant : VersionEntity, HungryDogs.Contracts.Persistence.IRestaurant
    {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string UniqueName { get; set; }
        public string Email { get; set; }
        public RestaurantState State { get; set; }
        // Navigation
        public ICollection<OpeningHour> OpeningHours { get; set; }
        public ICollection<SpecialOpeningHour> SepcialOpeningHours { get; set; }
    }
}
