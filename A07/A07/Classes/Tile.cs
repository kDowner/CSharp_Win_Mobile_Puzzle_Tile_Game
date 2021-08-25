/*
 * FILE				: Tile.cs
 * PROJECT			: A07 (PROG2121)
 * FIRST VERSION	: 2020-12-14 (Rev.07)
 * AUTHOR			: Dusan Sasic & Kevin Downer
 * DESCRIPTION		: Creates the Tile Set colour blocks and puts the numbers on them
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A07
{
   public class Tile
   {
      public string Number
      { get; set; }

      public int TileSize
      { get; set; }

      public string TilePath
      { get; set; }

      // tile selection define
      public Tile(string tile_number)
      {
         // blank spot
         if (tile_number == " ")
         {
            TilePath = " ";
         }
         else
         {
            int tempNum = Int16.Parse(tile_number);
            // blue tile
            if (tempNum % 2 == 0)
               TilePath = "/Assets/Tiles/blueButton300.png";
            // red tile
            else
               TilePath = "/Assets/Tiles/redButton300.png";
         }
         //tilesize define
         TileSize = 100;
         //number the tile
         Number = tile_number;
      }


      public Tile()
      {

      }
   }
}
