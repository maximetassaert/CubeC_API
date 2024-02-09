using Cube_C___API.Dtos.Role;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = Role.ADMIN)]
public class RoleController : ControllerBase
{
    private readonly IRepositoryData<Role> _repositoryData;

    public RoleController(IRepositoryData<Role> repositoryData)
    {
        _repositoryData = repositoryData;
    }

    [HttpPost]
    public IActionResult PostRole(RoleDto roleDto)
    {
        Role role = new();
        role.Name = roleDto.Name;
        if (_repositoryData.Create(role)) return Ok(role);
        return BadRequest();
    }

    [HttpGet]
    // [Route("FindAll")]
    public IActionResult GetRoles()
    {
        return Ok(_repositoryData.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var role = _repositoryData.GetById(id);
        if (role != null) return Ok(role);

        return NotFound(
            new
            {
                Message = "Role non trouvé"
            });
    }


    [HttpPut("{id}")]
    public IActionResult UpdateRole(RoleUpdateDto roleUpdateDto, int id)
    {
        var found = _repositoryData.GetById(id);
        if (found == null) return NotFound("Role introuvable");
        foreach (var prop in typeof(RoleUpdateDto).GetProperties())
            if (prop.GetValue(roleUpdateDto) != null)
                found.GetType().GetProperty(prop.Name).SetValue(found, prop.GetValue(roleUpdateDto));

        _repositoryData.Update(found);
        return Ok(new
        {
            Message = "Role mis a jour",
            Role = found
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRole(int id)
    {
        var found = _repositoryData.GetById(id);
        if (found == null) return NotFound("Role introuvable");
        _repositoryData.Delete(found);
        return Ok("Role supprimé");
    }
}