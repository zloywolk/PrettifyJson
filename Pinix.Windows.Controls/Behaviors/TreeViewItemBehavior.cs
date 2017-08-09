using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pinix.Windows.Controls.Behaviors
{
    class TreeViewItemBehavior
    {
        private TreeViewItem _item;

        public static bool GetIsRoot(DependencyObject d) => (bool) d.GetValue(IsRootProperty);
        public static void SetIsRoot(DependencyObject d, bool value) => d.SetValue(IsRootProperty, value);

        // Using a DependencyProperty as the backing store for IsRoot.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRootProperty =
            DependencyProperty.RegisterAttached("IsRoot", typeof (bool), typeof (TreeViewItemBehavior),
                new FrameworkPropertyMetadata(default(bool)));

        public static bool GetCheckIsRoot(DependencyObject d) => (bool) d.GetValue(CheckIsRootProperty);
        public static void SetCheckIsRoot(DependencyObject d, bool value) => d.SetValue(CheckIsRootProperty, value);

        // Using a DependencyProperty as the backing store for CheckIsRoot.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckIsRootProperty =
            DependencyProperty.RegisterAttached("CheckIsRoot", typeof (bool), typeof (TreeViewItemBehavior),
                new FrameworkPropertyMetadata(default(bool), CheckIsRootChanged));

        private static readonly DependencyPropertyKey ItemBehaviorPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("ItemBehavior", typeof (TreeViewItemBehavior),
                typeof (TreeViewItemBehavior), new FrameworkPropertyMetadata(null));

        public static DependencyProperty ItemBehaviorProperty = ItemBehaviorPropertyKey.DependencyProperty;

        public static TreeViewItemBehavior GetTreeViewItemBehavior(DependencyObject d)
            => (TreeViewItemBehavior) d.GetValue(ItemBehaviorProperty);

        private static void CheckIsRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TreeViewItem item = d as TreeViewItem;
            if (item != null)
            {
                if ((bool)e.NewValue)
                {
                    if (item.GetValue(ItemBehaviorProperty) == null)
                    {
                        TreeViewItemBehavior extender = new TreeViewItemBehavior(item);
                        item.SetValue(ItemBehaviorPropertyKey, extender);
                    }
                }
                else
                {
                    if (item.ReadLocalValue(ItemBehaviorProperty) != DependencyProperty.UnsetValue)
                    {
                        TreeViewItemBehavior extender = (TreeViewItemBehavior)item.GetValue(ItemBehaviorProperty);
                        extender.Detach();
                        item.SetValue(ItemBehaviorPropertyKey, null);
                    }
                }
            }
        }

        public TreeViewItemBehavior(TreeViewItem item)
        {
            _item = item;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(_item);
            var isRoot = ic is TreeView && ic.ItemContainerGenerator.IndexFromContainer(_item) == 0;
            _item.SetValue(IsRootProperty, isRoot);
        }

        private void Detach()
        {
            _item = null;
        }
    }
}
