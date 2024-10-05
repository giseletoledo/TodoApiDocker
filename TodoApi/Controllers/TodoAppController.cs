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
                return BadRequest(new { error = "The id must be a positive integer." });
            }

            var note = _noteService.GetNoteById(id);
            if (note == null)
            {
                return NotFound(new { error = "Note not found" });
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
                    return BadRequest(new { error = "Description is required" });
                }

                // Adicionar a nota (usando um serviço ou repositório)
                _noteService.AddNote(noteDto);

                return Ok(new { message = "Note added successfully" });
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
                return BadRequest(new { error = "The id must be a positive integer." });
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
                return BadRequest(new { error = "The id must be a positive integer." });
            }
            
            _noteService.DeleteNote(id);
            return Ok();
        }
    }
}
