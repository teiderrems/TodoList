using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Pages.Todo
{

    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly TodoList.Data.ApplicationDbContext _context;
        public CreateModel(TodoList.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TaskModel TaskModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (TaskModel.Title==null || TaskModel.Description==null)
            {
                return Page();
            }

            var user = await _context.Users.FirstAsync(u => u.UserName == User.Identity!.Name);
            TaskModel.Owner = user;
            _context.TodoList.Add(TaskModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
