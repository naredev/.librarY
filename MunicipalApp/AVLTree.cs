using System;

namespace MunicipalApp
{
    public class AVLTree<T> where T : class
    {
        private class AVLNode
        {
            public T Value;
            public AVLNode Left;
            public AVLNode Right;
            public int Height;

            public AVLNode(T value)
            {
                Value = value;
                Height = 1;
            }
        }

        private AVLNode root;
        private readonly Func<T, int> keySelector; // Function to extract keys for comparison

        public AVLTree(Func<T, int> keySelector)
        {
            this.keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
        }

        // Insert method with balancing
        public void Insert(T item)
        {
            root = Insert(root, item);
        }

        private AVLNode Insert(AVLNode node, T item)
        {
            if (node == null)
                return new AVLNode(item);

            int itemKey = keySelector(item);
            int nodeKey = keySelector(node.Value);

            if (itemKey < nodeKey)
                node.Left = Insert(node.Left, item);
            else if (itemKey > nodeKey)
                node.Right = Insert(node.Right, item);
            else
                return node; // Duplicate keys are not allowed

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
            return Balance(node);
        }

        // Search by key
        public T Search(int key)
        {
            return Search(root, key);
        }

        private T Search(AVLNode node, int key)
        {
            if (node == null) return null;

            int nodeKey = keySelector(node.Value);

            if (key < nodeKey)
                return Search(node.Left, key);
            else if (key > nodeKey)
                return Search(node.Right, key);
            else
                return node.Value;
        }

        // Helper Methods
        private int Height(AVLNode node)
        {
            return node?.Height ?? 0;
        }

        private int BalanceFactor(AVLNode node)
        {
            return Height(node.Left) - Height(node.Right);
        }

        //Part pf this code was adapted from Stackoverflow
        //https://stackoverflow.com/questions/19481815/avl-tree-for-java
        //Accessed 29 october 2024

        private AVLNode Balance(AVLNode node)
        {
            int balance = BalanceFactor(node);

            // Left heavy
            if (balance > 1)
            {
                if (BalanceFactor(node.Left) < 0)
                    node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Right heavy
            if (balance < -1)
            {
                if (BalanceFactor(node.Right) > 0)
                    node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private AVLNode LeftRotate(AVLNode y)
        {
            AVLNode x = y.Right;
            AVLNode T2 = x.Left;

            x.Left = y;
            y.Right = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private AVLNode RightRotate(AVLNode x)
        {
            AVLNode y = x.Left;
            AVLNode T2 = y.Right;

            y.Right = x;
            x.Left = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }
    }
}

