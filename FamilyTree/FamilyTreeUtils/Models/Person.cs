using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FamilyTreeUtils.Models
{
    public class Person
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)] public string Id { get; private set; }
        [BsonElement] public string Name { get; }

        protected Person() { }

        public Person(string name)
        {
            this.Name = name;
        }
    }
}
