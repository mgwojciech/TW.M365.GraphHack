using Microsoft.Graph;
using System.Net.Http.Json;

namespace TW.M365.GraphHack;

public partial class MainPage : ContentPage
{
	int count = 0;
	private HttpClient _httpClient;
	public MainPage(HttpClient httpClient)
	{
        _httpClient = httpClient;
        InitializeComponent();
	}
	private async Task GetMe()
	{
        Task<HttpResponseMessage> meResponse = _httpClient.GetAsync("https://graph.microsoft.com/v1.0/me");
        Task<HttpResponseMessage> myPhotoResponse = _httpClient.GetAsync("https://graph.microsoft.com/v1.0/me/photo/$value");
        Task<HttpResponseMessage> myPresenceResponse = _httpClient.GetAsync("https://graph.microsoft.com/v1.0/me/presence");
        await Task.WhenAll(meResponse, myPhotoResponse, myPresenceResponse);
        User user = await meResponse.Result.Content.ReadFromJsonAsync<User>();
		user.Presence = await myPresenceResponse.Result.Content.ReadFromJsonAsync<Presence>();
		byte[] photo = await myPhotoResponse.Result.Content.ReadAsByteArrayAsync();
        WelcomeLabel.Text = $"Welcome {user.DisplayName}";
    }
	private void OnCounterClicked(object sender, EventArgs e)
	{
		GetMe();
        count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

