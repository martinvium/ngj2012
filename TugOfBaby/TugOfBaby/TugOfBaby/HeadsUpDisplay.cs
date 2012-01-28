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
        Texture2D _evilOMeter;
        Texture2D _goodOMeter;
        Texture2D _noOMeter;
        Texture2D _god;
        Texture2D _evil;

        int _howEvil;
        GameObject _heldObject;


        public HeadsUpDisplay(ContentManager content)
        {
            _noOMeter = content.Load<Texture2D>("nobar");
            _evilOMeter = content.Load<Texture2D>("badbar");
            _goodOMeter = content.Load<Texture2D>("goodbar");
            _god = content.Load<Texture2D>("godpic");
            _evil = content.Load<Texture2D>("badpic");
            _howEvil = 309/2;
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch batch, GameWindow window)
        {                   
            batch.Draw(_evilOMeter, new Vector2(window.ClientBounds.Width / 2, 20), Color.White);
            batch.Draw(_goodOMeter, new Vector2(window.ClientBounds.Width / 2, 20), new Rectangle(0, 0, 93 + _howEvil, _evilOMeter.Height), Color.White);
          

            if (_heldObject != null)
            {
                batch.Draw(_heldObject.Sprite.Texture, new Vector2((float)window.ClientBounds.Width / 2, (float)window.ClientBounds.Height -50), Color.White);
            }

        }
        public void UpdateEvilOMeter(int value) 
        {
            _howEvil += value;
            _howEvil = (int)MathHelper.Clamp(_howEvil, 0, 309);
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
