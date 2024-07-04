using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JwtAuthAspNet7WebAPI.Core.Dtos;
using JwtAuthAspNet7WebAPI.Core.Interfaces;
using JwtAuthAspNet7WebAPI.Core.OtherObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthAspNet7WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PRODUCTION_WORKER + "," + StaticUserRoles.CARRIER)]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            return Ok(await _jobService.GetJobsAsync());
        }

        [HttpGet("done")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PRODUCTION_WORKER + "," + StaticUserRoles.CARRIER)]
        public async Task<ActionResult<IEnumerable<Job>>> GetDoneJobs()
        {
            return Ok(await _jobService.GetDoneJobsAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PRODUCTION_WORKER + "," + StaticUserRoles.CARRIER)]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return job;
        }

        [HttpPost]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PRODUCTION_WORKER)]
        public async Task<ActionResult<Job>> CreateJob([FromForm] JobRequestDto jobDto, IFormFile image)
        {
            var job = new Job
            {
                Name = jobDto.Name,
                Description = jobDto.Description,
                Department = jobDto.Department,
                AsignedBy = jobDto.AsignedBy,
                AsignedTo = jobDto.AsignedTo,
                AsignedOn = jobDto.AsignedOn,
                EditedBy = "",
                IsDone = false,
                CurrierDelivered = false,
            };

            var createdJob = await _jobService.CreateJobAsync(job, image);
            return CreatedAtAction(nameof(GetJob), new { id = createdJob.Id }, createdJob);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PRODUCTION_WORKER)]
        public async Task<IActionResult> UpdateJob(int id, [FromForm] JobRequestDto jobDto, IFormFile image)
        {
            var job = new Job
            {
                Id = id,
                Name = jobDto.Name,
                Description = jobDto.Description,
                Department = jobDto.Department,
                AsignedBy = jobDto.AsignedBy,
                AsignedTo = jobDto.AsignedTo,
                AsignedOn = jobDto.AsignedOn,
                EditedBy = User.Identity.Name // Assuming User.Identity.Name gives the current user
            };

            var updatedJob = await _jobService.UpdateJobAsync(id, job, image);
            if (updatedJob == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PRODUCTION_WORKER + "," + StaticUserRoles.CARRIER)]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var result = await _jobService.DeleteJobAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{id}/mark-done")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PRODUCTION_WORKER)]
        public async Task<IActionResult> MarkJobAsDone(int id, [FromBody] string editedBy)
        {
            var job = await _jobService.MarkJobAsDoneAsync(id, editedBy);
            if (job == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{id}/mark-delivered")]
        [Authorize(Roles = StaticUserRoles.CARRIER)]
        public async Task<IActionResult> MarkJobAsDelivered(int id)
        {
            var job = await _jobService.MarkJobAsDeliveredAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
