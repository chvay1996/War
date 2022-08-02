using System;
using System.Collections.Generic;

namespace War
{
    class Program
    {
        static void Main ( string [] args )
        {
            Arena arena = new Arena ();
            arena.Work ();
        }
    }

    class Arena
    {
        private List<Fighter> _horde = new List<Fighter> ();
        private List<Fighter> _alliance = new List<Fighter> ();
        private Fighter _hordeFighter;
        private Fighter _allianceFighter;
        private Random _random = new Random ();
        private string WinnerName;
        private bool _isWork = true;
        private int _speedGames;

        public Arena ()
        {
            CreateNewPlatoon ( 9, _horde );
            CreateNewPlatoon ( 9, _alliance );
        }

        public void Work ()
        {
            while ( _isWork == true )
            {
                Console.Write ("Бой между двумя взводами. Орда и Альянс" +
                    "\n1 - Выберать кто победит. И начать бой!" +
                    "\n2 - Выход\n\n\t\t");

                switch ( int.Parse ( Console.ReadLine () ) )
                {
                    case 1:
                        ChoosingTheWinner ();
                        break;
                    case 2:
                        _isWork = false;
                        break;
                    default:
                        Console.WriteLine ( "Неверно указано значение!" );
                        break;
                }
                Console.ReadLine ();
                Console.Clear ();

            }
        }

        private void SpeedGames ()
        {
            Console.Write ("Выберите скорость игры" +
                "\n1 - Быстрая" +
                "\n2 - Средняя" +
                "\n3 - Медленая\n\n\t\t");

            switch ( int.Parse ( Console.ReadLine () ) )
            {
                case 1:
                    _speedGames = 50;
                    break;
                case 2:
                    _speedGames = 150;
                    break;
                case 3:
                    _speedGames = 250;
                    break;
                default:
                    Console.WriteLine ( "Неверно указано значение!" );
                    break;
            }
        }

        private void WorkArena ()
        {
            SpeedGames ();

            while ( _horde.Count > 0 && _alliance.Count > 0 )
            {
                _hordeFighter = _horde [ _random.Next ( _horde.Count ) ];
                _allianceFighter = _alliance [ _random.Next ( _alliance.Count ) ];
                ShowInfo ( _horde );
                ShowInfo ( _alliance );
                _hordeFighter.TakeDamage ( _allianceFighter.Damage );
                _allianceFighter.TakeDamage ( _hordeFighter.Damage );
                _hordeFighter.UseAnAttack ();
                _allianceFighter.UseAnAttack ();
                RemoveFighter ();
                System.Threading.Thread.Sleep ( _speedGames );
                Console.Clear ();
            }
            ShowBattleResult ();
        }

        private void ShowBattleResult ()
        {
            if ( _horde.Count > 0 && _alliance.Count > 0 )
            {
                Console.WriteLine ( "Ничья, оба взвода погибли" );
            }
            else if ( _horde.Count <= 0 )
            {
                Console.WriteLine ( "Взвод страны Альянс победил!" );
                Console.WriteLine ($"Ваша ставка была сделана на {WinnerName}");
            }
            else if ( _alliance.Count <= 0 )
            {
                Console.WriteLine ( "Взвод страны Орда победил!" );
                Console.WriteLine ( $"Ваша ставка была сделана на {WinnerName}" );
            }
            _isWork = false;
        }

        private void ShowInfo ( List<Fighter> fighters )
        {
            Console.WriteLine ( " Взвод" );
            foreach ( var fighter in fighters )
            {
                Console.WriteLine ( $"{fighter.Name}. Здоровье: {fighter.Health}. Урон: {fighter.Damage}." );
            }
        }

        private void RemoveFighter ()
        {
            if ( _hordeFighter.Health <= 0 )
            {
                _horde.Remove ( _hordeFighter );
            }
            if ( _allianceFighter.Health <= 0 )
            {
                _alliance.Remove ( _allianceFighter );
            }
        }

        private void CreateNewPlatoon ( int numberOfSoldiers, List<Fighter> soldier )
        {
            for ( int i = 0; i < numberOfSoldiers; i++ )
            {
                soldier.Add ( GetSoldier () );
            }
        }

