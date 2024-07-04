using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JwtAuthAspNet7WebAPI.Core.Dtos;

namespace JwtAuthAspNet7WebAPI.Core.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetJobsAsync();
        Task<Job> GetJobByIdAsync(int id);
        Task<Job> CreateJobAsync(Job job, IFormFile image);
        Task<Job> UpdateJobAsync(int id, Job job, IFormFile image);
        Task<bool> DeleteJobAsync(int id);
        Task<Job> MarkJobAsDeliveredAsync(int id);
        Task<Job> MarkJobAsDoneAsync(int id, String editedBy);
        Task<IEnumerable<Job>> GetDoneJobsAsync();
    }



}
