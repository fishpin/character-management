using System;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace CharacterManagement
{
    class Character
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int HitPoints { get; set; }
        public int AvailableAttributePoints { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();

        public override string ToString()
        {
            return $"Character: {Name}, Class: {Class}, Level: {Level}, HP: {HitPoints}, Attribute Points: {AvailableAttributePoints}, Skills: {Skills}";
        }

    }
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
        static void CreateCharacter(List<Character> characters)
        {
            //Take user input for new character info
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter class: ");
            string charClass = Console.ReadLine();
            Console.Write("Enter total attribute points: ");
            int ap = int.Parse(Console.ReadLine());

            Character c = new Character 
            { 
                Name = name, 
                Class = charClass,
                Level = 1,
                //hit points (10 + AvailableAttributePoints/2, use integer division)
                HitPoints = 10 + ap / 2, 
                AvailableAttributePoints = ap, 
                Skills = new List<Skill>() 
            };
            //Adding the character to the list of characters.
            characters.Add(c);

        }
        static void AssignSkill(List<Character> characters, List<Skill> skills)
        {
            //Select a character by name
            Character selectedChar = null;

            while (selectedChar == null)
            {
                Console.Write("Enter character name: ");
                string charName = Console.ReadLine();

                foreach (Character c in characters)
                {
                    if (c.Name.ToLower() == charName.ToLower())
                    {
                        selectedChar = c;
                        break;
                    }
                }

                if (selectedChar == null)
                {
                    Console.WriteLine("Character not found, pleases try again.");
                }
            }
            Console.WriteLine("");
            Console.WriteLine($"Total attribute points available for this character: {selectedChar.AvailableAttributePoints}");
            Console.WriteLine("Available Skills: ");

            //Select skill to assign from the list
            int choice = 0;
            while (choice < 1 || choice > skills.Count)
            {
                int i = 1;
                foreach (Skill s in skills)
                {
                    Console.WriteLine($"{i}. {s.Name} - {s.Description} - {s.Attribute} - Point Requirement:{s.RequiredAttributePoints}");
                    i++;
                }
                Console.Write("Select a skill to assign: ");
                choice = int.Parse(Console.ReadLine());

                if (choice < 1 || choice > skills.Count)
                {
                    Console.WriteLine("Please enter a number from the list.");
                }
            }

            //Assign skill to character if points available and not already owned
            Skill selectedSkill = skills[choice - 1];
            if (selectedChar.AvailableAttributePoints < selectedSkill.RequiredAttributePoints)
            {
                Console.WriteLine("Not enough attribute points are available!");
                return;
            }
            if (selectedChar.Skills.Contains(selectedSkill))
            {
                Console.WriteLine($"{selectedChar.Name} already has the {selectedSkill.Name} skill.");
                return;
            }
            else
            {
                selectedChar.AvailableAttributePoints -= selectedSkill.RequiredAttributePoints;
                selectedChar.Skills.Add(selectedSkill);
            }

        }
        static void LevelUp(List<Character> characters)
        {
            //Select character by name
            Character selectedChar = null;
            while (selectedChar == null)
            {
                Console.Write("Enter character name: ");
                string charName = Console.ReadLine();

                foreach (Character c in characters)
                {
                    if (c.Name.ToLower() == charName.ToLower())
                    {
                        selectedChar = c;
                        break;
                    }
                }

                if (selectedChar == null)
                {
                    Console.WriteLine("Character not found, pleases try again.");
                }
            }
            Console.WriteLine($"{selectedChar.Name} is now a Level:{selectedChar.Level + 1} character.");

            //On level up: level increases by 1, hit points increase by 5, gain 10 attribute points
            selectedChar.Level += 1;
            selectedChar.HitPoints += 5;
            selectedChar.AvailableAttributePoints += 10;

        }
        static void DisplayCharSheet(List<Character> characters)
        {
            //Display all created characters
            Console.WriteLine("All Characters in the character sheet.......................");

            foreach (Character c in characters)
            {
                Console.WriteLine("");
                Console.WriteLine($"Name: {c.Name}, Class: {c.Class}, Level: {c.Level}, " +
                    $"HP: {c.HitPoints}, Available Attribute Points: {c.AvailableAttributePoints}");
                Console.WriteLine("Skills:");

                if (c.Skills == null || c.Skills.Count == 0)
                {
                    Console.WriteLine("There are no skills assigned yet...!");
                }
                else
                {
                    foreach (Skill s in c.Skills)
                    {
                        Console.WriteLine($"{s.Name} - {s.Description} - {s.Attribute} - " +
                            $"Points Requirement: {s.RequiredAttributePoints}");
                    }
                }
                    Console.WriteLine("");
            }
            if (characters.Count == 0)
            {
                Console.WriteLine("\nNo Characters have been created...\n");
            }

            Console.WriteLine("End.........................................................");


        }
        static int UseMenu()
        {
            //Display numbered menu and take user input
            Console.WriteLine("");
            Console.WriteLine("1. Create a character");
            Console.WriteLine("2. Assign skills");
            Console.WriteLine("3. Level up a character");
            Console.WriteLine("4. Display all character sheets");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            return choice;
        }

        static void Main(string[] args)
        {
            List<Character> characters = new List<Character>();
            List<Skill> skills = new List<Skill>

            {
                new Skill { Name = "Strike", Description = "A powerful strike.", Attribute = "Strength", RequiredAttributePoints=10 },
                new Skill { Name = "Dodge", Description = "Avoid an attack.", Attribute = "Dexterity", RequiredAttributePoints=15 },
                new Skill { Name = "Spellcast", Description = "Cast a spell.", Attribute = "Intelligence", RequiredAttributePoints=20 }
            };

            Console.WriteLine("=== Character Management ===");

            //running = false when Exit is selected to end game
            bool running = true;
            while (running)
            {
                int choice = UseMenu();
                switch (choice)
                {
                    case 1:
                        CreateCharacter(characters);
                        break;
                    case 2:
                        AssignSkill(characters, skills);
                        break;
                    case 3:
                        LevelUp(characters);
                        break;
                    case 4:
                        DisplayCharSheet(characters);
                        break;
                    case 5:
                        running = false;
                        break;
                }
            }
        }
    }
}
