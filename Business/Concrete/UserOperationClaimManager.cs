﻿using Business.Abstract;
using Business.BusinessAspects;
using Business.Constans;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        [SecuredOperation("Admin,UserOperationClaim.Add")]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.AddedUserOperationClaim);
        }

        [SecuredOperation("Admin,UserOperationClaim.Delete")]
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.DeletedUserOperationClaim);
        }

        [SecuredOperation("Admin,UserOperationClaim.GetById")]
        public IDataResult<UserOperationClaim> GetById(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(x => x.Id == id));
        }

        [SecuredOperation("Admin,UserOperationClaim.GetList")]
        public IDataResult<List<UserOperationClaim>> GetList(int userId, int companyId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetList(x => x.UserId == userId && x.CompanyId == companyId));
        }

        [SecuredOperation("Admin,UserOperationClaim.Update")]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.UpdatedUserOperationClaim);
        }
    }
}
