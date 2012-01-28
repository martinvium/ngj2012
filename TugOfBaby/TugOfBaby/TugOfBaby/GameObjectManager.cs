using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TugOfBaby
{
    class GameObjectManager
    {
        List<GameObject> _gameObjects = new List<GameObject>();

        public GameObject GetBaby()
        {
            GameObject baby = GetBase();
            baby.Sprite = new Sprite("baby");
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
