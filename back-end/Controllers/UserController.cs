using Microsoft.AspNetCore.Mvc;
using back_end.Models;
using back_end.Interfaces;

namespace back_end.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("L'utilisateur ne peut pas être null.");
                }

                var createdUser = _userService.CreateUser(user);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("L'utilisateur ne peut pas être null.");
                }

                // 🔹 Vérifie si l'ID de l'utilisateur correspond à celui de l'URL
                if (id != user.Id)
                {
                    return BadRequest("L'ID de l'utilisateur ne correspond pas à celui de l'URL.");
                }

                var updatedUser = _userService.UpdateUser(id, user);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok($"Utilisateur avec l'ID {id} supprimé.");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("introuvable", StringComparison.OrdinalIgnoreCase)) // 🔹 Vérification du message d'exception
                {
                    return NotFound($"Utilisateur avec l'ID {id} introuvable.");
                }

                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }

    }
}