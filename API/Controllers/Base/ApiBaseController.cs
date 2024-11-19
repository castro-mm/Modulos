using System.Net;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Base.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiBaseController : ControllerBase
{
    /// <summary>
    /// Método que define o validationResult com resultado da requisição realizada à API
    /// </summary>
    /// <param name="validationResult">Representa o objeto <see cref="ValidationResult"/> com as informações do resultado da requisição</param>
    /// <returns>A resposta da ação com o resultado da requisição.</returns>
    public ActionResult GetResult(ValidationResult validationResult, string methodName = "", int id = 0)
    {
        // retorna a mensagem de resposta da requsição para o client.
        return (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), validationResult.StatusCode.ToString()) switch
        {
            HttpStatusCode.OK => Ok(validationResult),
            HttpStatusCode.BadRequest => BadRequest(validationResult),
            HttpStatusCode.Unauthorized => Unauthorized(validationResult),
            HttpStatusCode.NotFound => NotFound(validationResult),
            HttpStatusCode.Created => Created(Url.Action(methodName, new { id }) ?? $"/{id}", validationResult),
            _ => NotFound()
        };
    }    
}
