using _20263RecetaAPI.DTOs.Categories;
using _20263RecetaAPI.DTOs.Cometarios;
using _20263RecetaAPI.DTOs.Identity;
using _20263RecetaAPI.DTOs.Ingredients;
using _20263RecetaAPI.DTOs.Recipe;

using _20263RecetaAPI.Models;
using _20263RecetaAPI.Models._20263RecetaAPI.Models;
using AutoMapper;

namespace _20263RecetaAPI.utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // --- Identity ---
            CreateMap<UserCredentialsDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Email));

            CreateMap<RegisterUserDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Email));

            // categorias
            CreateMap<Category, CategoryDtos>();
            CreateMap<CategoryCreationDtos, Category>();
            CreateMap<CategoryUpdateDtos, Category>();

            // ingrediente
            CreateMap<Ingrediente, IngredientsDto>();
            CreateMap<IngredientCreationDto, Ingrediente>();

            //  recetas
            CreateMap<Receta, RecipeDto>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Name))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario.UserName))
                .ForMember(dest => dest.Ingredientes, opt => opt.MapFrom(src => src.RecetaIngredientes));

            CreateMap<RecetaIngrediente, RecipeIngredientDtos>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdIngrediente))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Ingrediente.Nombre))
                .ForMember(dest => dest.Medida, opt => opt.MapFrom(src => src.Ingrediente.Medida));

            CreateMap<RecipeCreationDto, Receta>()
                .ForMember(dest => dest.RecetaIngredientes, opt => opt.MapFrom(src => src.Ingredientes));

            CreateMap<RecipeIngredientCreationDtos, RecetaIngrediente>();
            CreateMap<IngredientUpdateDto, Ingrediente>();
            CreateMap<RecipeUpdateDtos, Receta>();

            //comentarios
            CreateMap<Comentario, CommentDtos>()
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario.UserName));
            CreateMap<CommentUpdateDto, Comentario>();
            CreateMap<CommentCreationDtos, Comentario>();
        }
    }
}