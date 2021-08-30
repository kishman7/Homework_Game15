using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game15
{
    public partial class FormGame15 : Form
    {
        Game game; //створюємо змінну
        public FormGame15()
        {
            InitializeComponent();
            game = new Game(4);
        }

        int number = 0; //змінна рахує кількість ходів
        private void button0_Click(object sender, EventArgs e)
        {
            int position = Convert.ToInt16(((Button)sender).Tag);
            game.shift(position); //зміщаємо кнопку
            refresh(); //відображаємо зміст кнопки
            number++;
            if (game.check_numbers()) //перевіряємо чи завершилась гра, чи всі клавіші вірно розташовані
            {
                timer.Enabled = false; //відключаємо таймер
                time = 0; //обнуляємо таймер
                MessageBox.Show($"Ви перемогли!\nВи здійснили кількість ходів: {number}\nВаш час: {textBoxTimer.Text} сек.", "Вітаємо!");
                number = 0; //обнулення ходів
                start_game(); //після завершення гри починається нова гра
            }

            //button(position).Text = position.ToString(); //вертаємо цифри на кнопки
            //MessageBox.Show(position.ToString());
        }

        //метод для виводу цифри на кнопку
        private Button button (int position)
        {
         //return   (tableLayoutPanel.Controls.Find("button" + position, false))[0] as Button;

            switch (position)
            {
                case 0: return button0;
                case 1: return button1;
                case 2: return button2;
                case 3: return button3;
                case 4: return button4;
                case 5: return button5;
                case 6: return button6;
                case 7: return button7;
                case 8: return button8;
                case 9: return button9;
                case 10: return button10;
                case 11: return button11;
                case 12: return button12;
                case 13: return button13;
                case 14: return button14;
                case 15: return button15;
                default: return null;
            }
        }

        private void FormGame15_Load(object sender, EventArgs e)
        {
            start_game(); // запускаємо гру при завантажені програми
        }

        private void menu_start_Click(object sender, EventArgs e)
        {

            timer.Enabled = false; //відключаємо таймер
            time = 0; //обнуляємо таймер
            number = 0; //обнулення ходів
            start_game(); // запускаємо гру при натискані на клавішу "Почати гру"
        }

        //метод, який буде починати гру та перемішувати клавіши
        void start_game()
        {
            game.start();
            for (int i = 0; i < 200; i++)
            {
                game.shift_random(); //перемішуємо рандомно 200 разів кнопки перед початком гри
            }
            refresh();
            timer.Enabled = true; //запускаємо таймер
        }

        //метод, який буде відображати зміст кнопки, він буде оновлювати зміст кнопки
        void refresh()
        {
            for (int position = 0; position < 16; position++)
            {
                int nr = game.get_number(position); //записуємо номер позиції в окрему змінну
                button(position).Text = nr.ToString(); //записуємо значення позиції на кнопку
                button(position).Visible = (nr > 0); // якщо значення позиції більше 0, то цифра відображається на кнопці, а якщо менша 0, то не відображається
            }
        }
        int time = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            time++;
            textBoxTimer.Text = time.ToString();//відображення таймера
        }

        
    }
}
