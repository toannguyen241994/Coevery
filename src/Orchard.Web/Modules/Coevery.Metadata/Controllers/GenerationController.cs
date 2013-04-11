﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Coevery.Metadata.Services;
using Orchard.Localization;

namespace Coevery.Metadata.Controllers
{
    public class GenerationController : ApiController
    {
        private readonly IContentDefinitionService _contentDefinitionService;
        private readonly ITypeDeployService _typeDeployServicer;

        public GenerationController(IContentDefinitionService contentDefinitionService,
            ITypeDeployService typeDeployServicer)
        {
            _contentDefinitionService = contentDefinitionService;
            _typeDeployServicer = typeDeployServicer;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public HttpResponseMessage Post([FromBody]string name)
        {
            try
            {
                bool successful = _typeDeployServicer.DeployType(name);

                if (successful)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }
            catch (Exception) {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }
        }
    }
}