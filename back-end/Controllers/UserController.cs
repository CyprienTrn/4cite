using back_end.Interfaces;
using Microsoft.AspNetCore.Mvc;
using back_end.Models;

namespace back_end.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                if (users == null || users.Count == 0)
                {
                    return NotFound("Aucun utilisateur trouvé.");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }

        public IActionResult GetUserById(Guid id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound($"Utilisateur avec l'ID {id} introuvable.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }

        public IActionResult CreateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("L'utilisateur ne peut pas être null.");
                }

                var createdUser = _userService.CreateUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }

    }
}