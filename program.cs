using System;

namespace RPGProject
{
    class Program
    {
        static Player player;
        static Enemy enemy;
        static Random random = new Random();
        static bool isGameOver = false;

        static void Main(string[] args)
        {
            // Налаштування кодування для коректного відображення українського тексту
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створюємо персонажів
            player = new Player("Воїн", 100, 50);
            enemy = new Enemy("Гоблін", 100, 1);

            Console.WriteLine("=== ЛАСКАВО ПРОСИМО ДО RPG БОЮ ===");
            Console.WriteLine($"Ваш противник: {enemy.Name} (Рівень: {enemy.Level})\n");

            // Головний ігровий цикл
            while (!isGameOver)
            {
                ShowStatus();
                ShowMenu();
                
                string choice = Console.ReadLine();
                Console.Clear(); // Очищаємо екран для красивого виведення

                ProcessPlayerTurn(choice);
            }

            Console.WriteLine("\nДякуємо за гру! Натисніть Enter для виходу.");
            Console.ReadLine();
        }

        static void ShowStatus()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"[Гравець] HP: {player.HP}/100  |  MP: {player.MP}/50");
            Console.WriteLine($"[Ворог]   HP: {enemy.HP}/100  |  {enemy.Name}");
            Console.WriteLine("---------------------------------------------");
        }

        static void ShowMenu()
        {
            Console.WriteLine("Оберіть вашу дію:");
            Console.WriteLine("1. Звичайна атака (0 MP) -> 10 шкоди");
            Console.WriteLine("2. Сильна атака   (15 MP) -> 25 шкоди");
            Console.WriteLine("3. Лікування      (10 MP) -> +20 HP");
            Console.WriteLine("4. Відпочинок     (+15 MP)");
            Console.Write("Ваш вибір: ");
        }

        static void ProcessPlayerTurn(string choice)
        {
            switch (choice)
            {
                case "1":
                    enemy.TakeDamage(10);
                    Console.WriteLine("⚔️ Ви атакували ворога на 10 шкоди!");
                    break;

                case "2":
                    if (player.MP < 15)
                    {
                        Console.WriteLine("❌ Недостатньо MP для сильної атаки! Ви пропустили хід через неуважність.");
                        break;
                    }
                    player.MP -= 15;
                    enemy.TakeDamage(25);
                    Console.WriteLine("💥 Ви використали сильну атаку на 25 шкоди!");
                    break;

                case "3":
                    if (player.MP < 10)
                    {
                        Console.WriteLine("❌ Недостатньо MP для лікування! Ви пропустили хід через неуважність.");
                        break;
                    }
                    player.MP -= 10;
                    player.Heal(20);
                    Console.WriteLine("❤️ Ви відновили 20 HP!");
                    break;

                case "4":
                    player.MP += 15;
                    if (player.MP > 50) player.MP = 50;
                    Console.WriteLine("💤 Ви відпочили та відновили 15 MP.");
                    break;

                default:
                    Console.WriteLine("🤔 Невідома команда. Ворог користується вашою розгубленістю!");
                    break;
            }

            // Перевірка, чи не помер ворог після удару
            if (enemy.HP <= 0)
            {
                Console.WriteLine("\n🏆 ПЕРЕМОГА! Гобліна подолано!");
                isGameOver = true;
                return;
            }

            // Хід ворога
            EnemyTurn();
        }

        static void EnemyTurn()
        {
            Console.WriteLine("\n🎲 Хід ворога...");
            int action = random.Next(1, 5);

            switch (action)
            {
                case 1:
                    player.TakeDamage(10);
                    Console.WriteLine("🥊 Ворог атакував вас на 10 шкоди!");
                    break;
                case 2:
                    player.TakeDamage(20);
                    Console.WriteLine("🔥 Ворог сильно атакував вас на 20 шкоди!");
                    break;
                case 3:
                    enemy.Heal(15);
                    Console.WriteLine("🩹 Ворог підлікувався на 15 HP!");
                    break;
                case 4:
                    Console.WriteLine("💤 Ворог відпочиває і гарчить на вас.");
                    break;
            }

            // Перевірка, чи живий гравець
            if (player.HP <= 0)
            {
                Console.WriteLine("\n💀 ПОРАЗКА! Вас було подолано...");
                isGameOver = true;
                return;
            }

            // Випадкова подія в кінці раунду
            RandomEvent();
        }

        static void RandomEvent()
        {
            int chance = random.Next(1, 6);
            Console.WriteLine(); // Порожній рядок для читаємості

            switch (chance)
            {
                case 1:
                    Console.WriteLine("✨ Подія: Ви знайшли торбинку, а там 50 золота!");
                    break;
                case 2:
                    player.Heal(10);
                    Console.WriteLine("✨ Подія: Повіяв цілющий вітер. Ваше здоров'я збільшилось на 10!");
                    break;
                case 3:
                    player.TakeDamage(5);
                    Console.WriteLine("✨ Подія: Ви перечепилися об камінь і втратили 5 HP!");
                    if (player.HP <= 0)
                    {
                        Console.WriteLine("\n💀 Подія виявилася фатальною... Поразка!");
                        isGameOver = true;
                    }
                    break;
            }
        }
    }

    // Класи Player та Enemy тепер знаходяться всередині одного файлу і доступні для програми
    public class Player
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }

        public Player(string name, int hp, int mp)
        {
            Name = name;
            HP = hp;
            MP = mp;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 0) HP = 0;
        }

        public void Heal(int amount)
        {
            HP += amount;
            if (HP > 100) HP = 100;
        }
    }

    public class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Level { get; set; }

        public Enemy(string name, int hp, int level)
        {
            Name = name;
            HP = hp;
            Level = level;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 0) HP = 0;
        }

        public void Heal(int amount)
        {
            HP += amount;
            if (HP > 100) HP = 100;
        }
    }
}
