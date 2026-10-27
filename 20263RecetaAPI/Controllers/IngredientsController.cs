using _20263RecetaAPI.Context;
using _20263RecetaAPI.DTOs.Ingredients;
using _20263RecetaAPI.DTOs.Recipe;
using _20263RecetaAPI.Models;
using _20263RecetaAPI.Models._20263RecetaAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _20263RecetaAPI.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class IngredientsController:ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public IngredientsController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<IngredientsDto>>> Get()
        {
            var ingredients = await context.Ingredientes.ToListAsync();
            return mapper.Map<List<IngredientsDto>>(ingredients);

        }
        [HttpGet("id:int")]
        public async Task<ActionResult<IngredientsDto>> Get(int id)
        {
            var ingredients = await context.Ingredientes.FirstOrDefaultAsync(x => x.Id == id);

            if(ingredients is null)
            {
                NotFound();
            }
            return mapper.Map<IngredientsDto>(ingredients);
        }
        [HttpPost]
        public async Task<ActionResult<IngredientsDto>> Crear(IngredientCreationDto ingredientCreationDto)
        {
            var ingredient = mapper.Map<Ingrediente>(ingredientCreationDto);
            context.Ingredientes.Add(ingredient);
            await context.SaveChangesAsync();
            var response = mapper.Map<IngredientsDto>(ingredient);
            return CreatedAtAction(nameof(Get), new { id = ingredient.Id }, response);


        }
        [HttpDelete("id:int")]
        public async Task<ActionResult> Delete(int id)
        {
            var ingredient = await context.Ingredientes.FindAsync(id);

            if(ingredient== null)
            {
                NotFound();
            }
            context.Ingredientes.Remove(ingredient);
            await context.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut("id:int")]
        public async Task<ActionResult>Update(int id, IngredientUpdateDto ingredientUpdateDto)
        {
            var ingredient = await context.Ingredientes.FindAsync(id);
            if(ingredient is null)
            {
                NotFound();
            }
             mapper.Map(ingredientUpdateDto, ingredient);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
