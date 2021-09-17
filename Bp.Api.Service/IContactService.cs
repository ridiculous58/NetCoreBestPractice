using Bp.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bp.Api.Service
{
    public interface IContactService
    {
        public ContactDTO GetContectById(int id);
    }
}
