using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
public class SuppliersController : ControllerBase
{
    
    private readonly ILogger<SuppliersController> _logger;
    private readonly SuppliersRepository SuppliersRepository;
    
    
    public SuppliersController(SuppliersRepository suppliersRepository, ILogger<SuppliersController> logger) {
        SuppliersRepository = suppliersRepository;
        _logger = logger;
    }

    [HttpGet]
    // [Route("FindAll")]
    public IEnumerable<Supplier> FindAllSuppliers()
    {
        return SuppliersRepository.FindAll();
    }

    [HttpPost]
    public void Create(Supplier supplier)
    {
        SuppliersRepository.Insert(supplier);
        SuppliersRepository.Save();
    }

    [HttpGet]
    [Route("{id}")]
    public Supplier FindById(int id)
    {
        return SuppliersRepository.FindById(id);
    }
}

