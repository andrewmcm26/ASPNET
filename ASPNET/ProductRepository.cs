using System;
using System.Collections.Generic;
using System.Data;
using ASPNET.Models;
using Dapper;

namespace ASPNET
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public ProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM Products;");
        }

        public Product GetProduct(int id)
        {
            return _conn.QuerySingle<Product>("SELECT * from Products " +
                "WHERE ProductID = @id;", new {id});
        }

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE Products SET Name=@name, Price=@price, StockLevel=@stockLevel WHERE ProductID=@productID",
                new
                {
                    productID = product.ProductID,
                    name = product.Name,
                    price = product.Price,
                    stockLevel = product.StockLevel,
                }); 
        }
    }
}
