using System.Windows;
using System.Windows.Controls;

namespace OtakuLib.WPF.Components;

public class ItemsControl2 : ItemsControl
{
    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return false;
    }

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
        base.PrepareContainerForItemOverride(element, item);
        ((ContentPresenter)element).ContentTemplate = ItemTemplate;
    }
}
