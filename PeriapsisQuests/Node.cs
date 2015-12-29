using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PeriapsisQuests
{


    [Serializable]
    public class Node
    {
        private int id;
        public string Title = "Step 1";
        public string Description
        {
            get { return GenerateDescription(); }
        }
        public string Comment = "";

        public Dictionary<PlayerStat, float> PreReqs;
        public Dictionary<PlayerStat, float> CompletedReqs;
        public Dictionary<PlayerStat, float> Rewards;

        public List<Node> Parents;
        public List<Node> Children;

        public Point Location;

        Font TitleFont;
        Font DescriptionFont;
        Font CommentFont;

        public Rectangle Bounds;

        public Point Center;

        RectangleF titleRect;
        RectangleF descriptionRect;
        RectangleF commentRect;

        Quest parentQuest;

        public bool IsRequestingInvalidate = false;

        public Node(Quest parent)
        {
            Parents = new List<Node>();
            Children = new List<Node>();

            TitleFont = new Font("Calibri", 12, FontStyle.Bold);
            CommentFont = new Font("Calibri", 12, FontStyle.Regular);
            DescriptionFont = new Font("Calibri", 8, FontStyle.Italic);

            PreReqs = new Dictionary<PlayerStat, float>();
            CompletedReqs = new Dictionary<PlayerStat, float>();
            Rewards = new Dictionary<PlayerStat, float>();

            parentQuest = parent;
            parentQuest.AddNode(this);

            Location = new Point(100, 100);

            CalculateSize();
        }

        public string GenerateDescription()
        {
            string d = "";

            string preReqDescription = "";
            foreach(KeyValuePair<PlayerStat, float> kvp in PreReqs)
            {
                if (kvp.Value != 0)
                    preReqDescription += kvp.Key.ToString() + ": " + kvp.Value + ", ";
            }
            if(preReqDescription.Length > 0)
                preReqDescription = "Prerequisites: " + preReqDescription + Environment.NewLine;
            d += preReqDescription;

            string completedReqDescription = "";
            foreach (KeyValuePair<PlayerStat, float> kvp in CompletedReqs)
            {
                if (kvp.Value != 0)
                    completedReqDescription += kvp.Key.ToString() + ": " + kvp.Value + ", ";
            }
            if (completedReqDescription.Length > 0)
                completedReqDescription = "Victory reqs: " + completedReqDescription + Environment.NewLine;
            d += completedReqDescription;

            string rewardDescription = "";
            foreach (KeyValuePair<PlayerStat, float> kvp in Rewards)
            {
                if (kvp.Value != 0)
                    rewardDescription += kvp.Key.ToString() + ": " + kvp.Value + ", ";
            }
            if (rewardDescription.Length > 0)
                rewardDescription = "Rewards: " + rewardDescription + Environment.NewLine;
            d += rewardDescription;

            return d;
        }

        public void MakeChildOf(Node parent)
        {
            if (!parent.Children.Contains(this))
            {
                parent.Children.Add(this);
                Parents.Add(parent);
            }
        }

        public void MakeParentOf(Node child)
        {
            if (!child.Parents.Contains(this))
            {
                child.Parents.Add(this);
                Children.Add(child);
            }
        }


        public void Draw(Graphics g)
        {
            CalculateSize();

            // Draw Node
            g.FillRectangle(Brushes.White, Bounds);
            g.DrawRectangle(Pens.Black, Bounds);

            g.DrawString(Title, TitleFont, Brushes.Black, titleRect);
            g.DrawString(Comment, CommentFont, Brushes.Black, commentRect);// n.Location.X + 10, n.Location.Y + titleSize.Height + 5);
            g.DrawString(Description, DescriptionFont, Brushes.Black, descriptionRect);// n.Location.X + 10, n.Location.Y + titleSize.Height + descriptionSize.Height + 10);

            foreach (Node c in Children)
                c.Draw(g);

            if (Children.Count == 0)
            {
                // Draw + button
                g.FillRectangle(Brushes.White, new Rectangle(Bounds.Right, Bounds.Y, 15, 15));
                g.DrawRectangle(Pens.Black, new Rectangle(Bounds.Right, Bounds.Y, 15, 15));
                g.DrawString("+", SystemFonts.DefaultFont, Brushes.Black, new Point(Bounds.Right + 2, Bounds.Y + 2));

                if (Parents.Count > 0)
                {
                    // Draw - button
                    g.FillRectangle(Brushes.White, new Rectangle(Bounds.Right, Bounds.Y + 15, 15, 15));
                    g.DrawRectangle(Pens.Black, new Rectangle(Bounds.Right, Bounds.Y + 15, 15, 15));
                    g.DrawString("-", SystemFonts.DefaultFont, Brushes.Black, new Point(Bounds.Right + 2, Bounds.Y + 17));
                }
            }
        }

        public void ProcessClick(Point clickedAt)
        {
            if (Children.Count == 0)
            {
                // + button clicked
                if (clickedAt.X > Bounds.Right && clickedAt.X <= Bounds.Right + 15 &&
                    clickedAt.Y > Bounds.Y && clickedAt.Y <= Bounds.Y + 15)
                {
                    // Add new node
                    Node newNode = new Node(parentQuest);
                    this.MakeParentOf(newNode);
                    newNode.Location = Location;
                    newNode.Location.Offset(new Point(0, (int)((float)Bounds.Height * 1.5f)));

                    IsRequestingInvalidate = true; // Redraw the form
                }
                else if (Parents.Count > 0 && clickedAt.X > Bounds.Right && clickedAt.X <= Bounds.Right + 15 &&
                    clickedAt.Y > Bounds.Y + 15 && clickedAt.Y <= Bounds.Y + 30)
                {
                    // Remove node
                    parentQuest.RemoveNode(this);
                    foreach (Node p in Parents)
                        p.Children.Remove(this);

                    IsRequestingInvalidate = true; // Redraw the form
                }
            }
        }

        public void CalculateSize()
        {
            Image tmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(tmp);

            SizeF titleSize = g.MeasureString(Title, TitleFont, 200);
            SizeF descriptionSize = g.MeasureString(Description, DescriptionFont, 200);
            SizeF commentSize = g.MeasureString(Comment, CommentFont, 200);

            Bounds = new Rectangle(Location,
                new Size(200,
                    (int)titleSize.Height +
                    (int)descriptionSize.Height +
                    (int)commentSize.Height + 10));

            titleRect = new RectangleF(Location, titleSize);
            commentRect = new RectangleF(Location + new Size(0, (int)titleRect.Height + 5), commentSize);
            descriptionRect = new RectangleF(Location + new Size(0, (int)titleRect.Height + (int)commentRect.Height + 10), descriptionSize);

            Center = new Point(Location.X + (Bounds.Width / 2), Location.Y + (Bounds.Height / 2));
        }
    }
}
