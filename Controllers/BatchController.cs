using BatchProcessingFramework;
using BatchProcessingFramework.Model;
using BatchProcessingFramework.Requests;
using BatchProcessingFramework.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BatchProcessingFramework.Profiles;

namespace BatchProcessingAPI.Controllers
{
    [RoutePrefix("api/batch")]
    public class BatchController : ApiController
    {
        [HttpGet]
        [Route("processnodedataset")]
        public GenericResponse<BatchResponse> ProcessNodeDataSet()
        {
            return new GenericResponse<BatchResponse>();
        }
        [HttpPost]
        [Route("processnodedataset")]
        public GenericResponse<BatchResponse> ProcessNodeDataSet(BaseRequest request)
        {
            var returnValue = new GenericResponse<BatchResponse>();
            try
            {
                var applicationRepository = new ApplicationRepository();
                if (applicationRepository.IsProfileExists(request.profileName))
                {
                    if (!string.IsNullOrEmpty(request.DataString))
                    {
                        var baseProfile = new BaseProfile();
                        baseProfile.ProcessProfile(request);
                    }
                    else
                    {
                        var invalidProfileName = new Exception("Empty JSON string found.");
                        throw invalidProfileName;
                    }
                }
                else
                {
                    var invalidProfileName = new Exception("Profilename not found.");
                    throw invalidProfileName;
                }
                returnValue.Data = null;
                returnValue.HasError = false;
            }
            catch (Exception ex)
            {
                returnValue.HasError = true;
                returnValue.ErrorMessage = ex.Message;
            }
            return returnValue;
        }


    }
}
