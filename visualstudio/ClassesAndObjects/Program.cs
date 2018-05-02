using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesAndObjects
{
    class Colour
    {
        public float r, g, b;
    }

    class Dog
    {
        
        public string name;
        public int size;
        public string breed;
        public Colour colour;
        public ConsoleColor color;

        public void Walk()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " is Walking. ");
            
        }

        public void Eat(string food)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " is eating " + food);  
        }

        public void Shit()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " is shitting.");
        }

        public void Sleep()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " is sleeping.");
        }

        public void Wag()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " is wagging it's tail.");
        }

        public void Speak()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " says: Woof!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Create instance of colour
            /*Colour brown = new Colour();
            brown.r = 139;
            brown.g = 69;
            brown.b = 19;
            */

            // Create instance of dog
            Dog dog1 = new Dog();
            dog1.name = "Lassie";
            dog1.size = 5;
            dog1.breed = "Cavoodle";
            dog1.color = ConsoleColor.Green;

            Dog dog2 = new Dog();
            dog2.name = "Pood";
            dog2.size = 5;
            dog2.breed = "Something";
            dog2.color = ConsoleColor.Yellow;

            //Call methods on dog
            dog1.Speak();
            dog1.Walk();
            dog1.Eat("Meat");
            dog1.Shit();

            dog2.Speak();
            dog2.Walk();
            dog2.Eat("Meat");
            dog2.Shit();

            Console.ReadLine();
        }
    }
}
