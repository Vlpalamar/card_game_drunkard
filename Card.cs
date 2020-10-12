using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace ConsoleApp10
{
    class Swap
    {
        static public void swap<T>(ref T el1, ref T el2)
        {
            T temp = el1;
            el1 = el2;
            el2 = temp;

        }
    }
    enum name { Six=6, Seven, Eight, Nine, Ten, Jake, Quin, King, Ace };
    enum suit{ Heart=0,Diamond,Club,Spades};
    class Card
    {
        public name Name;

        public suit Suit;
        
        
        public Card(name name, suit suit)
        {
            Name = name;
            Suit = suit;
        }

       

        public override string ToString()
        {
            return $"Card: {Name} of {Suit}s ";
        }
    }

    class Deck
    {   
       

      public  Card[] card_deck = new Card[36];
        
        public Deck()
        {
            name car = name.Six;
            int card_caunt = 0;
            for (int i = 0; i < 9; i++)
            {
                Card c;
                suit suit = suit.Heart;
                for (int j = 0; j < 4; j++)
                {
                    c = new Card(car, suit);
                    card_deck[card_caunt++] = c;
                   
                    suit++;
                }
                car++;
            }
        }
        public void shuffle_the_deck() //перетусовать колоду 
        {
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                Swap.swap(ref this.card_deck[r.Next(0, 36)], ref this.card_deck[r.Next(0, 36)]);
            }
        }
    }

    class Player
    {
        public Queue<Card> HendDeck= new Queue<Card>();

    }
   class Game : Deck
    {
        Array array;
        
        Player[] playerN;
        public void Play()
        {
            int pl1 = 0;


            do
            {
                int pl2 = pl1 + 1;
                if (pl2 > playerN.Length - 1)
                    pl2 = 0;
                Console.WriteLine($"\tигрок с номером {pl1 + 1} ходит на игрока {pl2 + 1}");
                Console.WriteLine($"у игрока {pl1 + 1} {playerN[pl1].HendDeck.Count()} карт \t\t у игрока у игрока {pl2 + 1} {playerN[pl2].HendDeck.Count()}карт ");
                Console.WriteLine($"{playerN[pl1].HendDeck.Peek()} \t vs \t {playerN[pl2].HendDeck.Peek()} \n\t\t");


                if (playerN[pl1].HendDeck.Peek().Name.ToString() == "Six" && playerN[pl2].HendDeck.Peek().Name.ToString() == "Ace")
                {
                    Console.WriteLine($"победил {pl1 + 1} игрок");
                    playerN[pl1].HendDeck.Enqueue(playerN[pl2].HendDeck.Dequeue());
                 
                }
                else if (playerN[pl1].HendDeck.Peek().Name.ToString() == "Ace" && playerN[pl2].HendDeck.Peek().Name.ToString() == "Six")
                {
                    Console.WriteLine($"победил {pl2 + 1} игрок ");
                    playerN[pl1].HendDeck.Enqueue(playerN[pl2].HendDeck.Dequeue());
                   
                }
                else
                {


                    if (playerN[pl1].HendDeck.Peek().Name.CompareTo(playerN[pl2].HendDeck.Peek().Name) == 1)
                    {
                        Console.WriteLine($"победил {pl1 + 1} игрок");
                        playerN[pl1].HendDeck.Enqueue(playerN[pl2].HendDeck.Dequeue());
                    }
                    else if (playerN[pl1].HendDeck.Peek().Name.CompareTo(playerN[pl2].HendDeck.Peek().Name) == -1)
                    {
                        Console.WriteLine($"победил {pl2 + 1} игрок ");
                        playerN[pl1].HendDeck.Enqueue(playerN[pl2].HendDeck.Dequeue());
                    }
                    else
                    {
                        playerN[pl1].HendDeck.Enqueue(playerN[pl1].HendDeck.Dequeue());
                        playerN[pl2].HendDeck.Enqueue(playerN[pl2].HendDeck.Dequeue());
                        Console.WriteLine("ничья");
                    }
                }
                
                if (playerN[pl1].HendDeck.Count() == 35)
                {
                    Console.WriteLine($"игрок с номером {pl1 + 1} победил в игре");
                    Console.Read();
                    break;
                } else if (playerN[pl2].HendDeck.Count() == 35)
                {
                    Console.WriteLine($"игрок с номером {pl2 + 1} победил в игре");
                    Console.Read();
                    break;
                }
                if (playerN[pl1].HendDeck.Count() == 0&&playerN.Length!=2)
                {
                    Console.WriteLine($"игрок №{pl1 + 1} выбыл");
                    Thread.Sleep(5000);
                    Game g1 = new Game(playerN.Length-1);
                    g1.Play();
                }
                if (playerN[pl2].HendDeck.Count() == 0 && playerN.Length != 2)
                {
                    Console.WriteLine($"игрок №{pl2+1} выбыл");
                    Thread.Sleep(5000);

                    Game g1 = new Game(playerN.Length - 1);
                    g1.Play();
                    
                }
                Thread.Sleep(1000);
                Console.Clear();
                pl1++;
                if (pl1 > playerN.Length-1)
                    pl1 = 0;
               
            } while (true);

        }
        public void Defoult_shuffle_deck( IComparer<Card> comparer)
        {
            Array.Sort(card_deck,comparer);
        }
       public Game(int amount_of_players)
        {
            
            playerN = new Player[amount_of_players];
            Deck d = new Deck();
            d.shuffle_the_deck();
            int card_count = 0;
            for (int i = 0; i < playerN.Length; i++)
            {
                playerN[i] = new Player();

                for (int j = 0; j < (d.card_deck.Length/playerN.Length); j++)
                {
                   
                    playerN[i].HendDeck.Enqueue(d.card_deck[card_count++]);

                }
            }     
       }

     
    }

    class CardComparer : IComparer<Card>
    {
        public int Compare(Card X, Card Y)
        {
            if (X.Name == Y.Name)
                return X.Suit.CompareTo(Y.Suit);
            else
                return X.Name.CompareTo(Y.Name);
        }
    }
}


