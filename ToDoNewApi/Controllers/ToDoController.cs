using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoNewApi.Modes;

namespace ToDoNewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _toDoContext;
        public ToDoController(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
            //if(toDoContext.todoItems.Count() == 0)
            //{
            //    _toDoContext.todoItems.AddRange(new TodoItem { Name = "linh" }, new TodoItem { Name = "quang"});
            //    _toDoContext.SaveChanges();
            //}
        }
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _toDoContext.todoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _toDoContext.todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _toDoContext.todoItems.Add(item);
            _toDoContext.SaveChanges();
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }
            var todo = _toDoContext.todoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            _toDoContext.todoItems.Update(todo);
            _toDoContext.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _toDoContext.todoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            _toDoContext.todoItems.Remove(todo);
            _toDoContext.SaveChanges();
            return new NoContentResult();
        }



    }
}
