using _20263RecetaAPI.DTOs.Identity;
using _20263RecetaAPI.Models;
using AutoMapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace _20263RecetaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
        //inyeccion de dependencias un objeto de la clase para poder llamar sus metodos
    {
        private readonly UserManager<ApplicationUser> userManager; //crea y busca usuraios
        private readonly IConfiguration configuration;//lee valores de configuracion como la llave jwt
        private readonly SignInManager<ApplicationUser> signInManager;//coprueba el inicio de esion

        private readonly IMapper mapper;//convierte los dtos a moselos



        public AccountsController(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }
        //metodo de registrar 
        [HttpPost("register")]//responde a una peticion http post
        public async Task<ActionResult<AuthenticationResponseDto>> Register  //el action result es para devolver el error y un codigo de error
            (UserCredentialsDto userCredentialsDto)//recibe los datos enviados por el cliente
        {
            var usuario = mapper.Map<ApplicationUser>(userCredentialsDto);//convierte el dto recibido en un objeto AplicationUser
            var resultado = await userManager.CreateAsync(usuario, userCredentialsDto.Password);//le pide a usermanager que cree el usuraio await espera que termine la operacion, 
            //createAsync crea la cuenta mediante identity
            if (resultado.Succeeded)
            {
                return await BuildToken(userCredentialsDto.Email);//llama al metodo buildtoken pasandole el correo y devuelve el token jwt
            }
            return BadRequest(resultado.Errors);//no jalo badrequest 400
        }
        //los claims son los datos relaionados con la identifiacion del usuario
        private async Task<AuthenticationResponseDto> BuildToken
            (string email)
        { 
            var claims = new List<Claim>
            {
                new Claim ("email", email),
                new Claim (ClaimTypes.Name, email)
            };
            var usuario = await userManager.FindByEmailAsync(email); //vrificar si el usario existe mediante el correo
            var claimsRoles = await userManager.GetClaimsAsync(usuario);//obtenemos los claims
            var usuarioId = usuario!.Id;//obtiene el identificador del usuario
            var roles = await userManager.GetRolesAsync(usuario);//obtiene los roles del usuario
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }//recorre cada rol y lo añade a la lista de claims que ira dentro del token
            claims.AddRange(claimsRoles);//añade la lista de los demas claims

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["LlaveJWT"]!));//lee la llave secreta 
            var creds = new SigningCredentials(
    llave, SecurityAlgorithms.HmacSha256);//creamos la firma usando esa lave y h
            var expiracion = DateTime.UtcNow.AddDays(30);//expiracon del token de 30 dias

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);//construimos nuestros token usando los claims la expiracion y creds
            return new AuthenticationResponseDto//crea y devuelve la respuesat para el cliente
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiracion,
                Username = email,
                UserId = usuarioId
            };

        }
        [HttpGet("renew")] // endpoint para renovar token 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//debe teber un jwt valido
        public async Task<ActionResult<AuthenticationResponseDto>> Renew()//actio
        {
            var emailClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email"); //busca entro los usuarios identificados el email
            return await BuildToken(emailClaim!.Value);//genera otro token


        }
        [HttpPost("login")] //meto
        public async Task<ActionResult<AuthenticationResponseDto>> Login(UserCredentialsDto userCredentialsDto)
        {
            var resultado = await signInManager.PasswordSignInAsync(userCredentialsDto.Email, //le pide a identiy que compruebe las cedenciales
                userCredentialsDto.Password, isPersistent: false, lockoutOnFailure: false);
            if(resultado.Succeeded)
            {
                return await BuildToken(userCredentialsDto.Email);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }
        

        
    }
}
