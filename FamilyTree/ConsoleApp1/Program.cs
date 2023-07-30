// See https://aka  .ms/new-console-template for more information

using FamilyTreeUtils.Models;
using FamilyTreeUtils.Service;

public class Program
{
    public static async Task Main()
    {
        PersonDatabaseService personDatabaseService = new MongoPersonDatabaseService();
        await TestFulano(personDatabaseService);

        //var client = new MongoClient("mongodb://localhost:27017");
        //var database = client.GetDatabase("family");

        //var commonDataBase = database.GetCollection<Person>("common");
        //var newPerson = new Person("Nicolas1");

        //commonDataBase.InsertOne(newPerson); //INSERT
        //var commonPeople = commonDataBase.Find(x => true).ToList(); //GET
        //var filter = Builders<Person>.Filter.Eq(x => x.Id, "64c5349ca501e893b942b9e6");
        //var concretePerson = commonDataBase.Find(filter).FirstOrDefault(); //GET CONCRETO

        //if (concretePerson != null)
        //{
        //    var newCoolPerson = new Person("Fulano")
        //    {
        //        Id = concretePerson.Id
        //    };
        //}




        //var 
        //commonDataBase.ReplaceOne(x => x.Id == "64c5349ca501e893b942b9e6", new Person("Manolo")); //REPLACE
    }

    static async Task TestFulano(PersonDatabaseService personDatabaseService)
    {
        var newPerson = new Person("Menganito");
        await personDatabaseService.CreatePersonTo("common", newPerson);

        var persons = personDatabaseService.PersonsBy("common");
        persons.ToList().ForEach(x => Console.WriteLine(x.Name));

        var lala = await personDatabaseService.EditPersonDataFrom(persons.First().ChangeName("Cacafuti"));
        Console.WriteLine(lala.Name);
        var caca = 0;
    }

}
