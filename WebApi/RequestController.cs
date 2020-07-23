using System;
using System.Collections.Generic;
using Helpers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.DTOs;
using Services.Contracts;

namespace WebApi
{
    public class RequestController : BaseController<Request>
    {
        private readonly IRequestService requestService;
        private readonly ISessionService sessionService;

        public RequestController(IRequestService requestService, ISessionService sessionService) : base(requestService)
        {
            this.requestService = requestService;
            this.sessionService = sessionService;
        }


        [HttpGet("GetReceivedNotRespondedRequests")]
        [ProducesResponseType(200, Type = typeof(List<RequestDTO>))]
        [Authorize]
        public IActionResult GetReceivedNotRespondedRequests()
        {
            var userId = sessionService.UserId;
            if (userId.HasValue)
            {
                return Ok(requestService.GetReceivedNotRespondedRequests(userId.Value));
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpGet("GetSentNotRespondedRequests")]
        [ProducesResponseType(200, Type = typeof(List<RequestDTO>))]
        [Authorize]
        public IActionResult GetSentNotRespondedRequests()
        {
            var userId = sessionService.UserId;
            if (userId.HasValue)
            {
                return Ok(requestService.GetSentNotRespondedRequests(userId.Value));
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpGet("GetAcceptedReceivedRequests")]
        [ProducesResponseType(200, Type = typeof(List<RequestDTO>))]
        [Authorize]
        public IActionResult GetAcceptedReceivedRequests()
        {
            var userId = sessionService.UserId;
            if (userId.HasValue)
            {
                return Ok(requestService.GetAcceptedReceivedRequests(userId.Value));
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpGet("GetRejectedReceivedRequests")]
        [ProducesResponseType(200, Type = typeof(List<RequestDTO>))]
        [Authorize]
        public IActionResult GetRejectedReceivedRequests()
        {
            var userId = sessionService.UserId;
            if (userId.HasValue)
            {
                return Ok(requestService.GetRejectedReceivedRequests(userId.Value));
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpGet("GetAcceptedSentRequests")]
        [ProducesResponseType(200, Type = typeof(List<RequestDTO>))]
        [Authorize]
        public IActionResult GetAcceptedSentRequests()
        {
            var userId = sessionService.UserId;
            if (userId.HasValue)
            {
                return Ok(requestService.GetAcceptedSentRequests(userId.Value));
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpGet("GetRejectedSentRequests")]
        [ProducesResponseType(200, Type = typeof(List<RequestDTO>))]
        [Authorize]
        public IActionResult GetRejectedSentRequests()
        {
            var userId = sessionService.UserId;
            if (userId.HasValue)
            {
                return Ok(requestService.GetRejectedSentRequests(userId.Value));
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost("SendRequest")]
        [ProducesResponseType(200, Type = typeof(void))]
        [Authorize]
        public IActionResult SendRequest([FromBody] CreateRequestDTO request)
        {
            var req = this.requestService.GetRequest(request.BookId, request.SenderId, request.ReceiverId);
            if (req == null)
            {
                try
                {
                    var result = requestService.SendRequest(request);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("SentBefore");
            }
        }
    }
}
