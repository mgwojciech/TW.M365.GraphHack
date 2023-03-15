Hack Together: Microsoft Graph and .NET

![hacktogether](https://user-images.githubusercontent.com/2223355/225382423-54fadcbe-d85c-4892-8c36-4fdef32b48e3.png)


Graph TicTacToe

Simple mobile app to play tic tac toe with colleagues.We use AAD app to login to a tenant. Once we're in, You can create new game which will create a game file in root site collection in document library in TicTacToe folder.Game state is represented as a json file. When user makes a move our app updates the state and the file, opponent is pooling for updates on specific documents and once it detects the file has changed it updates app state.We are using graph endpoints to upload, modify and download game state file![hacktogether](https://user-images.githubusercontent.com/2223355/225382342-8d6d6b49-fd1d-439b-a27a-c2692637f4f9.png)
