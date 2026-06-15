
using System;
using System.Windows.Forms;

namespace RPGProject
{
    public partial class Form1 : Form
    {
        Player player;
        Enemy enemy;
        Random random = new Random();
        bool isGameOver = false; // Прапорець для відстеження кінця гри

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

            // Якщо гра закінчена, вимикаємо кнопки управління
            if (isGameOver)
            {
                btnAttack.Enabled = false;
                btnStrongAttack.Enabled = false;
                btnHeal.Enabled = false;
                btnRest.Enabled = false;
            }
        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            // Звичайна атака тепер безкоштовна (0 MP)
            enemy.TakeDamage(10);
            richTextBox1.AppendText($"Гравець атакував ворога на 10 шкоди\n");

            if (CheckBattleStatus()) return; // Перевіряємо, чи не помер ворог

            EnemyTurn();
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
            richTextBox1.AppendText($"Гравець використав сильну атаку на 25 шкоди\n");

            if (CheckBattleStatus()) return;

            EnemyTurn();
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
            richTextBox1.AppendText($"Гравець відновив 20 HP\n");

            EnemyTurn();
        }

        private void btnRest_Click(object sender, EventArgs e)
        {
            player.MP += 15;
            if (player.MP > 50) player.MP = 50; // Обмежуємо ману початковим максимумом (50)

            richTextBox1.AppendText($"Гравець відпочив та отримав 15 MP\n");

            EnemyTurn();
        }

        // Окремий метод для перевірки смерті ворога відразу після удару гравця
        private bool CheckBattleStatus()
        {
            if (enemy.HP <= 0)
            {
                MessageBox.Show("Перемога!");
                isGameOver = true;
                UpdateInfo();
                return true;
            }
            return false;
        }

        private void EnemyTurn()
        {
            int action = random.Next(1, 5);

            switch (action)
            {
                case 1:
                    player.TakeDamage(10);
                    richTextBox1.AppendText("Ворог атакував на 10 шкоди\n");
                    break;
                case 2:
                    player.TakeDamage(20);
                    richTextBox1.AppendText("Ворог сильно атакував на 20 шкоди\n");
                    break;
                case 3:
                    enemy.Heal(15);
                    richTextBox1.AppendText("Ворог відновив 15 HP\n");
                    break;
                case 4:
                    richTextBox1.AppendText("Ворог відпочив та відновив сили\n");
                    break;
            }

            // Перевірка поразки гравця
            if (player.HP <= 0)
            {
                MessageBox.Show("Поразка!");
                isGameOver = true;
                UpdateInfo();
                return; // Виходимо, випадкова подія не відбувається
            }

            RandomEvent();
            UpdateInfo(); // Оновлюємо інтерфейс в самому кінці ходу
        }

        private void RandomEvent()
        {
            int chance = random.Next(1, 6);

            switch (chance)
            {
                case 1:
                    richTextBox1.AppendText("Подія: знайдено 50 золота!\n");
                    break;
                case 2:
                    player.Heal(10); // Використовуємо метод Heal, щоб не перевищити 100 HP
                    richTextBox1.AppendText("Подія: здоров'я збільшилось на 10!\n");
                    break;
                case 3:
                    player.TakeDamage(5);
                    richTextBox1.AppendText("Подія: втрачено 5 HP!\n");
                    
                    // Додаткова перевірка, якщо випадкова подія добила гравця
                    if (player.HP <= 0)
                    {
                        MessageBox.Show("Поразка від випадкової події!");
                        isGameOver = true;
                    }
                    break;
            }
        }
    }
}

