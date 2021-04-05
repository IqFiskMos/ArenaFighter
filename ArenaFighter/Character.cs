using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexicon.CSharp.InfoGenerator;

namespace ArenaFighter {
	public class Character {
		Random rnd = new Random();
		static public InfoGenerator infoGen = new InfoGenerator(DateTime.Now.Millisecond);
		private string name;
		public string Name {
			get {
				return name;
			}
		}
		public int Strength { get; set; }
		public int Health { get; set; }
		public string HealthString
		{
			get {
				if(Health > 0) {
					return Health.ToString();
				} else {
					return "Död";
				}
			}
		}

		
		// Lista över slagen
		public List<Battle> Battles { get; set; }

		
		// Styrkan av spelaren delat på 2
		public int Damage
		{
			get { return Strength / 2; }
		}

		
		// Kollar om någon är död
		public bool IsDead
		{
			get
			{
				if(Health > 0) {
					return false;
				} else {
					return true;
				}
			}
		}
		
		// Skapar spelaren
		public Character(string name, int strength, int health) {
			this.name = name;
			this.Strength = strength;
			this.Health = health;
			this.Battles = new List<Battle>();
		}

		
		// Skapar motståndarna
		public static List<Character> GenerateCharacters(int count) {
			List<Character> characterList = new List<Character>();
			for(int i = 0; i < count; i++) {
				string name = infoGen.NextFirstName();
				name = name.Substring(0, 1).ToUpper() + name.Substring(1);
				characterList.Add(new Character(name, infoGen.Next(1, 8) + 2, infoGen.Next(1, 8) + 2));
			}
			return characterList;
		}

		
		// Här skriver vi ut slutliga poäng
		public void ShowScore() {
			int score = 0;
			foreach(var item in Battles) {
				if(item.LastRound.Winner == this) {
					score += 5;
					Console.WriteLine($"{this.Name} Slogs och dödade {item.LastRound.Opponent.Name}.");
				} else {
					score += 2;
					Console.WriteLine($"{this.Name} Dödades av {item.LastRound.Opponent.Name}.");
				}
			}
			Console.WriteLine($"{this.Name} Sammanlagda poäng är {score}.");
		}

		
		// Skriver ut spelarens stats
		public void Print(bool includeScore = false) {
			Console.WriteLine($"Namn: {Name}");
			Console.WriteLine($"Styrka: {Strength}");
			Console.WriteLine($"Skada: {Damage}");
			Console.WriteLine($"Hälsa: {HealthString}");

			// Skriver ut vilka vi möt och hur det gick
			if(includeScore) {
				this.ShowScore();
			}
		}

	}
}
