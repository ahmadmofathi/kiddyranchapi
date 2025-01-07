using KiddyRanchWeb.DAL.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class IncomeRepo : IIncomeRepo
    {

        private readonly AppDbContext _context;

        public IncomeRepo(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Income income)
        {
            _context.Incomes.Add(income);
        }

        public void Delete(Income income)
        {
            _context.Incomes.Remove(income);
        }

        public Income? GetIncomeByID(string id)
        {
            return _context.Incomes.Find(id);
        }

        public async  Task<(List<Income> incomes, int totalRecords, int totalMoney)> GetAllIncome(
            int pageNumber = 1,
            int pageSize = 10,
            string paymentType = "All",
            string branch = "All",
            DateTime? startDate = null,
            DateTime? endDate = null,
            string searchText = ""
            )
        {
            var query =  _context.Incomes.AsQueryable();

            if (paymentType != "All")
            {
                query = query.Where(i => i.PaymentType == paymentType);
            }

            if (branch != "All")
            {
                query = query.Where(i => i.Branch == branch);
            }

            if (startDate.HasValue || endDate.HasValue)
            { 
              query = query.Where (i=>(!startDate.HasValue || i.PaymentDate >= startDate.Value) &&
                                      (!endDate.HasValue || i.PaymentDate <= endDate.Value));
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(i => i.IncomeName.Contains(searchText) || i.IncomeDescription.Contains(searchText));
            }
            int totalIncome =  (int)query.Sum(i => i.PaymentAmount);

            return (await query.OrderByDescending(i => i.PaymentDate)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync() ,query.Count(),totalIncome);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(Income income)
        {

        }
    }
}
