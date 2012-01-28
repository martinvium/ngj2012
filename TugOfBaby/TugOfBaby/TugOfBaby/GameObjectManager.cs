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
        enum Items { DRUGS, KNIFE, BUNNY, BIBLE, VEGETABLES };
        
        List<GameObject> _gameObjects = new List<GameObject>();
        World _world;

        public GameObjectManager(World world)
        {
            _world = world;
        }

        public GameObject GetBaby()
        {
            GameObject angel = GetBase();
            angel.Sprite = new Sprite("Child/child_face", new Vector2(-55, -55));
            angel.Body = getCircle(.5f);
            return angel;
        }

        public GameObject GetDevil()
        {
            GameObject angel = GetBase();
            angel.Sprite = new Sprite("devil", new Vector2(-18, -18));
            angel.Body = getCircle(.3f);
            return angel;
        }

        public GameObject GetAngel()
        {
            GameObject angel = GetBase();
            angel.Sprite = new Sprite("angel", new Vector2(-18, -18));
            angel.Body = getCircle(.3f);
            return angel;
        }

        private Body getCircle(float radius)
        {
            Body body = BodyFactory.CreateCircle(_world, radius, 1f, new Vector2(5, 5), this);
            body.BodyType = BodyType.Dynamic;
            body.Mass = 5;
            body.OnCollision += OnCollision;
            body.BodyType = BodyType.Dynamic;
            body.Mass = 5;
            body.LinearDamping = 3.5f;
            body.OnCollision += OnCollision;
            return body;
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

            switch(name){
                case "drugs":
                    item.Reward.Effect = (int)Items.DRUGS;
                    break;
                case "knife":
                    item.Reward.Effect = (int)Items.KNIFE;
                    break;
                case "bunny":
                    item.Reward.Effect = (int)Items.BUNNY;
                    break;
                case "bible":
                    item.Reward.Effect = (int)Items.BIBLE;
                    break;
                case "vegetables":
                    item.Reward.Effect = (int)Items.VEGETABLES;
                    break;
            }

            item.Pickupable = true;

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
            if (fixtureB.Body.UserData is GameObject)
            {
                if ((fixtureB.Body.UserData as GameObject).Pickupable == true)
                {
                    if ((fixtureB.Body.UserData as GameObject).Reward.Effect == (int)Items.DRUGS)
                        GetItem("bunny");
                    else if ((fixtureB.Body.UserData as GameObject).Reward.Effect == (int)Items.KNIFE)
                        GetItem("bunny");
                    else if ((fixtureB.Body.UserData as GameObject).Reward.Effect == (int)Items.BUNNY)
                        GetItem("bunny");
                    else if ((fixtureB.Body.UserData as GameObject).Reward.Effect == (int)Items.BIBLE)
                        GetItem("bunny");
                    else if ((fixtureB.Body.UserData as GameObject).Reward.Effect == (int)Items.VEGETABLES)
                        GetItem("bunny");

                    (fixtureA.Body.UserData as GameObject).HeldItem = (fixtureB.Body.UserData as GameObject);
                    Destroy((fixtureB.Body.UserData as GameObject));
                }
                else
                {
                    (fixtureB.Body.UserData as GameObject).Reward.enable();
                    Destroy((fixtureB.Body.UserData as GameObject));
                }
            }
            
            return true;
        }

        private void Destroy(GameObject _gameobject)
        {
            _gameObjects.Remove(_gameobject);
        }
    }
}
