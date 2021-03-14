using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CmdApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CmdApi.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase {

        private readonly CommandContext _context;

        public CommandsController(CommandContext context) => _context = context;

        //GET: /api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands() {
            return _context.CommandItems;
        }

        //GET: /api/commands/{id}
        [HttpGet("{id}")]
         public ActionResult<Command> GetCommandById(int id) {
            var commandItem = _context.CommandItems.Find(id);
            if(commandItem == null){
                return NotFound();
            }else{
                return commandItem;
            }
        }

        //POST:/api/commands
        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command){
            _context.CommandItems.Add(command);
            _context.SaveChanges();
            
            return CreatedAtAction("GetCommandById", new Command{Id = command.Id}, command);
        }

        //PUT: /api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult<Command> PutCommandItem(int id, Command command){
            if(id != command.Id){
                return BadRequest();
            }

            _context.Entry(command).State = EntityState.Modified;
            _context.SaveChanges();

            return CreatedAtAction("GetCommandById", new Command{Id = command.Id}, command);
        }

        //DELETE:/api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult<Command> DeleteCommandItem(int id){
            var commandItem = _context.CommandItems.Find(id);
            if(commandItem != null){
                _context.CommandItems.Remove(commandItem);
                _context.SaveChanges();
                return commandItem;
            }else{
                return NotFound();
            }
        }
    }
}