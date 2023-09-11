using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TascApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TascApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public TasksController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/<TasksController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> Get(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST api/<TasksController>
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Models.Task>> UpdateTask(Guid id, Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(task);
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null) 
            { 
                return NotFound(); 
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }


        // Check if Task exists
        private bool TaskExists(Guid id)
        {
            return _context.Tasks.Any(task => task.Id == id);
        }
    }
}
