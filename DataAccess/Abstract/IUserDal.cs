using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Core.Entities.Concrete; 

namespace DataAccess.Abstract
{
    public interface IUserDal : IRepositoryDal<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
