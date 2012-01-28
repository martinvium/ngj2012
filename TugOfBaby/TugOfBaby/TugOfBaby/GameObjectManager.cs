using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

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
            return GetPlayer("Child/child_face");
        }

        public GameObject GetDevil()
        {
            return GetPlayer("devil");
        }

        public GameObject GetAngel()
        {
            return GetPlayer("angel");
        }

        private GameObject GetPlayer(string name)
        {
            GameObject player = GetBase();
            player.Sprite = new Sprite(name);
            player.Body = BodyFactory.CreateCircle(_world, .5f, 1f, new Vector2(5, 5), this);
            player.Body.BodyType = BodyType.Dynamic;
            player.Body.Mass = 5;
            player.Body.OnCollision += OnCollision;
            return player;
        }

        private GameObject GetBase()
        {
            GameObject gameObject = new GameObject();
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public List<GameObject> GetAll()
        {
            return _gameObjects;
        }

        private bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            return true;
        }
    }
}
