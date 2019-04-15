using ProaireTest.Data;
using ProaireTest.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProaireTest.Services
{
    public class BaseService
    {

        private BaseRepository _repo;

        public BaseService()
        {

            if (_repo == null)
            {
                _repo = new BaseRepository();
            }
        }

        public void Add(Vendas obj)
        {

            _repo.Add(obj);
        }

        public void SalvarVendaEmLote(List<Vendas> objList)
        {

            foreach (var item in objList)
            {
                _repo.Add(item);
            }
        }

        public void Dispose()
        {
            _repo.Dispose();
        }

        public IEnumerable<Vendas> GetAll()
        {
            return this._repo.GetAll();
        }

        public Vendas GetById(int id)
        {
            return this._repo.GetById(id);
        }

        public void Remove(Vendas obj)
        {
            _repo.Remove(obj);

        }

        public void Update(Vendas obj)
        {
            _repo.Update(obj);
        }

    }
}