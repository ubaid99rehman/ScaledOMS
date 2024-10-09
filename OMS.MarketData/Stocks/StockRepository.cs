using OMS.Core.Models.Stocks;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.SqlData.Model;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System;

namespace OMS.MarketData
{
    public class StockRepository : IStockRepository
    {
        OMSContext Context;
        IMapper Mapper;

        //Constructor
        public StockRepository(IMapper mapper)
        {
            Context = new OMSContext();
            Mapper = mapper;
        }

        //Public Data Access Methods
        public IEnumerable<IStock> GetAll()
        {
            var stocks = Context.Stocks.ToList();
            if (stocks.Count < 0 || stocks == null)
            {
                return new List<IStock>();
            }

            ICollection<IStock> stocksList = new List<IStock>();
            try
            {
                stocksList = stocks.Select(o => Mapper.Map<IStock>(o)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stocksList;
        }
        public IStock GetBySymbol(string symbol)
        {

            Stocks stockEntity = Context.Stocks.Where(s=> s.StockSymbol == symbol).FirstOrDefault();
            IStock stock = Mapper.Map<IStock>(stockEntity);
            return stock;
        }
        public IEnumerable<string> GetStockSymbols()
        {
            var list = GetAll().Select(s => s.Symbol).ToList();
            return list;
        }
    }
}