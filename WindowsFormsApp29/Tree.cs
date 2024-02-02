using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp29
{
    public class Tree<T> where T : IComparable<T>
    {
        private Tree<T> parent, left, right;
        private T data;
        private List<T> list = new List<T>();

        public Tree(T data, Tree<T> parent)
        {
            this.data = data;
            this.parent = parent;
        }
        public Tree<T> Parent { get { return parent; } }
        public Tree<T> Right { get { return right; } }
        public Tree<T> Left { get { return left; } }
        public T Data { get { return this.data; } }

        public void add(T val)
        {
            if (val.CompareTo(this.data) < 0)
            {
                if (this.left == null)
                {
                    this.left = new Tree<T>(val, this);
                }
                else if (this.left != null)
                    this.left.add(val);
            }
            else
            {
                if (this.right == null)
                {
                    this.right = new Tree<T>(val, this);
                }
                else if (this.right != null)
                    this.right.add(val);
            }
        }

        private Tree<T> Search(Tree<T> tree, T val)
        {
            if (tree == null) return null;
            switch (val.CompareTo(tree.data))
            {
                case 1: return Search(tree.right, val);
                case -1: return Search(tree.left, val);
                case 0: return tree;
                default: return null;
            }
        }

        public Tree<T> search(T val)
        {
            return Search(this, val);
        }

        public bool Remove(T data)
        {
            //Проверяем, существует ли данный узел
            Tree<T> tree = search(data);
            if (tree == null)
            {
                //Если узла не существует, вернем false
                return false;
            }
            Tree<T> curTree;

            //Если удаляем корень
            if (tree == this)
            {
                if (tree.right != null)
                {
                    curTree = tree.right;
                }
                else curTree = tree.left;

                while (curTree.left != null)
                {
                    curTree = curTree.left;
                }
                T temp = curTree.data;
                this.Remove(temp);
                tree.data = temp;

                return true;
            }

            //Удаление листьев
            if (tree.left == null && tree.right == null && tree.parent != null)
            {
                if (tree == tree.parent.left)
                    tree.parent.left = null;
                else
                {
                    tree.parent.right = null;
                }
                return true;
            }

            //Удаление узла, имеющего левое поддерево, но не имеющее правого поддерева
            if (tree.left != null && tree.right == null)
            {
                //Меняем родителя
                tree.left.parent = tree.parent;
                if (tree == tree.parent.left)
                {
                    tree.parent.left = tree.left;
                }
                else if (tree == tree.parent.right)
                {
                    tree.parent.right = tree.left;
                }
                return true;
            }

            //Удаление узла, имеющего правое поддерево, но не имеющее левого поддерева
            if (tree.left == null && tree.right != null)
            {
                //Меняем родителя
                tree.right.parent = tree.parent;
                if (tree == tree.parent.left)
                {
                    tree.parent.left = tree.right;
                }
                else if (tree == tree.parent.right)
                {
                    tree.parent.right = tree.right;
                }
                return true;
            }

            //Удаляем узел, имеющий поддеревья с обеих сторон
            if (tree.right != null && tree.left != null)
            {
                curTree = tree.right;

                while (curTree.left != null)
                {
                    curTree = curTree.left;
                }

                //Если самый левый элемент является первым потомком
                if (curTree.parent == tree)
                {
                    curTree.left = tree.left;
                    tree.left.parent = curTree;
                    curTree.parent = tree.parent;
                    if (tree == tree.parent.left)
                    {
                        tree.parent.left = curTree;
                    }
                    else if (tree == tree.parent.right)
                    {
                        tree.parent.right = curTree;
                    }
                    return true;
                }
                //Если самый левый элемент НЕ является первым потомком
                else
                {
                    if (curTree.right != null)
                    {
                        curTree.right.parent = curTree.parent;
                    }
                    curTree.parent.left = curTree.right;
                    curTree.right = tree.right;
                    curTree.left = tree.left;
                    tree.left.parent = curTree;
                    tree.right.parent = curTree;
                    curTree.parent = tree.parent;
                    if (tree == tree.parent.left)
                    {
                        tree.parent.left = curTree;
                    }
                    else if (tree == tree.parent.right)
                    {
                        tree.parent.right = curTree;
                    }

                    return true;
                }
            }
            return false;
        }

        private void Print(Tree<T> node)
        {
            if (node == null) return;
            Print(node.left);
            list.Add(node.data);
            Console.Write(node + " ");
            if (node.right != null)
                Print(node.right);
        }
        public void print()
        {
            list.Clear();
            Print(this);
        }

        public override string ToString()
        {
            return data.ToString();
        }
        public void Clear()
        {
            parent = null;
            left = null;
            right = null;
            //data = null;
        }
        public int GetDeep()
        {
            return GetDeep(this);
        }
        private int GetDeep(Tree<T> subroot)
        {
            if (subroot == null)
                return 0;
            return 1 + Math.Max(GetDeep(subroot.Left), GetDeep(subroot.Right));
        }

        public int GetLeafs()
        {
            return GetLeafs(this);
        }
        private int GetLeafs(Tree<T> subroot)
        {
            if (subroot == null)
                return 0;
            if (subroot.Left == null && subroot.Right == null)
                return 1;
            return GetLeafs(subroot.Left) + GetLeafs(subroot.Right);
        }
    }
}
