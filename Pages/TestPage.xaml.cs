namespace BluetoothApp.Pages;

public partial class TestPage : TabbedPage
{
    private int documentCounter = 3;
    public TestPage()
	{
		InitializeComponent();

        var addButton = new ToolbarItem
        {
            Text = "Add Tab",
            Command = new Command(AddNewTab)
        };

        ToolbarItems.Add(addButton);

    }

    private void AddNewTab()
    {
        var newTab = new ContentPage
        {
            Title = $"Document {documentCounter++}",
            Content = new StackLayout
            {
                Children =
                    {
                        new Label
                        {
                            Text = $"This is Document {documentCounter - 1}",
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        }
                    }
            }
        };

        Children.Add(newTab);
    }
}