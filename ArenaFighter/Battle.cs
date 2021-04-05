using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter {
	public class Battle {
		// Stats över hela slaget
		public Character Player { get; set; }
		public Character Opponent { get; set; }
		public Round LastRound { get; set; }
		public List<Round> Rounds { get; set; }
		public bool IsFinished { get; private set; }

		// Hur slaget genomförs
		public Battle(Character player, Character opponent) {
			this.Player = player;
			player.Battles.Add(this);
			this.Opponent = opponent;
			this.IsFinished = false;
		}

		public void Fight(bool inputRequired = true) {
			do {
				//ny runda
				FightRound();

				//kollar om spelaren måste trycka på en knapp och väntar
				if(inputRequired) { Console.ReadKey(); }
			} while(!LastRound.IsFinal); // körs till slaget är över

			// slaget är över
			this.IsFinished = true;
			// skriver ut vem som vann slaget
			Console.WriteLine("\n--------------");
			Console.WriteLine($"{this.LastRound.Winner.Name} är KINGEN!");

			//kollar om spelaren måste trycka på en knapp och väntar
			if (inputRequired) { Console.ReadKey(); }
		}

		public void FightRound(bool rollDice = true) {
			// Skapar rundan
			this.LastRound = new Round(Player, Opponent, rollDice);

			// skriver ut rundans information
			Console.WriteLine("\n--------------");
			this.LastRound.Print();
		}

		// Skriver ut alla rundor
		public void Print() {
			foreach(var item in Rounds) {
				Console.WriteLine("\n--------------");
				item.Print();
			}
			Console.WriteLine("\n--------------");
			Console.WriteLine($"{this.LastRound.Winner.Name} är KINGEN!");
		}
	}
}
