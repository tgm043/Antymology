using UnityEngine;

namespace Antymology.Terrain
{
    /// <summary>
    /// A solid stone block.
    /// </summary>
    public class StoneBlock : AbstractBlock
    {

        #region Fields

        /// <summary>
        /// Statically held tile map coordinate.
        /// </summary>
        private static Vector2 _tileMapCoordinate = new Vector2(3, 1);

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
        /// The tile at the 3, 1, position in the tilemap.
        /// </summary>
        public override Vector2 tileMapCoordinate()
        {
            return _tileMapCoordinate;
        }

        /// <summary>
        /// stone is a visible block.
        /// </summary>
        public override bool isVisible()
        {
            return _isVisible;
        }

        /// <summary>
        /// grass and stone are ok.
        /// </summary>
        public override int score(){
            return _score;
        }

        #endregion

    }
}
