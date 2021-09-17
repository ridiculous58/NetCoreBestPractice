using AutoMapper;
using Bp.Api.Data.Models;
using Bp.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bp.Api.Service
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactService(IMapper mapper,IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }
        public ContactDTO GetContectById(int id)
        {
            //veritabanından kaydın getirilmesi

            Contact contact = GetDummyContact();

            var client = _httpClientFactory.CreateClient("garantiapi");

            //ContactDTO contactDTO = new ContactDTO();
            //var result = _mapper.Map(contact, contactDTO);

            var result = _mapper.Map<ContactDTO>(contact);
            return result;

        }

        private Contact GetDummyContact()
        {
            return new Contact
            {
                Id = 1,
                FirstName = "Emir",
                LastName = "Han"
            };
        }
    }
}
