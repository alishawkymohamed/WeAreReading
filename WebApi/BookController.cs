using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.DbModels;
using Models.DTOs;
using Models.HelperModels;
using Services.Contracts;
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
        [Authorize]
        public IActionResult GetAllForUser()
        {
            int? userId = sessionService.UserId;
            if (userId.HasValue)
            {
                return Ok(bookService.GetAllForUser(userId.Value));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("Insert")]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [Authorize]
        public IActionResult Insert([FromBody]InsertBookDTO book)
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
        public IActionResult Update([FromQuery] int bookId, [FromBody]InsertBookDTO book)
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
        public IActionResult Update([FromQuery] int bookId)
        {
            this.bookService.Delete(new List<int> { bookId });
            return Ok();
        }

        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(string))]
        [AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> UploadBookCoverImage(UploadImageViewModel uploadImageViewModel)
        {
            if (ModelState.IsValid)
            {
                string path = webHostEnvironment.WebRootPath + appSettings.Value.FileSettings.BookCovers;
                string imageId = await fileService.WriteImage(uploadImageViewModel.Photo, path);
                return Ok(imageId);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
