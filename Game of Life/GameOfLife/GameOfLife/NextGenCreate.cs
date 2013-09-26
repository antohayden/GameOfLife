using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class NextGenCreate 
    {
        
        public Boolean[,] GenCreate(Boolean[,] prevGeneration, int rows, int columns)
        {
            Boolean[,] nextGen = new Boolean[rows, columns];

            //If user has clicked blank board button
            if (Board.blankBoard)
            {
                for (int x = 0; x < rows; x++)
                {
                    for (int y = 0; y < columns; y++)
                    {
                        nextGen[x, y] = false;
                    }
                }
            }

            else
            {

                for (int x = 0; x < rows; x++)
                {
                    for (int y = 0; y < columns; y++)
                    {
                        int totalAlive = 0;

                        //left centre
                        if (x - 1 > 0)
                        {
                            if (prevGeneration[x - 1, y] == true)
                                totalAlive++;
                        }

                        //right centre
                        if (x + 1 < rows)
                        {
                            if (prevGeneration[x + 1, y] == true)
                                totalAlive++;
                        }

                        //left top
                        if (x - 1 >= 0 && y + 1 < columns)
                        {
                            if (prevGeneration[x - 1, y + 1] == true)
                                totalAlive++;
                        }

                        //top centre
                        if (y + 1 < columns)
                        {
                            if (prevGeneration[x, y + 1] == true)
                                totalAlive++;
                        }

                        //top right
                        if (x + 1 < rows && y + 1 < columns)
                        {
                            if (prevGeneration[x + 1, y + 1] == true)
                                totalAlive++;
                        }

                        //bottom left
                        if (x - 1 >= 0 && y - 1 >= 0)
                        {
                            if (prevGeneration[x - 1, y - 1] == true)
                                totalAlive++;
                        }

                        //bottom centre
                        if (y - 1 >= 0)
                        {
                            if (prevGeneration[x, y - 1] == true)
                                totalAlive++;
                        }

                        //bottom right
                        if (x + 1 < rows && y - 1 >= 0)
                        {
                            if (prevGeneration[x + 1, y - 1] == true)
                                totalAlive++;
                        }

                        /*************      RULES       *********************************/

                        /*Any living Cell with fewer than two live neighbours dies
                         * as if caused by under population*/

                        if (prevGeneration[x, y] == true && totalAlive < 2)
                            nextGen[x, y] = false;

                        /*Any living Cell with two or three neighbour's lives on to 
                         * the next generation*/
                        if (prevGeneration[x, y] == true && (totalAlive == 2 || totalAlive == 3))
                            nextGen[x, y] = true;

                        /*Any living cell with more than 3 live neighbours dies, as if by
                         * overcrowding*/
                        if (prevGeneration[x, y] == true && totalAlive > 3)
                            nextGen[x, y] = false;

                        /*Any dead cell with exactly 3 live neighbours becomes a live cell, 
                         * as if by reproduction*/
                        if (prevGeneration[x, y] == false && totalAlive == 3)
                            nextGen[x, y] = true;

                    }

                }//end outer for
            }

            return nextGen;
           
        }
        
    }
    
}

