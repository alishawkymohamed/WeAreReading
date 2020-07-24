using Models.DbModels;
using Models.DTOs;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IRequestService : IService<Request>
    {
        RequestDTO GetRequest(int bookId, int senderId, int receiverId);
        List<RequestDTO> GetReceivedNotRespondedRequests(int userId);
        List<RequestDTO> GetSentNotRespondedRequests(int userId);
        List<RequestDTO> GetAcceptedReceivedRequests(int userId);
        List<RequestDTO> GetRejectedReceivedRequests(int userId);
        List<RequestDTO> GetAcceptedSentRequests(int userId);
        List<RequestDTO> GetRejectedSentRequests(int userId);
        RequestDTO SendRequest(CreateRequestDTO request);
        void AcceptRequest(int requestId);
        void RejectRequest(int requestId);
        void DeleteRequest(int requestId);
    }
}
