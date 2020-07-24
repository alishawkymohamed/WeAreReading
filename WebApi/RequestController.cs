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


        [HttpGet("GetNotRespondedReceivedRequests")]
        [ProducesResponseType(200, Type = typeof(List<RequestDTO>))]
        [Authorize]
        public IActionResult GetNotRespondedReceivedRequests()
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


        [HttpGet("GetNotRespondedSentRequests")]
        [ProducesResponseType(200, Type = typeof(List<RequestDTO>))]
        [Authorize]
        public IActionResult GetNotRespondedSentRequests()
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


        [HttpPost("AcceptRequest")]
        [ProducesResponseType(200, Type = typeof(void))]
        [Authorize]
        public IActionResult AcceptRequest(int requestId)
        {
            try
            {
                requestService.AcceptRequest(requestId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("RejectRequest")]
        [ProducesResponseType(200, Type = typeof(void))]
        [Authorize]
        public IActionResult RejectRequest(int requestId)
        {
            try
            {
                requestService.RejectRequest(requestId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("DeleteRequest")]
        [ProducesResponseType(200, Type = typeof(void))]
        [Authorize]
        public IActionResult DeleteRequest(int requestId)
        {
            try
            {
                requestService.DeleteRequest(requestId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
