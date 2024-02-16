using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Pages.Todo
{
    public class DeleteModel : PageModel
    {
        private readonly TodoList.Data.ApplicationDbContext _context;

        public DeleteModel(TodoList.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskModel TaskModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskmodel = await _context.TodoList.FirstOrDefaultAsync(m => m.Id == id);

            if (taskmodel == null)
            {
                return NotFound();
            }
            else
            {
                TaskModel = taskmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskmodel = await _context.TodoList.FindAsync(id);
            if (taskmodel != null)
            {
                TaskModel = taskmodel;
                _context.TodoList.Remove(TaskModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
