using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CharacterRecords
{
    public partial class mainForm : Form
    {
        List<Character> characterList = new List<Character>();

        public mainForm()
        {
            InitializeComponent();
            loadDB();
        }

        private void addButton_Click(object sender, EventArgs e)
        { 
            string name = nameInput.Text;
            string characterClass = classInput.Text;
            string level = levelInput.Text;
            string health = healthInput.Text;

            Character newCharacter = new Character(name, characterClass, level, health);    
            characterList.Add(newCharacter);

            ClearLabels();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
           
        }

        private void listButton_Click(object sender, EventArgs e)
        {
            outputLabel.Text = "";

            foreach(Character c in characterList)
            {
                outputLabel.Text += $"{c.name} - {c.characterClass} - {c.level} - {c.health}\n";
            }
        }

        private void ClearLabels()
        {
            nameInput.Text = "";
            classInput.Text = "";
            levelInput.Text = "";
            healthInput.Text = "";
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveDB();
        }

        public void loadDB()
        {
            XmlReader reader = XmlReader.Create("Resources/characterData.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    string name = reader.ReadString();

                    reader.ReadToNextSibling("characterClass");
                    string characterClass = reader.ReadString();

                    reader.ReadToNextSibling("level");
                    string level = reader.ReadString();

                    reader.ReadToNextSibling("health");
                    string health = reader.ReadString();

                    Character newCharacter = new Character(name, characterClass, level, health);
                    characterList.Add(newCharacter);
                }
            }

            reader.Close();
        }

        public void saveDB()
        {
            XmlWriter writer = XmlWriter.Create("Resources/characterData.xml", null);

            writer.WriteStartElement("Characters");

            foreach (Character c in characterList)
            {
                writer.WriteStartElement("Character");

                writer.WriteElementString("name", c.name);
                writer.WriteElementString("characterClass", c.characterClass);
                writer.WriteElementString("level", c.level);
                writer.WriteElementString("health", c.health);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.Close();
        }
    }
}
