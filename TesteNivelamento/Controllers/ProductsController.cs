using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteNivelamento.Models;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private  static List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Sapato",Description = "Estoque", Price = 100.0 },
        new Product { Id = 2, Name = "Tenis", Description = "Estoque", Price = 250.0 },
        new Product { Id = 3, Name = "Relogio",Description = "Estoque", Price = 350.0 },
        new Product { Id = 4, Name = "Camisa", Description = "Estoque", Price = 150.0}
    };


    [Authorize]
    [HttpGet]       
    public IActionResult GetAll()
    {
        return Ok(_products);
    }

    [Authorize]
    [HttpGet("{id}")]       
    public IActionResult GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }
}
