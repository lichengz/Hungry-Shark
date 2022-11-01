using UnityEngine;

namespace Shark
{
    public enum Color
    {
        Blue,
        Green,
        Red
    }
    
    public enum Track
    {
        Left,
        Mid,
        Right
    }
    
    public class FishID
    {
        private Color _color;
        private Track _track;

        public FishID(Color color, Track track)
        {
            this._color = color;
            this._track = track;
        }

        public Color GetColor()
        {
            return _color;
        }

        public Track GetTrack()
        {
            return _track;
        }
    }  
}

