using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WitnessReports.Models;
using WitnessReports.Services.Interfaces;

namespace WitnessReports.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IPhoneNumberValidityService _numberValidityService;

        public ReportController(IPhoneNumberValidityService numberValidityService)
        {
            _numberValidityService = numberValidityService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromBody] RequestData data)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Full name and phone number must not be empty");
            }

            if (!_numberValidityService.IsPhoneNumberValid(data.PhoneNumber))
            {
                return new BadRequestObjectResult("Invalid phone number!");
            }

            // check if the report exists on FBI api

            // generate file

            return Ok();
        }
    }
}
