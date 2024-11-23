
namespace HSELAB4;

class Program
{

    static void PrintHelpPanel()
    {
        Console.WriteLine("Программа выполняет действия: \n");
        
        Console.WriteLine("1) Сформировать массив из n элементов");
        Console.WriteLine("2) Распечатать массив");
        Console.WriteLine("3) Удалить минимальный элемент");
        Console.WriteLine("4) Добавление K элементов в конец массива");
        Console.WriteLine("5) Сдвигает циклически на M элементов в право");
        Console.WriteLine("6) Поиск первого отрицательного элемента");
        Console.WriteLine("7) Сортировка массива простым выбором");
        Console.WriteLine("8) Бинарный поиск(в отсортированном массиве) указанного элемента");
        
        Console.WriteLine("\nДля выбора действия введите его номер, для вывода панели помощи введите: '101'\nЕсли хотите завершить выполнение программы введите: '100'");
    }
    
    static int EnterIntNum(Dictionary<int, string> textData)
    {
        bool isNumber;
        int num;

        do
        {
            Console.Write($"{textData[0]}");
            isNumber = Int32.TryParse(Console.ReadLine(), out num);
            if (!isNumber) Console.WriteLine($"{textData[1]}");
        } 
        while (!isNumber);

        return num;
    }

    static bool IsArraySorted(int[] array)
    {
        for (int i = 0; i < array.Length-1; i++)
        {
            if (array[i] > array[i + 1]) return false;
        }
        return true;
    }
    
    
    static void Main(string[] arg)
    {
        Console.WriteLine(-10 % 3);
        PrintHelpPanel();
        
        Random random = new Random();
        
        bool isGoing = true;
        bool isArrayCreated = false;
        int[] array = new int[0]; // колхозно, но зато компилятор не ругается =)
        int currentAction;
        int arrayLength = 0;
        int typeFill;
        
        Dictionary<int, string> currentActionDict = new() { { 0, "Введите номер действия: " }, { 1, "Введено некорректное действие, попробуйте еще раз." } };
        Dictionary<int, string> arrayLengthDict = new() { { 0, "\nВведите длину массива: " }, { 1, "Введена некорректная длина массива, попробуйте еще раз." } };
        Dictionary<int, string> arrayFillDict = new() { { 0, "\nВведите тип заполнения массива(неотрицательное число - случайными числами; отрицательное - путем ввода с клавиатуры): " }, { 1, "Введено некорректное число, попробуйте еще раз" } };
        Dictionary<int, string> arrayAddElemsDict = new() { {0, "\nВведите количество элементов, которые хотите добавить в конец массива: "}, {1, "Введено некорректное число, поробуйте еще раз."} };
        Dictionary<int, string> moveOnDict = new() { {0, "\nВведите на сколько вы хотите сдивинуть элементы массива циклически вправо: "}, {1, "Введено неверное число, попробуйте еще раз."}};
        Dictionary<int, string> enterTargetDict = new() { { 0, "\nВведите элемент, который хотите найти в массиве: " }, {1, "\nВведено неверное число, попробуйте еще раз."} };
        
        while (isGoing)
        {
            currentAction = EnterIntNum(currentActionDict);

            switch (currentAction)
            {
                case 1:
                    arrayLength = EnterIntNum(arrayLengthDict);
                    if (arrayLength < 0)
                    {
                        Console.WriteLine("Длина массива не может быть отрицательной, попробуйте еще раз");
                        goto case 1;
                    }
                    else
                    {
                        typeFill = EnterIntNum(arrayFillDict);
                        array = new int[arrayLength];
                    
                        if (typeFill >= 0) for (int i = 0; i < arrayLength; i++) array[i] = random.Next(-100, 100);
                        else
                        {
                            for (int i = 0; i < arrayLength; i++)
                            {
                                Dictionary<int, string> arrayNumDict = new() { {0, $"Введите элемент массива №{i+1}: "}, {1, $"Число введено неверно, попробуйте еще раз"} };
                                array[i] = EnterIntNum(arrayNumDict);
                            }
                        }
                        isArrayCreated = true;
                        
                        break;
                    }
                    
                case 2:
                    if(!isArrayCreated) Console.WriteLine("Прежде чем выполнять действия с масивом создайте его.");
                    else
                    {
                        for(int i = 0; i < array.Length; i++)
                        {
                            Console.Write(array[i] + " ");
                        }

                        Console.WriteLine("\n");
                    }

                    break;
                
                case 3:
                    if(!isArrayCreated) Console.WriteLine("Прежде чем выполнять действия с масивом создайте его.");
                    else if(array.Length == 0) Console.WriteLine("Массив имеет нулевую длину, удаление минимального элемента невозможно");
                    else
                    {
                        int minElem = array[0];
                        int minIndex = 0;
                        
                        for (int i = 1; i < array.Length; i++)
                        {
                            if (array[i] < minElem)
                            {
                                minElem = array[i];
                                minIndex = i;
                            }
                        }
                        
                        int[] tempArray = new int[array.Length - 1];
                        for (int i = 0, j = 0; i < array.Length; i++)
                        {
                            if (i != minIndex)
                            {
                                tempArray[j] = array[i];
                                j++;
                            }
                        }

                        array = tempArray;
                        Console.WriteLine($"Минимальный элемент {minElem}, на позиции {minIndex+1} - удален из массива.");
                    }
                    
                    break;
                
                case 4:
                    if(!isArrayCreated) Console.WriteLine("Прежде чем выполнять действия с масивом создайте его.");
                    else
                    {
                        int countAdd = EnterIntNum(arrayAddElemsDict);
                        if (countAdd < 0)
                        {
                            Console.WriteLine("Нельзя добавить в конец массива отрицательное количество элементов, попробуйте еще раз.");
                            goto case 4;
                        }
                        else
                        {
                            int[] tempArray = new int[array.Length + countAdd];
                            for (int i = 0; i < array.Length; i++) tempArray[i] = array[i];
                            for (int i = array.Length; i < tempArray.Length; i++)
                            {
                                Dictionary<int, string> arrayNumDict = new() { {0, $"Введите новый элемент массива на позицию №{i+1}: "}, {1, $"Число введено неверно, попробуйте еще раз"} };
                                tempArray[i] = EnterIntNum(arrayNumDict);
                            }
                            array = tempArray;
                        }
                    }
                    
                    break;
                
                case 5:
                    if(!isArrayCreated) Console.WriteLine("Прежде чем выполнять действия с масивом создайте его.");
                    else if(array.Length == 0) Console.WriteLine("Нельзя сдвигать элементы массива, если их нет");
                    else
                    {
                        int moveOn = EnterIntNum(moveOnDict) % array.Length;
                        if (moveOn <= 0 || array.Length == 1) Console.WriteLine("При данных параметрах нельзя осуществить сдвиг массива.");
                        else
                        {
                            int[] tempArray = new int[moveOn];
                            for (int i = 0; i < moveOn; i++) tempArray[i] = array[array.Length - moveOn + i];
                            for (int i = array.Length - 1; i >= moveOn; i--) array[i] = array[i - moveOn];
                            for (int i = 0; i < moveOn; i++) array[i] = tempArray[i];

                            Console.WriteLine($"Элементы массива успешно сдвинуты на {moveOn} элемента(ов) в вправо.\n");
                        }
                    }

                    break;
                
                case 6:
                    if(!isArrayCreated) Console.WriteLine("Прежде чем выполнять действия с масивом создайте его.");
                    else if(array.Length == 0) Console.WriteLine("Массив имеет длину 0, найти отрицательный элемент нельзя");
                    else
                    {
                        bool isElemIn = false;
                        int operationsCount = 0;
                        
                        for (int i = 0; i < array.Length; i++)
                        {
                            operationsCount++;
                            if (array[i] < 0)
                            {
                                Console.WriteLine($"Первый отрицательный элемент: {array[i]}, на позиции {i + 1}. Операций потребовалось: {operationsCount}");
                                isElemIn = true;
                                break;
                            }
                        }
                        
                        if (!isElemIn) Console.WriteLine("Отрицательный элемент в массиве отсутствует.\n");
                        
                    }

                    break;
                
                case 7:
                    if(!isArrayCreated) Console.WriteLine("Прежде чем выполнять действия с масивом создайте его.");
                    else if(array.Length == 0) Console.WriteLine("Нельзя отсортировать массив нулевой длины");
                    else
                    {
                        for (int i = 0; i < array.Length - 1; i++)
                        {
                            int minIndex = i;
                            for (int j = i + 1; j < array.Length; j++) if (array[j] < array[minIndex]) minIndex = j;
                            if (minIndex != i) (array[i], array[minIndex]) = (array[minIndex], array[i]);
                        }

                        Console.WriteLine("Массив успешно отсортирован!\n");
                    }
                    
                    break;
                    
                case 8:
                    if(!isArrayCreated) Console.WriteLine("Прежде чем выполнять действия с масивом создайте его.");
                    else if(array.Length == 0) Console.WriteLine("Нельзя найти элемент в массиве нулевой длины.");
                    else if(IsArraySorted(array) == false) Console.WriteLine("Массив не отсортирован, нельзя найти элемент при помощи бинарного поиска.");
                    else
                    {
                        int target = EnterIntNum(enterTargetDict);
                        int left = 0;
                        int right = array.Length - 1;
                        int operationsCount = 0;
                        bool isFinded = false;

                        while (left <= right)
                        {
                            operationsCount++;
                            int middle = left + (right - left) / 2;
                            if (array[middle] == target)
                            {
                                Console.WriteLine($"Искомый элемент: {target}, находится на позиции {middle + 1}. Операций потребовалось: {operationsCount}");
                                isFinded = true;
                                break;
                            }
                            else if (array[middle] < target) left = middle + 1;
                            else right = middle - 1;
                        }
                        if(!isFinded) Console.WriteLine("Искомого элемента нет в массиве.");
                    }
                    break;
                
                case 100:
                    isGoing = false;
                    Console.WriteLine("Программа завершена");
                    break;
                
                case 101:
                    PrintHelpPanel();
                    break;
                
                default:
                    Console.WriteLine("\nДанной команды не существует введите другую.");
                    Console.WriteLine("Если нужна помощь введите 101\n");
                    break;
            }
        }
    }
}

