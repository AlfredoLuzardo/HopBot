using UnityEngine;

namespace HopBotNamespace
{
    /// <summary>
    /// Author: Alfredo Luzardo A01379913
    /// Represents a map in a level.
    /// version 1.1
    /// </summary>
    public class Map
    {
        const double noTilePercEasy         = 0.2;
        const double safeTilePercEasy       = 0.8;
        const double breakTilePercEasy      = 0.9;
        const int tileDurabilityEasy        = 6;
        const double noTilePercMedium       = 0.2;
        const double safeTilePercMedium     = 0.6;
        const double breakTilePercMedium    = 0.8;
        const int tileDurabilityMedium      = 4;
        const double noTilePercHard         = 0.2;
        const double safeTilePercHard       = 0.5;
        const double breakTilePercHard      = 0.75;
        const int tileDurabilityHard        = 2;

        private Tile[,] map;
        private int mapRowNum;
        private int mapColNum;
        private SafeTileFactory safeTileFactory;
        private DangerousTileFactory dangerousTileFactory;
        private BreakableTileFactory breakableTileFactory;

        /// <summary>
        /// Constructor for the map.
        /// </summary>
        public Map(int row, int col)
        {
            map = new Tile[row, col];
            this.mapRowNum = row;
            this.mapColNum = col;
            safeTileFactory = new SafeTileFactory();
            dangerousTileFactory = new DangerousTileFactory();
            breakableTileFactory = new BreakableTileFactory();
        }

        /// <summary>
        /// Indexer for map
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile this[int x, int y] => map[x, y];

        /// <summary>
        /// Getter for the number of rows
        /// </summary>
        /// <returns></returns>
        public int GetNumRows() => mapRowNum; 

        /// <summary>
        /// Getter for the number of columns
        /// </summary>
        /// <returns></returns>
        public int GetNumCols() => mapColNum;

        /// <summary>
        /// Getter for the map
        /// </summary>
        /// <returns></returns>
        public Tile[,] GetMap() => map;

        /// <summary>
        /// Fills the rest of the tiles after a path has been generated.
        /// </summary>
        /// <param name="difficulty">
        /// The difficulty level which determines tile generation probabilities:
        /// "easy", "medium", or "hard".
        /// </param>
        /// </exception>
        private void FillTiles(string difficulty)
        {
            switch(difficulty)
            {
                case "easy":
                    GenerateTiles(noTilePercEasy, safeTilePercEasy, breakTilePercEasy, tileDurabilityEasy);
                    break;

                case "medium":
                    GenerateTiles(noTilePercMedium, safeTilePercMedium, breakTilePercMedium, tileDurabilityMedium);
                    break;

                case "hard":
                    GenerateTiles(noTilePercHard, safeTilePercHard, breakTilePercHard, tileDurabilityHard);
                    break;
                    
                default:
                    Debug.Log("Invalid difficulty level. Choose easy, medium or hard.");
                    break;
            }
        }

        /// <summary>
        /// Generates tiles for the map based on given probability thresholds and tile durability.
        /// </summary>
        /// <param name="noTilePerc"></param>
        /// <param name="safeTilePerc"></param>
        /// <param name="breakTilePerc"></param>
        /// <param name="tileDurability"></param>
        public void GenerateTiles(double noTilePerc, 
                                double safeTilePerc, 
                                double breakTilePerc,
                                int    tileDurability)
        {
            int rows;
            int cols;

            rows = mapRowNum;
            cols = mapColNum;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double p = Random.Range(0f, 1.0f);

                    if (!(map[i, j] is SafeTile) || (!((SafeTile)map[i, j]).GetIsStart() && !((SafeTile)map[i, j]).GetIsEnd()))
                    {
                        if (p < noTilePerc)
                            map[i, j] = null;
                        else if (p < safeTilePerc)
                        {
                            SafeTile safeTile = (SafeTile)CreateTile(safeTileFactory);
                            safeTile.Initialize(false, false, i, j);
                            map[i, j] = safeTile;
                        }
                        else if (p < breakTilePerc)
                        {
                            BreakableTile breakableTile = (BreakableTile)CreateTile(breakableTileFactory);
                            breakableTile.Initialize(tileDurability, i, j);
                            map[i, j] = breakableTile;
                        }
                        else
                        {
                            DangerousTile dangerousTile = (DangerousTile)CreateTile(dangerousTileFactory);
                            dangerousTile.Initialize(i, j);
                            map[i, j] = dangerousTile;  
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates a safe path of tiles from a randomly selected start tile to a randomly selected end tile.
        /// </summary>
        private void GeneratePath()
        {
            SafeTile startTile;
            SafeTile endTile;
            int rows;
            int cols;
            int y;
            int x;
            int startX;
            int startY;
            int endX;
            int endY;

            rows = mapRowNum;
            cols = mapColNum;

            startX = Random.Range(0, rows);
            startY = Random.Range(0, cols);

            do
            {
                endX = Random.Range(0, rows);
                endY = Random.Range(0, cols);
            }
            while (endX == startX && endY == startY);

            startTile = (SafeTile)CreateTile(safeTileFactory);
            endTile   = (SafeTile)CreateTile(safeTileFactory);

            startTile.Initialize(true, false, startX, startY);
            endTile.Initialize(false, true, endX, endY);
            
            map[startX, startY] = startTile;
            map[endX, endY]     = endTile;

            x = startX;
            y = startY;

            while (x != endX || y != endY)
            {
                bool moveRight;

                moveRight = Random.Range(0, 2) == 0;

                if (moveRight)
                {
                    y += System.Math.Sign(endY - y);
                }
                else
                {
                    x += System.Math.Sign(endX - x);
                }

                if ((x != startX || y != startY) && (x != endX || y != endY))
                {
                    SafeTile safeTile;
                    
                    safeTile = (SafeTile)CreateTile(safeTileFactory);
                    safeTile.Initialize(false, false, x, y);
                    map[x, y] = safeTile;
                }
            }
        }

        /// <summary>
        /// Creates a Tile
        /// </summary>
        /// <param name="factory">The factory</param>
        private Tile CreateTile(TileFactory factory)
        {
            return factory.CreateTile();
        }

        /// <summary>
        /// Calls the two map generation functions.
        /// </summary>
        /// <param name="difficulty">
        /// The difficulty level of the map, which influences the probabilities used in tile generation.
        /// </param>
        public void GenerateMap(string difficulty)
        {
            GeneratePath();
            FillTiles(difficulty);
        }
    }
}
