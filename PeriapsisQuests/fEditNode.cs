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
    public partial class fEditNode : Form
    {
        Node myNode;

        public fEditNode(Node node)
        {
            InitializeComponent();

            myNode = node;
        }

        private void fEditNode_Load(object sender, EventArgs e)
        {
            textBox1.Text = myNode.Title;
            textBox2.Text = myNode.Comment;
            //checkBox1.Checked = myNode.ArbitraryFlag;
            
            psPreReq.StatCounterMode = PlayerStatCounter.PlayerStatCounterMode.PreReq;
            psCompletedReq.StatCounterMode = PlayerStatCounter.PlayerStatCounterMode.CompletedReq;
            psReward.StatCounterMode = PlayerStatCounter.PlayerStatCounterMode.Rewards;

            psPreReq.Node = myNode;
            psCompletedReq.Node = myNode;
            psReward.Node = myNode;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            myNode.Comment = textBox2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            myNode.Title = textBox1.Text;
            Text = "Edit - " + myNode.Title;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //myNode.ArbitraryFlag = checkBox1.Checked;
        }
    }
}
