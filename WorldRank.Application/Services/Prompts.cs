using WorldRank.src.WorldRank.Application.Strategies;
using WorldRank.src.WorldRank.Domain.Enums;
using SysConsole = System.Console;
namespace WorldRank.src.WorldRank.Application.Services;

// Small shared helpers for reading and validating console input.
public static class Prompts
{
	public static int? PromptPlayerId()
	{
        SysConsole.Write("Give player id: ");
		if (int.TryParse(SysConsole.ReadLine(), out var playerId))
			return playerId;

        SysConsole.WriteLine("Player id must be a whole number.");
		return null;
	}

	public static Currency? PromptCurrency()
	{
        SysConsole.Write("Give Currency: 1 - EUR | 2 - USD\n");
		switch (SysConsole.ReadLine())
		{
			case "1":
				return Currency.EUR;
			case "2":
				return Currency.USD;
			default:
                SysConsole.WriteLine("Unknown currency.");
				return null;
		}
	}

	public static decimal? PromptAmount(string label)
	{
        SysConsole.Write($"{label}: ");
		if (decimal.TryParse(SysConsole.ReadLine(), out var amount))
			return amount;

        SysConsole.WriteLine("Amount must be a number.");
		return null;
	}

	public static FundsOperation? PromptFundsOperation()
	{
        SysConsole.Write("Operation: 1 - Add | 2 - Subtract | 3 - Force subtract (penalty)\n");
		switch (SysConsole.ReadLine())
		{
			case "1":
				return FundsOperation.Add;
			case "2":
				return FundsOperation.Subtract;
			case "3":
				return FundsOperation.ForceSubtract;
			default:
                SysConsole.WriteLine("Unknown operation.");
				return null;
		}
	}
}
