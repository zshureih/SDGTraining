﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeoplePro.Dal.Extensions;
using PeoplePro.Dal.Interfaces;
using PeoplePro.Dal.Models;

namespace PeoplePro.Dal.Infrastructure
{
    public sealed class PeopleProRepo : IPeopleProRepo
    {
        private IPeopleProContext p_context;
        private bool p_disposed;


        public PeopleProRepo(IPeopleProContext context)
        {
            p_context = context;
        }

        #region access

        #region employees
        public IQueryable<Employee> GetEmployees()
        {
            return p_context.Employees;
        }
        public IEnumerable<Employee> GetEmployees(Func<Employee, bool> predicate)
        {
            return p_context.Employees.Where(predicate);
        }

        public Employee GetEmployee(Func<Employee, bool> predicate)
        {
            return GetEmployees().SingleOrDefault(predicate);
        }
        public Employee GetEmployee(int id)
        {
            return GetEmployee(t => t.Id == id);
        }

        #endregion
        #endregion



        /// <summary>
        /// Abstracts the saving process for an item in the Db Context.
        /// </summary>
        private void DoSave<T>(DbSet<T> dbSet, T entity, int entityId, Func<T, bool> predicate) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(entity.GetType().Name);
            }

            if (entityId <= 0)
            {
                dbSet.Add(entity);
            }
            else
            {
                var old = dbSet.SingleOrDefault(predicate);
                entity.CopyTo(old);
            }

            p_context.SaveChanges();
        }

        #region Disposal
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031",
            Justification = "Swallows general exceptions, to prevent the service from being disabled.")]
        private void Dispose(bool isDisposing)
        {
            if (!p_disposed)
            {
                try
                {
                    // Called from the IDisposable Method
                    // Nothing happens when isDisposing is false, since we don't have unmanaged resources
                    if (isDisposing)
                    {
                        try
                        {
                            if (p_context != null)
                            {
                                p_context.Dispose();
                            }
                        }
                        catch (Exception)
                        {
                            // This is intended to swallow up any exceptions to prevent the service from being crashing.
                        }
                    }
                }
                finally
                {
                    p_disposed = true;
                }
            }
        }

        IQueryable<PeoplePro.Dal.Models.Employee> IPeopleProRepo.GetEmployees()
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}
