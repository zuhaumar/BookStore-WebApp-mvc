using Microsoft.AspNetCore.Mvc; 
using Ecommerce.Repositories; 
using Ecommerce.Models; 

public class UserController : Controller
{
    private readonly UserRepository _userRepository;
    private readonly BookRepository _bookRepository;
    public UserController(UserRepository userRepository, BookRepository bookRepository)
    {
        _userRepository = userRepository;
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public ActionResult Signup()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Signup(User user)
    {
        if (ModelState.IsValid)
        {
            // Check if the username is not null or empty
            if (string.IsNullOrEmpty(user.Username))
            {
                ModelState.AddModelError(string.Empty, "Please enter a suitable username!");
                return View(user);
            }

            // Check if the username already exists
            var existingUser = await _userRepository.GetUserByUsername(user.Username);
            
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Select another username. This one already exists!");
                return View(user);
            }

            await _userRepository.CreateUser(user);
        }

        return View(user);
    }

    //Implement the following two functions
    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var books = await _bookRepository.GetAllBooksAsync();
        return View(books);
    }

        [HttpGet]
    public async Task<IActionResult> Login()
    {
        var books = await _bookRepository.GetAllBooksAsync();
        return View(books);
    }
    
}