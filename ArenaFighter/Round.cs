using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter {
	public class Round {
	
		// För att underlätta så låter vi denna vara öppen för alla
		static Random RandomNumberGenerator = new Random();

		// Slumpar fram en siffra på tärningen
		public int RollDice {
			get { return RandomNumberGenerator.Next(1, 7); }
		}

		public Character Player { get; set; }	
		public Character Opponent { get; set; }
	
		// Vem är vinnare?
		public Character Winner { get; set; }
	
		// Vem är förloraren?
		public Character Loser { get; set; }

	
		// Tärning för spelaren
		public int PlayerRoll { get; set; }
	
		// Tärning för motståndaren
		public int OpponentRoll { get; set; }

		// Kollar om tärningarna har samma siffra
		public bool IsDraw { get; set; }
	
		// Kollar om någon dog
		public bool IsFinal { get; set; }

		public Round(Character player, Character opponent, bool rollDice = true) {
			// Görklart för slaget
			this.Player = player;
			this.Opponent = opponent;
			this.IsDraw = false;
			this.IsFinal = false;

			// Skall tärningen rullas?
			this.PlayerRoll = (rollDice) ? RollDice : 0;
			this.OpponentRoll = (rollDice) ? RollDice : 0;

			if((player.Strength + PlayerRoll) > (opponent.Strength + OpponentRoll)) {
				// Är spelaren starkast vinner han
				this.Winner = player;
				this.Loser = opponent;
			} else if((player.Strength + PlayerRoll) == (opponent.Strength + OpponentRoll)) {
				// Ifall båda är lika starka
				this.IsDraw = true;
			} else {
				// Är motståndaren starkare vinner han
				this.Winner = opponent;
				this.Loser = player;
			}

			// Ifall tärningarna inte är samma skada den svagaste
			if(!this.IsDraw) { this.Loser.Health -= this.Winner.Damage; }

			// Kollar om någon dog och säg att det är sista rundan
			if(player.IsDead || opponent.IsDead) { this.IsFinal = true; }
		}

	
		// Vad som skall skrivas ut i konsolen
	
		public void Print() {
			Console.WriteLine($"Tärningen: {Player.Name} {Player.Strength + PlayerRoll} ({Player.Strength}+{PlayerRoll}) mot "+
				$"{Opponent.Name} {Opponent.Strength + OpponentRoll} ({Opponent.Strength}+{OpponentRoll})");
			if(this.IsDraw) {
				Console.WriteLine("Tärningarna visar samma så båda tar och pustar ut inför nästa runda");
			} else {
				Console.ForegroundColor = (Winner == Player) ? ConsoleColor.Green:ConsoleColor.Red;
				Console.WriteLine($"{Winner.Name} Svingar mot {Loser.Name}! {Loser.Name} tar {Winner.Damage} skada{((Loser.IsDead)?" och faller mot marken och dör":"")}.");
				Console.ResetColor();
			}
			Console.WriteLine($"Hälsa kvar: {Player.Name} ({Player.HealthString}), {Opponent.Name} ({Opponent.HealthString})");
		}
	}
}