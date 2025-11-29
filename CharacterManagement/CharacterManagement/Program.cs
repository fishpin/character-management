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
            /*Each character has a Name, Class, Level, HitPoints, AvailableAttributePoints, and a list of Skills. 
             * The ToString() method is overridden to display the character’s details and their skills. Calculate 
             * the character's starting level (1) and hit points (10 + AvailableAttributePoints/2, use integer division). 
             * Add the character to the list of characters.
             */
        }
        static void AssignSkill(List<Character> characters, List<Skill> skills)
        {
            /*Each skill has a Name, Description, Attribute, and RequiredAttributePoints. The ToString method 
             * is overridden to display the skill’s details. The user should be able to select a character by 
             * name and assign a skill to that character. A character can have multiple skills, but each skill 
             * can only be assigned once to a character. A skill can be assigned if the character's AvailableAttributePoints 
             * meet the RequiredAttributePoints for that skill. Once a skill is assigned, deduct the RequiredAttributePoints 
             * value from the caharacter’s AvailableAttributePoints.
             */
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
        static void UseMenu()
        {

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

            }
        }
    }
}
