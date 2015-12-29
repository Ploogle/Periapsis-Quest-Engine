using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PeriapsisQuests
{
    public partial class fMainForm : Form
    {
        BindingList<Quest> Quests;

        public List<fQuest> QuestForms;

        public fMainForm()
        {
            InitializeComponent();
        }

        private void fMainForm_Load(object sender, EventArgs e)
        {
            Quests = new BindingList<Quest>();
            QuestForms = new List<fQuest>();

            lbQuests.DisplayMember = "Title";
            lbQuests.DataSource = Quests;

            {
                string[] files = Directory.GetFiles(Program.Directory);
                foreach(string file in files)
                {
                    if (file.ToLower().EndsWith(".qst"))
                    {
                        // Deserialize
                        IFormatter formatter = new BinaryFormatter();
                        Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                        Quest quest = (Quest)formatter.Deserialize(stream);
                        Quests.Add(quest);
                    }
                }

            }

            //Quest defaultQuest = new Quest();
            //defaultQuest.Title = "Default Quest";
            //Quests.Add(defaultQuest);

            //Node n1 = new Node(defaultQuest);
            //n1.Title = "Step 1";
            //n1.Comment = "Comment.";
            //n1.Location = new Point(30, 30);

            //Node n2 = new Node(defaultQuest);
            //n2.Title = "Step 2";
            //n2.Comment = "Comment.";
            //n2.Location = new Point(335, 50);

            //n1.MakeParentOf(n2);

            //defaultQuest.AddNode(n1);
            //defaultQuest.AddNode(n2);
            

            //foreach (Quest q in Quests)
            //    lbQuests.Items.Add(q);
        }

        private void lbQuests_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbQuests_DoubleClick(object sender, EventArgs e)
        {
            if (lbQuests.SelectedIndex == -1) return;

            Quest selectedQuest = lbQuests.Items[lbQuests.SelectedIndex] as Quest;

            foreach(fQuest fq in QuestForms)
            {
                if(fq.quest == selectedQuest)
                {
                    fq.BringToFront();
                    return;
                }
            }

            fQuest newQuestform = new fQuest(selectedQuest);
            QuestForms.Add(newQuestform);
            newQuestform.Show();
            newQuestform.Tag = this;
        }

        private void lbQuests_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Quest newQuest = new Quest();
            Node defaultNode = new Node(newQuest);
            newQuest.Title = "New Quest";

            Quests.Add(newQuest);
            //lbQuests.Items.Add(newQuest);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
