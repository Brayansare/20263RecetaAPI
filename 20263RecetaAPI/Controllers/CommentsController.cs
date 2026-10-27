using _20263RecetaAPI.Context;
using _20263RecetaAPI.DTOs.Cometarios;
using _20263RecetaAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace _20263RecetaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController: ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public CommentsController(IMapper mapper, ApplicationDbContext contex)
        {
            this.mapper = mapper; 
            this.context = contex;


        }
        [HttpGet("receta/{idReceta}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CommentDtos>>> Get(int idReceta)
        {
            var comments = await context.Comentarios.Include(c => c.Usuario)
                .Where(c => c.IdReceta == idReceta)
                .OrderByDescending(c => c.Creacion)
                .ToListAsync();

            return Ok(mapper.Map<List<CommentDtos>>(comments));
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommentDtos >> Crear( CommentCreationDtos dto)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId)) return Unauthorized();

            //validar que la receta exista 
            var receta = await context.Recetas.AnyAsync(x => x.Id == dto.IdReceta);

            if (!receta) return BadRequest("La receta no wxiste");
            var comentario = mapper.Map<Comentario>(dto);
            comentario.UsuarioId = usuarioId;
            comentario.Creacion = DateTime.UtcNow;
            context.Comentarios.AddAsync(comentario);
            await context.SaveChangesAsync();

            //traer el comentario con el usuarion
            var comentarioCreado = await context.Comentarios
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == comentario.Id);

            return Ok(mapper.Map<CommentDtos>(comentarioCreado));



        }
        [HttpDelete("id:int")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var comentario = await context.Comentarios.FindAsync(id);
            if (comentario == null) return NotFound("Comentario no encontrado");

            //solo el dueño puedo borrar el comentario
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (comentario.UsuarioId != usuarioId) return Forbid("No puedes borrar el comentario de otra persona");
            context.Comentarios.Remove(comentario);
            await context.SaveChangesAsync();
            return NoContent();




        }
        public async Task<ActionResult<CommentDtos>> Update(int id, CommentUpdateDto commentUpdateDto)
        {
            var comentario = await context.Comentarios.FindAsync(id);

            if(comentario == null)
            {
                NotFound("Comentario no encontrado");
            }
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (comentario.UsuarioId != usuarioId) return Forbid("No puedes modificar el comentario");
            //mapea los datos del dto al objeto coentario existente
            //solo sobreescribe las propiedades qie coincidan manteniendo intactos l feha y el usuario id
            mapper.Map(commentUpdateDto, comentario);
            await context.SaveChangesAsync();
            return NoContent();
            

        }
    }
}
