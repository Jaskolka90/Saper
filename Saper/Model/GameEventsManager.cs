using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model
{
    public class GameEventsManager
    {
        Board board;

        public GameEventsManager(Board board)
        {
            this.board = board;
        }

        internal void OnRestart()
        {
            board.CreateFields();
            board.QuantityBomb = board.StartQuantityBombs;
            gameTimer.ResetTimer();
            view.SetFace(View.FaceType.normalFace);
            view.Show(fieldTable, quantityBomb);
        }


        private void OnWin()
        {
            gameTimer.StopTime();
            view.SetFace(View.FaceType.winnerFace);
            View.RecordInputView riv = new View.RecordInputView(gameTimer.GetTime(), ShowPlayAgainDialog);
            riv.Show();
        }

        private void ShowPlayAgainDialog()
        {
            DialogResult dialogResult = MessageBox.Show("Czy chcesz zagrać ponownie?", "Wygrałeś!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                OnRestart();
            else
                Application.Exit();
        }

        private void OnLoose()
        {
            gameTimer.StopTime();
            view.SetFace(View.FaceType.loserFace);
            ShowAllBombs();
            DialogResult dialogResult = MessageBox.Show("Czy chcesz zagrać ponownie?", "Przegrałeś!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                OnRestart();
            else
                Application.Exit();
        }



        internal void OnFieldRightClick(int i, int j)
        {
            if (!gameTimer.IsRunning())
            {
                gameTimer.StartTime();
            }
            board.OnFieldRightClick(i, j);

            //if (fieldTable[i, j].FType == FieldType.Hidden)
            //{
            //    fieldTable[i, j].FType = FieldType.Flag;
            //    QuantityBomb--;
            //    view.Show(fieldTable, QuantityBomb);
            //}
            //else if (fieldTable[i, j].FType == FieldType.Flag)
            //{
            //    fieldTable[i, j].FType = FieldType.Hidden;
            //    QuantityBomb++;
            //    view.Show(fieldTable, QuantityBomb);
            //}

            if (board.CheckIfWon())
            {
                view.Show(fieldTable, QuantityBomb);
                OnWin();
            }

        }
    }
}
