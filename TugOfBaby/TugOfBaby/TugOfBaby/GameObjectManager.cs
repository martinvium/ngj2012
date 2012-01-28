using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;

namespace TugOfBaby
{
    class GameObjectManager
    {
        List<GameObject> _gameObjects = new List<GameObject>();
        World _world;

        public GameObjectManager(World world)
        {
            _world = world;
        }

        public GameObject GetBaby()
        {
            GameObject baby = GetBase();
            baby.Sprite = new Sprite("baby");
            baby.Body = BodyFactory.CreateCircle(_world, .5f, 1f);
            return baby;
        }

        private GameObject GetBase()
        {
            return new GameObject();
        }

        public List<GameObject> GetAll()
        {
            return _gameObjects;
        }
    }
}
