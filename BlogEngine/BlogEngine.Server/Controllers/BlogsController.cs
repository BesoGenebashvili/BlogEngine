using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Shared.DTOs;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IReadingTimeEstimator _readingTimeEstimator;

        public BlogsController(IBlogRepository blogRepository, IMapper mapper, IReadingTimeEstimator readingTimeEstimator)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _readingTimeEstimator = readingTimeEstimator;
        }

        //GET api/blogs
        [HttpGet]
        public async Task<ActionResult<List<BlogDTO>>> Get()
        {
            var blogEntities = await _blogRepository.GetAllWithAllReferenceEntityes();

            return _mapper.Map<List<BlogDTO>>(blogEntities.ToList());
        }

        //GET api/blogs/{id}
        [HttpGet("{id:int}", Name = "Get")]
        public async Task<ActionResult<BlogDTO>> Get(int id)
        {
            var blogEntity = await _blogRepository.GetByIdAsync(id);

            if (blogEntity == null) return NotFound();

            return ToDTO(blogEntity);
        }

        //POST api/blogs
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BlogCreationDTO blogCreationDTO)
        {
            var blog = ToEntity(blogCreationDTO);

            blog.EstimatedReadingTimeInMinutes = _readingTimeEstimator.GetEstimatedReadingTime(blog.HTMLContent);
            blog.DateCreated = DateTime.Now;

            var insertedBlog = await _blogRepository.InsertAsync(blog);

            return new CreatedAtRouteResult(nameof(Get), new { insertedBlog.ID }, ToDTO(insertedBlog));
        }

        //GET api/blogs/update/{id}
        [HttpGet("update/{id:int}")]
        public async Task<ActionResult<BlogUpdateDTO>> PutGet(int id)
        {
            var blogEntity = await _blogRepository.GetByIdAsync(id);

            if (blogEntity == null) return NotFound();

            return ToUpdateDTO(blogEntity);
        }

        //PUT api/blogs/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BlogDTO>> Put(int id, [FromBody] BlogUpdateDTO blogUpdateDTO)
        {
            var blogEntity = await _blogRepository.GetByIdAsync(id);

            if (blogEntity == null) return NotFound();

            _mapper.Map(blogUpdateDTO, blogEntity);

            blogEntity.EstimatedReadingTimeInMinutes = _readingTimeEstimator.GetEstimatedReadingTime(blogUpdateDTO.HTMLContent);
            blogEntity.LastUpdateDate = DateTime.Now;

            var updatedEntity = await _blogRepository.UpdateAsync(blogEntity);

            return ToDTO(updatedEntity);
        }

        //DELETE api/blogs/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var blogEntity = await _blogRepository.GetByIdAsync(id);

            if (blogEntity == null) return NotFound();

            return await _blogRepository.DeleteAsync(blogEntity.ID);
        }

        private Blog ToEntity(BlogCreationDTO blogCreationDTO) => _mapper.Map<Blog>(blogCreationDTO);
        private BlogDTO ToDTO(Blog blogEntity) => _mapper.Map<BlogDTO>(blogEntity);
        private BlogUpdateDTO ToUpdateDTO(Blog blogEntity) => _mapper.Map<BlogUpdateDTO>(blogEntity);
    }
}