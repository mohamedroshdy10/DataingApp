using Api.DataContext;
using Api.Entitis;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
   [ApiController]
   [Route("api/[Controller]")]
    public class UsersController:ControllerBase
    {
        //Fildes
        private readonly AppDBContext _dBContext;
    public UsersController(AppDBContext  dBContext)
    {
        _dBContext=dBContext;
    }


    //GetData
  [HttpGet]
    public ActionResult<IEnumerable<User>>GetAllUsers()
    {
        return _dBContext.users.ToList();
    }
    [HttpGet("{id}") ]
    public ActionResult<User>GetUser(int id)
    {
        return _dBContext.users.Find(id);
    }
    
    }
}