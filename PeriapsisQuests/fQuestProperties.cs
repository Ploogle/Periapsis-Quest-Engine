using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeriapsisQuests
{
    public partial class fQuestProperties : Form
    {
        Quest myQuest;
        public bool TitleChanged = false;

        public fQuestProperties(Quest quest)
        {
            InitializeComponent();

            myQuest = quest;
        }

        private void fQuestProperties_Load(object sender, EventArgs e)
        {
            textBox1.Text = myQuest.Title;
            textBox2.Text = myQuest.Description;
            textBox3.Text = myQuest.Issuer;
            TitleChanged = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            myQuest.Title = textBox1.Text;
            Text = "Quest - " + myQuest.Title;
            TitleChanged = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            myQuest.Issuer = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            myQuest.Description = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
