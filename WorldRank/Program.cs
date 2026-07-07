using WorldRank;
using WorldRank.Interfaces;
using WorldRank.Objects;
public class Program
{
    public static void Main(string[] args)
    {
        InMemoryPlayerRepository repo = new InMemoryPlayerRepository(new List<IPlayer>());

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
                    Console.Write("Enter name: ");
                    string nameInput = Console.ReadLine()?.Trim()!;
                    if (string.IsNullOrWhiteSpace(nameInput))
                    {
                        Console.WriteLine("Name is required.");
                        break;
                    }
                    Player player = new Player(nameInput);
                    repo.AddPlayer(player);
                    Console.WriteLine($"Player {player.Name} added with Id {player.Id}!");
                    break;



                case 2:
                    //Player.ListPlayers(players);
                    break;


                case 3:
                    //Player.FindPlayerByName(players);
                    break;
            }
        }
    }
}