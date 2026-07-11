using WorldRank.src.WorldRank.Application.Interfaces;
using WorldRank.src.WorldRank.Domain.Entities;
using SysConsole = System.Console;

namespace WorldRank.src.WorldRank.Application.Services;

public class PlayerService
{
	private readonly IPlayerRepository _playerRepository;

	public PlayerService(IPlayerRepository playerRepository)
	{
		_playerRepository = playerRepository;
	}

	public void AddPlayer()
	{
        SysConsole.Write("Name: ");
		var name = SysConsole.ReadLine();
		if (string.IsNullOrWhiteSpace(name))
		{
			SysConsole.WriteLine("Name cannot be empty.");
			return;
		}

        SysConsole.Write("Score: ");
		var scoreInput = SysConsole.ReadLine();
		if (!int.TryParse(scoreInput, out var score))
		{
			SysConsole.WriteLine("Score must be a whole number.");
			return;
		}

		var player = new Player(GeneratePlayerId(), name);
		player.AddScore(score);
		_playerRepository.AddPlayer(player);
		SysConsole.WriteLine("Player added successfully.");
	}

	public void ListPlayers()
	{
		var all = _playerRepository.GetAllPlayers().ToList();

		if (all.Count == 0)
		{
            SysConsole.WriteLine("No players registered.");
			return;
		}

		foreach (var player in all)
            SysConsole.WriteLine(player);
	}

	public void ListPlayersByScore()
	{
		var groups = _playerRepository.GroupPlayersByScore().ToList();

		if (groups.Count == 0)
		{
            SysConsole.WriteLine("No players registered.");
			return;
		}

		foreach (var group in groups)
		{
            SysConsole.WriteLine($"Score {group.Key}:");
			foreach (var player in group)
                SysConsole.WriteLine($"  {player}");
		}
	}

	public void FindPlayerByName()
	{
        SysConsole.Write("Search by name: ");
		var term = SysConsole.ReadLine() ?? string.Empty;

		var player = _playerRepository.GetAllPlayers()
			.FirstOrDefault(p => p.Name.Equals(term, StringComparison.OrdinalIgnoreCase));

        SysConsole.WriteLine(player is null ? "No player found." : player.ToString());
	}

	public void FindPlayerById()
	{
		var playerId = Prompts.PromptPlayerId();
		if (playerId is null)
			return;

		var player = _playerRepository.FindPlayer(playerId.Value);

        SysConsole.WriteLine(player is null ? "No player found." : player.ToString());
	}

	public void DeletePlayer()
	{
		var playerId = Prompts.PromptPlayerId();
		if (playerId is null)
			return;

		_playerRepository.DeletePlayer(playerId.Value);
        SysConsole.WriteLine("Player deleted (if it existed).");
	}

	// Generates a random, unique player id (avoids collisions with already-registered players).
	private int GeneratePlayerId()
	{
		var existingIds = _playerRepository.GetAllPlayers().Select(p => p.Id).ToHashSet();

		int id;
		do
		{
			id = Random.Shared.Next(1, int.MaxValue);
		}
		while (existingIds.Contains(id));

		return id;
	}
}
