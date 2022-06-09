﻿using Business.Abstract;
using Business.Constans;
using Core.Aspects.Caching;
using Core.Aspects.Transaction;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BaBsReconciliationDetailManager : IBaBsReconciliationDetailService
    {
        private readonly IBaBsReconciliationDetailDal _baBsReconciliationDetailDal;

        public BaBsReconciliationDetailManager(IBaBsReconciliationDetailDal baBsReconciliationDetailDal)
        {
            _baBsReconciliationDetailDal = baBsReconciliationDetailDal;
        }

        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]
        public IResult Add(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            _baBsReconciliationDetailDal.Add(baBsReconciliationDetail);
            return new SuccessResult(Messages.AddedBaBsReconciliationDetail);
        }

        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath, int baBsReconciliationId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string description = reader.GetString(1);


                        if (description != "Açıklama" && description != null)
                        {
                            DateTime date = reader.GetDateTime(0);
                            double amount = reader.GetDouble(2);

                            BaBsReconciliationDetail baBsReconciliationDetail = new BaBsReconciliationDetail()
                            {
                                BaBsReconciliationId = baBsReconciliationId,
                                Amount = Convert.ToDecimal(amount),
                                Date = date,
                                Description = description,


                            };
                            _baBsReconciliationDetailDal.Add(baBsReconciliationDetail);
                        }
                    }
                }
            }

            File.Delete(filePath);

            return new SuccessResult(Messages.AddedBaBsReconciliationDetail);

        }

        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]
        public IResult Delete(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            _baBsReconciliationDetailDal.Delete(baBsReconciliationDetail);
            return new SuccessResult(Messages.DeletedBaBsReconciliationDetail);
        }

        [CacheAspect(60)]
        public IDataResult<BaBsReconciliationDetail> GetById(int id)
        {
            return new SuccessDataResult<BaBsReconciliationDetail>(_baBsReconciliationDetailDal.Get(x => x.BaBsReconciliationId == id));
        }

        [CacheAspect(60)]
        public IDataResult<List<BaBsReconciliationDetail>> GetList(int baBsReconciliationId)
        {
            return new SuccessDataResult<List<BaBsReconciliationDetail>>(_baBsReconciliationDetailDal.GetList(x => x.BaBsReconciliationId == baBsReconciliationId));
        }

        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]
        public IResult Update(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            _baBsReconciliationDetailDal.Update(baBsReconciliationDetail);
            return new SuccessResult(Messages.UpdatedBaBsReconciliationDetail);
        }
    }

}

