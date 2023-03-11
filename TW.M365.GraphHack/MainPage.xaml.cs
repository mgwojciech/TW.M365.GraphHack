using Android.App;
using Microsoft.Graph;
using System.Net.Http.Json;
using TW.M365.GraphHack.Lib.GameManager;
using TW.M365.GraphHack.Lib.Graph;
using TW.M365.GraphHack.Lib.Services;
using TW.M365.GraphHack.ViewModels;
using TW.M365.GraphHack.Views;

namespace TW.M365.GraphHack;

public partial class MainPage : ContentPage
{
    private HttpClient _httpClient;
    public IPeopleService PeopleService { get; }
    protected TicTacToeManager _gameManager;

    public MainPage(HttpClient httpClient, IPeopleService peopleService, TicTacToeManager gameManager)
    {
        _httpClient = httpClient;
        PeopleService = peopleService;
        _gameManager = gameManager;
        InitializeComponent();
    }
    private async Task GetMe()
    {
    }
    private void OnCounterClicked(object sender, EventArgs e)
    {
        GetMe();
    }

    private void StartGameButton_Clicked(object sender, EventArgs e)
    {
    }

    private void _subscriptionService_OnRemoteUpdate(object sender, TicTacToeState e)
    {

    }
}

