﻿using IMS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Data;

namespace IMS.Data
{
    public class UnitOfWork
    {
        private Net1710_221_4_IMSContext _unitOfWorkContext;
        private MentorRepository _mentor;
        private CompanyRepository _company;
        private WorkingResultRepository _workingResult;
        private InternRepository _intern;
        private TaskRepository _task;
        public UnitOfWork()
        {
            _unitOfWorkContext ??= new Net1710_221_4_IMSContext();
        }
        public InternRepository InternRepository
        {
            get
            {
                return _intern ??= new Repository.InternRepository(_unitOfWorkContext);
            }
        }

        public MentorRepository MentorRepository
        {
            get
            {
                return _mentor ?? new MentorRepository(_unitOfWorkContext);
            }
        }

        public CompanyRepository CompanyRepository
        {
            get
            {
                return _company ?? new CompanyRepository();
            }

        }
        public WorkingResultRepository WorkingResultRepository
        {
            get
            {
                return _workingResult ?? new WorkingResultRepository();
            }
        }

        public TaskRepository TaskRepository
        {
            get
            {
                return _task ?? new TaskRepository();
            }
        }

        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = _unitOfWorkContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = await _unitOfWorkContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion
    }
}