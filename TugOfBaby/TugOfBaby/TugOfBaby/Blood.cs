using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace TugOfBaby
{
    class Blood
    {
        string type;
        Vector2 position;
        float rotation;

        public bool isAnimation = false;
        public bool removeOnTime = true;

        public float spawnTime = 0.0f;
        float liveTime = 3.0f;

        public Blood(string _type, Vector2 _position, float _rotation)
        {
            type = _type;
            position = _position;
            rotation = _rotation;            
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
    }
}
