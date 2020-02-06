using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day6 : AbstractDay
    {
        private TreeNode _root;
        private List<TreeNode> _nodes = new List<TreeNode>();
        private int _orbitCount;
        
        public Day6() : base(6)
        {
            // root node is always COM
            _root = new TreeNode("COM");
            BuildOrbitMap(_root);
        }

        public override string Part1()
        {            
            CountChilds(_root);      

            return _orbitCount.ToString();
        }

        public override string Part2()
        {
            var you = _nodes.FirstOrDefault(n => n.Name == "YOU");
            var san = _nodes.FirstOrDefault(n => n.Name == "SAN");

            List<TreeNode> youParents = GetAllParents(you);
            List<TreeNode> sanParents = GetAllParents(san);

            // find common parent
            var common = youParents.Intersect(sanParents).First();

            var youPath = youParents.IndexOf(common);
            var sanPath = sanParents.IndexOf(common);

            return (youPath + sanPath).ToString();
        }

        private void BuildOrbitMap(TreeNode root)
        {
            _nodes.Add(root);
            var orbits = Data.Where(d => d.Split(')')[0] == root.Name).Select(d => d.Split(')')[1]);

            foreach(var orbit in orbits)
            {
                var childNode = new TreeNode(orbit, root);
                root.AddChild(childNode);
                BuildOrbitMap(childNode);
            }
        }

        private int CountChilds(TreeNode node)
        {            
            var childCount = 0;
            childCount += node.Childs.Count;
            node.Childs.ForEach(c => childCount += CountChilds(c));

            _orbitCount += childCount;
            return childCount;
        }

        private List<TreeNode> GetAllParents(TreeNode child)
        {
            var result = new List<TreeNode>();

            if(child.Parent != null)
            {
                result.Add(child.Parent);
                result.AddRange(GetAllParents(child.Parent));
            }

            return result;
        }

        private class TreeNode
        {
            private string _name;
            private TreeNode _parent;
            private List<TreeNode> _childs;

            public TreeNode(string name, TreeNode parent = null)
            {
                _name = name;
                _parent = parent;
                _childs = new List<TreeNode>();
            }

            public void AddChild(TreeNode child)
            {
                _childs.Add(child);
            }

            public string Name { get { return _name; } }
            public TreeNode Parent { get { return _parent; } }
            public List<TreeNode> Childs { get { return _childs; } }

            public override string ToString()
            {
                return $"{Name}";
            }
        }
    }
}