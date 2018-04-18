using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

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

            public static XElement Save(Scenario scenario)
            {
                XElement scenarioNode = new XElement("Scenario");

                scenarioNode.SetAttributeValue("Name", scenario.Name);
                scenarioNode.SetAttributeValue("Description", scenario.Description);
                scenarioNode.SetAttributeValue("Bitmap", "blob");

                return scenarioNode;
            }

            public static Scenario Load(XElement scenarioNode)
            {
                Scenario scenario = new Scenario();

                scenario.Name = XmlUtils.GetAttributeValue<string>(scenarioNode, "Name");
                scenario.Description = scenarioNode.Value;
                scenario.Map = Bitmaps.Get(XmlUtils.GetAttributeValue<string>(scenarioNode, "Bitmap"));

                return scenario;
            }
        }

        public ScenarioSelectionForm()
        {
            InitializeComponent();

            foreach (var fileName in Directory.GetFiles("Data\\Scenarios", "*.scenario"))
            {
                XElement element = XElement.Load(fileName);
                ScenarioList.Items.Add(Scenario.Load(element));
            }

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
                new Form1(new Game(scenario.Map, scenario.Name), this).ShowDialog();
            }
        }

        private void LoadButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Naval Game Files (*.nvg)|*.nvg";

            fileDialog.FileOk += FileDialogFileOk;

            fileDialog.ShowDialog();

        }

        private void FileDialogFileOk(object sender, CancelEventArgs e)
        {
            XElement element = XElement.Load(((FileDialog)sender).FileName);
            new Form1(Game.Load(element), this).ShowDialog();
        }

        public static string GetScenarioNameFromFileName(string fileName)
        {
            string name = "";
            bool hasExt = true;

            name = Path.GetFileName(fileName);

            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == '.' && hasExt == true)
                {
                    name = name.Remove(i);
                    hasExt = false;
                }
            }

            return name;
        }
    }
}
