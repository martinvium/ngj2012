using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TugOfBaby
{
    class Stats
    {
        private int _pointsCollected;
        private int _deedsDone;
        private int _itemsCollected;

        public Stats()
        {
            _pointsCollected = 1;
            _itemsCollected = 0;
            _deedsDone = 0;
        }

        public void CollectItem(GameObject go, int points)
        {
            _itemsCollected += 1;
            _pointsCollected += points;
            go.Label = new FloatingScoreLabel("+" + points);
        }

        public void CompleteDeed(GameObject go, int points)
        {
            _deedsDone += 1;
            _pointsCollected += points;
            go.Label = new FloatingScoreLabel("+" + points);
        }

        #region Properties
        public int ItemsCollected
        {
            get { return _itemsCollected; }
            set { _itemsCollected = value; }
        }
        
        public int DeedsDone
        {
            get { return _deedsDone; }
            set { _deedsDone = value; }
        }

        public int PointsCollected
        {
            get { return _pointsCollected; }
            set { _pointsCollected = value; }
        }
        #endregion
    }
}
