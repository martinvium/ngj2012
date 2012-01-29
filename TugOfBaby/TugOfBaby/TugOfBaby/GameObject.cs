﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace TugOfBaby
{
    class GameObject
    {
        Sprite _sprite;
        Body _body;
        bool pickupable;
        Reward reward;
        GameObject heldItem;
        Animation _animation;
        GameObject target;
        bool disposed = false;
        FloatingScoreLabel _label;
        Color _labelColor = Color.Black;
        bool enabled = true;

        public Color LabelColor
        {
            get { return _labelColor; }
            set { _labelColor = value; }
        }

        public FloatingScoreLabel Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public bool Disposed
        {
            get { return disposed; }
            set { 
               disposed = value;
               if(disposed)
                   _body.Dispose();
            }
        }



        internal GameObject Target
        {
            get { return target; }
            set { target = value; }
        }
        
        Stats statistics;
        
        internal Stats Statistics
        {
            get { return statistics; }
            set { statistics = value; }
        }


        internal Animation Animation
        {
            get { return _animation; }
            set { _animation = value; }
        }

        internal GameObject HeldItem
        {
            get { return heldItem; }
            set { heldItem = value; }
        }        

        internal Reward Reward
        {
            get { return reward; }
            set { reward = value; }
        }
        
        public bool Pickupable
        {
            get { return pickupable; }
            set { pickupable = value; }
        }

        public Body Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        public Vector2 Position
        {
            get { return _body.Position * Game1.METER_IN_PIXEL; }
        }
    }
}
