using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuronNetwork
{
    internal class Program
    {
        /// <summary>
        /// Класс нейрона
        /// </summary>
        public class Neuron
        {
            private decimal weight = 0.5m; // вес первого входного значения нейрона
            public decimal LastError { get; private set; } // ошибка
            public decimal Smoothing { get; set; } = 0.00001m; // сглаживание шага обучения
            /// <summary>
            /// Получает входящий сингал для обратики его нейроном
            /// </summary>
            /// <param name="input">Входное значние</param>
            /// <returns>Выходное значние</returns>
            public decimal ProccessInputData(decimal input)
            {
                return input * weight; //входной сигнал умножаем на вес и получаем ответ
            }
            /// <summary>
            ///  Получает входящий сингал для обратики его нейроном в обратную сторону
            /// </summary>
            /// <param name="output">Входное значние</param>
            /// <returns>Выходное значние</returns>
            public decimal RestoreInputData(decimal output)
            {
                return output / weight; //обратное действие нейрона
            }
            /// <summary>
            /// Метод для обучения нейрона
            /// </summary>
            /// <param name="input">Входящий сигнал нейрона</param>
            /// <param name="expectedResult">Ожидаемый результат обратотки</param>
            public void Train(decimal input, decimal expectedResult)
            {
                var actualResult = input * weight; // результат работы нейрона
                LastError = expectedResult - actualResult; // вычисляем ошибку работу нейрона (погрешность)
                var corretion = (LastError / actualResult) * Smoothing; // вычисляем значение для корректировки нейрона
                weight += corretion; // корретируем вес 
            }
        }
        static void Main(string[] args)
        {
            decimal km = 100; // 100 километров
            decimal miles = 10000m; // в 100 км 10000 метров

            Neuron neuron = new Neuron();
            int i = 0; // счетчик итераций обучения нейрона
            do
            {
                i++;
                neuron.Train(km, miles); // обучаем нейрон, предоставляя ему правльный результат работы
                Console.WriteLine($"Итерация: {i}\t Ошибка: \t {neuron.LastError}"); // выводим 

            } while (neuron.LastError > neuron.Smoothing || neuron.LastError < -neuron.Smoothing); // обучаем пока ошибка < или > сглавживания
            Console.WriteLine("Обучения завершено!");
            Console.WriteLine($"{neuron.ProccessInputData(100)} метров в {100} км");

            Console.WriteLine($"{neuron.ProccessInputData(541)} метров в {541} км");
            Console.WriteLine($"{neuron.RestoreInputData(10)} км в {10} метрах");
            Console.ReadKey();

        }
    }
}
