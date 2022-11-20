using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WitnessReport.Models;
using WitnessReport.Services.Interfaces;

namespace WitnessReport.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IPhoneNumberValidityService _numberValidityService;
        private readonly IFbiDataService _fbiDataService;

        public ReportController(IPhoneNumberValidityService numberValidityService,
            IFbiDataService fbiDataService)
        {
            _numberValidityService = numberValidityService;
            _fbiDataService = fbiDataService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromBody] RequestData data)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Full name and phone number must not be empty");
            }

            if (!_numberValidityService.IsPhoneNumberValid(data.PhoneNumber))
            {
                return new BadRequestObjectResult("Invalid phone number!");
            }

            var fugitive = await _fbiDataService.GetMostWanted(data.FullName);

            if (fugitive != null)
            {
                //generate file
            }

            return Ok();
        }
    }
}
