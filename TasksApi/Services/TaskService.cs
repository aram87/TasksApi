using Microsoft.EntityFrameworkCore;
using TasksApi.Interfaces;
using TasksApi.Responses;

namespace TasksApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly TasksDbContext tasksDbContext;

        public TaskService(TasksDbContext tasksDbContext)
        {
            this.tasksDbContext = tasksDbContext;
        }

        public async Task<DeleteTaskResponse> DeleteTask(int taskId, int userId)
        {
            var task = await tasksDbContext.Tasks.FindAsync(taskId);

            if (task == null)
            {
                return new DeleteTaskResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }

            if (task.UserId != userId)
            {
                return new DeleteTaskResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }

            tasksDbContext.Tasks.Remove(task);

            var saveResponse = await tasksDbContext.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new DeleteTaskResponse
                {
                    Success = true,
                    TaskId = task.Id
                };
            }

            return new DeleteTaskResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetTasksResponse> GetTasks(int userId)
        {
            var tasks = await tasksDbContext.Tasks.Where(o => o.UserId == userId).ToListAsync();

            if (tasks.Count == 0)
            {
                return new GetTasksResponse
                { 
                    Success = false, 
                    Error = "No tasks found for this user", 
                    ErrorCode = "T04"
                };
            }

            return new GetTasksResponse { Success = true, Tasks = tasks };

        }

        public async Task<SaveTaskResponse> SaveTask(Task task)
        {
            await tasksDbContext.Tasks.AddAsync(task);

            var saveResponse = await tasksDbContext.SaveChangesAsync();
            
            if (saveResponse >= 0)
            {
                return new SaveTaskResponse
                {
                    Success = true,
                    Task = task
                };
            }
            return new SaveTaskResponse
            {
                Success = false,
                Error = "Unable to save task",
                ErrorCode = "T05"
            };
        }
    }
}
