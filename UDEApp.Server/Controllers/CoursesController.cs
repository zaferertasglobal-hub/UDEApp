using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UDEApp.Server.Data;
using UDEApp.Shared.Models;

namespace UDEApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CoursesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<List<Course>> Get()
            => await _db.Courses.ToListAsync();

        [HttpGet("thesis")]
        public async Task<string> GetThesis()
        {
            var list = await _db.Courses.ToListAsync();
            var rnd = new Random();
            var selected = list.OrderBy(x => rnd.Next()).Take(3).ToList();

            return $"Bitirme Projesi Önerisi:\n\n" +
                   $"\"{selected[0].Name}\" ve \"{selected[1].Name}\" konularını birleştirerek\n" +
                   $"\"{selected[2].Name}\" alanında bir sistem geliştir.\n\n" +
                   $"Örnek başlık: {selected[0].Name.Split(' ')[0]} + {selected[1].Name.Split(' ')[0]} Tabanlı {selected[2].Name.Split(' ')[0]} Sistemi";
        }
    }
}