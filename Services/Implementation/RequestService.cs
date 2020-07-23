using AutoMapper;
using Models.DbModels;
using Models.DTOs;
using Repos.Contracts;
using Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementation
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepo requestRepo;
        private readonly IMapper mapper;

        public RequestService(IRequestRepo requestRepo, IMapper mapper)
        {
            this.requestRepo = requestRepo;
            this.mapper = mapper;
        }

        public void AcceptRequest(int requestId)
        {
            var req = this.requestRepo.Get(x => x.Id == requestId);
            req.IsAccepted = true;
            this.requestRepo.Update(req);
        }

        public RequestDTO GetRequest(int bookId, int senderId, int receiverId)
        {
            return this.mapper.Map<RequestDTO>(this.requestRepo.Get(x => x.BookId == bookId && x.SenderId == senderId && x.ReceiverId == receiverId));
        }

        public List<RequestDTO> GetReceivedNotRespondedRequests(int userId)
        {
            var requests = this.requestRepo.GetAll(x => x.ReceiverId == userId && x.IsAccepted == null).ToList();
            var mapped = this.mapper.Map<List<RequestDTO>>(requests);
            return mapped;
        }

        public List<RequestDTO> GetSentNotRespondedRequests(int userId)
        {
            var requests = this.requestRepo.GetAll(x => x.SenderId == userId && x.IsAccepted == null).ToList();
            var mapped = this.mapper.Map<List<RequestDTO>>(requests);
            return mapped;
        }

        public void RejectRequest(int requestId)
        {
            var req = this.requestRepo.Get(x => x.Id == requestId);
            req.IsAccepted = false;
            this.requestRepo.Update(req);
        }

        public RequestDTO SendRequest(CreateRequestDTO request)
        {
            var mapped = this.mapper.Map<CreateRequestDTO, Request>(request);
            var result = this.requestRepo.Insert(mapped);
            return this.mapper.Map<Request, RequestDTO>(result);
        }

        public List<RequestDTO> GetAcceptedReceivedRequests(int userId)
        {
            var requests = this.requestRepo.GetAll(x => x.ReceiverId == userId && x.IsAccepted == true).ToList();
            var mapped = this.mapper.Map<List<RequestDTO>>(requests);
            return mapped;
        }

        public List<RequestDTO> GetRejectedReceivedRequests(int userId)
        {
            var requests = this.requestRepo.GetAll(x => x.ReceiverId == userId && x.IsAccepted == false).ToList();
            var mapped = this.mapper.Map<List<RequestDTO>>(requests);
            return mapped;
        }

        public List<RequestDTO> GetAcceptedSentRequests(int userId)
        {
            var requests = this.requestRepo.GetAll(x => x.SenderId == userId && x.IsAccepted == true).ToList();
            var mapped = this.mapper.Map<List<RequestDTO>>(requests);
            return mapped;
        }

        public List<RequestDTO> GetRejectedSentRequests(int userId)
        {
            var requests = this.requestRepo.GetAll(x => x.SenderId == userId && x.IsAccepted == false).ToList();
            var mapped = this.mapper.Map<List<RequestDTO>>(requests);
            return mapped;
        }
    }
}
