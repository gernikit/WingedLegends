using System;

namespace Heroes
{
    [Serializable]
    public enum HeroType
    {
        Hawk,
        Seagull,
        Owl,
        Raven
    }
    
    [Serializable]
    public class Hero
    {
        private HeroType _type;
        private int _points;
        
        public Hero(HeroType type, int points)
        {
            _type = type;
            _points = points;
        }
        
        public HeroType Type => _type;
        public int Points => _points;

        public void AddPoints(int points)
        {
            _points += points;
        }
    }
}
