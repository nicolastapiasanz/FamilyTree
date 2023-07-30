using FamilyTreeUtils.Models;
using MongoDB.Driver;

namespace FamilyTreeUtils.Service
{
    public class MongoPersonDatabaseService : PersonDatabaseService
    {
        private readonly IMongoDatabase _database;

        public MongoPersonDatabaseService() 
        {
            var client = new MongoClient("mongodb://localhost:27017");
            this._database = client.GetDatabase("family");
        }

        public async Task CreatePersonTo(string collectionName, Person newPerson)
        {
            try
            {
                var collectionDataBase = this._database.GetCollection<Person>(collectionName);
                await collectionDataBase.InsertOneAsync(newPerson);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task<Person?> EditPersonDataFrom(Person person)
        {
            await ReplacePersonDataFrom(person);

            var personDatabase = await PersonBy(person.Id);
            return personDatabase;
        }

        private async Task ReplacePersonDataFrom(Person person)
        {
            if (await DoesIdExists(person.Id))
            {
                var collection = await CollectionFrom(person.Id);
                await collection.ReplaceOneAsync(x => x.Id == person.Id, person);
            }
        }

        public async Task<bool> DoesIdExists(string id)
        {
            var person = await PersonBy(id);
            return person != null;
        }

        public async Task<Person?> PersonBy(string id)
        {
            var collection = await CollectionNames();
            return collection.Select(x => PersonBy(id, x)).FirstOrDefault() ?? null;
        }

        private Person? PersonBy(string id, string collectionName)
        {
            var personsInCollectionName = PersonsBy(collectionName);
            return personsInCollectionName.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Person> PersonsBy(string collectionName)
        {
            var collection = this._database.GetCollection<Person>(collectionName);
            return collection.Find(x => true).ToList();
        }


        private async Task<List<string>> CollectionNames()
        {
            var collectionNames = await this._database.ListCollectionNamesAsync();
            return collectionNames.ToList();
        }

        private async Task<IMongoCollection<Person>?> CollectionFrom(string id)
        {
            var collectionNames = await CollectionNames();
            return collectionNames.Select(x => PersonBy(id, x) != null ? this._database.GetCollection<Person>(x) : null).FirstOrDefault();
        }

    }
}
