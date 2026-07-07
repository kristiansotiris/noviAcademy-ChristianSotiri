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
            Console.WriteLine("2. Find Players");
            Console.WriteLine("3. Group By Player");
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
                    Console.Write("Enter Player id: ");
                    string idInput = Console.ReadLine()?.Trim()!;

                    if(int.TryParse(idInput, out int playerId))
                    {
                        IPlayer? foundPlayer = repo.FindPlayer(playerId);
                        if (foundPlayer != null)
                        {
                            Console.WriteLine($"Player found: Id: {foundPlayer.Id}, Name: {foundPlayer.Name}, Score: {foundPlayer.Score}");
                        }
                        else
                        {
                            Console.WriteLine("Player not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Id input.");
                    }
                    break;


                case 3:
                    //Group players by score and display the groups
                    break;
            }
        }
    }
}