using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IHelpers;
using GesEdu.Shared.Interfaces.ISevices;

namespace GesEdu.ServiceLayer.Services
{
    public class UserServices : BaseServices, IUserServices
    {
        public UserServices(IUnitOfWork unitOfWork, IGenericRestRequests genericRestRequests) : base(unitOfWork, genericRestRequests)
        {
        }
    }
}
