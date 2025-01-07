using KiddyRanchWeb.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class IncomeManager : IIncomeManager
    {
        private readonly IIncomeRepo _incomeRepo;

        public IncomeManager(IIncomeRepo incomeRepo)
        {
            _incomeRepo = incomeRepo;
        }
        public string Add(IncomeAddDTO income)
        {
            Income _income = new Income
            {
                IncomeId = Guid.NewGuid().ToString(),
                IncomeName = income.IncomeName,
                IncomeDescription = income.IncomeDescription,
                IncomeType = income.IncomeType,
                PaymentAmount = income.PaymentAmount,
                PaymentType = income.PaymentType,
                PaymentDate = income.PaymentDate,
                TotalPaymentAmount = income.TotalPaymentAmount,
                RemainingAmount = income.RemainingAmount,
                RemainingDeadlineDate = income.RemainingDeadlineDate,
                Branch = income.Branch,
            };
            if (_income.IncomeType != null)
            {
            _income.nextPaymentDate = CalculateNextPaymentDate(_income.IncomeType);
            }
            _incomeRepo.Add(_income);
            _incomeRepo.SaveChanges();
            if (_income.IncomeId == null)
            {
                return "Not Found";
            }
            return _income.IncomeId;
        }

    private DateTime CalculateNextPaymentDate(string incomeType)
    {
      DateTime paymentDate = DateTime.Now; 

      switch (incomeType)
      {
        case "Monthly":
          paymentDate = paymentDate.AddMonths(1);
          break;
        case "Quarterly":
          paymentDate = paymentDate.AddMonths(3);
          break;
        case "SemiAnnual":
          paymentDate = paymentDate.AddMonths(6);
          break;
        case "Annual":
          paymentDate = paymentDate.AddYears(1);
          break;
        default:
          break;
      }

      return paymentDate;
    }

        public bool Delete(string id)
        {
            Income? income = _incomeRepo.GetIncomeByID(id);
            if (income is null)
            {
                return false;
            }
            _incomeRepo.Delete(income);
            _incomeRepo.SaveChanges();
            return true;
        }

        public async Task<(IEnumerable<IncomeReadDTO> incomes, int totalRecords, int totalMoney)> GetAllIncome(
            int pageNumber = 1,
            int pageSize = 10,
            string paymentType = "All",
            string branch = "All",
            DateTime? startDate = null,
            DateTime? endDate = null,
            string searchText ="")
        {
            var count=0;
            var total = 0;
            (IEnumerable<Income> incomeFromDB,count,total) = await _incomeRepo.GetAllIncome(pageNumber,pageSize,paymentType,branch,startDate,endDate,searchText);
            return (incomeFromDB.Select(i => new IncomeReadDTO
            {
                IncomeId = i.IncomeId,
                IncomeDescription = i.IncomeDescription,
                IncomeType = i.IncomeType,
                IncomeName = i.IncomeName,
                PaymentDate = i.PaymentDate,
                PaymentType = i.PaymentType,
                PaymentAmount = i.PaymentAmount,
                nextPaymentDate = i.nextPaymentDate,
                RemainingAmount = i.RemainingAmount,
                RemainingDeadlineDate = i.RemainingDeadlineDate,
                TotalPaymentAmount = i.TotalPaymentAmount,
                Branch = i.Branch,
            }),count,total);
        }

        public IncomeReadDTO? GetIncomeById(string id)
        {
            Income? income = _incomeRepo.GetIncomeByID(id);
            if (income is null)
            {
                return null;
            }

            return new IncomeReadDTO
            {
                IncomeId = income.IncomeId,
                IncomeDescription = income.IncomeDescription,
                IncomeType = income.IncomeType,
                IncomeName = income.IncomeName,
                PaymentDate = income.PaymentDate,
                PaymentType = income.PaymentType,
                PaymentAmount = income.PaymentAmount,
                RemainingAmount = income.RemainingAmount,
                RemainingDeadlineDate = income.RemainingDeadlineDate,
                TotalPaymentAmount = income.TotalPaymentAmount,
                Branch = income.Branch,
            };
        }

        public bool Update(IncomeUpdateDTO income)
        {
            if (income.IncomeId is null)
            {
                return false;
            }
            Income? _income = _incomeRepo.GetIncomeByID(income.IncomeId);
            if (_income is null)
            {
                return false;
            }
            _income.IncomeDescription = income.IncomeDescription;
            _income.PaymentAmount = income.PaymentAmount;
            _income.PaymentType = income.PaymentType;
            _income.IncomeName = income.IncomeName;
            _income.IncomeId = income.IncomeId;
            _income.PaymentDate = income.PaymentDate;
            _income.IncomeType = income.IncomeType;
            _income.RemainingAmount = income.RemainingAmount;
            _income.TotalPaymentAmount = income.TotalPaymentAmount;
            _income.RemainingDeadlineDate = income.RemainingDeadlineDate;
            _income.Branch = income.Branch;
            _incomeRepo.Update(_income);
            _incomeRepo.SaveChanges();
            return true;
        }
    }
}
