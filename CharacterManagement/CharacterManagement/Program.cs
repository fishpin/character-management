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
            return $"Character: {Name} | Class: {Class} | Level: {Level}\nHP: {HitPoints} | Attribute Points: {AvailableAttributePoints}\nSkills: {Skills}";
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
            return $"{Name} | Attribute: {Attribute} | Cost: {RequiredAttributePoints} points\nDescription: {Description}";
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

        }
        static void LevelUp(List<Character> characters)
        {
            /*The user should be able to select a character by name and level up the character. When a character levels up, 
             * their level increases by 1, their hit points increase by 5, and they gain 10 additional attribute points.
             */
        }
        static void DisplayCharSheet(List<Character> characters)
        {
            //The user should be able to view the details of all created characters.
        }
        static int UseMenu()
        {
            Console.WriteLine("=== Character Management ===");
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
