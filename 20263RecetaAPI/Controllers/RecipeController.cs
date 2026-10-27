using _20263RecetaAPI.Context;
using _20263RecetaAPI.DTOs.Recipe;
using _20263RecetaAPI.Models;
using _20263RecetaAPI.Models._20263RecetaAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace _20263RecetaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController:ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public RecipeController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper= mapper;
            this.context = context;

        }
        [HttpGet]
        public async Task<ActionResult<List<RecipeDto>>> Get()
        {
            var recipe = await context.Recetas.ToListAsync();
            return mapper.Map<List<RecipeDto>>(recipe);


        }
        [HttpGet("id:int")]
        public async Task<ActionResult<RecipeDto>>Get(int id)
        {
            var recipe = await context.Recetas.FirstOrDefaultAsync(x => x.Id==id);
            if(recipe is null)
            {
                NotFound();
            }
            return mapper.Map<RecipeDto>(recipe);

        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RecipeCreationDto>> Crear(RecipeCreationDto recipeCreationDto)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId)) return Unauthorized();
            var recipe = mapper.Map<Receta>(recipeCreationDto);
            recipe.UsuarioId = usuarioId;

            context.Recetas.Add(recipe);
            await context.SaveChangesAsync();
            var response = mapper.Map<RecipeDto>(recipe);
            return CreatedAtAction(nameof(Get), new { id = recipe.Id }, response);


        }
        [HttpDelete("id:int")]
        public async Task<ActionResult> Delete(int id)
        {
            var recipe = await context.Recetas.FindAsync(id);
            if(recipe == null)
            {
                NotFound();
            }
            context.Recetas.Remove(recipe);
            await context.SaveChangesAsync();
            return NoContent();
        }
        

    }
}
