using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface INoteService
    {
        IEnumerable<NoteDto> GetNotes();
        NoteDto GetNoteById(int id);
        void AddNote(NoteDto noteDto);
        void UpdateNote(NoteDto noteDto);
        void DeleteNote(int id);
    }
}
