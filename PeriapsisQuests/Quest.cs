using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriapsisQuests
{
    [Serializable]
    public class Quest
    {
        string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                if(TitleChanged != null)
                    TitleChanged(null, EventArgs.Empty);
            }
        }

        public string Issuer = "";
        public string Description = "";

        public string OnStart = "";
        public string OnEnd = "";

        List<Node> Nodes;
        List<Node> TopLevelNodes;

        EventHandler TitleChanged;

        public Quest()
        {
            Nodes = new List<Node>();
            TopLevelNodes = new List<Node>();
        }

        //public override string ToString()
        //{
        //    return Title;
        //}

        public void AddNode(Node n)
        {
            Nodes.Add(n);
            if (n.Parents.Count == 0)
                TopLevelNodes.Add(n);
        }

        public void RemoveNode(Node n)
        {
            Nodes.Remove(n);
            if (TopLevelNodes.Contains(n))
                TopLevelNodes.Remove(n);

            // Fix any gaps in child/parent hierarchy that may have been made.
            //for(int i = 0; i < Nodes.Count; i++)
            //{
            //    if(i > 0 && Nodes[i].Parents.Count == 0)
            //    {
            //        Nodes[i].Parents.Add(Nodes[i - 1]);
            //        Nodes[i - 1].Children.Add(Nodes[i]);
            //    }
            //}
        }

        public List<Node> GetNodes()
        {
            return Nodes;
        }

        public List<Node> GetTopLevelNodes()
        {
            return TopLevelNodes;
        }
    }
}
