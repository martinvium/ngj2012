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
using System.Security.Cryptography;

namespace TugOfBaby
{
    class GameObjectManager
    {
        GameObject evil, good;

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
                    item.Reward.EvilPoints = 20;
                    break;
                case RenderManager.Texture.KNIFE:
                    item.Reward.EvilPoints = 50;
                    item.Reward.Type = Reward.RewardType.BUNNY;
                    break;
                case RenderManager.Texture.BUNNY_GIRL:
                    item.Reward.GoodPoints = 50;
                    item.Reward.Type = Reward.RewardType.BUNNY;
                    break;
                case RenderManager.Texture.BUNNY:
                    item.Reward.Type = Reward.RewardType.BUNNY;
                    break;
                case RenderManager.Texture.BIBLE:
                    item.Reward.GoodPoints = 20;
                    break;
                case RenderManager.Texture.VEGETABLE:
                    item.Reward.GoodPoints = 10;
                    break;
                case RenderManager.Texture.GAME:
                    item.Reward.EvilPoints = 10;
                    break;
            }

            item.Pickupable = true;

            item.Body = BodyFactory.CreateRectangle(_world, 1f, 1f, 1.0f);
            item.Body.Position = GetRandomVector2();
            item.Body.UserData = item;
            item.Body.BodyType = BodyType.Static;
            item.Body.Mass = 1.0f;
            item.Type = _type;
            return item;
        }

        private Vector2 GetRandomVector2()
        {
            return new Vector2(GetRandom(0, Game1.WIDTH) / Game1.METER_IN_PIXEL, GetRandom(0, Game1.HEIGHT) / Game1.METER_IN_PIXEL);
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
            if (fixtureB.Body.UserData is GameObject == false)
            {
                return true;
            }

            GameObject goCollider = (fixtureB.Body.UserData as GameObject);
            GameObject goPlayer = (player.Body.UserData as GameObject);

            if (goCollider.Reward == null)
            {
                return true;
            }

            if (goCollider.Reward.Type == Reward.RewardType.COLLECT)
            {
                goCollider.Reward.Apply(goCollider, good, evil);
            }
            else if (goCollider.Reward.Type == Reward.RewardType.BUNNY)
            {
                // already have an item, and collided is a valid destination
                if (goPlayer.HeldItem != null)
                        DespawnItems();
                {
                    if (goPlayer.HeldItem.InteractionTargetOptions.Contains(goCollider))
                    {
                        DespawnItems(goPlayer.HeldItem.InteractionTargetOptions);
                        goPlayer.HeldItem.Disposed = true;
                        goCollider.Reward.Apply(goCollider, good, evil);
                    }
                }
                else // we dont have an item
                {
                    goPlayer.HeldItem = goCollider;
                    goPlayer.HeldItem.InteractionTargetOptions.Add(GetItem(RenderManager.Texture.KNIFE));
                    goPlayer.HeldItem.InteractionTargetOptions.Add(GetItem(RenderManager.Texture.BUNNY_GIRL));
                    goCollider.Enabled = false;
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

        public void DespawnItems(List<GameObject> gameObjects)
        {
            foreach(GameObject gameObject in gameObjects) {
                gameObject.Disposed = true;
            }
        }
    }
}
