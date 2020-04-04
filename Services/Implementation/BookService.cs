using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Models.DbModels;
using Models.DTOs;
using Repos.Contracts;
using Services.Contracts;

namespace Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepo bookRepo;
        private readonly IMapper mapper;

        public BookService(IBookRepo bookRepo, IMapper mapper)
        {
            this.bookRepo = bookRepo;
            this.mapper = mapper;
        }

        public List<BookDTO> GetAllForOthers(int userId)
        {
            return this.bookRepo.GetAll(x => x.OwnerId != userId).Select(a => mapper.Map<BookDTO>(a)).ToList();
        }

        public List<BookDTO> GetAllForUser(int userId)
        {
            return this.bookRepo.GetAll(x => x.OwnerId == userId).Select(a => mapper.Map<BookDTO>(a)).ToList();
        }

        public BookDTO Insert(InsertBookDTO bookDTO)
        {
            Book book = mapper.Map<Book>(bookDTO);
            Book dbBook = bookRepo.Insert(book);
            return mapper.Map<BookDTO>(dbBook);
        }

        public BookDTO Update(int bookId, InsertBookDTO book)
        {
            var bookToUpdate = this.mapper.Map<Book>(book);
            bookToUpdate.Id = bookId;
            var bookToReturn = this.bookRepo.Update(bookToUpdate);
            return this.mapper.Map<BookDTO>(bookToReturn);
        }

        public void Delete(IEnumerable<int> bookIds)
        {
            var booksToDelete = this.bookRepo.GetAll(x => bookIds.Contains(x.Id));
            this.bookRepo.Delete(booksToDelete);
        }
    }
}
