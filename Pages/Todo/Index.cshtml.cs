using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Pages.Todo
{
    
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TodoList.Data.ApplicationDbContext _context;

        public IndexModel(TodoList.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TaskModel> TaskModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TaskModel = await _context.TodoList.Where(t => t.Owner.Email == User.Identity!.Name).ToListAsync();// _context.TodoList.Where(t=>t.Owner.Id.Equals(GetIdentityUser(User.Identity!.Name!)!.Id)).ToListAsync();
        }
    }
}
