using BlazorWasm.Compartilhado.Entidades;
using BlazorWasmServer.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWasm.BackEnd.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {


        private readonly ApplicationDbContext context;
        public ProfessorController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet("login/{prof}/senha/{pass}")]
        public async Task<ActionResult<bool>> Get(string prof, string pass)
        {
            var professor = await context.Professor.FirstOrDefaultAsync(p => p.NomeProf == prof);
            if (professor != null)
            {
                if (pass == "admin123")
                    return true;
            }

            return false;
        }

        [HttpGet]
        public async Task<ActionResult<List<Professor>>> Get()
        {
            return await context.Professor.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> Get(int id)
        {
            var resp = await context.Professor.FirstOrDefaultAsync(x => x.Id == id);
            if (resp == null) { return NotFound(); }
            return resp;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(Professor professor)
        {
            context.Professor.Add(professor);
            await context.SaveChangesAsync();
            return professor.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Professor professor)
        {
            context.Attach(professor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProf(int id)
        {
            var professor = await context.Professor.FirstOrDefaultAsync(x => x.Id == id);
            if (professor == null)
            {
                return NotFound();
            }

            context.Remove(professor);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
