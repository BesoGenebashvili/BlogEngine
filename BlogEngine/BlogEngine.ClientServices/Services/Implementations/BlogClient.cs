﻿using BlogEngine.ClientServices.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.ClientServices.Extensions;
using BlogEngine.Shared.DTOs;
using BlogEngine.ClientServices.Helpers;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class BlogClient : IBlogClient
    {
        private readonly IHttpService _httpService;
        private const string Url = "api/blogs";

        public BlogClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<BlogDTO> GetAsync(int id)
        {
            return await _httpService.GetHelperAsync<BlogDTO>($"{Url}/{id}");
        }

        public async Task<List<BlogDTO>> GetAllAsync()
        {
            return await _httpService.GetHelperAsync<List<BlogDTO>>(Url);
        }

        public async Task<PaginatedResponse<List<BlogDTO>>> GetAllPaginatedAsync(PaginationDTO paginationDTO)
        {
            return await _httpService.GetHelperAsync<List<BlogDTO>>(Url, paginationDTO);
        }

        public async Task<BlogEditPageDTO> GetEditPageDTOAsync(int id)
        {
            return await _httpService.GetHelperAsync<BlogEditPageDTO>($"{Url}/edit/{id}");
        }

        public async Task<List<BlogDTO>> SearchAsync(BlogSearchDTO blogSearchDTO)
        {
            string blogSearchDTOQueryString = blogSearchDTO.ToQueryString();

            return await _httpService.GetHelperAsync<List<BlogDTO>>($"{Url}/search?{blogSearchDTOQueryString}");
        }

        public async Task<BlogDTO> CreateAsync(BlogCreationDTO blogCreationDTO)
        {
            return await _httpService.PostHelperAsync<BlogCreationDTO, BlogDTO>(Url, blogCreationDTO);
        }

        public async Task<BlogDTO> UpdateAsync(int id, BlogUpdateDTO blogUpdateDTO)
        {
            return await _httpService.PutHelperAsync<BlogUpdateDTO, BlogDTO>($"{Url}/{id}", blogUpdateDTO);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{Url}/{id}");
        }
    }
}