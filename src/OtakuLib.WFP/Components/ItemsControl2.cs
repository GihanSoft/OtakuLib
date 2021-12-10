namespace OtakuLib.WPF.Components;

public class ItemsControl2 : ItemsControl
{
    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return false;
    }

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
        if (element is null)
        { return; }

        base.PrepareContainerForItemOverride(element, item);
        element.SetCurrentValue(ContentPresenter.ContentTemplateProperty, ItemTemplate);
    }
}