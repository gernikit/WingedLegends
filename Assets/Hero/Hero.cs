namespace Heroes
{
    public enum HeroType
    {
        Hawk,
        Seagull,
        Owl,
        Raven
    }
    
    public class Hero
    {
        private HeroType _type;
        private int _points;
        
        public Hero(HeroType type, int points)
        {
            _type = type;
            _points = points;
        }
    }
}
