 using System;
using System.Windows.Forms;

namespace RPGProject
{
    public partial class Form1 : Form
    {
        Player player;
        Enemy enemy;
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            player = new Player("Hero", 100, 50);
            enemy = new Enemy("Goblin", 100, 1);

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            lblPlayerHP.Text = "HP: " + player.HP;
            lblPlayerMP.Text = "MP: " + player.MP;

            lblEnemyName.Text = enemy.Name;
            lblEnemyHP.Text = "HP: " + enemy.HP;
            lblEnemyLevel.Text = "Level: " + enemy.Level;
        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            if (player.MP < 5)
            {
                MessageBox.Show("Недостатньо MP!");
                return;
            }

            player.MP -= 5;
            enemy.TakeDamage(10);

            richTextBox1.AppendText(
                $"Гравець атакував ворога на 10 шкоди\n");

            EnemyTurn();
            UpdateInfo();
        }

        private void btnStrongAttack_Click(object sender, EventArgs e)
        {
            if (player.MP < 15)
            {
                MessageBox.Show("Недостатньо MP!");
                return;
            }

            player.MP -= 15;
            enemy.TakeDamage(25);

            richTextBox1.AppendText(
                $"Гравець використав сильну атаку на 25 шкоди\n");

            EnemyTurn();
            UpdateInfo();
        }

        private void btnHeal_Click(object sender, EventArgs e)
        {
            if (player.MP < 10)
            {
                MessageBox.Show("Недостатньо MP!");
                return;
            }

            player.MP -= 10;
            player.Heal(20);

            richTextBox1.AppendText(
                $"Гравець відновив 20 HP\n");

            EnemyTurn();
            UpdateInfo();
        }

        private void btnRest_Click(object sender, EventArgs e)
        {
            player.MP += 15;

            if (player.MP > 100)
                player.MP = 100;

            richTextBox1.AppendText(
                $"Гравець відпочив та отримав 15 MP\n");

            EnemyTurn();
            UpdateInfo();
        }

        private void EnemyTurn()
        {
            if (enemy.HP <= 0)
            {
                MessageBox.Show("Перемога!");
                return;
            }

            int action = random.Next(1, 5);

            switch (action)
            {
                case 1:
                    player.TakeDamage(10);
                    richTextBox1.AppendText(
                        "Ворог атакував на 10 шкоди\n");
                    break;

                case 2:
                    player.TakeDamage(20);
                    richTextBox1.AppendText(
                        "Ворог сильно атакував на 20 шкоди\n");
                    break;

                case 3:
                    enemy.Heal(15);
                    richTextBox1.AppendText(
                        "Ворог відновив 15 HP\n");
                    break;

                case 4:
                    richTextBox1.AppendText(
                        "Ворог відпочив та відновив сили\n");
                    break;
            }

            if (player.HP <= 0)
            {
                MessageBox.Show("Поразка!");
            }

            RandomEvent();
        }

        private void RandomEvent()
        {
            int chance = random.Next(1, 6);

            switch (chance)
            {
                case 1:
                    richTextBox1.AppendText(
                        "Подія: знайдено 50 золота!\n");
                    break;

                case 2:
                    player.HP += 10;
                    richTextBox1.AppendText(
                        "Подія: здоров'я збільшилось на 10!\n");
                    break;

                case 3:
                    player.TakeDamage(5);
                    richTextBox1.AppendText(
                        "Подія: втрачено 5 HP!\n");
                    break;
            }
        }
    }

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

            if (HP < 0)
                HP = 0;
        }

        public void Heal(int amount)
        {
            HP += amount;

            if (HP > 100)
                HP = 100;
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

            if (HP < 0)
                HP = 0;
        }

        public void Heal(int amount)
        {
            HP += amount;

            if (HP > 100)
                HP = 100;
        }
    }
} 
