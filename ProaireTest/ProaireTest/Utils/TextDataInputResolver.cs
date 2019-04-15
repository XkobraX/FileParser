using ProaireTest.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace ProaireTest.Utils
{
    public static class TextDataInputResolver
    {
        public static List<Vendas> NormalizarTexto(HttpPostedFileBase file)
        {
            List<Vendas> listaTotal = new List<Vendas>();

            using (StreamReader sr = new StreamReader(file.InputStream))
            {
                string fileText = sr.ReadToEnd();
                var splitTextTotal = fileText.Split('\n');
                var cabecalho = splitTextTotal[0];

                List<String> listaDados = new List<string>();

                for (int i = 1; i < splitTextTotal.Length; i++)
                    listaDados.Add(splitTextTotal[i]);



                foreach (var item in listaDados)
                {
                    var newItem = item.Replace("\r", string.Empty);
                    var dados = newItem.Split('\t');
                    decimal precoConvertido;
                    decimal.TryParse(dados[2], NumberStyles.Currency, CultureInfo.InvariantCulture, out precoConvertido);
                    int qtdConvertida;
                    Int32.TryParse(dados[3], out qtdConvertida);

                    listaTotal.Add(new Vendas()
                    {
                        Comprador = dados[0],
                        Descricao = dados[1],
                        Preco = precoConvertido,//Ajustar Formatação de Decimal
                        Quantidade = qtdConvertida,
                        Endereco = dados[4],
                        Fornecedor = dados[5]
                    }
                     );
                }

                return listaTotal;
            }
        }


    }
}