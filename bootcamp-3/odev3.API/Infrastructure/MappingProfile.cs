using AutoMapper;
using odev3.DB.Models;
using odev3.Models.Product;
using odev3.Models.User;

namespace odev3.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //-------USER-----------
            //CREATE
            CreateMap<User, CreateUserModel>();
            CreateMap<CreateUserModel, User>();
            //GET
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();
            //LOGIN
            CreateMap<User, LoginUserModel>();
            CreateMap<LoginUserModel, User>();

            //-------PRODUCT-----------
            //CREATE
            CreateMap<Product, CreateProductModel>();
            CreateMap<CreateProductModel, Product>();
            //GET
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            //UPDATE
            CreateMap<Product, UpdateProductModel>();
            CreateMap<UpdateProductModel, Product>();



        }
    }
}