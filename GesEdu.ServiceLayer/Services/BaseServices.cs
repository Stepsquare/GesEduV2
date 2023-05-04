using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.ServiceLayer.Services
{
    public abstract class BaseServices
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IGenericRestRequests _genericRestRequests;

        public BaseServices(IUnitOfWork unitOfWork, IGenericRestRequests genericRestRequests)
        {
            _unitOfWork = unitOfWork;
            _genericRestRequests = genericRestRequests;
        }
    }
}
