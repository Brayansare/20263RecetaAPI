using _20263RecetaAPI.Models;
using _20263RecetaAPI.DTOs.Categories;
using _20263RecetaAPI.Context;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _20263RecetaAPI.DTOs;
//cuando trabajo con controladores heredo de controller base

namespace _20263RecetaAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {//inyeccion de dependencias
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        //variables que vamos a utiliar que deoenden de otras clases 
        //constructor
        public CategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        //verbo http get

        [HttpGet]
        //clase especial para retornar ya sea un error o un json si es error mando 400 si es exitoso mando una lista 
        public async Task<ActionResult<List<CategoryDtos>>> Get() // metodo para obtener todas las categorias
        {//hago la consulta 
            var categorias = await context.Categories.ToListAsync(); //tolistasync es coo le seletfrom todo el context es como si hicera una consulta a la base de datos y el ToListAsync es para que se ejecute de manera asincrona
            return mapper.Map<List<CategoryDtos>>(categorias); //regresa dto de categorias, el mapper es para mapear los datos de la base de datos a los dto
        }
        //metodo oara obtener una categoria por eso se pasa el id, como es uno no es ina lista
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDtos>> Get(int id) // metodo para encontara una categoria en especifico por su id, el id es un parametro que se pasa en la url
        {
            var categoria = await context.Categories.FirstOrDefaultAsync(x => x.Id == id); // igual aqui es como u consua en sql 
            if (categoria == null)
            {
                return NotFound();
            }
            return mapper.Map<CategoryDtos>(categoria); //hacemos un mapeo de la categoria a un dto para que no se expongan datos sensibles
        }
        //cada de estos es un endpoint 
        //es para crear una categoria
        [HttpPost]
        public async Task<ActionResult<CategoryDtos>> Create(CategoryCreationDtos categoryCreationDtos)
        {
            var category = mapper.Map<Category>(categoryCreationDtos);
            context.Categories.Add(category);//aqui lo creamos 
            await context.SaveChangesAsync();//lo mandamos a nuestra base de datos 
            var response = mapper.Map<CategoryDtos>(category);//mapeamos alreves
            return CreatedAtAction(nameof(Get), new { id = category.Id }, response); // el CreatedAtAction es para devolver un codigo 201 y la url de la categoria creada, y mandamos a llamar el get de arriba
        }
         //metodo para actualizar una categoria
         //el endpoint es el punto final de nuestra busqueda, patch tambien se usa para acualizar
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CategoryUpdateDtos categoryUpdateDto) {
            {
                var category = await context.Categories.FindAsync(id);
                if (category is null)
                {
                    return NotFound();

                }
                mapper.Map(categoryUpdateDto,category);
                await context.SaveChangesAsync();
                return NoContent();//retornamos un 201


            }


        }
        //metodo de eliminar 
       
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await context.Categories.FindAsync(id);
            // revisamos si existe el id 
            if (category is null)
            {
                return NotFound();
            }
            context.Categories.Remove(category);//aqui lo elimina de mi conexto
            await context.SaveChangesAsync();//aqui ya se elimina de la base de datos
            return NoContent();//retornamos un 201
        }
        [HttpGet("name")]

        public async Task<ActionResult<CategoryDtos>> GetName(String name)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.
            Name == name);

            if (category== null)
            {
                NotFound();
            }
            return mapper.Map<CategoryDtos>(category);
        }


    } 
}
    //controlador applicationdbcontext DTOs controller investigar como hacer las relaciones Efcore y propiedades de navegacion , investigar como relacionar una clase ApplicationUser que hereda de Idetity a otra tabla
//relaciones 1:m m:m falta 1:1