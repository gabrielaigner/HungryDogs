using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HungryDogs.Contracts.Business
{
    public interface ITimeController
    {
        Task SetOpen(int restaurantId);
        Task SetClosed(int restaurantId);
        Task SetBusy(int restaurantId,int value);
        Task SetClosedIndefinitely(int restaurantId);

    }
}