        private Fighter GetSoldier ()
        {
            int maximumNumberClassFighter = 5;
            int newSolider = _random.Next ( maximumNumberClassFighter );

            if ( newSolider == 1 )
            {
                return new Warrior ( "Варвар", 300, 60, 0 );
            }
            else if ( newSolider == 2 )
            {
                return new Paladin ( "Паладин", 150, 30, 30 );
            }
            else if ( newSolider == 3 )
            {
                return new Hunter ( "Охотник", 130, 50, 15 );
            }
            else if ( newSolider == 4 )
            {
                return new Berserck ( "Берсерк", 200, 60, 5 );
            }
            else
            {
                return new Magician ( "Маг", 100, 50, 10 );
            }
        }

        private void ChoosingTheWinner ()
        {
            Console.Write ("\nВыберите кто победит: 1 - Орда. 2 - Альянс\n\n\t\t");

            switch ( int.Parse ( Console.ReadLine () ) )
            {
                case 1:
                    WinnerName = "Орда";
                    break;
                case 2:
                    WinnerName = "Альянс";
                    break;
                default:
                    Console.WriteLine ( "Неверно указано значение!" );
                    break;
            }
            WorkArena ();
        }
    }

    class Fighter
    {
        protected int Armor;

        public Fighter ( string name, int health, int damage, int armor )
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }

        public void TakeDamage ( int damage )
        {
            Health -= damage - Armor;
        }

        public void ShowStats ()
        {
            Console.WriteLine ( $"{Name} - {Health} хп, {Damage} урона, {Armor} брони." );
        }

        public void UseAnAttack ()
        {
            Random random = new Random ();
            int randomNumber;
            int number = 1;
            int maximumNumber;
            int minimumNumber;
            maximumNumber = 4;
            minimumNumber = 1;
            randomNumber = random.Next ( minimumNumber, maximumNumber );

            if ( number == randomNumber )
            {
                Skill ();
            }
        }

        public virtual void Skill () { }
    }
    class Warrior : Fighter
    {
        public Warrior ( string name, int health, int damage, int armor ) : base ( name, health, damage, armor ) { }

        public override void Skill ()
        {
            base.Skill ();
            int damageBuff = 10;
            int armorBuff = 10;
            Console.WriteLine ( $"{Name} Собралю всю свою ярость и увеличел урон на 10 едениц и увеличинает браню на 10" );
            Damage += damageBuff;
            Armor += armorBuff;
        }
    }

    class Paladin : Fighter
    {
        public Paladin ( string name, int health, int damage, int armor ) : base ( name, health, damage, armor ) { }

        public override void Skill ()
        {
            base.Skill ();
            int healthBuff = Health / 100 * 40;
            Console.WriteLine ( $"{Name} Скорбит над падшими и отхиливается на 40%" );
            Health += healthBuff;
        }
    }

    class Hunter : Fighter
    {
        public Hunter ( string name, int health, int damage, int armor ) : base ( name, health, damage, armor ) { }

        public override void Skill ()
        {
            base.Skill ();
            int damageBuff = 15;
            Console.WriteLine ( $"{Name} Призвал спутника и сливается с ним воедино урон увеличен на 15 " );
            Damage += damageBuff;
        }
    }

    class Berserck : Fighter
    {
        public Berserck ( string name, int health, int damage, int armor ) : base ( name, health, damage, armor ) { }

        public override void Skill ()
        {
            base.Skill ();
            int damageBuff = Damage * 2;
            int armorDebaff = 20;
            Console.WriteLine ( $"{Name} Впадает в ярость: уменьшает браню на 20 и увеличивает урон х2" );
            Damage += damageBuff;
            Armor -= armorDebaff;
        }
    }

    class Magician : Fighter
    {
        public Magician ( string name, int health, int damage, int armor ) : base ( name, health, damage, armor ) { }

        public override void Skill ()
        {
            base.Skill ();
            int damageBuff = 100;
            Console.WriteLine ( $"{Name} Кидает глыбу огня и наносит 100 урона" );
            TakeDamage ( damageBuff );
        }
    }
}
/*Задача:
Есть 2 взвода. 1 взвод страны один, 2 взвод страны 2.
Каждый взвод внутри имеет солдат.
Нужно написать программу которая будет моделировать бой этих взводов.
Каждый боец - это уникальная единица, он может иметь уникальные способности или же уникальные характеристики, такие как повышенная сила.
Побеждает та страна, во взводе которой остались выжившие бойцы.
Не важно какой будет бой, рукопашный, стрелковый.*/