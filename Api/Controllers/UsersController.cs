using Api.DataContext;
using Api.Entitis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
   
    public class UsersController:Controller
    {
        //Fildes
        private readonly AppDBContext _dBContext;
    public UsersController(AppDBContext  dBContext)
    {
        _dBContext=dBContext;
    }

    //GetData
    [AllowAnonymous]
  [HttpGet]
    public ActionResult<IEnumerable<User>>GetAllUsers()
    {
        return _dBContext.users.ToList();
    }
    [Authorize]
    [HttpGet("{id}") ] 
    public ActionResult<User>GetUser(int id)
    {
        return _dBContext.users.Find(id);
    }
    
    }
}