﻿using Application.DTOs;
using Transversal.Common;

namespace Application.Interface
{
    public interface IProductService
    {
        Task<Response<IEnumerable<ProductDto>>> GetAllProducts();
        Task<Response<ProductDto>> GetProductById(int id);
        Task<Response<ProductDto>> CreateProduct(ProductDto dto);
        Task<Response<ProductDto>> UpdateProduct(int id, ProductDto dto);
        Task<Response<bool>> DeleteProduct(int id);
    }
}
