using System.Windows.Forms;
using Saper.SaperController;
using Saper.Model;
using System;
using Saper.View;
using System.Drawing;

namespace Saper.SaperView
{
    public partial class View : Form
    {
        short margin = 20;
        short offsetX = 30;
        short offsetY = 30;
        short buttonSize = 30;
        Label lblTime;
        Label lblBomb;
        Button btnRestart;
        Button btnRecords;
        FieldButton[,] buttons;
        Controller controller;
        Image mine = Image.FromFile("Images\\mina.jpg");
        Image normalFace = Image.FromFile("Images\\smiling-face.jpg");
        Image winnerFace = Image.FromFile("Images\\smiling-face-with-sunglasses.jpg");
        Image loserFace = Image.FromFile("Images\\astonished-face.jpg");

        public View()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            lblTime = new Label();
            lblBomb = new Label();
            btnRestart = new Button();
            btnRecords = new Button();
            btnRestart.Click += OnRestartClick;
            btnRecords.Click += BtnRecords_Click;

            lblTime.Font = new Font("Arial", 12, FontStyle.Bold);
            lblBomb.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTime.SetBounds(margin, 350, 100, 50);
            lblBomb.SetBounds(margin + 200, 350, 100, 50);
            btnRestart.SetBounds(margin + 125, 335, 50, 50);
            btnRecords.SetBounds(margin + 380, 50, 100, 50);
            btnRestart.Image = new Bitmap(normalFace, new Size(40, 40));
            btnRecords.Text = "Pokaż Rekordy:";

            Controls.Add(lblTime);
            Controls.Add(lblBomb);
            Controls.Add(btnRestart);
            Controls.Add(btnRecords);
        }

        internal void Init(Controller controller)
        {
            this.controller = controller;
        }

        internal void Init(short boardWidth, short boardHeight, GameTimer gameTimer)
        {
            gameTimer.SetLabel(lblTime);
            this.buttons = new FieldButton[boardWidth, boardHeight];
            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                { 
                    buttons[i, j] = new FieldButton(i,j);
                    buttons[i, j].MouseDown += OnFieldClick;
                    buttons[i, j].SetBounds(margin+offsetX*i, margin+offsetY*j, buttonSize, buttonSize);
                    Controls.Add(buttons[i, j]);
                }
            }
        }

        internal void SetFace(FaceType faceType)
        {
            switch (faceType)
            {
                case FaceType.normalFace:
                    btnRestart.Image = new Bitmap(normalFace, new Size(40, 40));
                    break;
                case FaceType.winnerFace:
                    btnRestart.Image = new Bitmap(winnerFace, new Size(40, 40));
                    break;
                case FaceType.loserFace:
                    btnRestart.Image = new Bitmap(loserFace, new Size(40, 40));
                    break;
                default:
                    break;
            }
        }

        private void OnFieldClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                controller.OnFieldRightClick(((FieldButton)sender).I, ((FieldButton)sender).J);
            }
            else
            {
                controller.OnFieldLeftClick(((FieldButton)sender).I, ((FieldButton)sender).J);
            }
        }

        private void OnRestartClick(object sender, EventArgs e)
        {
            controller.ClickRestart();
        }

        private void BtnRecords_Click(object sender, EventArgs e)
        {
            controller.ShowRecords();
        }

        public void Show(Field[,] fieldTable, int quantityBomb)
        {            
            lblBomb.Text = "Bomby: " + quantityBomb.ToString();

            for (int i = 0; i < fieldTable.GetLength(0); i++)
            {
                for (int j = 0; j < fieldTable.GetLength(1); j++)
                {
                    switch (fieldTable[i, j].FType)
                    {
                        case FieldType.Empty:
                            buttons[i, j].BackColor = Color.Gray;
                            break;
                        case FieldType.Hidden:
                            buttons[i, j].BackColor = Color.LightGray;
                            buttons[i, j].Text = String.Empty;
                            buttons[i, j].Image = null;
                            break;
                        case FieldType.Bomb:                        
                            buttons[i, j].BackColor = Color.Black;
                            buttons[i, j].Image = new Bitmap(mine, new Size(22, 22));
                            break;
                        case FieldType.Number:
                            buttons[i, j].ForeColor = GetColorByNumber(fieldTable[i, j].BombsAround);
                            buttons[i, j].Font = new Font("Arial", 11, FontStyle.Bold);
                            buttons[i, j].Text = fieldTable[i, j].BombsAround.ToString();
                            break;
                        case FieldType.Flag:
                            buttons[i, j].Text = "?";
                            buttons[i, j].BackColor = Color.IndianRed;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private Color GetColorByNumber(int bombsAround)
        {
            switch (bombsAround)
            {
                case 1:
                    return Color.Green;
                case 2:
                    return Color.YellowGreen;
                case 3:
                    return Color.Red;
                case 4:
                    return Color.Purple;
                default:
                    return Color.Black;
            }
        }
    }
}

//postrzątać bajzel!
//logika coś nie działa