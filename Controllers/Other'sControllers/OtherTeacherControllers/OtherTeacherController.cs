﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registeration.DTOs;
using Registeration.DTOs.CrmDTOs;
using Registeration.DTOs.SchoolDTOs;
using Registeration.Models;
using Registeration.Data;
using Registeration.DTOs.OtherTeacherDTOs;
using Registeration.Services.OtherTeacherServices;
using Registeration.Services.CrmServices;

namespace Registeration.Controllers
{
    [ApiController]
    [Route("api/otherteacher")]
    public class OtherTeacherController : ControllerBase
    {
        private readonly OtherTeacherService _service;
        private readonly CrmService _crmService;
        private readonly AppDbContext _context;

        public OtherTeacherController(
            OtherTeacherService service,
            CrmService crmService,
            AppDbContext context)
        {
            _service = service;
            _crmService = crmService;
            _context = context;
        }

        /// <summary>
        /// Save OtherTeacher with region and school info.
        /// </summary>
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] OtherTeacherDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(dto.Region?.Name) || string.IsNullOrWhiteSpace(dto.School?.Name))
                return BadRequest("Region and School names must be provided.");

            try
            {
                var region = await _crmService.SaveRegionIfNotExistsAsync(dto.Region.Name);

                var school = await _crmService.SaveSchoolIfNotExistsAsync(
                    dto.School.Name,
                    dto.School.Address ?? "Not Provided",
                    region.Id);

                var model = new OtherTeacher
                {
                    Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                    SchoolId = school.Id,
                    RegionId = region.Id,
                    FullName = dto.FullName,
                    TeachingSubject = dto.TeachingSubject,
                    SocNumber = dto.SocNumber ?? "",
                    Email = dto.Email ?? ""
                };

                await _service.SaveAsync(model);
                return Ok(new { message = "OtherTeacher saved successfully", model.Id });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Foreign key violation: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<OtherTeacherDTO>> GetById(Guid id)
        {
            var teacher = await _service.GetByIdAsync(id);

            if (teacher == null)
                return NotFound(new { Message = "OtherTeacher not found" });

            var dto = new OtherTeacherDTO
            {
                Id = teacher.Id,
                Region = new RegionDTO
                {
                    Id = teacher.Region.Id,
                    Name = teacher.Region.Name
                },
                School = new SchoolDTO
                {
                    Id = teacher.School.Id,
                    Name = teacher.School.Name,
                    Address = teacher.School.Address,
                    RegionId = teacher.School.RegionId
                },
                FullName = teacher.FullName,
                TeachingSubject = teacher.TeachingSubject,
                SocNumber = teacher.SocNumber,
                Email = teacher.Email
            };

            return Ok(dto);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var teacher = await _context.OtherTeacher.FindAsync(id);
            if (teacher == null)
                return NotFound();

            _context.OtherTeacher.Remove(teacher);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted" });
        }

        [HttpGet("identity-string/{id}")]
        public async Task<IActionResult> GetIdentityString(Guid id)
        {
            var result = await _service.GetConcatenatedIdentityAsync(id);

            if (string.IsNullOrWhiteSpace(result))
                return NotFound(new { message = "OtherTeacher not found." });

            return Ok(new { identity = result });
        }

        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> Confirm(Guid id, [FromBody] ConfirmCredentialsDTO dto)
        {
            var teacher = await _context.OtherTeacher.FindAsync(id);
            if (teacher == null)
                return NotFound();

            teacher.Login = teacher.Email;
            teacher.Password = dto.Password;
            teacher.AccessCode = dto.AccessCode;
            teacher.Role = dto.Role;
            teacher.UserType = dto.UserType;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
