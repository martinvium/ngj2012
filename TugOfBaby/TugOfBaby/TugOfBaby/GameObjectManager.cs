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
            return GetPlayer("baby");
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

        public GameObject GetItem(string name)
        {
            GameObject item = GetBase();
            item.Sprite = new Sprite(name);
            
            if (name == "knife")
            {
                item.Pickupable = true;
                //do knife stuff
                item.Reward.Effect = 1;
            }

            item.Body = BodyFactory.CreateRectangle(_world, 0.5f, 0.5f, 1.0f);
            item.Body.BodyType = BodyType.Static;
            item.Body.Mass = 0;
            item.Body.OnCollision += OnItemCollision;
            return item;
        }

        public List<GameObject> GetAll()
        {
            return _gameObjects;
        }

        private bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            return true;
        }

        private bool OnItemCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            if(fixtureB.Body.UserData is GameObject)
                if ((fixtureB.Body.UserData as GameObject).Pickupable == true)
                {
                    (fixtureB.Body.UserData as GameObject).Reward.enable();
                    Destroy((fixtureB.Body.UserData as GameObject));
                }
            return true;
        }

        private void Destroy(GameObject _gameobject)
        {
            _gameObjects.Remove(_gameobject);
        }
    }
}
