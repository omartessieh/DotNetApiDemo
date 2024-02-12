using CoreApiResponse;
using DotNetApiDemo.Entities;
using DotNetApiDemo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseController
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }

        //Get All
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var posts = await _postRepository.GetAll();

                if (posts == null)
                {
                    return CustomResult("Posts Not Found", HttpStatusCode.NotFound);
                }

                return CustomResult("Posts Loaded Successfully", posts);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        //Get By ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var post = await _postRepository.GetById(id);

                if (post == null)
                {
                    return CustomResult("Post Not Found", HttpStatusCode.NotFound);
                }

                return CustomResult("Data Found.", post);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        //Get By Title
        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            try
            {
                var post = await _postRepository.GetByTitle(title);

                if (post == null)
                {
                    return CustomResult("Post Not Found", HttpStatusCode.NotFound);
                }

                return CustomResult("Data Found", post);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        //Filtering
        [HttpGet("text/{text}")]
        public async Task<IActionResult> SearchPost(string text)
        {
            try
            {
                var posts = await _postRepository.SearchPost(text);

                if (posts == null)
                {
                    return CustomResult("Post Not Found", HttpStatusCode.NotFound);
                }

                return CustomResult("Searching result.", posts);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        //Pagination
        [HttpGet("page")]
        public async Task<IActionResult> PagePost(int page = 1)
        {
            try
            {
                var post = await _postRepository.PagePost(page, 10);

                if (post == null)
                {
                    return CustomResult("Post Not Found", HttpStatusCode.NotFound);
                }

                return CustomResult("Paging Data For Page No " + page, post.ToList());
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        //Add
        [HttpPost]
        public async Task<IActionResult> Add(Post post)
        {
            try
            {
                await _postRepository.Add(post);
                return CustomResult("Post Has Been Created", post);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        //Update
        [HttpPut]
        public async Task<IActionResult> Edit(Post post)
        {
            try
            {
                await _postRepository.Update(post);
                return CustomResult("Post Updated Done", post);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _postRepository.Delete(id);
                return CustomResult("Post Has Been Deleted.");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}