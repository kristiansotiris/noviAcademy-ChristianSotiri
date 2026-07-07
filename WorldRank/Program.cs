using WorldRank.Enums;
using WorldRank.Interfaces;
using WorldRank.Objects;

public class Program
{
    public static void Main(string[] args)
    {
        var players = new List<IPlayer>();

        IPlayerRepository playerRepository = new InMemoryPlayerRepository(players);
        IWalletRepository walletRepository = new InMemoryWalletRepository(players);

        bool running = true;

        while (running)
        {
            Console.WriteLine("1. Add Player");
            Console.WriteLine("2. Find Players");
            Console.WriteLine("3. Add Wallet");
            Console.WriteLine("4. Get Player Wallets");
            Console.WriteLine("5. Delete Player");
            Console.WriteLine("6. Deposit");
            Console.WriteLine("7. Exit");
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
                        playerRepository.AddPlayer(player);
                        Console.WriteLine($"Player {player.Name} added with Id {player.Id}!");
                        break;
                    }

                case 2:
                    {
                        Console.Write("Enter Player id: ");
                        if (int.TryParse(Console.ReadLine()?.Trim(), out int playerId))
                        {
                            IPlayer? foundPlayer = playerRepository.FindPlayer(playerId);
                            if (foundPlayer != null)
                                Console.WriteLine($"Player found: Id: {foundPlayer.Id}, Name: {foundPlayer.Name}, Score: {foundPlayer.Score}");
                            else
                                Console.WriteLine("Player not found.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Id input.");
                        }
                        break;
                    }

                case 3:
                    {
                        Console.Write("Enter your id: ");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine("Invalid id");
                            break;
                        }

                        Console.Write("Enter currency (EUR/USD/GBP): ");
                        string inputCurrency = Console.ReadLine()!.Trim();

                        if (!Enum.TryParse<Currency>(inputCurrency, true, out Currency currency))
                        {
                            Console.WriteLine("Invalid currency!");
                            break;
                        }

                        try
                        {
                            walletRepository.AddWallet(new Wallet(currency), id);
                            Console.WriteLine($"{currency} wallet added to player {id}.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }

                case 4:
                    {
                        Console.Write("Enter player Id: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            try
                            {
                                IReadOnlyList<IWallet> wallets = walletRepository.GetWalletsByPlayer(id);

                                if (wallets.Count == 0)
                                    Console.WriteLine("This player has no wallets.");
                                else
                                    foreach (IWallet w in wallets)
                                        Console.WriteLine($"{w.Currency} - {w.Balance}");
                            }
                            catch (InvalidOperationException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Id");
                        }
                        break;
                    }

                case 5:
                    {
                        Console.Write("Enter player Id to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            try
                            {
                                playerRepository.DeletePlayer(id);
                                Console.WriteLine($"Player {id} deleted.");
                            }
                            catch (InvalidOperationException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Id");
                        }
                        break;
                    }


                case 6: 
                    {
                        Console.Write("Enter player Id: ");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine("Invalid Id");
                            break;
                        }

                        Console.Write("Enter currency (EUR/USD/GBP): ");
                        if (!Enum.TryParse<Currency>(Console.ReadLine()!.Trim(), true, out Currency currency))
                        {
                            Console.WriteLine("Invalid currency!");
                            break;
                        }

                        Console.Write("Enter amount: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            Console.WriteLine("Invalid amount!");
                            break;
                        }

                        try
                        {
                            IReadOnlyList<IWallet> wallets = walletRepository.GetWalletsByPlayer(id);
                            IWallet? wallet = wallets.FirstOrDefault(w => w.Currency == currency);

                            if (wallet == null)
                            {
                                Console.WriteLine($"Player {id} has no {currency} wallet.");
                                break;
                            }

                            wallet.Deposit(amount);
                            Console.WriteLine($"Deposited {amount} {currency}. New balance: {wallet.Balance}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }

                case 7:
                    running = false;
                    break;

                default:
                    Console.WriteLine("Please choose a valid option (1-6).");
                    break;
            }
        }
    }
}