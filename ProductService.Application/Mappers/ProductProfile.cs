using AutoMapper;
using ProductService.Application.DTOs.Requests;
using ProductService.Application.DTOs.Response;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateRequest, ProductEntity>();
            CreateMap<ProductEntity, ProductResponse>();
        }
    }
}
