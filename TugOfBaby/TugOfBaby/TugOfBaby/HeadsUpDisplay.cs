using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace TugOfBaby
{
    class HeadsUpDisplay
    {
        public static int HOW_EVIL = 309/2;
        Texture2D _evilOMeter;
        Texture2D _goodOMeter;
        Texture2D _noOMeter;
        Texture2D _god;
        Texture2D _evil;

   
        GameObject _heldObject;


        public HeadsUpDisplay(ContentManager content)
        {
            _noOMeter = content.Load<Texture2D>("nobar");
            _evilOMeter = content.Load<Texture2D>("badbar");
            _goodOMeter = content.Load<Texture2D>("goodbar");
            _god = content.Load<Texture2D>("godpic");
            _evil = content.Load<Texture2D>("badpic");
         
        }

        public void Update(GameObject _devil, GameObject _angel)
        {
            //todo->devil.points + angel.points /2 -- 309 * taht
            int percent = (_devil.Statistics.PointsCollected + _angel.Statistics.PointsCollected / 2) * 309;
            Console.WriteLine("" + percent);
            
        }

        public void Draw(SpriteBatch batch, GameWindow window)
        {                   
            batch.Draw(_evilOMeter, new Vector2(360, 20), Color.White);
            batch.Draw(_goodOMeter, new Vector2(360, 20), new Rectangle(0, 0, 93 + HOW_EVIL, _evilOMeter.Height), Color.White);

            if (_heldObject != null)
            {
                if (_heldObject.Disposed)
                {
                    _heldObject = null;
                }
                else if (_heldObject.Sprite.Animation != null)
                {
                    _heldObject.Sprite.Animation.Draw(batch, new Vector2((float)window.ClientBounds.Width / 2, (float)window.ClientBounds.Height - 50), 0f, false);
                }
                else
                {
                    batch.Draw(_heldObject.Sprite.Texture, new Vector2((float)window.ClientBounds.Width / 2, (float)window.ClientBounds.Height - 50), Color.White);
                }
            }

        }
     
        public void PopItem() 
        {
            _heldObject = null;
        }

        public void PushItem(GameObject gameObject) // to
        {
            _heldObject = gameObject;
        }
        
    }
}
