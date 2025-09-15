using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class Produto
{
    [Required]
    [MinLength(3)]
    public string Descricao { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
    public decimal Preco { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Estoque não pode ser negativo")]
    public int Estoque { get; set; }

    [Required]
    [CustomValidation(typeof(Produto), nameof(ValidarCodigoProduto))]
    public string CodigoProduto { get; set; }

    // Validação customizada para Código do Produto
    public static ValidationResult ValidarCodigoProduto(string codigo, ValidationContext context)
    {
        if (string.IsNullOrEmpty(codigo))
            return new ValidationResult("Código do Produto é obrigatório");

        if (!System.Text.RegularExpressions.Regex.IsMatch(codigo, @"^[A-Z]{3}-\d{4}$"))
            return new ValidationResult("O código deve seguir o formato AAA-1234.");

        return ValidationResult.Success;
    }
}

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    [HttpPost]
    public IActionResult CriarProduto([FromBody] Produto produto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(produto);
    }
}
