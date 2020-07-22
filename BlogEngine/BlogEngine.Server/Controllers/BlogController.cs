using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogController(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogModel>> Get(int id)
        {
            var blogEntity = await _blogRepository.GetByIdAsync(id);

            if (blogEntity == null) return NotFound();

            return ToModel(blogEntity);
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogModel>>> Get()
        {
            var blogEntities = await _blogRepository.GetAllWithAllReferenceEntityes();
            return blogEntities.Select(b => ToModel(b)).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(BlogModel blog)
        {
            var insertedBlog = await _blogRepository.InsertAsync(ToEntity(blog));
            return insertedBlog.ID;
        }

        [HttpPut]
        public async Task<ActionResult<BlogModel>> Put(BlogModel blogModel)
        {
            var blogEntity = await _blogRepository.UpdateAsync(ToEntity(blogModel));
            return ToModel(blogEntity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _blogRepository.DeleteAsync(id);
        }

        private Blog ToEntity(BlogModel blogModel) => _mapper.Map<Blog>(blogModel);
        private BlogModel ToModel(Blog blogEntity) => _mapper.Map<BlogModel>(blogEntity);
    }
}