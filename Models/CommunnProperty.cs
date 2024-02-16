namespace TodoList.Models
{
    public class CommunnProperty
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }=DateTime.Now;

        public DateTime? UpdatedAt {  get; set; }=default;
    }
}