using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class Aluno
{
    [Required]
    [MinLength(3)]
    public string Nome { get; set; }

    [Required]
    [CustomValidation(typeof(Aluno), nameof(ValidarRa))]
    public string Ra { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public string Cpf { get; set; }

    public bool Ativo { get; set; }

    // Validação customizada para o RA
    public static ValidationResult ValidarRa(string ra, ValidationContext context)
    {
        if (string.IsNullOrEmpty(ra)) 
            return new ValidationResult("RA é obrigatório");

        if (!System.Text.RegularExpressions.Regex.IsMatch(ra, @"^RA[0-9]{6}$"))
            return new ValidationResult("RA deve começar com 'RA' seguido de 6 números.");

        return ValidationResult.Success;
    }
}

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    [HttpPost]
    public IActionResult CriarAluno([FromBody] Aluno aluno)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(aluno);
    }
}
