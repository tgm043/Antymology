using UnityEngine;

namespace Antymology.Terrain
{
    /// <summary>
    /// A block of terrain which is poisonous to ants.
    /// </summary>
    public class AcidicBlock : AbstractBlock
    {

        #region Fields

        /// <summary>
        /// Statically held tile map coordinate.
        /// </summary>
        private static Vector2 _tileMapCoordinate = new Vector2(0, 3);

        /// <summary>
        /// Statically held is visible.
        /// </summary>
        private static bool _isVisible = true;

        /// <summary>
        /// Statically held score.
        /// </summary>
        private static int _score = -2;

        #endregion

        #region Methods

        /// <summary>
        /// The tile at the 0, 3, position in the tilemap.
        /// </summary>
        public override Vector2 tileMapCoordinate()
        {
            return _tileMapCoordinate;
        }

        /// <summary>
        /// acid is a visible block.
        /// </summary>
        public override bool isVisible()
        {
            return _isVisible;
        }
        
        /// <summary>
        /// acid is the worst block.
        /// </summary>
        public override int score(){
            return _score;
        }

        #endregion
    }
}
