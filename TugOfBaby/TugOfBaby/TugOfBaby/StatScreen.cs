using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TugOfBaby
{
    class StatScreen
    {
        GraphicsDevice _graphics;
        Vector2 _angelPos = new Vector2(480, 200);
        Vector2 _devilPos = new Vector2(800, 200);

        public StatScreen(GraphicsDevice graphics)
        {
            _graphics = graphics;
        }


        public void Draw(GameObject devil, GameObject angel, SpriteBatch batch)
        {
            _graphics.Clear(Color.AliceBlue);
            devil.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Static;

            devil.Sprite.Animation.Draw(batch, _devilPos, 0f, false);
            angel.Sprite.Animation.Draw(batch, _angelPos, 0f, true);
            
            angel.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Static;
            


        }
    }
}
