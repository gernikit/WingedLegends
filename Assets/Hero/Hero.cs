namespace Heroes
{
    public enum HeroName
    {
        Hawk,
        Seagull,
        Owl,
        Raven
    }
    
    public class Hero
    {
        private HeroName _name;
        private int _points;
        
        public Hero(HeroName name, int points)
        {
            _name = name;
            _points = points;
        }
    }
}
