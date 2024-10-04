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

        [HttpGet]
        public IActionResult GetNotes()
        {
            var notes = _noteService.GetNotes();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public IActionResult GetNoteById(int id)
        {
            var note = _noteService.GetNoteById(id);
            return Ok(note);
        }

        [HttpPost]
        public IActionResult AddNote([FromBody] NoteDto noteDto)
        {
            _noteService.AddNote(noteDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, [FromBody] NoteDto noteDto)
        {
            noteDto.Id = id;
            _noteService.UpdateNote(noteDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            _noteService.DeleteNote(id);
            return Ok();
        }
    }
}
