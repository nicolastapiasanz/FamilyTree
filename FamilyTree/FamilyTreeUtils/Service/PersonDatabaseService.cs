using FamilyTreeUtils.Models;

namespace FamilyTreeUtils.Service
{
    public interface PersonDatabaseService
    {
        public void CreatePersonTo(string collectionName, Person person);

        public Task<Person?> PersonBy(string id);

        public Task<Person?> EditPersonDataFrom(Person person);

        public Task<bool> DoesIdExists(string id);
    }
}
