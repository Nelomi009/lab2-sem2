/**************************
* Автор: Nikita Chernenko *
* Дата: 28  .02.2026        *
* Вариант -               *
***************************/

using System;
using System.Collections.Generic;

namespace Zoo {

  // Базовый абстрактный класс
  public abstract class Animal {

    public string Name { get; private set; }
    public int Age { get; private set; }
    public string Habitat { get; private set; }
    public string Food { get; private set; }

    protected Animal(string name, int age, string habitat, string food) {
      Name = name;
      Age = age;
      Habitat = habitat;
      Food = food;
    }

    public virtual string GetInfo() {
      return $"Name: {Name}, Age: {Age}, Habitat: {Habitat}, Food: {Food}";
    }
  }

  // Производные классы
  public class Mammal : Animal {

    public bool HasFur { get; private set; }

    public Mammal(string name, int age, string habitat, string food, bool hasFur)
        : base(name, age, habitat, food) {
      HasFur = hasFur;
    }

    public override string GetInfo() {
      string fur = HasFur ? "yes" : "no";
      return base.GetInfo() + $", Type: Mammal, Fur: {fur}";
    }
  }

  public class Bird : Animal {

    public double Wings { get; private set; }

    public Bird(string name, int age, string habitat, string food, double wings)
        : base(name, age, habitat, food) {
      Wings = wings;
    }

    public override string GetInfo() {
      return base.GetInfo() + $", Type: Bird, Wingspan: {Wings}cm";
    }
  }

  public class Fish : Animal {

    public string Water { get; private set; }

    public Fish(string name, int age, string habitat, string food, string water)
        : base(name, age, habitat, food) {
      Water = water;
    }

    public override string GetInfo() {
      return base.GetInfo() + $", Type: Fish, Water type: {Water}";
    }
  }

  public class Reptile : Animal {

    public bool Poison { get; private set; }

    public Reptile(string name, int age, string habitat, string food, bool poison)
        : base(name, age, habitat, food) {
      Poison = poison;
    }

    public override string GetInfo() {
      string poisonText = Poison ? "yes" : "no";
      return base.GetInfo() + $", Type: Reptile, Poisonous: {poisonText}";
    }
  }

  public class Amphibian : Animal {

    public string Skin { get; private set; }

    public Amphibian(string name, int age, string habitat, string food, string skin)
        : base(name, age, habitat, food) {
      Skin = skin;
    }

    public override string GetInfo() {
      return base.GetInfo() + $", Type: Amphibian, Skin type: {Skin}";
    }
  }

  // singleton 
  public class ZooManager {

    private static ZooManager s_instance;

    public static ZooManager Instance {
      get {
        if (s_instance == null) {
          s_instance = new ZooManager();
        }
        return s_instance;
      }
    }

    private List<Animal> _animals;

    private ZooManager() {
      _animals = new List<Animal>();
    }

    public void Add(Animal a) {
      _animals.Add(a);
      Console.WriteLine($"Added: {a.Name}");
    }

    public void ShowAll() {
      if (_animals.Count == 0) {
        Console.WriteLine("No animals");
        return;
      }

      Console.WriteLine("\n=== ALL ANIMALS ===");
      for (int animalIndex = 0; animalIndex < _animals.Count; ++animalIndex) {
        Console.WriteLine($"{animalIndex + 1}. {_animals[animalIndex].GetInfo()}");
      }
    }

    public void ShowByIndex(int index) {
      if (index >= 0 && index < _animals.Count) {
        Console.WriteLine(_animals[index].GetInfo());
      }
      else {
        Console.WriteLine("Invalid number");
      }
    }

    public void ShowByName(string name) {
      bool found = false;
      foreach (var animal in _animals) {
        if (animal.Name.ToLower() == name.ToLower()) {
          Console.WriteLine(animal.GetInfo());
          found = true;
        }
      }
      if (!found) {
        Console.WriteLine($"No animal with name {name}");
      }
    }
  }

  // Точка входа в программу
  class Program {

    private const int MenuExitOption = 5;
    private const int DisplayOffset = 1;

    static void Main() {
      ZooManager zoo = ZooManager.Instance;

      // Тестовые животные
      zoo.Add(new Mammal("Gazirovkin", 5, "Savanna", "Meat", true));
      zoo.Add(new Bird("Parkurov", 2, "Forest", "Grain", 20));
      zoo.Add(new Fish("Lolkekvich", 1, "Ocean", "Plankton", "Salt water"));

      RunMenu();
    }

    private static void RunMenu() {
      ZooManager zoo = ZooManager.Instance;
      bool exit = false;

      while (!exit) {
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

        switch (choice) {
          case "1":
            zoo.ShowAll();
            break;

          case "2":
            Console.Write("Number: ");
            if (int.TryParse(Console.ReadLine(), out int num)) {
              zoo.ShowByIndex(num - DisplayOffset);
            }
            else {
              Console.WriteLine("Invalid input");
            }
            break;

          case "3":
            Console.Write("Name: ");
            string name = Console.ReadLine();
            zoo.ShowByName(name);
            break;

          case "4":
            AddAnimal();
            break;

          case "5":
            exit = true;
            break;

          default:
            Console.WriteLine("Error: enter 1-5");
            break;
        }
      }
    }

    private static void AddAnimal() {
      ZooManager zoo = ZooManager.Instance;

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
      if (!int.TryParse(Console.ReadLine(), out int age)) {
        Console.WriteLine("Invalid age");
        return;
      }

      Console.Write("Habitat: ");
      string habitat = Console.ReadLine();

      Console.Write("Food: ");
      string food = Console.ReadLine();

      switch (type) {
        case "1": // млекопитающие
          Console.Write("Has fur? (yes/no): ");
          bool fur = Console.ReadLine()?.ToLower() == "yes";
          zoo.Add(new Mammal(name, age, habitat, food, fur));
          break;

        case "2": // птицы
          Console.Write("Wingspan (cm): ");
          if (double.TryParse(Console.ReadLine(), out double wings)) {
            zoo.Add(new Bird(name, age, habitat, food, wings));
          }
          else {
            Console.WriteLine("Invalid wingspan");
          }
          break;

        case "3": // рыбы
          Console.Write("Water type (fresh/salt): ");
          string water = Console.ReadLine();
          zoo.Add(new Fish(name, age, habitat, food, water));
          break;

        case "4": // рептилии
          Console.Write("Poisonous? (yes/no): ");
          bool poison = Console.ReadLine()?.ToLower() == "yes";
          zoo.Add(new Reptile(name, age, habitat, food, poison));
          break;

        case "5": // земноводные
          Console.Write("Skin type: ");
          string skin = Console.ReadLine();
          zoo.Add(new Amphibian(name, age, habitat, food, skin));
          break;

        default:
          Console.WriteLine("Invalid type");
          break;
      }
    }
  }
}