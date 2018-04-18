using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavalGame
{
    public partial class VictoryForm : Form
    {

        Form GameForm;

        public VictoryForm(Form gameForm, Faction victor)
        {
            InitializeComponent();

            GameForm = gameForm;

            BackColor = Game.GetFactionColor(victor);

            switch (victor)
            {
                case Faction.England:
                    VictoryPictureBox.Image = Bitmaps.Get("Data\\BritishVictory.jpg");
                    break;
                case Faction.Germany:
                    VictoryPictureBox.Image = Bitmaps.Get("Data\\GermanVictory.jpg");
                    break;
                case Faction.USA:
                    VictoryPictureBox.Image = Bitmaps.Get("Data\\USAVictory.jpg");
                    break;
                case Faction.Japan:
                    VictoryPictureBox.Image = Bitmaps.Get("Data\\JapaneseVictory.jpg");
                    break;
                default:
                    VictoryPictureBox.Image = Bitmaps.Get("Data\\Title.jpg");
                    break;
            }

        }

        private void ContinueButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void EndGameButtonClick(object sender, EventArgs e)
        {
            GameForm.Close();
            Close();
        }
    }
}