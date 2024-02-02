using StarlabsCRUD.Models;

namespace StarlabsCRUD.Interfaces
{
    public interface IPersonService
    {
        void AddPerson(Person person);
        void UpdatePerson(int personId, Person updatedPerson);
        void DeletePerson(int personId);
    }
}
