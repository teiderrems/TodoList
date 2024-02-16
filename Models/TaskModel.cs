using Microsoft.AspNetCore.Identity;

namespace TodoList.Models
{
    public class TaskModel:CommunnProperty
    {
        public string Title { get; set; }=String.Empty;
        public string Description { get; set; } = String.Empty;

        public bool Completed { get; set; }=false;

        public required IdentityUser Owner { get; set; }
    }
}
