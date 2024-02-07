using Cube_C___API.Dtos.Role;
using Cube_C___API.Models;

namespace Cube_C___API.Dtos.User;

public class GetUserDto
{
        public int Id { get; set; }
        public string Mail { get; set; }
        
        public List<GetRoleDto> Roles { get; set; }
}