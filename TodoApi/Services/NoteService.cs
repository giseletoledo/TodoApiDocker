using TodoApi.DTOs;
using TodoApi.Exceptions;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public IEnumerable<NoteDto> GetNotes()
        {
            var notes = _noteRepository.GetNotes();
            return notes.Select(note => new NoteDto
            {
                Id = note.Id,
                Description = note.Description
            }).ToList();
        }

        public NoteDto GetNoteById(int id)
        {
            var note = _noteRepository.GetNoteById(id);
            if (note == null) throw new CustomException("Nota não encontrada!");

            return new NoteDto { Id = note.Id, Description = note.Description };
        }

        public void AddNote(NoteDto noteDto)
        {
            var note = new Note
            {
                Description = noteDto.Description,
                CreatedAt = DateTime.UtcNow
            };
            _noteRepository.AddNote(note);
        }

        public void UpdateNote(NoteDto noteDto)
        {
            var note = new Note { Id = noteDto.Id, Description = noteDto.Description };
            _noteRepository.UpdateNote(note);
        }

        public void DeleteNote(int id)
        {
            _noteRepository.DeleteNote(id);
        }
    }
}
