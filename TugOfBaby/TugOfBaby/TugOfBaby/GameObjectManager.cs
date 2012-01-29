﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using System.Security.Cryptography;

namespace TugOfBaby
{
    class GameObjectManager
    {
        GameObject evil, good;

        enum Items { DRUGS, KNIFE, BUNNY, MANBUNNY, BIBLE, VEGETABLES };
        
        List<GameObject> _gameObjects = new List<GameObject>();
        World _world;
        RenderManager _renderManager;
        

        public GameObjectManager(World world, RenderManager renderMan)
        {
            _world = world;
            _renderManager = renderMan;
            
        }

        public GameObject GetBaby()
        {
            GameObject angel = GetBase();
            angel.Sprite = _renderManager.GetSprite(RenderManager.Texture.BABY);
            angel.Body = getCircle(.5f, angel);
            angel.Body.OnCollision += OnItemCollision;
            return angel;
        }

        public GameObject GetDevil()
        {
            GameObject angel = GetBase();
            angel.Sprite = _renderManager.GetSprite(RenderManager.Texture.DEVIL);            
            angel.Body = getCircle(.3f, angel);
            angel.LabelColor = Color.Red;

            evil = angel;
            evil.Statistics = new Stats();

            return angel;
        }

        public GameObject GetAngel()
        {
            GameObject angel = GetBase();
            angel.Sprite = _renderManager.GetSprite(RenderManager.Texture.ANGEL);
            angel.Body = getCircle(.3f, angel);
            angel.LabelColor = Color.SkyBlue;
            good = angel;        
            

            good.Statistics = new Stats();
            return angel;
        }

        public GameObject GetReaper()
        {
            GameObject angel = GetBase();
            angel.Sprite = _renderManager.GetSprite(RenderManager.Texture.REAPER);
            angel.Body = getCircle(.3f, angel);
            return angel;
        }
        public GameObject GetFemaleBunny()
        {
            GameObject bunny = GetBase();
            bunny.Sprite = _renderManager.GetSprite(RenderManager.Texture.BUNNY);
            bunny.Body = getCircle(.3f, bunny);
            return bunny;
        }

