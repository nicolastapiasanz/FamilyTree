// See https://aka  .ms/new-console-template for more information

using FamilyTreeUtils.Models;
using FamilyTreeUtils.Service;

public class Program
{
    public static void Main()
    {
        PersonDatabaseService personDatabaseService = new MongoPersonDatabaseService();
        TestFulano(personDatabaseService);

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

    static async void TestFulano(PersonDatabaseService personDatabaseService)
    {
        var newPerson = new Person("Fulano2");
        personDatabaseService.CreatePersonTo("common", newPerson);
        try
        {
            var person = await personDatabaseService.PersonBy(newPerson.Id);
            Console.WriteLine(person.Name);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        var caca = 0;
    }

}
