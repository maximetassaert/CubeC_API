using Cube_C___API.DTO.Category;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IRepositoryData<Category> _repositoryData;

    public CategoryController(IRepositoryData<Category> repositoryData)
    {
        _repositoryData = repositoryData;
    }

    [HttpPost]
    [Authorize(Roles = Role.ADMIN)]
    public IActionResult PostCategory(CategoryDto categoriesDto)
    {
        Category category = new();
        category.Name = categoriesDto.Name;
        // category.Description = categoriesDto.Description;
        if (_repositoryData.Create(category)) return Ok(category);

        return BadRequest();
    }

    [HttpGet]
    [Authorize(Roles = Role.ADMIN)]
    public IActionResult GetCategories()
    {
        return Ok(_repositoryData.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var category = _repositoryData.GetById(id);
        if (category != null) return Ok(category);
        return NotFound(
            new
            {
                Message = "Contact non trouvé"
            }
        );
    }


    [HttpPut("{id}")]
    [Authorize(Roles = Role.ADMIN)]
    public IActionResult UpdateCategory(CategoryUpdateDto categoryDto, int id)
    {
        var found = _repositoryData.GetById(id);
        if (found == null) return NotFound("Categorie introuvable");
        foreach (var prop in typeof(CategoryUpdateDto).GetProperties())
            if (prop.GetValue(categoryDto) != null)
                found.GetType().GetProperty(prop.Name).SetValue(found, prop.GetValue(categoryDto));

        _repositoryData.Update(found);
        return Ok(new
        {
            Message = "Categorie mis a jour",
            Category = found
        });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Role.ADMIN)]
    public IActionResult DeleteCategory(int id)
    {
        var found = _repositoryData.GetById(id);
        if (found == null) return NotFound("Categorie introuvable");
        _repositoryData.Delete(found);
        return Ok("Catégorie suprimée");
    }
}