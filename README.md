Hack Together: Microsoft Graph and .NET

![hacktogether](https://user-images.githubusercontent.com/2223355/225382423-54fadcbe-d85c-4892-8c36-4fdef32b48e3.png)


Graph TicTacToe

Simple mobile app to play tic tac toe with colleagues. We use AAD app to login to a tenant. Once we're in, You can create new game which will create a game file in root site collection in document library in TicTacToe folder. Game state is represented as a json file. When user makes a move our app updates the state and the file, opponent is pooling for updates on specific documents and once it detects the file has changed it updates app state.We are using graph endpoints to upload, modify and download game state file.
When user awaits opponent move, we send a request each second to graph api to get etag for a file and compare it with file metadata already stored in app memory, once etag changes (opponent made a move) we update local state and stop pooling.

![Simulator Screen Shot - iPhone 14 Pro - 2023-03-15 at 18 40 27](https://user-images.githubusercontent.com/2223355/225389304-f3d917fc-7660-461d-83f4-0ce0361b94f5.png)
![Simulator Screen Shot - iPhone 14 Pro - 2023-03-15 at 18 40 52](https://user-images.githubusercontent.com/2223355/225389309-f6a9da8d-2988-4847-8fcd-9acf7528c33c.png)
![Simulator Screen Shot - iPhone 14 Pro - 2023-03-15 at 18 41 26](https://user-images.githubusercontent.com/2223355/225389314-f9b8358d-45dd-473c-a554-dee5fcb431af.png)
![Simulator Screen Shot - iPhone 14 Pro - 2023-03-15 at 18 42 48](https://user-images.githubusercontent.com/2223355/225389316-f67a13bd-d527-417c-8a84-306241b78e41.png)
