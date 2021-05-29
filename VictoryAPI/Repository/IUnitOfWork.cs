using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IUnitOfWork
    {
        int Save();
        public Repository<T> Repository<T>() where T : BaseEntity;
    }
}
