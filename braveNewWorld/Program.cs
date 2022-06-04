class program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        bool isPlaying = true;

        char symbolWall = '*';
        char symbolRoad = ' ';

        char symbolPlayer = '#';
        int playerPositionX;
        int playerPositionY;
        int playerDirectionalX = 0;
        int playerDirectionalY = 1;

        char[,] symbolsMap = ReadMap("mapWorld", out playerPositionX, out playerPositionY, symbolPlayer);

        DrawMap(symbolsMap);

        while (isPlaying)
        {

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                ChangeDirection(key, ref playerDirectionalX, ref playerDirectionalY);
            }

            if (symbolsMap[playerPositionX + playerDirectionalX, playerPositionY + playerDirectionalY] != symbolWall)
            {
                Move(ref playerPositionX, ref playerPositionY, playerDirectionalX, playerDirectionalY, symbolPlayer, symbolRoad);
            }
            System.Threading.Thread.Sleep(150);
        }
    }

    static char[,] ReadMap(string mapName, out int playerX, out int playerY, char player)
    {
        playerX = 0;
        playerY = 0;
        string[] fileMapWorld = File.ReadAllLines($"Maps/{mapName}.txt");
        char[,] mapWorld = new char[fileMapWorld.Length, fileMapWorld[0].Length];

        for (int i = 0; i < mapWorld.GetLength(0); i++)
        {
            for (int j = 0; j < mapWorld.GetLength(1); j++)
            {
                mapWorld[i, j] = fileMapWorld[i][j];

                if (mapWorld[i, j] == player)
                {
                    playerX = i;
                    playerY = j;
                }
            }
        }
        return mapWorld;
    }

    static void DrawMap(char[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine();
        }
    }

    static void Move(ref int playerX, ref int playerY, int directionalX, int directionalY, char symPlayer, char symRoad)
    {

        Console.SetCursorPosition(playerY, playerX);
        Console.Write(symRoad);

        playerX += directionalX;
        playerY += directionalY;

        Console.SetCursorPosition(playerY, playerX);
        Console.Write(symPlayer);
    }

    static void ChangeDirection(ConsoleKeyInfo key, ref int directionalX, ref int directionalY)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                directionalX = -1;
                directionalY = 0;
                break;
            case ConsoleKey.DownArrow:
                directionalX = 1;
                directionalY = 0;
                break;
            case ConsoleKey.LeftArrow:
                directionalX = 0;
                directionalY = -1;
                break;
            case ConsoleKey.RightArrow:
                directionalX = 0;
                directionalY = 1;
                break;
        }
    }
}