using WorldRank.Objects;

public class Program
{
    public static void Main(string[] args)
    {
        List<Player> players = new List<Player>();

        bool running = true;

        while (running)
        {
            Console.WriteLine("1. Add Player");
            Console.WriteLine("2. List Players");
            Console.WriteLine("3. Search Player");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Please enter a valid number!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Player.AddPlayer(players);
                    break;

                case 2:
                    Player.ListPlayers(players);
                    break;


                case 3:
                    Player.FindPlayerByName(players);
                    break;
            }
        }
    }
}