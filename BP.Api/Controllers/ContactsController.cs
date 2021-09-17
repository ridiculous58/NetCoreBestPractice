using Bp.Api.Models;
using Bp.Api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IContactService _contactService;
        private readonly ILogger<ContactsController> _logger;
        public ContactsController(IConfiguration configuration, IContactService contactService, ILogger<ContactsController> logger)
        {
            _configuration = configuration;
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogTrace("LogTrace => Working Get Function");
            _logger.LogInformation("LogInformation => Working Get Function");
            _logger.LogWarning("LogWarning => Working Get Function");
            _logger.LogDebug("LogDebug => Working Get Function");
            _logger.LogError("LogError => Working Get Function");

            return _configuration["ReadMe"].ToString();
        }

        [ResponseCache(Duration = 10)]
        [HttpGet("{id}")]
        public ContactDTO GetContactById(int id)
        {
            return _contactService.GetContectById(id);
        }

        [HttpPost]
        public ContactDTO CreateContact(ContactDTO contact)
        {
            //create contact on DB
            return contact;
        }
    }
}
