using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProaireTest.Services;
using System.Linq;
namespace TestProject.Proaire
{
    [TestClass]
    public class ProaireTestUnit
    {
        private BaseService _service;

        public ProaireTestUnit()
        {
            if (_service == null)
            {
                this._service = new BaseService();
            }
        }
        [TestMethod]
        public void GetAllVendasTest()
        {
            var dados = this._service.GetAll();
            Assert.IsNotNull(dados);
        }
        [TestMethod]
        public void InsertVendasTest()
        {
            this._service.Add(new ProaireTest.Data.Vendas()
            {
                Comprador = "Teste-Unit",
                Quantidade = 99999
           
            });

            var venda = _service.GetAll().FirstOrDefault(vd => vd.Comprador == "Teste-Unit");

            Assert.IsNotNull(venda);
        }
        [TestMethod]
        public void DeleteVendasTest()
        {
            this._service.Add(new ProaireTest.Data.Vendas()
            {
                Comprador = "Teste-Unit-Delete",
                Quantidade = 99999

            });

            var venda = _service.GetAll().FirstOrDefault(vd => vd.Comprador == "Teste-Unit-Delete");
            _service.Remove(venda);

            venda = _service.GetAll().FirstOrDefault(vd => vd.Comprador == "Teste-Unit-Delete");

            Assert.IsNull(venda);
        }
        [TestMethod]
        public void UpdateVendasTest()
        {
           
            var venda = _service.GetAll().FirstOrDefault();
            venda.Comprador = "Teste-Unit-Update-02";

            _service.Update(venda);
            var lista = _service.GetAll();

            venda = lista.FirstOrDefault(vd => vd.Id == venda.Id);

            Assert.AreEqual(venda.Comprador, "Teste-Unit-Update-02");
        }

    }
}
