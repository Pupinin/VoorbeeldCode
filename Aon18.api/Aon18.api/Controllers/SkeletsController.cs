using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aon18.data.DomainClasses;
using Aon18.data.Interfaces;

namespace Aon18.api.Controllers
{
    public class SkeletsController : ApiController
    {
        private readonly ISkeletDbRepository _skeletDbRepository;

        public SkeletsController(ISkeletDbRepository skeletDbRepository)
        {
            _skeletDbRepository = skeletDbRepository;
        }

        public IHttpActionResult Post_addSkelet(Skelet skeletToAdd)
        {

            return Ok(_skeletDbRepository.AddSkelet(skeletToAdd));
        }

        public IHttpActionResult Delete_SkeletById(int id)
        {
            if (_skeletDbRepository.GetById(id) is null)
            {
                return NotFound();
            }

            _skeletDbRepository.DeleteById(id);

            return Ok();
        }
    }
}
