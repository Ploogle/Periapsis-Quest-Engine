using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PeriapsisQuests
{
    public partial class fQuest : Form
    {
        public Quest quest;

        // Style

        Pen NodeLine;

        bool isDragging;
        Node dragTarget;
        Size dragOffset;

        // Temp, if title changed in fQuestProperties, we need to replace the old file when saving.
        bool TitleChanged = false;
        string OldTitle = "";

        enum ControlButtonType
        {
            AddChild,
            RemoveNode
        }

        Dictionary<Node, SizeF> ControlButtons;

        public fQuest(Quest quest)
        {
            InitializeComponent();

            this.quest = quest;

            ControlButtons = new Dictionary<Node, SizeF>();

            NodeLine = new Pen(Brushes.Black, 2);
        }

        private void fQuest_Load(object sender, EventArgs e)
        {
            Text = quest.Title + " - View";
            OldTitle = quest.Title;
        } 

        private void fQuest_Paint(object sender, PaintEventArgs e)
        {            
            foreach(Node n in quest.GetTopLevelNodes())
            {
                DrawNodeLines(n, e.Graphics);
                n.Draw(e.Graphics);
            }
        }
        
        void DrawNodeLines(Node n, Graphics g)
        {
            n.CalculateSize();
            foreach(Node c in n.Children)
            {
                c.CalculateSize();
                g.DrawLine(NodeLine, n.Center, c.Center);
                DrawNodeLines(c, g);
            }
        }

        private void fQuest_FormClosing(object sender, FormClosingEventArgs e)
        {
            (Tag as fMainForm).QuestForms.Remove(this);
        }

        private void fQuest_Click(object sender, EventArgs e)
        {
            
        }

        private void fQuest_MouseDown(object sender, MouseEventArgs e)
        {
            for(int i = 0; i < quest.GetNodes().Count; i++)
            {
                Node n = quest.GetNodes()[i];

                if(n.Bounds.Contains(e.Location))
                {
                    if (e.Clicks == 1)
                    {
                        dragTarget = n;
                        isDragging = true;

                        dragOffset = (Size)n.Location - (Size)e.Location;
                    }
                    else if(e.Clicks == 2)
                    {
                        fEditNode editNode = new fEditNode(n);
                        editNode.ShowDialog();
                    }
                    break;
                }
                else
                {
                    int nodeCount = quest.GetNodes().Count;
                    n.ProcessClick(e.Location);

                    // Stop infinite loops!
                    if (quest.GetNodes().Count > nodeCount)
                    {
                        break;
                    }
                }
            }
        }

        private void fQuest_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            dragTarget = null;
            dragOffset = Size.Empty;

            CheckForInvalidateRequests();
        }

        private void fQuest_MouseLeave(object sender, EventArgs e)
        {
            isDragging = false;
            dragTarget = null;
            dragOffset = Size.Empty;
        }

        private void fQuest_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                dragTarget.Location = e.Location + dragOffset;

                Invalidate();
            }
        }

        void CheckForInvalidateRequests()
        {
            foreach (Node n in quest.GetNodes())
            {
                if (n.IsRequestingInvalidate)
                {
                    n.IsRequestingInvalidate = false;
                    Invalidate();
                    return;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void fQuest_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Program.Directory))
                Directory.CreateDirectory(Program.Directory);

            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Program.Directory + quest.Title.Replace(" ", "-") + ".qst", FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                formatter.Serialize(stream, quest);
                stream.Close();

                // If the title changed and we saved this quest under a different name before, delete the old file.
                if(TitleChanged)
                {
                    if (File.Exists(Program.Directory + OldTitle.Replace(" ", "-") + ".qst"))
                        File.Delete(Program.Directory + OldTitle.Replace(" ", "-") + ".qst");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving " + quest.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Program.Directory + quest.Title.Replace(" ", "-") + ".qst", FileMode.Open, FileAccess.Read, FileShare.Read);
            quest = (Quest)formatter.Deserialize(stream);
        }

        private void questPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuestProperties questProperties = new fQuestProperties(quest);
            questProperties.ShowDialog();
            if (questProperties.TitleChanged)
                TitleChanged = true;
        }

        private void exportJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder json = new StringBuilder();
            StringBuilder sections = new StringBuilder();

            json.Append(@"{
    title:'#TITLE#',
    description:'#DESCRIPTION#',
    issuer:'#ISSUER#',
    sections: [
        #SECTIONS#
    ]
}");
            string JSON_SECTION = @"    {
        title: '#TITLE#',
        description: '#DESCRIPTION#',
        preReq: { #PREREQ# },
        completedReq: { #COMPLETEDREQ# },
        rewards: { #REWARDS# }
    }";
            json = json.Replace("#TITLE#", quest.Title);
            json = json.Replace("#DESCRIPTION#", quest.Description.Replace("'", @"\'").Replace(@"""", @"\"""));
            json = json.Replace("#ISSUER#", quest.Issuer);

            // Linear quest system. Recursion will be needed if nodes are allowed to branch in the future.
            Node n = quest.GetTopLevelNodes()[0];
            do
            {
                StringBuilder newSection = new StringBuilder(JSON_SECTION);
                newSection = newSection.Replace("#TITLE#", n.Title.Replace("'", @"\'").Replace(@"""", @"\"""));
                newSection = newSection.Replace("#DESCRIPTION#", n.Comment.Replace("'", @"\'").Replace(@"""", @"\"""));

                string preReqs = "";
                if (n.PreReqs.Count > 0)
                {
                    foreach (KeyValuePair<PlayerStat, float> kvp in n.PreReqs)
                    {
                        if (kvp.Value != 0)
                        {
                            preReqs += kvp.Key + ": '" + kvp.Value + "',";
                        }
                    }
                    // Remove trailing comma
                    preReqs = preReqs.Substring(0, preReqs.Length - 1);
                }

                string completedReqs = "";
                if (n.CompletedReqs.Count > 0)
                {
                    foreach (KeyValuePair<PlayerStat, float> kvp in n.CompletedReqs)
                    {
                        if (kvp.Value != 0)
                        {
                            completedReqs += kvp.Key + ": '" + kvp.Value + "',";
                        }
                    }
                    // Remove trailing comma
                    completedReqs = completedReqs.Substring(0, completedReqs.Length - 1);
                }

                string rewards = "";
                if (n.Rewards.Count > 0)
                {
                    foreach (KeyValuePair<PlayerStat, float> kvp in n.Rewards)
                    {
                        if (kvp.Value != 0)
                        {
                            rewards += kvp.Key + ": '" + kvp.Value + "',";
                        }
                    }
                    // Remove trailing comma
                    rewards = rewards.Substring(0, rewards.Length - 1);
                }

                newSection = newSection.Replace("#PREREQ#", preReqs);
                newSection = newSection.Replace("#COMPLETEDREQ#", completedReqs);
                newSection = newSection.Replace("#REWARDS#", rewards);
                sections.Append(newSection);
                if (n.Children.Count > 0)
                {
                    sections.AppendLine(",");
                    n = n.Children[0];
                }
                else
                    n = null;
            }
            while (n != null);

            json = json.Replace("#SECTIONS#", sections.ToString());
            MessageBox.Show(json.ToString());
        }
    }
}
