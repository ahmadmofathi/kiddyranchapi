using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public interface IIncomeRepo
    {
        Task<(List<Income> incomes,int totalRecords,int totalMoney)> GetAllIncome(
            int pageNumber,
            int pageSize,
            string incomeType,
            string branch,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string searchText = "");
        Income? GetIncomeByID(string id);
        void Add(Income income);
        void Update(Income income);
        void Delete(Income income);
        int SaveChanges();
    }
}
