using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Saper.Model
{
    public class Board
    {
        SaperView.View view;//do game manage
        short boardWidth = 10;
        short boardHeight = 10;
        Field[,] fieldTable;
        int tableSize;
        public int QuantityBomb { get; set; }
        bool[] bombTable;
        public int StartQuantityBombs { get; set; }
        GameTimer gameTimer;//nie bedzie

        public Board(SaperView.View v)
        {
            this.view = v;
            this.fieldTable = new Field[boardWidth, boardHeight];
            this.tableSize = (boardWidth * boardHeight);
            this.StartQuantityBombs = 5;//Convert.ToInt32(tableSize * 0.2);
            this.QuantityBomb = StartQuantityBombs;
            this.bombTable = new bool[tableSize];
            this.gameTimer = new GameTimer();
            CreateFields();
            view.Init(boardWidth, boardHeight, gameTimer);
            view.Show(fieldTable, QuantityBomb);
        }



        internal void CreateFields()
        {
            int boardSize = tableSize;
            bombTable = RandomBombs(boardSize, QuantityBomb); 

            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    bool b = bombTable[--boardSize];
                    fieldTable[i, j] = new Field(b);//jest bomba?
                }
            }
        }

        private bool[] RandomBombs(int tableSize, int quantityBomb)
        {
            for (int i = 0; i < tableSize; i++)
            {
                bombTable[i] = (i < quantityBomb) ? true : false; 
            }
            Random rnd = new Random();
            bool[] randomTable = bombTable.OrderBy(x => rnd.Next()).ToArray();
            return randomTable;
        }

        internal void OnFieldRightClick(int i, int j)
        {
            //if (!gameTimer.IsRunning())
            //{
            //    gameTimer.StartTime();
            //}

            if (fieldTable[i, j].FType == FieldType.Hidden)
            {
                fieldTable[i, j].FType = FieldType.Flag;
                QuantityBomb--;
                view.Show(fieldTable, QuantityBomb);
            }
            else if(fieldTable[i, j].FType == FieldType.Flag)
            {
                fieldTable[i, j].FType = FieldType.Hidden;
                QuantityBomb++;
                view.Show(fieldTable, QuantityBomb);
            }

            //if (CheckIfWon())
            //{
            //    view.Show(fieldTable, QuantityBomb);
            //    OnWin();
            //}

        }

        internal void OnFieldLeftClick(int i, int j)
        {
            if (!gameTimer.IsRunning())
            {
                gameTimer.StartTime();
            }

            if (fieldTable[i,j].HasBomb && fieldTable[i, j].FType != FieldType.Bomb)
            {
                fieldTable[i, j].FType = FieldType.Bomb;
                view.Show(fieldTable, QuantityBomb);
                OnLoose();                           
                return;
            }

            if (!fieldTable[i, j].HasBomb && fieldTable[i, j].FType == FieldType.Hidden)
            {
                OnHiddenClick(i, j);
            }

            if (CheckIfWon())
            {
                view.Show(fieldTable, QuantityBomb);
                OnWin();
                return;
            }

            view.Show(fieldTable, QuantityBomb);
        }

        private bool CheckIfWon()
        {
            return StartQuantityBombs == CountNotHiddenFields();
        }

        private void OnHiddenClick(int i, int j)
        {
            int bombSum = 0;
            List<Field> fieldsAround = GetFieldsAround(i, j);
            foreach(Field item in fieldsAround)
            {
                if(item.HasBomb)
                {
                    bombSum++;
                }
            }
            fieldTable[i, j].BombsAround = bombSum;
            fieldTable[i, j].FType = (bombSum > 0) ? FieldType.Number : FieldType.Empty;
            if(fieldTable[i, j].FType == FieldType.Empty)
            {
                ShowFieldsAround(i, j);
            }
        }

        private void ShowFieldsAround(int x, int y)
        {
            for(int i = x - 1; i < x + 2; i++)
            {
                for(int j = y - 1; j < y + 2; j++)
                {
                    if(i >= 0 && i < fieldTable.GetLength(0) && j >= 0 && j < fieldTable.GetLength(1))
                    {
                        if(fieldTable[i, j].FType == FieldType.Hidden && (i != x || j != y))
                        {
                            OnHiddenClick(i, j);
                        }
                    }
                }
            }
        }

       

        

        private int CountNotHiddenFields()
        {
            int count = 0;
            for (int i = 0; i < fieldTable.GetLength(0); i++)
            {
                for (int j = 0; j < fieldTable.GetLength(1); j++)
                {
                    if (fieldTable[i, j].FType == FieldType.Hidden)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        

        private void ShowAllBombs()
        {
            for (int i = 0; i < fieldTable.GetLength(0); i++)
            {
                for (int j = 0; j < fieldTable.GetLength(1); j++)
                {
                    if (fieldTable[i, j].HasBomb)
                    {
                        fieldTable[i, j].FType = FieldType.Bomb;
                    }
                }
            }
            view.Show(fieldTable, QuantityBomb);
        }

        private List<Field> GetFieldsAround(int x, int y)
        { 
            List<Field> fields = new List<Field>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x+i >= 0 && x+i < fieldTable.GetLength(0) && y+j >= 0 && y+j < fieldTable.GetLength(1))
                    {
                        if (!(i == 0 && y == 0))
                        {
                            fields.Add(fieldTable[x+i,y+j]);
                        }
                    }
                }
            }
            return fields;
        }
    }
}
