using System;
using Saper.Model;
using Saper.SaperView;

namespace Saper.SaperController
{
    class Controller
    {
        private SaperView.View view;
        private Board board;
        private GameEventsManager gameEventsManager;

        public Controller(SaperView.View view, Board board, GameEventsManager gameEventsManager)
        {
            this.view = view;
            this.board = board;
            this.gameEventsManager = gameEventsManager;
            view.Init(this);
        }
        
        internal void Run()
        {
            System.Windows.Forms.Application.Run(this.view);
        }

        internal void OnFieldLeftClick(int i, int j)
        {
            board.OnFieldLeftClick(i, j);
        }

        internal void OnFieldRightClick(int i, int j)
        {
            gameEventsManager.OnFieldRightClick(i, j);
        }

        internal void ClickRestart()
        {
            board.OnRestart();
        }

        internal void ShowRecords()
        {
            GameRecordsDisplay grd = new GameRecordsDisplay();
            grd.ShowRecords();
        }
    }
}
