using Model.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using VictoryAPI.Models;

namespace Repository
{
    public class UnitOfwork : IUnitOfWork
    {
        private readonly Context context;
        private Hashtable repositories;

        public UnitOfwork(Context context)
        {
            this.context = context;
        }

        public Repository<T> Repository<T>() where T : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)repositories[type];
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
