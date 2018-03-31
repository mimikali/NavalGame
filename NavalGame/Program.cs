using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Drawing;

namespace NavalGame
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
            UnitType.InitializeUnitTypes();
            Application.Run(new ScenarioSelectionForm());
        }

    }

    static class XmlUtils
    {
        static public T GetAttributeValue<T>(XElement node, string name)
        {
            XAttribute attribute = node.Attribute(name);

            if (attribute != null)
            {
                if (typeof(T) == typeof(int))
                {
                    return (T)(object)int.Parse(attribute.Value);
                }
                else if (typeof(T) == typeof(float))
                {
                    return (T)(object)float.Parse(attribute.Value);
                }
                else if (typeof(T) == typeof(string))
                {
                    return (T)(object)attribute.Value;
                }
                else if (typeof(T) == typeof(Point))
                {
                    string[] tokens = attribute.Value.Split(',');
                    if (tokens.Length == 2)
                    {
                        int x = int.Parse(tokens[0]);
                        int y = int.Parse(tokens[1]);

                        return (T)(object)new Point(x, y);
                    }
                    else
                    {
                        throw new Exception("Attribute malformed.");
                    }
                }
                else if (typeof(T) == typeof(Faction))
                {
                    return (T)Enum.Parse(typeof(Faction), attribute.Value);
                }
                else if (typeof(T) == typeof(bool))
                {
                    if (attribute.Value.ToLower() == "true") return (T)(object)true;
                    if (attribute.Value.ToLower() == "false") return (T)(object)false;
                    throw new Exception("Invalid bool.");
                }
                else
                    throw new Exception("Unsupported type.");
            }
            throw new Exception("Missing attribute.");
        }
    }
}
