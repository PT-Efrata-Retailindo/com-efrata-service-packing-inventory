using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearCountry;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.Monitoring
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/monitoring/omzet-year-country")]
    [Authorize]
    public class OmzetYearCountryController : ControllerBase
    {
        private readonly IOmzetYearCountryService _service;
        private readonly IIdentityProvider _identityProvider;

        public OmzetYearCountryController(IOmzetYearCountryService service, IIdentityProvider identityProvider)
        {
            _service = service;
            _identityProvider = identityProvider;
        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                VerifyUser();

                var accept = Request.Headers["Accept"];

                if (accept == "application/xls")
                {
                    var result = _service.GenerateExcel(year);

                    return File(result.Data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.FileName);
                }
                else
                {
                    var data = _service.GetReportData(year);

                    return Ok(new
                    {
                        data
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
