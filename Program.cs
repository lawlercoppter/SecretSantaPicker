using System;
using System.Collections.Generic;

namespace SecretSantaPicker
{
    class Program
    {
        static void Main(string[] args)
        {

            //initialize shit
            Console.WriteLine("Hello Santas!");
            var input = "";
            var people = new List<Person> { };
            var idTrack = 0;
            int numOfPeople = 0;


            Console.Write("How many people are involved?: ");
            while (numOfPeople == 0)
            {
                numOfPeople = GetPeople();

            }

            Console.WriteLine("\n \n \n");


            //take input to create list of santas
            while (people.Count < numOfPeople)
            {
                Console.Write("Please Enter the Person's name: ");
                input = Console.ReadLine().ToString();

                Person person = new Person { Name = input, Id = idTrack++ };
                Console.WriteLine(person.Name.ToString() + person.Id.ToString());
                people.Add(person);

            }

            //create list of ID's to assign
            idTrack = 1;
            List<int> gifteeIdList = new List<int>() { };


            //assign the ids
            foreach (var person in people)
            {
                gifteeIdList = AssignGiftee(people.Count, person.Id, gifteeIdList);
                //check that the ID assigned doesnt match the person they're giving the gift to
                person.GifteeId = gifteeIdList[gifteeIdList.Count - 1];
            }

            //output each object to a text doc that will be called the persons name and contain the name of the person they are assigned to
            Console.Write("Please enter the file path you would like to export to(example:\"D:\\SecretSanta\\\"): ");
            var path = Console.ReadLine();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path+@"master.txt"))
            {

                foreach (var person in people)
                {
                    var giftee = (people.Find(x => x.Id == person.GifteeId)).Name;
                    file.WriteLine(person.Name+ " is getting a gift for " +giftee);
                    using (System.IO.StreamWriter subFile = new System.IO.StreamWriter(path+ person.Name + ".txt"))
                    {
                        subFile.WriteLine("You are the Secret Santa for " + giftee! +" Shhhhh! Keep it a secrt!");
                    }
                }

            }



            Console.WriteLine("Done");

        }

        public static int GetPeople()
        {
            int input = 0;
            try
            {
                Console.Write("Please enter the number of participants: ");
                input = Int32.Parse(Console.ReadLine());
                return input;
            }
            catch
            {
                Console.Write("That is not a number: ");

            }
            return 0;
        }

        public static List<int> AssignGiftee(int count, int personId, List<int> gifteeIdList)
        {



            Random rnd = new Random();
            int assignment = 0;
            bool noConflict = false;
            //check that the ID assigned doesnt match the person they're giving the gift to
            while (!noConflict)
            {
                assignment = rnd.Next(0, count);
                if (assignment != personId && !gifteeIdList.Contains(assignment))
                {
                    noConflict = true;
                }

            }
            gifteeIdList.Add(assignment);
            return gifteeIdList;
        }
    }
    public class Person
    {


        public string Name { get; set; }
        public int Id { get; set; }
        public int GifteeId { get; set; }



    }
}
