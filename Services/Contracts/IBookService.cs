﻿using System.Collections.Generic;
using Models.DbModels;
using Models.DTOs;

namespace Services.Contracts
{
    public interface IBookService : IService<Book>
    {
        List<BookDTO> GetAll();
        BookDTO GetDetails(int bookId);
        List<BookDTO> GetAllForUser(int userId);
        List<BookDTO> GetAllForOthers(int userId);
        BookDTO Insert(InsertBookDTO book);
        BookDTO Update(int bookId, InsertBookDTO book);
        void Delete(IEnumerable<int> bookIds);
    }
}
