using WorldRank.Enums;
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
            Console.WriteLine("3. Add Wallet");
            Console.WriteLine("4. Get Player Wallets");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Please enter a valid number!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    {
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
                    }

                case 2:
                    {
                        Console.Write("Enter Player id: ");
                        string idInput = Console.ReadLine()?.Trim()!;

                        if (int.TryParse(idInput, out int playerId))
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
                    }

                case 3:
                    {
                        Console.WriteLine("Enter your id: ");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine("Invalid id");
                            break;
                        }

                        IPlayer? founded = repo.FindPlayer(id);
                        if (founded == null)
                        {
                            Console.WriteLine("Player not found.");
                            break;
                        }

                        Console.Write("Enter currency (EUR/USD/GBP): ");

                        string inputCurrency = Console.ReadLine()!.Trim();

                        if (!Enum.TryParse<CurrencyEnums>(inputCurrency, true, out CurrencyEnums currency))
                        {
                            Console.WriteLine("Invalid currency!");
                            break;

                        }

                        try
                        {
                            Player p = (Player)founded;
                            p.AddWallet(new Wallet(currency));
                            Console.WriteLine($"{currency} wallet added to {founded.Name}.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex);

                        }
                        break;
                    }

                case 4:
                    {
                        Console.Write("Enter player Id: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            IReadOnlyList<IWallet>? wallets = repo.GetPlayerWallets(id);

                            if (wallets == null) Console.WriteLine("Player not found.");
                            else if (wallets.Count == 0) Console.WriteLine("There's no any wallets for this player.");
                            else
                            {
                                foreach (IWallet w in wallets)
                                {
                                    Console.WriteLine($"{w.Currency} - {w.Balance}");
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid Id");
                        }
                        break;
                    }
            }
        }
    }
}