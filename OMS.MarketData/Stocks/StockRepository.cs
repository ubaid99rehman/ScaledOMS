using OMS.Core.Models;
using OMS.Core.Models.Stocks;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Helpers;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OMS.MarketData.Stocks
{
    public class StockRepository : IStockRepository
    {
        private readonly string _connectionString;
        //Constructor
        public StockRepository()
        {
            _connectionString = DbHelper.Connection;
        }
        //Public Data Access Methods
        public IEnumerable<IStock> GetAll()
        {
            var stocks = new List<Stock>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM STOCKS", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stocks.Add(new Stock
                        {
                            ID = (int)reader["StockID"],
                            Name= (string)reader["StockName"],
                            Symbol = (string)reader["StockSymbol"],
                            LastPrice = (decimal)reader["LastPrice"]
                        });
                    }
                }
            }
            return stocks;
        }
        public IStock GetById(int id)
        {
            IStock stock = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM STCKS WHERE STOCKID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stock = new Stock
                        {
                            ID = (int)reader["StockID"],
                            Name = (string)reader["StockName"],
                            Symbol = (string)reader["StockSymbol"],
                            LastPrice = (decimal)reader["LastPrice"]
                        };
                    }
                }
            }
            return stock;
        }
        public IStock GetBySymbol(string symbol)
        {
            IStock stock = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM STOCKS WHERE StockSymbol = @Symbol", connection);
                command.Parameters.AddWithValue("@Symbol", symbol);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stock = new Stock
                        {
                            ID = (int)reader["StockID"],
                            Name = (string)reader["StockName"],
                            Symbol = (string)reader["StockSymbol"],
                            LastPrice = (decimal)reader["LastPrice"]
                        };
                    }
                }
            }
            return stock;
        }
        public IEnumerable<string> GetStockSymbols()
        {
            var stocks = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT StockSymbol FROM STOCKS", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stocks.Add(reader["StockSymbol"].ToString());
                    }
                }
            }
            return stocks;
        }
    }
}