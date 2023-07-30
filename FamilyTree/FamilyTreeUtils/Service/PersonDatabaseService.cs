using FamilyTreeUtils.Models;

namespace FamilyTreeUtils.Service
{
    public interface PersonDatabaseService
    {
        public Task CreatePersonTo(string collectionName, Person person);

        public IEnumerable<Person> PersonsBy(string collectionName);

        public Task<Person?> PersonBy(string id);

        public Task<Person?> EditPersonDataFrom(Person person);

        public Task<bool> DoesIdExists(string id);
    }
}
