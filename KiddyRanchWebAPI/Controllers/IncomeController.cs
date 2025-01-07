
using KiddyRanchWeb.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KiddyRanchWeb.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {

        private readonly IIncomeManager _incomeManager;

        public IncomeController(IIncomeManager incomeManager)
        {
            _incomeManager = incomeManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncome(
            int pageNumber = 1,
            int pageSize = 10,
            string paymentType = "All",
            string branch = "All",
            DateTime? startDate = null,
            DateTime? endDate = null,
            string searchText = "")

        {
            (var income,var count,var total) = await _incomeManager.GetAllIncome(pageNumber,pageSize,paymentType,branch,startDate,endDate,searchText);

            return Ok(new { data = income, totalMoney=total, totalRecords= count,totalPages= count/pageSize });
        }


        [HttpGet("{id}")]
        public IActionResult GetIncomeById(string id)
        {
            var income = _incomeManager.GetIncomeById(id);

            if (income == null)
            {
                return NotFound();
            }

            return Ok(income);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIncome(string id)
        {
            if (id == null)
            {
                return NotFound("Income id not found");
            }
            _incomeManager.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateIncome(string id, IncomeUpdateDTO income)
        {
            if (id != income.IncomeId)
            {
                return BadRequest();
            }

            _incomeManager.Update(income);

            return NoContent();
        }

    [HttpPost]
    public IActionResult CreateIncome(IncomeAddDTO income)
    {
      try
      {
        var res = _incomeManager.Add(income);
        return Ok(new
        {
          message = "Income added successfully",
          Id = res
        });
      }
      catch (Exception ex)
      {
        while (ex.InnerException != null)
        {
          ex = ex.InnerException;
        }
        return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
      }
    }


   
    }
}
