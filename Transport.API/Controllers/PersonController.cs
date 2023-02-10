namespace Transport.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Transport.API.Context;
    using Transport.API.Models;
    using Transport.API.Models.ViewModels;

    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly TransportContext db;

        public PersonController(ILogger<PersonController> logger, TransportContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet("[action]")]
        public async Task<CustomResponseModel<IEnumerable<ViewPersonModel>>> GetAll()
        {
            try
            {
                // get all the people from Database
                var persons = await db.Person.ToListAsync();

                // create empty list of view models
                var viewPersonModels = new List<ViewPersonModel>();

                // map entities to models
                foreach (var person in persons)
                {
                    viewPersonModels.Add(new()
                    {
                        Id = person.Id,
                        FullName = person.FullName,
                        Age = person.Age,
                        IsDriver = person.IsDriver
                    });
                }

                // return response model
                return new CustomResponseModel<IEnumerable<ViewPersonModel>>()
                {
                    StatusCode = 200,
                    Result = viewPersonModels
                };
            }
            catch (Exception ex)
            {
                // log problem
                _logger.LogCritical(ex.Message);

                // return status code 500
                return new CustomResponseModel<IEnumerable<ViewPersonModel>>()
                {
                    StatusCode = 500,
                    ErrorMessage = "Something went wrong, please contact support"
                };
            }
        }
    }
}
