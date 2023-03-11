using Microsoft.Graph;
using TW.M365.GraphHack.Lib.Services;
using TW.M365.GraphHack.ViewModels;

namespace TW.M365.GraphHack.Views;

public partial class PersonView : ContentView
{
    public static readonly BindableProperty PeopleServiceProperty = BindableProperty.Create(nameof(PeopleService), typeof(IPeopleService), typeof(PersonView), propertyChanged: OnServiceChanged);
    static void OnServiceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((PersonView)bindable).LoadUser();
    }
    public IPeopleService PeopleService
    {
        get => (IPeopleService)GetValue(PersonView.PeopleServiceProperty);
        set => SetValue(PersonView.PeopleServiceProperty, value);
    }
    public string UserId
    {
        get => (string)GetValue(PersonView.UserIdProperty);
        set => SetValue(PersonView.UserIdProperty, value);
    }
    public static readonly BindableProperty UserIdProperty = BindableProperty.Create(nameof(UserId), typeof(string), typeof(PersonView), propertyChanged: OnUserChanged);
    static void OnUserChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((PersonView)bindable).LoadUser();
    }
    public User User { get; set; }
    public PersonView()
    {
        InitializeComponent();
    }
    public async Task LoadUser()
    {
        if (PeopleService != null)
        {
            User = await PeopleService.GetUser(UserId, true, true);
            ImageSource source = ImageSource.FromStream(() => new MemoryStream(User.AdditionalData["photoBytes"] as byte[]));
            //ImageSource.FromFile(SaveImageToTempFolder());
            UserImage.Source = source;

            UserDisplayNameLabel.Text = User.DisplayName;
            UserDescriptionLabel.Text = User.JobTitle;
            UserPresenceLabel.Text = User.Presence.Availability;
            UserLoadingIndicator.IsVisible = false;
            
        }
    }
    private string SaveImageToTempFolder()
    {
        string cacheDirectory = FileSystem.Current.CacheDirectory;
        string path = Path.Combine(cacheDirectory, User.Id);
        path += ".jpg";
        System.IO.File.WriteAllBytes(path, User.AdditionalData["photoBytes"] as byte[]);
        return path;
    }
}