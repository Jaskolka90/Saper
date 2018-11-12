using System;
using System.Windows.Forms;

namespace Saper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SaperView.View view = new SaperView.View();
            Model.Board board = new Model.Board(view);
            SaperController.Controller controller = new SaperController.Controller(view, board);
            controller.Run();
        }
    }
}
