using Microsoft.AspNetCore.Mvc;
using TP24LendingApi.Models;
using TP24Entities.Models;
using AutoMapper;
using TP24LendingApi.Services;

namespace TP24LendingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class ReceivablesController : ControllerBase
    {
        private readonly IReceivablesService _receivablesService;
        private IMapper _mapper;

        public ReceivablesController(IReceivablesService receivablesService, IMapper mapper)
        {
            _receivablesService = receivablesService;
            _mapper = mapper;
        }

        [HttpPost(Name = "StoreReceivables")]
        public IActionResult StoreReceivables(List<ReceivableForCreationDto> data)
        {
            if (data is null)
            {
                return BadRequest("Data is null.");
            }

            var entitiyData = _mapper.Map<List<Receivable>>(data);
            _receivablesService.CreateReceivables(entitiyData);
            return Ok("Receivables data stored successfully.");
        }

        [HttpGet("summary")]
        public IActionResult GetSummaryStatistics()
        {
            return Ok(_receivablesService.GetSummaryStatistics());
        }

        [HttpGet("summary/{debtorReference}")]
        public IActionResult GetDebtorSummary(string debtorReference)
        {
            return Ok(_receivablesService.GetDebtorSummary(debtorReference));
        }
    }
}