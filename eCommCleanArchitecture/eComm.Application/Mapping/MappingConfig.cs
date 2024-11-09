﻿using AutoMapper;
using eComm.Application.DTOs.Category;
using eComm.Application.DTOs.Product;
using eComm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComm.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            CreateMap<CreateProduct, Product>();
            CreateMap<CreateCategory, Category>();

            CreateMap<Category, GetCategory>();
            CreateMap<Product, GetProduct>();
        }
    }
}