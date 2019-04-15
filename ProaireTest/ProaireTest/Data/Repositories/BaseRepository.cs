using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProaireTest.Data.Repositories
{
    public class BaseRepository
    {
        protected ProDBEntities db = new ProDBEntities();
        public void Add(Vendas obj)
        {
          
            db.Set<Vendas>().Add(obj);
            db.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vendas> GetAll()
        {
            return db.Set<Vendas>().ToList();
        }

        public Vendas GetById(int id)
        {
            return db.Set<Vendas>().Find(id);
        }

        public void Remove(Vendas obj)
        {
            db.Set<Vendas>().Remove(obj);
            db.SaveChanges();

        }

        public void Update(Vendas obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Detached;
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}