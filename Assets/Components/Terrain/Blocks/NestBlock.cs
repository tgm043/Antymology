using UnityEngine;

namespace Antymology.Terrain
{
    /// <summary>
    /// A block of nest.
    /// </summary>
    public class NestBlock : AbstractBlock
    {

        #region Fields

        /// <summary>
        /// Statically held tile map coordinate.
        /// </summary>
        private static Vector2 _tileMapCoordinate = new Vector2(1, 3);

        /// <summary>
        /// Statically held is visible.
        /// </summary>
        private static bool _isVisible = true;


        /// <summary>
        /// Statically held score.
        /// </summary>
        private static int _score = 255;


        #endregion

        #region Methods

        /// <summary>
        /// The tile at the 1, 3, position in the tilemap.
        /// </summary>
        public override Vector2 tileMapCoordinate()
        {
            return _tileMapCoordinate;
        }

        /// <summary>
        /// nest is a visible block.
        /// </summary>
        public override bool isVisible()
        {
            return _isVisible;
        }

    
        /// <summary>
        /// nest: the win condition.
        /// </summary>
        public override int score(){
            return _score;
        }

        #endregion

    }
}
