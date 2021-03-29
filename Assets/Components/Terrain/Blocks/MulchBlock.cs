﻿using UnityEngine;

namespace Antymology.Terrain
{
    /// <summary>
    /// A block of mulch. Ants can use this resource to replenish their hunger.
    /// </summary>
    public class MulchBlock : AbstractBlock
    {

        #region Fields

        /// <summary>
        /// Statically held tile map coordinate.
        /// </summary>
        private static Vector2 _tileMapCoordinate = new Vector2(0, 1);

        /// <summary>
        /// Statically held is visible.
        /// </summary>
        private static bool _isVisible = true;
        
        /// <summary>
        /// Statically held score.
        /// </summary>
        private static int _score = ConfigurationManager.Instance.MulchHp;
    
        #endregion

        #region Methods

        /// <summary>
        /// The tile at the 0, 1, position in the tilemap.
        /// </summary>
        public override Vector2 tileMapCoordinate()
        {
            return _tileMapCoordinate;
        }

        /// <summary>
        /// mulch is a visible block.
        /// </summary>
        public override bool isVisible()
        {
            return _isVisible;
        }
        
        /// <summary>
        /// mulch has a nice value.
        /// </summary>
        public override int score(){
            return _score;
        }
       
        #endregion

    }
}
