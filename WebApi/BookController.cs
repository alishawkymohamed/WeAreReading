﻿using Helpers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.DbModels;
using Models.DTOs;
using Models.HelperModels;
using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.ViewModels;

namespace WebApi
{
    public class BookController : BaseController<Book>
    {
        private readonly IFileService fileService;
        private readonly ISessionService sessionService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IOptions<AppSettings> appSettings;
        private readonly IBookService bookService;

        public BookController(
                        IFileService fileService,
                        ISessionService sessionService,
                        IWebHostEnvironment webHostEnvironment,
                        IOptions<AppSettings> appSettings,
                        IBookService bookService) : base(bookService)
        {
            this.fileService = fileService;
            this.sessionService = sessionService;
            this.webHostEnvironment = webHostEnvironment;
            this.appSettings = appSettings;
            this.bookService = bookService;
        }


        [HttpGet("GetAllForUser")]
        [ProducesResponseType(200, Type = typeof(List<BookDTO>))]
        public IActionResult GetAllForUser(int? userId, int? count, string search = null, List<int> categoriesIds = null)
        {
            userId = userId.HasValue ? userId : sessionService.UserId;
            if (userId.HasValue)
            {
                return Ok(bookService.GetAllForUser(userId.Value, count, search, categoriesIds));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("GetAllForOthers")]
        [ProducesResponseType(200, Type = typeof(List<BookDTO>))]
        public IActionResult GetAllForOthers(string search = null, List<int> categoriesIds = null)
        {
            var userId = sessionService.UserId;
            if (userId.HasValue)
            {
                return Ok(bookService.GetAllForOthers(userId.Value, search, categoriesIds));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(200, Type = typeof(List<BookDTO>))]
        [AllowAnonymous]
        public IActionResult GetAll(string search = null, List<int> categoriesIds = null)
        {
            return Ok(bookService.GetAll(search, categoriesIds));
        }

        [HttpGet("GetDetails")]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [AllowAnonymous]
        public IActionResult GetDetails(int bookId)
        {
            return Ok(bookService.GetDetails(bookId));
        }

        [HttpPost("Insert")]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [Authorize]
        public IActionResult Insert([FromBody] InsertBookDTO book)
        {
            if (ModelState.IsValid)
            {
                return Ok(bookService.Insert(book));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [Authorize]
        public IActionResult Update([FromQuery] int bookId, [FromBody] InsertBookDTO book)
        {
            if (ModelState.IsValid)
            {
                return Ok(bookService.Update(bookId, book));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(200, Type = typeof(void))]
        [Authorize]
        public IActionResult Delete([FromQuery] int bookId)
        {
            try
            {
                this.bookService.Delete(new List<int> { bookId });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ExceptionModel
                {
                    Message = "This book can't be deleted !!",
                    Exception = ex
                });
            }

            return Ok(bookId);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(UploadImageResponseDTO))]
        [Authorize]
        public async Task<IActionResult> UploadBookCoverImage(UploadImageViewModel uploadImageViewModel)
        {
            if (ModelState.IsValid)
            {
                string path = webHostEnvironment.WebRootPath + appSettings.Value.FileSettings.BookCovers;
                string imageId = await fileService.WriteImage(uploadImageViewModel.Photo, path);
                return Ok(new UploadImageResponseDTO
                {
                    ImageId = imageId
                });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetLastAddedBooks")]
        [ProducesResponseType(200, Type = typeof(List<BookDTO>))]
        public async Task<IActionResult> GetLastAddedBooks(int count)
        {
            return Ok(this.bookService.GetLastAddedBooks(count));
        }

        [HttpGet("GetRecommendedBooks")]
        [ProducesResponseType(200, Type = typeof(List<BookDTO>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetRecommendedBooks(int count)
        {
            return Ok(this.bookService.GetRecommendedBooks(count));
        }
    }
}
