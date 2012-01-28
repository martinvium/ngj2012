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
        List<GameObject> list = new List<GameObject>();
        Texture2D evilOMeter;
        int _howEvil;
        GameObject _heldObject;


        public HeadsUpDisplay(ContentManager content)
        {
            evilOMeter = content.Load<Texture2D>("EvilOMeter");
            _howEvil = 50;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch batch, GameWindow window)
        {
            //Draw the negative space for the health bar
            batch.Draw(evilOMeter, new Rectangle(window.ClientBounds.Width / 2 - evilOMeter.Width / 2, 30, evilOMeter.Width, 44), new Rectangle(0, 45, evilOMeter.Width, 44), Color.Blue);

            //Draw the current health level based on the current Health
            batch.Draw(evilOMeter, new Rectangle(window.ClientBounds.Width / 2 - evilOMeter.Width / 2, 30, (int)(evilOMeter.Width * ((double)_howEvil / 100)), 44), new Rectangle(0, 45, evilOMeter.Width, 44), Color.Red);

            //Draw the box around the health bar
            batch.Draw(evilOMeter, new Rectangle(window.ClientBounds.Width / 2 - evilOMeter.Width / 2, 30, evilOMeter.Width, 44), new Rectangle(0, 0, evilOMeter.Width, 44), Color.White);

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
