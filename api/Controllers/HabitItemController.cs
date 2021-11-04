using System.Collections;
using System.Collections.Generic;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HabitItemController : ControllerBase
    {
        private readonly HabitItemService _habitItemService;

        public HabitItemController(HabitItemService habitItemService)
        {
            _habitItemService = habitItemService;
        }
        
        [Route("GetAll")]
        [HttpGet]
        public IEnumerable<HabitItem> GetAll()
        {
            return _habitItemService.GetAll();
        }
        
        [HttpGet]
        public HabitItem Get(string id)
        {
            return _habitItemService.Get(id);
        }
        
        [HttpPost]
        public void Insert(HabitItem habitItem)
        {
            _habitItemService.Insert(habitItem);
        }
        
        [HttpPut]
        public void Update(string id, HabitItem habitItem)
        {
            _habitItemService.Update(id, habitItem);
        }
        
        [HttpDelete]
        public void Delete(string id)
        {
            _habitItemService.Delete(id);
        }
    }
}