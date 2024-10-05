using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetNotes();
        Note GetNoteById(int id);
        void AddNote(Note note);
        void UpdateNote(Note note);
        void DeleteNote(int id);
    }
}
