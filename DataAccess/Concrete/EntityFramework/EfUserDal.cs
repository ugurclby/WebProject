using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfRepositoryBase<User, NorthWindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new NorthWindContext())
            {
                var result = from operationClaim in context.OperationClaim
                    join userOperationClaim in context.OperationClaimUsers
                        on operationClaim.id equals userOperationClaim.OperationClaimId
                    where userOperationClaim.UserId == user.id
                    select new OperationClaim { id = operationClaim.id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
    }
}
