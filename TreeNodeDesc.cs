using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторная_работа__8
{
    public class TreeNodeDesc: TreeNode
    {
        public GraphicObject obj;

        public TreeNodeDesc(string text)
            : base(text) { }
    }
}
