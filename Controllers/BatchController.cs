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

namespace BatchProcessingAPI.Controllers
{
    public class BatchController : ApiController
    {
        [HttpPost]
        [Route("processnodedataset")]
        public GenericResponse<BatchResponse> ProcessNodeDataSet(BaseRequest request)
        {
            //if (request.profileName == "order")
            //{
            //    JsonConvert.DeserializeObject<OrderProfileRequest>(JsonConvert.SerializeObject(request));
            //}
            //else
            //{

            //}
            var returnValue = new GenericResponse<BatchResponse>();
            try
            {
                var applicationRepository = new ApplicationRepository();
                if (applicationRepository.IsProfileExists(request.profileName))
                {
                    request.profile.ProcessProfile(request);
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
