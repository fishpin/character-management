using System;
using System.Collections.Generic;

namespace CharacterManagement
{
    //Represents a single playable character
    class Character
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; } = 1;
        public int HitPoints { get; set; }
        public int AvailableAttributePoints { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();

        public override string ToString()
        {
            return $"Character: {Name}, Class: {Class}, Level: {Level}, HP: {HitPoints}, Attribute Points: {AvailableAttributePoints}, Skills: {Skills}";
        }
    }

    //Represents a learnable skill with cost and description
    class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Attribute { get; set; }
        public int RequiredAttributePoints { get; set; }

        public override string ToString()
        {
            return $"{Name} Attribute: {Attribute}, Cost: {RequiredAttributePoints} points, Description: {Description}";
        }
    }

    internal class Program
    {
        //Helper: Safely reads an integer with validation loop
        private static int SafeReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a whole number.");
            }
        }

        // Helper: Asks for character name until a valid response is entered
        private static Character FindCharacter(List<Character> characters)
        {
            while (true)
            {
                Console.Write("Enter character name: ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Character name cannot be empty. Please try again.");
                    continue;
                }

                foreach (Character c in characters)
                {
                    if (c.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        return c;
                    }
                }

                Console.WriteLine("Character not found, please try again.");
            }
        }

        //Creates a new character with full input validation
        static void CreateCharacter(List<Character> characters)
        {
            //Name validation: cannot be empty or just spaces
            string name;
            while (true)
            {
                Console.Write("Enter name: ");
                name = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(name)) break;
                Console.WriteLine("Name cannot be empty. Please try again.");
            }

            //Class validation: same rules as name
            string charClass;
            while (true)
            {
                Console.Write("Enter class: ");
                charClass = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(charClass)) break;
                Console.WriteLine("Class cannot be empty. Please try again.");
            }

            //Prevent erroneous input
            int ap = SafeReadInt("Enter total attribute points: ");
            while (ap < 0)
            {
                Console.WriteLine("Attribute points cannot be negative.");
                ap = SafeReadInt("Enter total attribute points: ");
            }

            //Create character with calculated hit points (10 + attribute points/2)
            Character c = new Character
            {
                Name = name,
                Class = charClass,
                Level = 1,
                HitPoints = 10 + ap / 2,
                AvailableAttributePoints = ap,
                Skills = new List<Skill>()
            };

            characters.Add(c);
            Console.WriteLine($"\nCharacter '{name}' created successfully!\n");
        }

        //Assigns a skill to a character if they have enough points and don't already have it
        static void AssignSkill(List<Character> characters, List<Skill> skills)
        {
            //Guard clause: Don't proceed if no characters exist
            if (characters.Count == 0)
            {
                Console.WriteLine("No characters have been created yet. Please create a character first!\n");
                return;
            }

            Character selectedChar = FindCharacter(characters);

            Console.WriteLine($"\nTotal attribute points available for this character: {selectedChar.AvailableAttributePoints}");
            Console.WriteLine("Available Skills: ");

            //Display numbered skill list and get valid selection
            int choice;
            while (true)
            {
                for (int i = 0; i < skills.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {skills[i].Name} - {skills[i].Description} - {skills[i].Attribute} - Point Requirement:{skills[i].RequiredAttributePoints}");
                }

                choice = SafeReadInt("Select a skill to assign: ");

                if (choice >= 1 && choice <= skills.Count)
                    break;

                Console.WriteLine("Please enter a number from the list.");
            }

            Skill selectedSkill = skills[choice - 1];

            //Check if character already has the skill
            if (selectedChar.Skills.Contains(selectedSkill))
            {
                Console.WriteLine($"{selectedChar.Name} already has the {selectedSkill.Name} skill.\n");
                return;
            }

            //Check if character has enough attribute points
            if (selectedChar.AvailableAttributePoints < selectedSkill.RequiredAttributePoints)
            {
                Console.WriteLine("Not enough attribute points are available!\n");
                return;
            }

            //Assign skill and deduct cost
            selectedChar.Skills.Add(selectedSkill);
            selectedChar.AvailableAttributePoints -= selectedSkill.RequiredAttributePoints;
            Console.WriteLine($"\nSkill: {selectedSkill.Name} added to {selectedChar.Name}\n");
        }

        //Increases character level
        static void LevelUp(List<Character> characters)
        {
            if (characters.Count == 0)
            {
                Console.WriteLine("No characters have been created yet. Please create one first!\n");
                return;
            }

            Character selectedChar = FindCharacter(characters);

            //Apply level up bonuses
            selectedChar.Level += 1;
            selectedChar.HitPoints += 5;
            selectedChar.AvailableAttributePoints += 10;

            Console.WriteLine($"{selectedChar.Name} is now a Level:{selectedChar.Level} character.\n");
        }

        //Displays all characters and their current stats/skills
        static void DisplayCharSheet(List<Character> characters)
        {
            Console.WriteLine("All Characters in the character sheet.......................");

            if (characters.Count == 0)
            {
                Console.WriteLine("\nNo Characters have been created...\n");
            }
            else
            {
                foreach (Character c in characters)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Name: {c.Name}, Class: {c.Class}, Level: {c.Level}, " +
                        $"HP: {c.HitPoints}, Available Attribute Points: {c.AvailableAttributePoints}");
                    Console.WriteLine("Skills:");
                    if (c.Skills.Count == 0)
                    {
                        Console.WriteLine("There are no skills assigned yet...!");
                    }
                    else
                    {
                        foreach (Skill s in c.Skills)
                        {
                            Console.WriteLine($"{s.Name} - {s.Description} - {s.Attribute} - Points Requirement: {s.RequiredAttributePoints}");
                        }
                    }
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("End.........................................................\n");
        }

        //Displays main menu and returns valid user choice
        static int UseMenu()
        {
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Create a character");
                Console.WriteLine("2. Assign skills");
                Console.WriteLine("3. Level up a character");
                Console.WriteLine("4. Display all character sheets");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 5)
                {
                    return choice;
                }
                Console.WriteLine("Please enter a number between 1 and 5.");
            }
        }

        static void Main(string[] args)
        {
            List<Character> characters = new List<Character>();
            List<Skill> skills = new List<Skill>
            {
                new Skill { Name = "Strike", Description = "A powerful strike.", Attribute = "Strength", RequiredAttributePoints = 10 },
                new Skill { Name = "Dodge", Description = "Avoid an attack.", Attribute = "Dexterity", RequiredAttributePoints = 15 },
                new Skill { Name = "Spellcast", Description = "Cast a spell.", Attribute = "Intelligence", RequiredAttributePoints = 20 }
            };

            Console.WriteLine("=== Character Management ===");
            bool running = true;

            while (running)
            {
                int choice = UseMenu();

                switch (choice)
                {
                    case 1: CreateCharacter(characters); break;
                    case 2: AssignSkill(characters, skills); break;
                    case 3: LevelUp(characters); break;
                    case 4: DisplayCharSheet(characters); break;
                    case 5:
                        Console.WriteLine("Thank you for using Character Management. Goodbye!");
                        running = false;
                        break;
                }
            }
        }
    }
}