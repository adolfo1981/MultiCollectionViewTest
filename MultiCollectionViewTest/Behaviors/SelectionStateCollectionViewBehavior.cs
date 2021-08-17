using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MultiCollectionViewTest.Behaviors
{
    public class SelectionStateCollectionViewBehavior : Behavior<CollectionView>
    {
        private CollectionView AssociatedObject { get; set; }

        protected override void OnAttachedTo(CollectionView bindable)
        {
            AssociatedObject = bindable;
            bindable.ChildAdded += OnChildAdded;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(CollectionView bindable)
        {
            AssociatedObject = null;
            bindable.ChildAdded -= OnChildAdded;

            base.OnDetachingFrom(bindable);
        }

        private void OnChildAdded(object sender, ElementEventArgs e)
        {
            if (AssociatedObject.SelectionMode == SelectionMode.Single)
            {
                var selectedItem = AssociatedObject.SelectedItem;
                if (selectedItem == null)
                    return;

                if (e.Element?.BindingContext == selectedItem)
                {
                    VisualStateManager.GoToState(e.Element as VisualElement, "Selected");
                }
            }
            else if (AssociatedObject.SelectionMode == SelectionMode.Multiple)
            {
                var selectedItems = AssociatedObject.SelectedItems;
                if (selectedItems == null || selectedItems.Count == 0)
                    return;

                if (selectedItems.Contains(e.Element?.BindingContext))
                {
                    VisualStateManager.GoToState(e.Element as VisualElement, "Selected");
                }
            }
        }
    }
}
