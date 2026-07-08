using Microsoft.Extensions.Logging;
using WorldRank.Enums;
using WorldRank.Interfaces;
using WorldRank.Objects;

public class Program
{
    public static void Main(string[] args)
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        var players = new List<IPlayer>();

        ILogger<InMemoryPlayerRepository> playerRepoLogger = loggerFactory.CreateLogger<InMemoryPlayerRepository>();
        ILogger<InMemoryWalletRepository> walletRepoLogger = loggerFactory.CreateLogger<InMemoryWalletRepository>();

        IPlayerRepository playerRepository = new InMemoryPlayerRepository(players, playerRepoLogger);
        IWalletRepository walletRepository = new InMemoryWalletRepository(players, walletRepoLogger);

        bool running = true;

        while (running)
        {
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║          WORLD RANK MENU           ║");
            Console.WriteLine("╠════════════════════════════════════╣");
            Console.WriteLine("║  1. Add player                     ║");
            Console.WriteLine("║  2. Find all players               ║");
            Console.WriteLine("║  3. Find player                    ║");
            Console.WriteLine("║  4. Add wallet                     ║");
            Console.WriteLine("║  5. Get player wallets             ║");
            Console.WriteLine("║  6. Delete player                  ║");
            Console.WriteLine("║  7. Deposit                        ║");
            Console.WriteLine("║  8. Fetch Players Score            ║");
            Console.WriteLine("║  9.Withdraw                        ║");
            Console.WriteLine("║  10. Exit                          ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.Write("\n  Choose an option: ");
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
                        IReadOnlyList<IPlayer> playersList = playerRepository.GetAllPlayers();

                        if (playersList.Count == 0)
                        {
                            Console.WriteLine("No players found.");
                        }
                        else
                        {
                            foreach (IPlayer p in playersList)
                            {
                                Console.WriteLine($"Id: {p.Id}, Name: {p.Name}, Score: {p.Score}");
                            }
                        }

                        foreach (var group in playerRepository.GroupPlayersByScore())
                        {
                            Console.WriteLine($"Score: {group.Key}, Players: {string.Join(", ", group.Value.Select(p => p.Name))}");
                        }
                        ;

                        break;
                    }

                case 3:
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

                case 4:
                    {

                        Console.Write("Enter your id: ");
                        if (!int.TryParse(Console.ReadLine(), out int id2))
                        {
                            Console.WriteLine("Invalid id");
                            break;
                        }

                        Console.Write("Enter currency (EUR/USD/GBP): ");
                        string inputCurrency = Console.ReadLine()!.Trim();

                        if (!Enum.TryParse<Currency>(inputCurrency, true, out Currency currency2))
                        {
                            Console.WriteLine("Invalid currency!");
                            break;
                        }

                        try
                        {
                            walletRepository.AddWallet(new Wallet(currency2), id2);
                            Console.WriteLine($"{currency2} wallet added to player {id2}.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    }

                case 5:
                    {

                        Console.Write("Enter player Id: ");
                        if (int.TryParse(Console.ReadLine(), out int id3))
                        {
                            try
                            {
                                IReadOnlyList<IWallet> wallets = walletRepository.GetWalletsByPlayer(id3);

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


                case 6:
                    {

                        Console.Write("Enter player Id to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int id3))
                        {
                            try
                            {
                                playerRepository.DeletePlayer(id3);
                                Console.WriteLine($"Player {id3} deleted.");
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

                case 7:

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

                case 8:
                    {
                        var groupedPlayers = playerRepository.GroupPlayersByScore();
                        if (groupedPlayers.Count == 0)
                        {
                            Console.WriteLine("No players found.");
                        }
                        else
                        {
                            foreach (var group in groupedPlayers)
                            {
                                Console.WriteLine($"Score: {group.Key}, Players: {string.Join(", ", group.Value.Select(p => p.Name))}");
                            }
                        }
                        break;
                    }

                case 9:
                    Console.Write("Enter player Id: ");
                    if (!int.TryParse(Console.ReadLine(), out int withdrawId))
                    {
                        Console.WriteLine("Invalid Id");
                        break;
                    }
                    Console.Write("Enter currency (EUR/USD/GBP): ");
                    if (!Enum.TryParse<Currency>(Console.ReadLine()!.Trim(), true, out Currency withdrawCurrency))
                    {
                        Console.WriteLine("Invalid currency!");
                        break;
                    }
                    Console.Write("Enter amount to withdraw: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                    {
                        Console.WriteLine("Invalid amount!");
                        break;
                    }
                    try
                    {
                        IReadOnlyList<IWallet> wallets = walletRepository.GetWalletsByPlayer(withdrawId);
                        IWallet? wallet = wallets.FirstOrDefault(w => w.Currency == withdrawCurrency);
                        if (wallet == null)
                        {
                            Console.WriteLine($"Player {withdrawId} has no {withdrawCurrency} wallet.");
                            break;
                        }
                        wallet.Withdraw(withdrawAmount);
                        Console.WriteLine($"Withdrew {withdrawAmount} {withdrawCurrency}. New balance: {wallet.Balance}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                    case 10:
                        running = false;
                    break;

                default:
                    Console.WriteLine("Please choose a valid option (1-7).");
                    break;
            }
        }
    }
}