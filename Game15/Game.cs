using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game15
{
    class Game
    {
        int size;
        int[,] map;
        int space_x, space_y; //змінні, які будуть відповідати за розміщення вільної ячейки на полі
        static Random rand = new Random();
        public Game (int size) //конструктор
        {
            if (size < 2) size = 2;
            if (size > 5) size = 5;
            this.size = size;
            map = new int[size, size]; // масив чисел, який буде відображити поле гри
        }

        //метод підготовки поля до гри, він заповнить значеннями наш масив map 
        public void start()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    map[x, y] = coords_to_position(x, y) + 1;
                }
            }
            space_x = size - 1;
            space_y = size - 1;
            map[space_x, space_y] = 0; // буде розміщувати порожню ячейку в кінці поля на початку гри
        }

        //медот який буде передавати позицію кнопки, яка буде натиснута і відповідно, яка кнопка має переміститься
        public void shift(int position)
        {
            int x, y;
            position_to_coords(position, out x, out y); // вертаємо координати кнопки, яку натискаємо, визначаємо її за position
            if (Math.Abs(space_x - x) + Math.Abs(space_y - y) != 1) //перевіряємо позицію кнопки, щоб вона була поруч з пустим місцем
            {                           //перевіряємо по модулю різницю координат між порожнім місцем та натиснутою кнопкою
                return;                 //якщо воно буде доівнювати 1, значить координати будуть (0,1) або (1,0) і кнопка знаходиться поруч з постим місце
            }                           //інакше кнопка не буде знаходитись поруч з пустим місцем і метод завершиться
            map[space_x, space_y] = map[x, y]; // в кнопку з пробілом ми записуємо координати кнопки, яку натиснули
            map[x, y] = 0; //а в координати натиснутої кнопки записуємо те що було в пробілі, тобто 0. таким чином міняються значення кнопки і вони ніби переміщуються.
            space_x = x; //переміщаємо пробіл, на координати кнопки, яку ми натиснули
            space_y = y;
        }

        //метод, який буде робити випадкове переміщення клавішів, для початку гри
        public void shift_random()
        {
            int a = rand.Next(0, 4); //так, для одної ячейки може бути 4 хода
            int x = space_x;
            int y = space_y;
            switch (a)
            {
                case 0: x--; break; //відбуватиметься переміщення вліво
                case 1: x++; break; //відбуватиметься переміщення вправо
                case 2: y--; break; //відбуватиметься переміщення вверх
                case 3: y++; break; //відбуватиметься переміщення вниз
            }
            shift(coords_to_position(x, y)); //отримані координати пережаємо у метод shift, який безпосередньо перемішує ячейки
        }                                   // також метод shift буде перевіряти чи можливо переміщення ячейок при отриманих координатах

        //метод, який буде повідомляти, коли завершиться гра, тобто, коли всі клавіши на місці
        public bool check_numbers()
        {
            if (!(space_x == size - 1 && space_y == size - 1)) //Якщо порожня ячейка не стоїть в нижній правій частині поля, то гра точно ще не завершена
            {
                return false;
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!(x == size - 1 && y == size - 1)) //якщо остання ячейка не є останньою, то робимо наступну перевірку
                    {
                        if (map[x, y] != coords_to_position(x, y) + 1) // перебираємо всі позиції кнопок, і якщо вони не стоять у початковому вихідному положені,
                        {                                              // тобто від 1 до 15, то гра ще не завершена
                            return false;
                        }
                    }
                }
            }
            return true; //якщо ми пройшли всі ці перевірки, то гра завершеня, оскільки всі кнопки разом з пустою стоять на своїх місцях
        }

        //метод, який буде повертати ячейку в потрібне місце за допомогою числа, яке на ній написано
        public int get_number (int position)
        {
            int x, y;
            position_to_coords(position, out x, out y);
            if (x < 0 || x > size) return 0; //перевірка на переповнення масиву по х
            if (y < 0 || y > size) return 0; //перевірка на переповнення масиву по у
            return map[x, y];
        }

        //метод, який координати кнопки переводить в її позицію
        int coords_to_position (int x, int y)
        {
            if (x < 0) x = 0; //перевірка на переповнення масиву значеннями координат
            if (x > size - 1) x = size - 1;
            if (y < 0) y = 0;
            if (y > size - 1) y = size - 1;
            return y * size + x;
        }

        //метод, який проводить позицію кнопки в її координати
        void position_to_coords (int position, out int x, out int y)
        {
            if (position < 0) position = 0; //перевірка на переповнення масиву значеннями позицій
            if (position > size * size - 1) position = size * size - 1;
            x = position % size;
            y = position / size;
        }



    }
}
