using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TW.M365.GraphHack.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
        }

        [ObservableProperty]
        private string play0 = string.Empty;

        [ObservableProperty]
        private string play1 = string.Empty;

        [ObservableProperty]
        private string play2 = string.Empty;

        [ObservableProperty]
        private string play3 = string.Empty;

        [ObservableProperty]
        private string play4 = string.Empty;

        [ObservableProperty]
        private string play5 = string.Empty;

        [ObservableProperty]
        private string play6 = string.Empty;

        [ObservableProperty]
        private string play7 = string.Empty;

        [ObservableProperty]
        private string play8 = string.Empty;

        [RelayCommand]
        private void Play(string number)
        {
            switch (number)
            {
                case "0":
                    if (Play0 == "X")
                        Play0 = "O";
                    else
                        Play0 = "X";
                    break;
                case "1":
                    if (Play1 == "X")
                        Play1 = "O";
                    else
                        Play1 = "X";
                    break;
                case "2":
                    if (Play2 == "X")
                        Play2 = "O";
                    else
                        Play2 = "X";
                    break;
                case "3":
                    if (Play3 == "X")
                        Play3 = "O";
                    else
                        Play3 = "X";
                    break;
                case "4":
                    if (Play4 == "X")
                        Play4 = "O";
                    else
                        Play4 = "X";
                    break;
                case "5":
                    if (Play5 == "X")
                        Play5 = "O";
                    else
                        Play5 = "X";
                    break;
                case "6":
                    if (Play6 == "X")
                        Play6 = "O";
                    else
                        Play6 = "X";
                    break;
                case "7":
                    if (Play7 == "X")
                        Play7 = "O";
                    else
                        Play7 = "X";
                    break;
                case "8":
                    if (Play8 == "X")
                        Play8 = "O";
                    else
                        Play8 = "X";
                    break;
                default:
                    return;
            }
        }
    }
}

