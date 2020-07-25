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
        private readonly IBookRepo bookRepo;

        public RequestService(IRequestRepo requestRepo, IMapper mapper, IBookRepo bookRepo)
        {
            this.requestRepo = requestRepo;
            this.mapper = mapper;
            this.bookRepo = bookRepo;
        }

        public void AcceptRequest(int requestId)
        {
            var req = this.requestRepo.Get(x => x.Id == requestId);
            req.IsAccepted = true;
            this.requestRepo.Update(req);

            var book = this.bookRepo.Get(x => x.Id == req.BookId);
            if (book.CopiesCount > 1)
            {
                book.CopiesCount--;
                var mapped = this.mapper.Map<Book>(book);
                this.bookRepo.Update(book);

                this.bookRepo.Insert(new Book
                {
                    CopiesCount = 1,
                    Author = book.Author,
                    CategoryId = book.CategoryId,
                    CoverPhotoId = book.CoverPhotoId,
                    OwnerId = req.SenderId,
                    Rating = book.Rating,
                    StatusId = book.StatusId,
                    Title = book.Title,
                    Description = book.Description
                });
            }
            else
            {
                book.OwnerId = req.SenderId;
                this.bookRepo.Update(book);
            }
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

        public void DeleteRequest(int requestId)
        {
            var request = this.requestRepo.Get(x => x.Id == requestId);
            this.requestRepo.Delete(new List<Request> { this.mapper.Map<Request>(request) });
        }
    }
}
