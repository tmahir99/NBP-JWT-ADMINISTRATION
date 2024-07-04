using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthAspNet7WebAPI.Core.DbContext;
using JwtAuthAspNet7WebAPI.Core.Dtos;
using JwtAuthAspNet7WebAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class JobService : IJobService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<JobService> _logger;

    public JobService(ApplicationDbContext context, ILogger<JobService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Job>> GetJobsAsync()
    {
        try
        {
            return await _context.Jobs.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching jobs");
            throw;
        }
    }

    public async Task<IEnumerable<Job>> GetDoneJobsAsync()
    {
        try
        {
            return await _context.Jobs.Where(job => job.IsDone).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching done jobs");
            throw;
        }
    }

    public async Task<Job> GetJobByIdAsync(int id)
    {
        try
        {
            return await _context.Jobs.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching job with id {id}");
            throw;
        }
    }

    public async Task<Job> CreateJobAsync(Job job, IFormFile image)
    {
        try
        {
            if (image != null && image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    job.Image = memoryStream.ToArray();
                }
            }

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating job");
            throw;
        }
    }

    public async Task<Job> UpdateJobAsync(int id, Job job, IFormFile image)
    {
        try
        {
            var existingJob = await _context.Jobs.FindAsync(id);
            if (existingJob == null) return null;

            existingJob.Name = job.Name;
            existingJob.Description = job.Description;
            existingJob.Department = job.Department;
            existingJob.AsignedBy = job.AsignedBy;
            existingJob.AsignedTo = job.AsignedTo;
            existingJob.AsignedOn = job.AsignedOn;
            existingJob.EditedBy = job.EditedBy;

            if (image != null && image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    existingJob.Image = memoryStream.ToArray();
                }
            }

            await _context.SaveChangesAsync();
            return existingJob;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating job with id {id}");
            throw;
        }
    }

    public async Task<bool> DeleteJobAsync(int id)
    {
        try
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return false;

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting job with id {id}");
            throw;
        }
    }

    public async Task<Job> MarkJobAsDoneAsync(int id, string editedBy)
    {
        try
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return null;

            job.IsDone = true;
            job.EditedBy = editedBy;

            await _context.SaveChangesAsync();

            return job;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error marking job as done with id {id}");
            throw;
        }
    }

    public async Task<Job> MarkJobAsDeliveredAsync(int id)
    {
        try
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return null;

            job.CurrierDelivered = true;
            await _context.SaveChangesAsync();
            return job;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error marking job as delivered with id {id}");
            throw;
        }
    }
}
