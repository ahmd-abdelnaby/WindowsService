using Repository;
using System;
using System.Linq;
using VictoryAPI.Models;
using Service.ViewModels;

namespace Service
{
    public class RequestService
    {
        private readonly IUnitOfWork unit;
        public RequestService(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public bool MobileExist(int MobileNumber)
        {
            var MobleExist = unit.Repository<Request>().entity.FirstOrDefault(r => r.MobileNumber == MobileNumber);
            if (MobleExist != null)
                return true;
            return false;
        }

        public int AddRequest(RequestVM request)
        {
            Request NewRequest = new Request { MobileNumber = request.MobileNumber, ReuestDate = DateTime.Now, RequestId = 0 };
            unit.Repository<Request>().Add(NewRequest);
            return unit.Save();
        }
    }
}
