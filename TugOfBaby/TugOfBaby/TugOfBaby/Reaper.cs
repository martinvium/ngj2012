using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TugOfBaby
{
    class Reaper
    {

        bool grimReaper = false;

        public Vector2 babyDir = new Vector2();

        public float threeSecondTimer;
        public float tenSecondTimer;

        bool almostDeadlock = false;
        GameObject _reaper;
        GameObject _baby;
        EffectManager _effectManager;

        public Reaper(GameObject reaper, EffectManager _effectManager, GameObject baby)
        {
            this._reaper = reaper;
            this._baby = baby;
            this._effectManager = _effectManager;

            _reaper.Body.Position = new Vector2(100, 100);
        }

        public void reaperUpdate(GameTime _gameTime)
        {
            if (almostDeadlock)
                GamePad.SetVibration(PlayerIndex.One, 0.15f, 0.15f);
            else if (!grimReaper)
                GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);

            //Console.WriteLine(_reaper.Position);
            if (!grimReaper)
            {
                _reaper.Enabled = false;
                _reaper.Position = new Vector2(0.15f, 0.09f);
            }

            if (grimReaper)
            {

                if (!_reaper.Enabled)
                    _effectManager.AddDeathSpawnEffect(_reaper.Body.Position * Game1.METER_IN_PIXEL);
                _reaper.Enabled = true;

                reaperMove();
                tenSecondTimer += (float)_gameTime.ElapsedGameTime.TotalSeconds;
            }

            threeSecondTimer += (float)_gameTime.ElapsedGameTime.TotalSeconds;


            if (tenSecondTimer > 10)
            {
                if (grimReaper)
                    _effectManager.AddDeathDespawnEffect(_reaper.Body.Position * Game1.METER_IN_PIXEL);
                grimReaper = false;
                tenSecondTimer = 0;
            }

            if (threeSecondTimer > 2 && !grimReaper)
                deadLock();
        }

        public void reaperMove()
        {
            babyDir = new Vector2();

            babyDir = (_baby.Position - _reaper.Position);

            _reaper.Body.ApplyLinearImpulse(babyDir * 0.005f);

            //Console.WriteLine(Vector2.Distance(_baby.Position, _reaper.Position));

            if (Vector2.Distance(_baby.Position, _reaper.Position) < 300)
                GamePad.SetVibration(PlayerIndex.One, 0.25f, 0.25f);

            if (Vector2.Distance(_baby.Position, _reaper.Position) < 200)
                GamePad.SetVibration(PlayerIndex.One, 0.5f, 0.5f);

            if (Vector2.Distance(_baby.Position, _reaper.Position) < 100)
                GamePad.SetVibration(PlayerIndex.One, 1, 1);

        }

        public void deadLock()
        {


            Console.WriteLine(_baby.Body.LinearVelocity.X);

            if ((_baby.Body.LinearVelocity.X > -0.5f && _baby.Body.LinearVelocity.X < 0.5f) || (_baby.Body.LinearVelocity.Y > -0.5f && _baby.Body.LinearVelocity.Y < 0.5f))
            {
                //if (_baby.Body.LinearVelocity.X < 1.0f || _baby.Body.LinearVelocity.Y < 1.0f)
                if (almostDeadlock)
                {
                    Console.WriteLine("O HAI REAPER");
                    almostDeadlock = false;
                    grimReaper = true;
                }
                else
                {
                    Console.WriteLine("Doorbell");
                    almostDeadlock = true;
                }
                /*else
                    almostDeadlock = false;
                    
        else
            {
            almostDeadlock = false;
                 * */
            }
            else
                almostDeadlock = false;

            threeSecondTimer = 0;

        }

    }
}
