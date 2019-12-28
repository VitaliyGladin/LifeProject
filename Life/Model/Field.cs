using System;

namespace Life.Model
{
    public class Field
    {
        static Field instance;
        bool[][] Cells;
        static int maxNumRow;
        static int maxNumCol;
        public static bool hasAlive = false;

        ///<summary>Current value of field</summary>
        public static Field Instance
        {
            get
            {
                if (instance == null) Init();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        Field(bool[,] initValues)
        {
            int rows = initValues.GetLength(0);
            int cols = initValues.GetLength(1);
            Cells = new bool[rows][];
            for (int row = 0; row < rows; row++)
            {
                Cells[row] = new bool[cols];
                for (int col = 0; col < cols; col++)
                {
                    Cells[row][col] = initValues[row, col];
                    hasAlive = hasAlive || Cells[row][col];
                }
            }
            maxNumRow = rows - 1;
            maxNumCol = cols - 1;
        }

        ///<summary>Create new field, if current instance is null</summary>
        public static void Init(int length = 3)
        {
            if (instance != null) return;
            if (length < 1) throw new ArgumentException("Length must be more 0");
            Random random = new Random();
            bool[,] initValues = new bool[length, length];
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                {
                    initValues[i, j] = random.Next(10) > 5;
                }
            instance = new Field(initValues);
        }

        ///<summary>Evaluate new values for each cell</summary>
        public static void Iterate()
        {
            instance.UpdateField();
        }

        ///<summary>Itarate this field several times</summary>
        public static void Iterate(int cycles)
        {
            for (int i = 0; i < cycles; i++)
                Iterate();
        }
        void UpdateField()
        {
            hasAlive=false;
            bool[][] newValues = new bool[maxNumRow + 1][];
            for (int row = 0; row <= maxNumRow; row++)
            {
                newValues[row] = UpdateRow(row);
            }
            Cells = newValues;
        }

        bool[] UpdateRow(int rowNum)
        {
            bool isNotTop = (rowNum > 0);
            bool isNotBottom = (rowNum < maxNumRow);

            bool[] newRow = new bool[maxNumCol + 1];
            Array.Copy(Cells[rowNum], newRow, Cells[rowNum].Length);
            for (int colNum = 0; colNum <= maxNumCol; colNum++)
            {
                bool isNotLeft = (colNum > 0);
                bool isNotRight = (colNum < maxNumCol);

                int neighbors = 0;
                neighbors += (isNotTop && isNotLeft && Cells[rowNum - 1][colNum - 1]) ? 1 : 0;
                neighbors += (isNotTop && isNotRight && Cells[rowNum - 1][colNum + 1]) ? 1 : 0;
                neighbors += (isNotTop && Cells[rowNum - 1][colNum]) ? 1 : 0;
                neighbors += (isNotLeft && Cells[rowNum][colNum - 1]) ? 1 : 0;
                neighbors += (isNotRight && Cells[rowNum][colNum + 1]) ? 1 : 0;
                neighbors += (isNotBottom && isNotLeft && Cells[rowNum + 1][colNum - 1]) ? 1 : 0;
                neighbors += (isNotBottom && isNotRight && Cells[rowNum + 1][colNum + 1]) ? 1 : 0;
                neighbors += (isNotBottom && Cells[rowNum + 1][colNum]) ? 1 : 0;
                if (newRow[colNum])
                {
                    newRow[colNum] = (neighbors == 2 || neighbors == 3);
                }
                else
                {
                    newRow[colNum] = (neighbors == 3);
                }
                hasAlive = hasAlive || newRow[colNum];
            }
            return newRow;
        }

        public override string ToString()
        {
            string result = string.Empty;
            for (int row = 0; row <= maxNumRow; row++)
            {
                for (int col = 0; col <= maxNumCol; col++)
                {
                    result += $"[{(Cells[row][col] ? "+" : "-")}]";
                }
                result += "\n";
            }
            return result;
        }
    }
}