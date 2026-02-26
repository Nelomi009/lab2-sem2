/**************************
* Автор: Nikita Chernenko *
* Дата: 26.02.2026        *
* Вариант -               *
***************************/


using System;
using System.Collections.Generic;

namespace Zoo
{
    //базовый класс
    class Animal
    {
        public string Name;
        public int Age;
        public string Habitat;
        public string Food;

        public Animal(string name, int age, string habitat, string food)
        {
            Name = name;
            Age = age;
            Habitat = habitat;
            Food = food;
        }

        public virtual string GetInfo()
        {
            return $"Name: {Name}, Age: {Age}, Habitat: {Habitat}, Food: {Food}";
        }
    }

    // дочерний класс 
    class Mammal : Animal
    {
        public bool HasFur;

        public Mammal(string name, int age, string habitat, string food, bool hasFur)
          : base(name, age, habitat, food)
        {
            HasFur = hasFur;
        }

        public override string GetInfo()
        {
            string fur = HasFur ? "yes" : "no";
            return base.GetInfo() + $", Type: Mammal, Fur: {fur}";
        }
    }

    class Bird : Animal
    {
        public double Wings;

        public Bird(string name, int age, string habitat, string food, double wings)
          : base(name, age, habitat, food)
        {
            Wings = wings;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Type: Bird, Wingspan: {Wings}сm";
        }
    }

    class Fish : Animal
    {
        public string Water;

        public Fish(string name, int age, string habitat, string food, string water)
          : base(name, age, habitat, food)
        {
            Water = water;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Type: Fish, Water type: {Water}";
        }
    }

    class Reptile : Animal
    {
        public bool Poison;

        public Reptile(string name, int age, string habitat, string food, bool poison)
          : base(name, age, habitat, food)
        {
            Poison = poison;
        }

        public override string GetInfo()
        {
            string poisonText = Poison ? "yes" : "no";
            return base.GetInfo() + $", Type: Reptile, Poisonous: {poisonText}";
        }
    }

    class Amphibian : Animal
    {
        public string Skin;

        public Amphibian(string name, int age, string habitat, string food, string skin)
          : base(name, age, habitat, food)
        {
            Skin = skin;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Type: Amphibian, Skin type: {Skin}";
        }
    }

    // singleton
    class ZooManager
    {
        private static ZooManager instance;
        private List<Animal> animals = new List<Animal>();

        private ZooManager() { }

        public static ZooManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ZooManager();
                return instance;
            }
        }

        public void Add(Animal a)
        {
            animals.Add(a);
            Console.WriteLine($"Added: {a.Name}");
        }

        public void ShowAll()
        {
            if (animals.Count == 0)
            {
                Console.WriteLine("No animals");
                return;
            }

            Console.WriteLine("\n=== ALL ANIMALS ===");
            for (int i = 0; i < animals.Count; ++i)
            {
                Console.WriteLine($"{i + 1}. {animals[i].GetInfo()}");
            }
        }

        public void ShowByIndex(int index)
        {
            if (index >= 0 && index < animals.Count)
                Console.WriteLine(animals[index].GetInfo());
            else
                Console.WriteLine("Invalid number");
        }

        public void ShowByName(string name)
        {
            bool found = false;
            foreach (var a in animals)
            {
                if (a.Name.ToLower() == name.ToLower())
                {
                    Console.WriteLine(a.GetInfo());
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine($"No animal with name {name}");
        }
    }

    // програма
    class Program
    {
        static void Main()
        {
            ZooManager zoo = ZooManager.Instance;

            // тестовые животные
            zoo.Add(new Mammal("Gazirovkin", 5, "Savanna", "Meat", true));
            zoo.Add(new Bird("Parkurov", 2, "Forest", "Grain", 20));
            zoo.Add(new Fish("Lolkekvich", 1, "Ocean", "Plankton", "Salt water"));

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine(
                  "\n=== MENU ===" +
                  "\n1. All animals" +
                  "\n2. Find by number" +
                  "\n3. Find by name" +
                  "\n4. Add animal" +
                  "\n5. Exit" +
                  "\nChoose: "
                );

                string choice = Console.ReadLine();

                if (choice == "1") zoo.ShowAll();
                else if (choice == "2")
                {
                    Console.Write("Number: ");
                    int num = int.Parse(Console.ReadLine());
                    zoo.ShowByIndex(num - 1);
                }
                else if (choice == "3")
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    zoo.ShowByName(name);
                }
                else if (choice == "4") AddAnimal(zoo);
                else if (choice == "5") exit = true;
                else Console.WriteLine("Error: enter 1-5");
            }
        }

        static void AddAnimal(ZooManager zoo)
        {
            Console.WriteLine(
              "\n=== ANIMAL TYPE ===" +
              "\n1. Mammal" +
              "\n2. Bird" +
              "\n3. Fish" +
              "\n4. Reptile" +
              "\n5. Amphibian" +
              "\nChoose: "
            );

            string type = Console.ReadLine();

            // Общие данные
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Habitat: ");
            string habitat = Console.ReadLine();

            Console.Write("Food: ");
            string food = Console.ReadLine();

            if (type == "1") // млекопитающие
            {
                Console.Write("Has fur? (yes/no): ");
                bool fur = Console.ReadLine() == "yes";
                zoo.Add(new Mammal(name, age, habitat, food, fur));
            }
            else if (type == "2") // птицы
            {
                Console.Write("Wingspan (cm): ");
                double wings = double.Parse(Console.ReadLine());
                zoo.Add(new Bird(name, age, habitat, food, wings));
            }
            else if (type == "3") // рыбы
            {
                Console.Write("Water type (fresh/salt): ");
                string water = Console.ReadLine();
                zoo.Add(new Fish(name, age, habitat, food, water));
            }
            else if (type == "4") // рептилии
            {
                Console.Write("Poisonous? (yes/no): ");
                bool poison = Console.ReadLine() == "yes";
                zoo.Add(new Reptile(name, age, habitat, food, poison));
            }
            else if (type == "5") // земноводные
            {
                Console.Write("Skin type: ");
                string skin = Console.ReadLine();
                zoo.Add(new Amphibian(name, age, habitat, food, skin));
            }
            else
            {
                Console.WriteLine("Invalid type");
            }
        }
    }
}