using System;		/// <summary>
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter {
	class Program {
		static void Main(string[] args) {
			// Här frågar vi efter namn på spelaren
			Console.WriteLine("Vad vill du kalla dig?");
			string playerName = Console.ReadLine();

			// Här skapar vi spelarens stats
			Character player = new Character(playerName, Character.infoGen.Next(1, 4) + 4, Character.infoGen.Next(1, 4) + 4);

			// Sen är det dags och skapa motståndarna och spara dessa i en lista
			List<Character> Opponents = Character.GenerateCharacters(20);
			
			Console.Clear();

			// Nu har vi skapat spelaren och motståndarna
			bool isRunning = true;
			while(isRunning) {
				// Låt oss skriva ut spelaren och hans stats och låta han välja vad han vill göra
				player.Print();

				Console.WriteLine("\nVad vill du görra?");
				Console.WriteLine("L - Leta efter motståndare");
				Console.WriteLine("F - Fega ut och sluta slåss");

				// Här bestämmer vad som händer beroende på vad spelaren väljer att göra
				ConsoleKeyInfo cki = Console.ReadKey(true);
				switch(cki.Key) {
					case ConsoleKey.L:
						// Om spelaren tryckte på L
						if(Opponents.Count > 0) {
							// Rensa skärmen och visa spelarens stats
							Console.Clear();
							Character opponent = Opponents[0];

							Console.WriteLine("\nSpelare:");
							player.Print();

							Console.WriteLine("\nMotståndare:");
							opponent.Print();

							// Slaget börjar när spelaren trycker på en valfriknapp
							Console.ReadKey(true);

							// Låt oss skapa slaget
							Battle battle = new Battle(player, opponent);

							// När det är gjort startar vi slaget
							battle.Fight();

							// När slaget är slut så kollar vi vem som dog.
							if(opponent.IsDead) {
								Opponents.Remove(opponent);
							} else if (player.IsDead) {
								isRunning = false;
							}
						} else {
							// Bestäm vad som händer när det inte finns några motståndare kvar
							Console.WriteLine("Jävla KING, Du har dödat alla!");
							isRunning = false;
							Console.ReadKey(true);
						}
						break;
					case ConsoleKey.F:
						//Här bestämmer vi vad som händer om spelaren trycker på F
						Console.WriteLine("Jävla FEGIS! Du sprang iväg med svansen mellan benen");
						isRunning = false;
						Console.ReadKey(true);
						break;
					default:
						break;
				}
				Console.Clear();
			}
			// Visar statsen när spelet är slut
			Console.WriteLine("Slutlig poäng:\n");
			player.Print(includeScore: true);

			Console.ReadKey();

            
		}
	}
}
