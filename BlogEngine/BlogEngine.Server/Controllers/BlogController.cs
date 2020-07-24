using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Shared.Models;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IReadingTimeEstimator _readingTimeEstimator;

        public BlogController(IBlogRepository blogRepository, IMapper mapper, IReadingTimeEstimator readingTimeEstimator)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _readingTimeEstimator = readingTimeEstimator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogModel>>> Get()
        {
            var blogEntities = await _blogRepository.GetAllWithAllReferenceEntityes();
            return blogEntities.Select(b => ToModel(b)).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogModel>> Get(int id)
        {
            var blogEntity = await _blogRepository.GetByIdAsync(id);

            if (blogEntity == null) return NotFound();

            return ToModel(blogEntity);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(BlogModel blog)
        {
            blog.EstimatedReadingTimeInMinutes = _readingTimeEstimator.GetEstimatedReadingTime(blog.HTMLContent);
            blog.DateCreated = DateTime.Now;

            var insertedBlog = await _blogRepository.InsertAsync(ToEntity(blog));
            return insertedBlog.ID;
        }

        [HttpPut]
        public async Task<ActionResult<BlogModel>> Put(BlogModel blogModel)
        {
            blogModel.EstimatedReadingTimeInMinutes = _readingTimeEstimator.GetEstimatedReadingTime(blogModel.HTMLContent);
            blogModel.LastUpdateDate = DateTime.Now;

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