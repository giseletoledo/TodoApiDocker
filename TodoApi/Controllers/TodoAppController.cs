using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoAppController : ControllerBase
    {
        private readonly INoteService _noteService;

        public TodoAppController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("all")]
        public IActionResult GetNotes()
        {
            var notes = _noteService.GetNotes();
            return Ok(notes);
        }

        [HttpGet("note/{id}")]
        public IActionResult GetNoteById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "O id não deve ser um número negativo" });
            }

            var note = _noteService.GetNoteById(id);
            if (note == null)
            {
                return NotFound(new { error = "Nota não encontrada" });
            }

            return Ok(note);
        }

        [HttpPost]
        [Route("AddNote")]
        public IActionResult AddNote([FromBody] NoteDto noteDto)
        {
            try
            {
                // Verificar se o objeto deserializado está nulo ou se o campo Description está vazio
                if (noteDto == null || string.IsNullOrEmpty(noteDto.Description))
                {
                    return BadRequest(new { error = "Descrição é obrigatória" });
                }

                // Adicionar a nota (usando um serviço ou repositório)
                _noteService.AddNote(noteDto);

                return Ok(new { message = "Nota adicionada com successo" });
            }
            catch (Exception ex)
            {
                // Capturar e logar exceções para ajudar na depuração
                Console.WriteLine("Erro: " + ex.Message);
                return BadRequest(new { error = "An error occurred: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, [FromBody] NoteDto noteDto)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "O id não deve ser um número negativo" });
            }

            noteDto.Id = id;
            _noteService.UpdateNote(noteDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "O id não deve ser um número negativo" });
            }

            _noteService.DeleteNote(id);
            return Ok();
        }
    }
}
