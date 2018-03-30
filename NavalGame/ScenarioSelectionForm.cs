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
    public partial class ScenarioSelectionForm : Form
    {
        class Scenario
        {
            public string Name;
            public string Description;
            public Bitmap Map;
            public override string ToString()
            {
                return Name;
            }
        }

        public ScenarioSelectionForm()
        {
            InitializeComponent();

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "Test Scenario",
                    Description = "This is the description of this test scenario.",
                    Map = Bitmaps.Get("Data\\TestScenario.png")
                });

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "The Channel",
                    Description = "THE CHANNEL.",
                    Map = Bitmaps.Get("Data\\TheChannel.png")
                });

            ScenarioList.SelectedIndex = 0;
        }

        private void ScenarioListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ScenarioList.SelectedItem != null)
            {
                var scenario = (Scenario)ScenarioList.SelectedItem;
                ScenarioDescriptionBox.Text = scenario.Description;
                MapView.Terrain = new Terrain(scenario.Map);
            }
            else
            {
                ScenarioDescriptionBox.Text = "";
                MapView.Terrain = null;
            }

            MapView.Invalidate();

        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            if (ScenarioList.SelectedItem != null)
            {
                var scenario = (Scenario)ScenarioList.SelectedItem;
                Hide();
                new Form1(scenario.Map, this).ShowDialog();
            }
        }
    }
}
