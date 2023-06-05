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
    public class AlunoController : ControllerBase
    {
        
    
        private readonly ApplicationDbContext context;
        public AlunoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Aluno>>> Get()
        {
            return await context.Alunos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> Get(int id)
        {
            var resp = await context.Alunos.FirstOrDefaultAsync(x => x.Id == id);
            if (resp == null) { return NotFound(); }
            return resp;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(Aluno aluno)
        {
            context.Alunos.Add(aluno);
            await context.SaveChangesAsync();
            return aluno.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Aluno aluno)
        {
            context.Attach(aluno).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var aluno = await context.Alunos.FirstOrDefaultAsync(x => x.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            context.Remove(aluno);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}

