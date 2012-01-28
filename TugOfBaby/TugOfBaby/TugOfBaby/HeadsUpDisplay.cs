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
        Texture2D _god;
        Texture2D _evil;

        int _howEvil;
        GameObject _heldObject;


        public HeadsUpDisplay(ContentManager content)
        {
            _evilOMeter = content.Load<Texture2D>("EvilOMeter");
            _god = content.Load<Texture2D>("godpic");
            _evil = content.Load<Texture2D>("badpic");
            _howEvil = 50;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch batch, GameWindow window)
        {
            //Draw images near evilOMeter
            batch.Draw(_evil, new Vector2(window.ClientBounds.Width / 2 - _evilOMeter.Width / 2 - 35,0), Color.White);
            batch.Draw(_god, new Vector2(window.ClientBounds.Width / 2 + _evilOMeter.Width / 2 , 5), Color.White);   

            //Draw the negative space for the health bar
            batch.Draw(_evilOMeter, new Rectangle(window.ClientBounds.Width / 2 - _evilOMeter.Width / 2, 0, _evilOMeter.Width, 44), new Rectangle(0, 45, _evilOMeter.Width, 44), Color.Blue);

            //Draw the current health level based on the current Health
            batch.Draw(_evilOMeter, new Rectangle(window.ClientBounds.Width / 2 - _evilOMeter.Width / 2, 0, (int)(_evilOMeter.Width * ((double)_howEvil / 100)), 44), new Rectangle(0, 45, _evilOMeter.Width, 44), Color.Red);

            //Draw the box around the health bar
            batch.Draw(_evilOMeter, new Rectangle(window.ClientBounds.Width / 2 - _evilOMeter.Width / 2, 0, _evilOMeter.Width, 44), new Rectangle(0, 0, _evilOMeter.Width, 44), Color.White);

            if (_heldObject != null)
            {
                batch.Draw(_heldObject.Sprite.Texture, new Vector2((float)window.ClientBounds.Width / 2, (float)window.ClientBounds.Height -50), Color.White);
            }

        }
        public void UpdateEvilOMeter(int value) 
        {
            _howEvil += value;
            _howEvil = (int)MathHelper.Clamp(_howEvil, 0, 100);
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
