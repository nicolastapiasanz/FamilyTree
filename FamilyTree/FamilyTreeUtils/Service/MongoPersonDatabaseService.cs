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

        public async void CreatePersonTo(string collectionName, Person newPerson)
        {
            var collectionDataBase = this._database.GetCollection<Person>(collectionName);
            await collectionDataBase.InsertOneAsync(newPerson, new InsertOneOptions(), CancellationToken.None);
        }

        public async Task<Person?> EditPersonDataFrom(Person person)
        {
            if (await DoesIdExists(person.Id))
            {
                var collection = await CollectionFrom(person.Id);
                await collection.ReplaceOneAsync(x => x.Id == person.Id, person);
            }

            var personDatabase = await PersonBy(person.Id);
            return personDatabase;
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
            var collection = this._database.GetCollection<Person>(collectionName);
            var filter = Builders<Person>.Filter.Eq(person => person.Id, id);
            var person = collection.Find(filter).FirstOrDefault();

            return person ?? null;
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
