namespace WorldRank.Objects
{
    public class Player(string name, int score = 0) // Primary Constructor
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public int Score { get; private set; } = score;


        public static void AddPlayer(List<Player> player)
        {
            string? nameInput = null;

            while (string.IsNullOrWhiteSpace(nameInput))
            {
                Console.Write("Enter your name: ");
                nameInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(nameInput))
                {
                    Console.WriteLine("Name is required !");
                }
            }

            Player p = new(nameInput);
            player.Add(p);

            Console.WriteLine($"Player {nameInput} is added !");

        }

        public static void ListPlayers(List<Player> players)
        {
            if (players.Count == 0)
            {
                Console.WriteLine($"No Players found! ");
                return;
            }

            foreach (Player p in players)
            {
                Console.WriteLine($"{p.Name} - Score: {p.Score}");
            }
        }

        public static void FindPlayerByName(List<Player> players)
        {
            Console.WriteLine("Enter name: ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Name is required.");
                return;
            }

            //Player? player = players.FirstOrDefault(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

            List<Player>? foundPlayers = players.Where(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase)).ToList();


            //if (player == null)
            //{
            //    Console.WriteLine("Player not found");
            //    return;
            //}

            if (foundPlayers.Count == 0)
            {
                Console.WriteLine("Player not found.");
                return;
            }

            foreach (Player player in foundPlayers)
            {
                Console.WriteLine($"{player.Name} - Score: {player.Score}");
            }
        }

        public static void UpdateScore(Player player, int score)
        {
            if (player == null)
            {
                Console.WriteLine("Player not found");
                return;
            }

            player.Score += score;

            Console.WriteLine($"Player Score is updated to: {player.Score}");
        }
    }
}
