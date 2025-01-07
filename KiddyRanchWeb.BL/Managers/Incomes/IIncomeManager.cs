using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public interface IIncomeManager
    {
        Task<(IEnumerable<IncomeReadDTO> incomes,int totalRecords,int totalMoney)> GetAllIncome(
            int pageNumber,
            int pageSize,
            string incomeType,
            string branch,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string searchText =""
            );
        IncomeReadDTO? GetIncomeById(string id);
        string Add(IncomeAddDTO income);
        bool Update(IncomeUpdateDTO income);
        bool Delete(string id);
    }
}
