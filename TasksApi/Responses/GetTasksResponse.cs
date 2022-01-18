namespace TasksApi.Responses
{
    public class GetTasksResponse : BaseResponse
    {
        public List<Task> Tasks { get; set; }
    }
}
