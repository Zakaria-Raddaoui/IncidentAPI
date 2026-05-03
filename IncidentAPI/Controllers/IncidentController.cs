using IncidentAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IncidentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private static readonly List<Incident> _incidents = new();
        private static int _nextId = 1;
        private static readonly string[] AllowedSeverities =
        { "LOW", "MEDIUM", "HIGH", "CRITICAL" };
        private static readonly string[] AllowedStatuses =
        { "OPEN", "IN_PROGRESS", "RESOLVED" };

        // Create a new incident
        [HttpPost("create-incident")]
        public IActionResult CreateIncident([FromBody] Incident incident)
        {
            if (!AllowedSeverities.Contains(incident.Severity.ToUpper()))
                return BadRequest("Invalid severity");

            incident.Id = _nextId++;
            incident.Status = "OPEN";
            incident.CreatedAt = DateTime.Now;

            _incidents.Add(incident);

            return Ok(incident);
        }

        // Get all incidents
        [HttpGet("get-all")]
        public IActionResult GetAllIncidents()
        {
            return Ok(_incidents);
        }

        // Get an incident by ID
        [HttpGet("getbyid/{id}")]
        public IActionResult GetIncidentById(int id)
        {
            try
            {
                var incident = _incidents.First(i => i.Id == id);
                return Ok(incident);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Incident with ID: {id} not found.");
            }
        }

        // Update the status of an incident
        [HttpPut("update-status/{id}")]
        public IActionResult UpdateIncidentStatus(int id, [FromBody] string status)
        {
            if (!AllowedStatuses.Contains(status.ToUpper()))
                return BadRequest("Invalid status");
            try
            {
                var incident = _incidents.First(i => i.Id == id);
                incident.Status = status.ToUpper();
                return Ok(incident);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Incident with ID: {id} not found.");
            }
        }

        // Delete an incident by ID
        [HttpDelete("delete-incident/{id}")]
        public IActionResult DeleteIncident(int id)
        {
            try
            {
                var incident = _incidents.First(i => i.Id == id);
                if (!(incident.Status.ToUpper() == "OPEN") && (incident.Severity.ToUpper() == "CRITICAL"))
                {
                    _incidents.Remove(incident);
                    return Ok($"Incident with the ID {id} has been deleted successfully.");
                }
                else
                {
                    return BadRequest("The incident is still open and is critical, failed to delete.");
                }

            }
            catch (InvalidOperationException)
            {
                return NotFound("Incident with the ID {id} not found.");
            }
        }

        // Get incidents by status
        [HttpGet("get-by-status/{status}")]
        public IActionResult GetIncidentsByStatus(string status)
        {
            if (!AllowedStatuses.Contains(status.ToUpper()))
                return BadRequest("Invalid status");
            var incidents = _incidents.Where(i => i.Status.ToUpper() == status.ToUpper()).ToList();
            return Ok(incidents);
        }

        // Get incidents by severity
        [HttpGet("get-by-severity/{severity}")]
        public IActionResult GetIncidentsBySeverity(string severity)
        {
            if (!AllowedSeverities.Contains(severity.ToUpper()))
                return BadRequest("Invalid severity");
            var incidents = _incidents.Where(i => i.Severity.ToUpper() == severity.ToUpper()).ToList();
            return Ok(incidents);
        }
    }
}
