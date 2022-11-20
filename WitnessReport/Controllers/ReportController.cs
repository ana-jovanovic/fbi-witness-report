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
        private readonly IFileGenerationService _fileGenerationService;

        public ReportController(IPhoneNumberValidityService numberValidityService,
            IFbiDataService fbiDataService,
            IFileGenerationService fileGenerationService)
        {
            _numberValidityService = numberValidityService;
            _fbiDataService = fbiDataService;
            _fileGenerationService = fileGenerationService;
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
                _fileGenerationService.GenerateFile(fugitive);
                return new OkObjectResult("The file has been successfully generated in the C: directory.");
            }

            return new BadRequestObjectResult("The file was not created!");
        }
    }
}
