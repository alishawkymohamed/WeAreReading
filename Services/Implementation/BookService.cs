using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Helpers.Contracts;
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
        private readonly ISessionService sessionService;

        public BookService(IBookRepo bookRepo, IMapper mapper, ISessionService sessionService)
        {
            this.bookRepo = bookRepo;
            this.mapper = mapper;
            this.sessionService = sessionService;
        }

        public List<BookDTO> GetAll(string search = null, List<int> categoriesIds = null)
        {
            return this.bookRepo.GetAll(search, categoriesIds).Select(a => mapper.Map<BookDTO>(a)).ToList();
        }

        public BookDTO GetDetails(int bookId)
        {
            return mapper.Map<BookDTO>(this.bookRepo.Get(x => x.Id == bookId));
        }

        public List<BookDTO> GetAllForOthers(int userId, string search = null, List<int> categoriesIds = null)
        {
            return this.bookRepo.GetAll(search, categoriesIds, x => x.OwnerId != userId).Select(a => mapper.Map<BookDTO>(a)).ToList();
        }

        public List<BookDTO> GetAllForUser(int userId, int? count, string search = null, List<int> categoriesIds = null)
        {
            if (count.HasValue)
            {
                return this.bookRepo.GetAll(search, categoriesIds, x => x.OwnerId == userId)
                    .OrderByDescending(x => x.Rating)
                    .Take(count.Value)
                    .Select(a => mapper.Map<BookDTO>(a)).ToList();
            }
            else
            {
                return this.bookRepo.GetAll(search, categoriesIds, x => x.OwnerId == userId).Select(a => mapper.Map<BookDTO>(a)).ToList();
            }
        }

        public BookDTO Insert(InsertBookDTO bookDTO)
        {
            Book book = mapper.Map<Book>(bookDTO);
            book.OwnerId = this.sessionService.UserId.Value;
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
            var booksToDelete = this.bookRepo.Get(x => bookIds.Contains(x.Id));
            this.bookRepo.Delete(new List<Book> { booksToDelete });
        }

        public List<BookDTO> GetLastAddedBooks(int count)
        {
            return this.bookRepo.GetLastAddedBooks(count).Select(a => mapper.Map<BookDTO>(a)).ToList();
        }

        public List<BookDTO> GetRecommendedBooks(int count)
        {
            return this.bookRepo.GetRecommendedBooks(count).Select(a => mapper.Map<BookDTO>(a)).ToList();
        }
    }
}
