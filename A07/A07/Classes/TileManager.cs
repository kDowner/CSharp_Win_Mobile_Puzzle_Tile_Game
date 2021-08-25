/*
 * FILE				: TileManager.cs
 * PROJECT			: A07 (PROG2121)
 * FIRST VERSION	: 2020-12-14 (Rev.07)
 * AUTHOR			: Dusan Sasic & Kevin Downer
 * DESCRIPTION		: Builds the Tile Set colour blocks to prep for the game
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A07
{
   class TileManager
   {
      //Constant
      private const int NUM_OF_TILES = 16;

      //builds the tile set
      public static List<Tile> GetTiles()
      {
         var tiles = new List<Tile>();
         int number;
         for (int i = 0; i < NUM_OF_TILES; i++)
         {
            number = i + 1;
            if (number != 16)
            {
               if (number % 2 == 0)
                  tiles.Add(new Tile(number.ToString()));
               else
                  tiles.Add(new Tile(number.ToString()));
            }
            else
               tiles.Add(new Tile(" "));
         }

         // tiles for game setup
         return tiles;
      }
   }
}
