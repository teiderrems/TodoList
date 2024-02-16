using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Pages.Todo
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly TodoList.Data.ApplicationDbContext _context;

        public EditModel(TodoList.Data.ApplicationDbContext context)
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

            var taskmodel =  await _context.TodoList.FirstOrDefaultAsync(m => m.Id == id);
            if (taskmodel == null)
            {
                return NotFound();
            }
            TaskModel = taskmodel;
            ViewData["Title"]=taskmodel.Title;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (TaskModel.Description==null || TaskModel.Title==null)
            {
                return Page();
            }
            TaskModel.UpdatedAt=DateTime.Now;
            _context.Attach(TaskModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskModelExists(TaskModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TaskModelExists(int id)
        {
            return _context.TodoList.Any(e => e.Id == id);
        }

        public async Task<Microsoft.AspNetCore.Identity.IdentityUser>? GetIdentityUser(string email)
        {
            return await _context.Users.FindAsync(email)!;
        }
    }
}
