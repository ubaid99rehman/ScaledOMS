using OMS.Core.Models;
using OMS.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OMS.DataAccess.Repositories.AppRepositories;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Enums;
using OMS.Core.Models.Orders;

namespace OMS.SqlData.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        IStockRepository _stockDataService;
        //Constructor
        public OrderRepository(IStockRepository stockDataService)
        {
            _connectionString = DbHelper.Connection;
            _stockDataService = stockDataService;
        }

        //Public Data Access Methods Implementation
        public IEnumerable<IOrder> GetAll()
        {
            var orders = new List<IOrder>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT s.StockSymbol,o.* FROM Orders o inner join Stocks s on s.StockID = o.StockID", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            OrderID = (int)reader["OrderID"],
                            OrderDate = (DateTime)reader["OrderDate"],
                            Symbol = reader["StockSymbol"].ToString(),
                            OrderType = (OrderType)((int)reader["OrderType"]),
                            Quantity = (int)reader["Quantity"],
                            Price = (decimal)reader["Price"],
                            Total = (decimal)reader["Total"],
                            Status = (OrderStatus)((int)reader["Status"]),
                            AccountID = (int)reader["AccountID"],
                            CreatedDate = (DateTime)reader["CreatedDate"],
                            LasUpdatedDate = (DateTime)reader["LastUpdatedDate"],
                            AddedBy = (int)reader["AddedBy"]
                        });
                    }
                }
            }
            return orders;
        }
        public IOrder GetById(int orderID)
        {
            IOrder order = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Orders WHERE OrderID = @OrderID", connection);
                command.Parameters.AddWithValue("@OrderID", orderID);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order = new Order
                        {
                            OrderID = (int)reader["OrderID"],
                            OrderDate = (DateTime)reader["OrderDate"],
                            Symbol = reader["StockSymbol"].ToString(),
                            OrderType = (OrderType)((int)reader["OrderType"]),
                            Quantity = (int)reader["Quantity"],
                            Price = (decimal)reader["Price"],
                            Total = (decimal)reader["Total"],
                            Status = (OrderStatus)((int)reader["Status"]),
                            AccountID = (int)reader["AccountID"],
                            CreatedDate = (DateTime)reader["CreatedDate"],
                            AddedBy = (int)reader["AddedBy"]
                        };
                    }
                }
            }
            return order;
        }
        public bool Add(IOrder order)
        {
            int StockID = _stockDataService.GetBySymbol(order.Symbol).ID;
            bool isAdded = false;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Orders (OrderDate, StockID, OrderType, Quantity," +
                    " Price, Total, Status, AccountID, ExpirationDate, LastUpdatedDate, CreatedDate, AddedBy)" +
                    " VALUES (@OrderDate, @StockID, @OrderType, @Quantity, @Price, @Total, @Status, @AccountID," +
                    " @ExpirationDate, @LastUpdatedDate, @CreatedDate, @AddedBy)", connection);

                //command.Parameters.AddWithValue("@StockSymbol", order.Symbol);
                command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                command.Parameters.AddWithValue("@StockID", StockID);
                command.Parameters.AddWithValue("@OrderType", (int) order.OrderType);
                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@Price", order.Price);
                command.Parameters.AddWithValue("@Total", order.Total);
                command.Parameters.AddWithValue("@Status", (int) order.Status);
                command.Parameters.AddWithValue("@AccountID", order.AccountID);
                command.Parameters.AddWithValue("@ExpirationDate", DateTime.Now);
                command.Parameters.AddWithValue("@LastUpdatedDate", DateTime.Now.AddDays(2));
                command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                command.Parameters.AddWithValue("@AddedBy", order.AddedBy);
                connection.Open();
                command.ExecuteNonQuery();
                isAdded = true;
            }
            return isAdded; 
        }
        public bool Update(IOrder order)
        {
            bool isUpdated = false;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Orders SET Quantity = @Quantity, Price = @Price, Total = @Total, " +
                    "Status = @Status, AccountID = @AccountID WHERE OrderID = @OrderID", connection);
                command.Parameters.AddWithValue("@OrderID", order.OrderID);
                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@Price", order.Price);
                command.Parameters.AddWithValue("@Total", order.Total);
                command.Parameters.AddWithValue("@Status", (int) order.Status);
                command.Parameters.AddWithValue("@AccountID", order.AccountID);
                connection.Open();
                command.ExecuteNonQuery();
                isUpdated = true;
            }
            return isUpdated;
        }
        public bool Delete(IOrder order)
        {
            bool isDeleted = false;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE ORDERS SET STATUS = @OrderStaus WHERE OrderID = @OrderID", connection);
                command.Parameters.AddWithValue("@OrderID", order.OrderID);
                command.Parameters.AddWithValue("@OrderStaus", order.Status);
                connection.Open();
                command.ExecuteNonQuery();

                if(GetById((int)order.OrderID) == null)
                {
                    isDeleted = true;
                }
            }
            return isDeleted;
        }
    }
}
