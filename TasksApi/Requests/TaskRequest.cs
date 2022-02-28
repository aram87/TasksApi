namespace TasksApi.Requests
{
    public class TaskRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime Ts { get; set; }
    }
}
