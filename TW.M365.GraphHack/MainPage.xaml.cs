using Microsoft.Graph;
using System.Net.Http.Json;
using TW.M365.GraphHack.Lib.Services;
using TW.M365.GraphHack.ViewModels;
using TW.M365.GraphHack.Views;

namespace TW.M365.GraphHack;

public partial class MainPage : ContentPage
{
	private HttpClient _httpClient;
    public IPeopleService PeopleService { get; }
	public MainPage(HttpClient httpClient, IPeopleService peopleService)
	{
        _httpClient = httpClient;
        PeopleService = peopleService;
        InitializeComponent();
    }
	private async Task GetMe()
	{
    }
	private void OnCounterClicked(object sender, EventArgs e)
	{
		GetMe();
	}
}

