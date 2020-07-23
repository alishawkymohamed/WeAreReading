using System.Collections.Generic;
using Models.DbModels;
using Models.DTOs;

namespace Services.Contracts
{
    public interface IBookService : IService<Book>
    {
        List<BookDTO> GetAll(string search = null);
        BookDTO GetDetails(int bookId);
        List<BookDTO> GetAllForUser(int userId, int? count, string search = null);
        List<BookDTO> GetLastAddedBooks(int count);
        List<BookDTO> GetRecommendedBooks(int count);
        List<BookDTO> GetAllForOthers(int userId, string search = null);
        BookDTO Insert(InsertBookDTO book);
        BookDTO Update(int bookId, InsertBookDTO book);
        void Delete(IEnumerable<int> bookIds);
    }
}