 private Body getCircle(float radius, GameObject _gameobject)
        {
            Body body = BodyFactory.CreateCircle(_world, radius, 1f, new Vector2(5, 5), this);
            body.BodyType = BodyType.Dynamic;
            body.Mass = 5;
            body.OnCollision += OnCollision;
            body.BodyType = BodyType.Dynamic;
            body.UserData = _gameobject;
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

        public GameObject GetItem(RenderManager.Texture _type)
        {
            GameObject item = GetBase();
            item.Sprite = _renderManager.GetSprite(_type);

            item.Reward = new Reward();

            switch(_type){
                case RenderManager.Texture.DRUGS:
                    item.Reward.Effect = (int)Items.DRUGS;
                    break;
                case RenderManager.Texture.KNIFE:
                    item.Reward.Effect = (int)Items.KNIFE;
                    break;
                case RenderManager.Texture.BUNNY:
                    item.Reward.Effect = (int)Items.BUNNY;
                    break;
                case RenderManager.Texture.BIBLE:
                    item.Reward.Effect = (int)Items.BIBLE;
                    break;
                case RenderManager.Texture.VEGETABLE:
                    item.Reward.Effect = (int)Items.VEGETABLES;
                    break;
                case RenderManager.Texture.MANBUNNY:
                    item.Reward.Effect = (int)Items.MANBUNNY;
                    break;
            }

            item.Pickupable = true;

            Random rand = new Random();
            float rX = rand.Next(20);
            float rY = rand.Next(10);

            item.Body = BodyFactory.CreateRectangle(_world, 0.5f, 0.5f, 1.0f);
            item.Body.Position = new Vector2(rX, rY);
            item.Body.UserData = item;
            item.Body.BodyType = BodyType.Static;
            item.Body.Mass = 1.0f;
            item.Type = _type;
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

        private bool OnItemCollision(Fixture player, Fixture fixtureB, Contact contact)
        {
            if (fixtureB.Body.UserData is GameObject)
            {
                GameObject goCollider = (fixtureB.Body.UserData as GameObject);
                GameObject goPlayer = (player.Body.UserData as GameObject);

                if (goCollider.Pickupable == true)
                {
                    if (goCollider.Reward.Effect == (int)Items.DRUGS)
                    {
                        evil.Statistics.PointsCollected += 50;
                        HeadsUpDisplay.HOW_EVIL -= 50;
                    }
                    else if (goCollider.Reward.Effect == (int)Items.KNIFE)
                    {
                        //har vi kaninen?
                        if (goPlayer.HeldItem.Target == goCollider)
                        {
                            goPlayer.HeldItem.Disposed = true;
                            goCollider.Disposed = true;
                            evil.Statistics.PointsCollected += 50;
                            HeadsUpDisplay.HOW_EVIL -= 50;
                        }
                        

                    }
                    else if (goCollider.Reward.Effect == (int)Items.BUNNY)
                    {
                        goPlayer.HeldItem = (fixtureB.Body.UserData as GameObject);
                        goPlayer.HeldItem.Target = GetItem(RenderManager.Texture.KNIFE);
                        GetItem(RenderManager.Texture.MANBUNNY);
                        Console.Write("KILLS RABBIT");
                    }
                    else if (goCollider.Reward.Effect == (int)Items.BIBLE)
                    {
                        good.Statistics.PointsCollected += 50;
                        HeadsUpDisplay.HOW_EVIL += 50;
                    }
                    else if (goCollider.Reward.Effect == (int)Items.VEGETABLES)
                    {
                        good.Statistics.PointsCollected += 50;
                        HeadsUpDisplay.HOW_EVIL += 50;
                    }

                    goPlayer.HeldItem = goCollider;
                    Console.Write(evil.Statistics.PointsCollected);
                    
                    //Destroy((fixtureB.Body.UserData as GameObject));
                }
                else if ((fixtureB.Body.UserData as GameObject).Reward != null)
                {
                    (fixtureB.Body.UserData as GameObject).Reward.enable();
                    //Destroy((fixtureB.Body.UserData as GameObject));
                }
            }
            
            return true;
        }

        public void Update()
        {
            List<GameObject> removeList = new List<GameObject>();
            foreach (GameObject go in _gameObjects)
            {
                if (go.Disposed)
                    removeList.Add(go);
            }

            foreach (GameObject go in removeList){
                _gameObjects.Remove(go);
            }            
        }
        public void SpawnItem(EffectManager effectManager)
        {
            GameObject item;
            //evil!
            item = GetItem(RenderManager.Texture.DRUGS);
            item.Body.Position = RandomPlace();
            effectManager.AddSpawnEffect(item.Body.Position * Game1.METER_IN_PIXEL);
            item = GetItem(RenderManager.Texture.GAME);
            item.Body.Position = RandomPlace();
            effectManager.AddSpawnEffect(item.Body.Position * Game1.METER_IN_PIXEL);
            //good
            item = GetItem(RenderManager.Texture.VEGETABLE);
            item.Body.Position = RandomPlace();
            effectManager.AddSpawnEffect(item.Body.Position * Game1.METER_IN_PIXEL);
            item = GetItem(RenderManager.Texture.BIBLE);
            item.Body.Position = RandomPlace();
            effectManager.AddSpawnEffect(item.Body.Position * Game1.METER_IN_PIXEL);
        }

        private Vector2 RandomPlace() 
        {

            Random ran = new Random();
            Vector2 randomPlace = new Vector2((float)GetRandom(0,1280) / Game1.METER_IN_PIXEL, (float)GetRandom(0,720)/Game1.METER_IN_PIXEL);
            return randomPlace;
        }
        private int GetRandom(int min, int max) 
        {
            var _rand = RandomNumberGenerator.Create();
            if (min > max) throw new ArgumentOutOfRangeException("min");

            byte[] bytes = new byte[4];

            _rand.GetBytes(bytes);

            uint next = BitConverter.ToUInt32(bytes, 0);

            int range = max - min;

            return (int)((next % range) + min);
        }
        public void DespawnItems()
        {
            foreach (GameObject item in _gameObjects)
            {
                switch (item.Type)
                {
                    case RenderManager.Texture.DRUGS:
                        item.Disposed = true;
                        break;
                    case RenderManager.Texture.VEGETABLE:
                        item.Disposed = true;
                        break;            
                    case RenderManager.Texture.BIBLE:
                        item.Disposed = true;
                        break;
                    case RenderManager.Texture.GAME:
                        item.Disposed = true;
                        break;
                    default:
                        break;
                }

            }
        }
        public void DespawnItems(List<RenderManager.Texture> list)
        {
            //List<RenderManager.Texture> lists = new List<RenderManager.Texture>();
            //lists.Add(RenderManager.Texture.KNIFE);
            //DespawnItems(lists);
            foreach (GameObject item in _gameObjects)
            {
                foreach (RenderManager.Texture type in list)
                {
                    if (item.Type == type) 
                    {
                        item.Disposed = true;
                    }
                }
            }
        }
        private void Destroy(GameObject _gameobject)
        {
            //_gameObjects.Remove(_gameobject);
        }
    }
}
