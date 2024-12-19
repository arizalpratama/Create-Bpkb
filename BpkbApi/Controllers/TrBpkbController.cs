using BpkbApi.Data;
using BpkbApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BpkbApi.Controllers
{
    [ApiController]
    [Route("api/tr_bpkb")]
    public class TrBpkbController : ControllerBase
    {
        private readonly BpkbDbContext _context;

        public TrBpkbController(BpkbDbContext context)
        {
            _context = context;
        }

        [HttpGet("locations")]
        public IActionResult GetLocations()
        {
            var locations = _context.MsStorageLocations.ToList();
            return Ok(locations);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TrBpkb data, [FromHeader] string username)
        {
            if (data == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username is required.");
            }

            if (string.IsNullOrEmpty(data.AgreementNumber) ||
                string.IsNullOrEmpty(data.BpkbNo) ||
                string.IsNullOrEmpty(data.LocationId))
            {
                return BadRequest("AgreementNumber, BpkbNo, and LocationId are required fields.");
            }

            data.CreatedBy = username;
            data.LastUpdatedBy = username;
            data.CreatedOn = DateTime.UtcNow;
            data.LastUpdatedOn = DateTime.UtcNow;

            _context.TrBpkbs.Add(data);

            try
            {
                _context.SaveChanges();
                return Ok(new { message = "Data successfully inserted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving data: {ex.Message}");
            }
        }

        /*[HttpPost]
        public IActionResult Create([FromBody] TrBpkb data, [FromHeader] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            data.CreatedBy = username;
            data.LastUpdatedBy = username;
            data.CreatedOn = DateTime.UtcNow;
            data.LastUpdatedOn = DateTime.UtcNow;

            _context.TrBpkbs.Add(data);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving data: {ex.Message}");
            }

            return Ok("Data successfully inserted");
        }*/

        /*[HttpPost]
        public IActionResult Create([FromBody] TrBpkb data, [FromHeader] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username is required");
            }

            // Validasi data
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            data.CreatedBy = username;
            data.LastUpdatedBy = username;
            data.CreatedOn = DateTime.UtcNow; // Tambahkan field ini untuk mendukung CreatedOn jika ada di database.

            _context.TrBpkbs.Add(data);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving data: {ex.Message}");
            }

            return Ok("Data successfully inserted");
        }*/
    }
}
