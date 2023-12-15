using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;


[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{

    private readonly ILogger<CategoriesController> _logger;
    private readonly CategoriesRepository CategoriesRepository;


    public CategoriesController(CategoriesRepository categoriesRepository, ILogger<CategoriesController> logger)
    {
        CategoriesRepository = categoriesRepository;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Category> FindAllUsers()
    {
        return CategoriesRepository.FindAll();
    }
    
    [HttpPost]
    public void Create(Category category)
    {
        CategoriesRepository.Insert(category);
        CategoriesRepository.Save();
    }
}
    
    
