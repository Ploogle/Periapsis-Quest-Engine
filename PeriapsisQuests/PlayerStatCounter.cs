using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeriapsisQuests
{
    public enum PlayerStat
    {
        gold,
        iron,
        h3,
        science,
        planet_count,
        maxIronMinLevel,
        maxGasExtractorLevel,
        pirate_disposition
    }

    public partial class PlayerStatCounter : UserControl
    {
        Node myNode;
        public Node Node
        {
            get { return myNode; }
            set
            {
                myNode = value;

                LoadValues();
            }
        }
        public PlayerStatCounterMode StatCounterMode;// = PlayerStatCounterMode.Rewards;

        public enum PlayerStatCounterMode
        {
            PreReq,
            CompletedReq,
            Rewards
        }

        public PlayerStatCounter()
        {
            InitializeComponent();

            int index = 0;
            foreach (PlayerStat ps in Enum.GetValues(typeof(PlayerStat)))
            {
                Label lblPlayerStat = new Label();
                lblPlayerStat.Text = ps.ToString() + ":";
                lblPlayerStat.Top = index * lblPlayerStat.Height;
                lblPlayerStat.Width = 125;
                lblPlayerStat.TextAlign = ContentAlignment.MiddleRight;
                Controls.Add(lblPlayerStat);

                NumericUpDown nudPlayerStat = new NumericUpDown();
                nudPlayerStat.Maximum = int.MaxValue;
                nudPlayerStat.Minimum = int.MinValue;
                nudPlayerStat.Increment = 1;
                nudPlayerStat.Tag = ps;
                nudPlayerStat.Top = index * lblPlayerStat.Height;
                nudPlayerStat.Left = 125;
                nudPlayerStat.Width = 75;
                Controls.Add(nudPlayerStat);

                nudPlayerStat.ValueChanged += NudPlayerStat_ValueChanged;

                index++;
            }
        }

        private void NudPlayerStat_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;

            switch (StatCounterMode)
            {
                case PlayerStatCounterMode.PreReq:
                    myNode.PreReqs[(PlayerStat)nud.Tag] = (float)nud.Value;
                    break;
                case PlayerStatCounterMode.CompletedReq:
                    myNode.CompletedReqs[(PlayerStat)nud.Tag] = (float)nud.Value;
                    break;
                case PlayerStatCounterMode.Rewards:
                    myNode.Rewards[(PlayerStat)nud.Tag] = (float)nud.Value;
                    break;
            }
        }

        private void LoadValues()
        {
            foreach(Control c in Controls)
            {
                if(c is NumericUpDown)
                {
                    NumericUpDown nud = c as NumericUpDown;
                    switch (StatCounterMode)
                    {
                        case PlayerStatCounterMode.PreReq:
                            if(myNode.PreReqs.ContainsKey((PlayerStat)nud.Tag))
                                nud.Value = (decimal)myNode.PreReqs[(PlayerStat)nud.Tag];
                            break;
                        case PlayerStatCounterMode.CompletedReq:
                            if (myNode.CompletedReqs.ContainsKey((PlayerStat)nud.Tag))
                                nud.Value = (decimal)myNode.CompletedReqs[(PlayerStat)nud.Tag];
                            break;
                        case PlayerStatCounterMode.Rewards:
                            if (myNode.Rewards.ContainsKey((PlayerStat)nud.Tag))
                                nud.Value = (decimal)myNode.Rewards[(PlayerStat)nud.Tag];
                            break;
                    }
                }
            }
        }


    }
}
