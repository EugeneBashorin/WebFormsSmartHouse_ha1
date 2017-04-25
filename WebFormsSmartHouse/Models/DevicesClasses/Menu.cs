using System;
using System.Collections.Generic;

namespace SimpleSmartHouse1._0
{
    public class Menu
    {
        public void Show()
        {
            int temp;
            IDictionary<string, Device> deviceDict = new Dictionary<string, Device>();
            deviceDict.Add("Heater", new Heater("Heater", false, Mode.Eco, new Parametr(8, 15, 12), new ChangeSetting()));
            deviceDict.Add("AC", new AirCondition("AC", false, Mode.Low, new Parametr(8, 15, 12), new ChangeSetting()));
            deviceDict.Add("TV", new TV("TV", false, new Parametr(1, 100, 3), new Parametr(1, 45, 15), new ChangeSetting(), BrightnessLevel.Default, new ListFunction()));
            deviceDict.Add("Radio", new Radio("Radio", false, new Parametr(1, 100, 3), new Parametr(1, 45, 15), new ChangeSetting(), new ListFunction()));
            deviceDict.Add("Lamp", new Illuminator("Lamp", false, BrightnessLevel.Medium));

            while (true)
            {
                Console.Clear();
                foreach (KeyValuePair<string, Device> div in deviceDict)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Название: " + div.Key + " " + div.Value.ToString());
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Введите команду: ");
                string[] commands = Console.ReadLine().Split(' ');
                if (commands[0].ToLower() == "exit" & commands.Length == 1)
                {
                    return;
                }
                if (commands.Length != 2)
                {
                    Help();
                    continue;
                }
                if (commands[0].ToLower() == "add" && !deviceDict.ContainsKey(commands[1]))
                {
                    if (commands[1].StartsWith("Heater"))
                    {
                        deviceDict.Add(commands[1], new Heater("Heater", false, Mode.Eco, new Parametr(8, 15, 12), new ChangeSetting()));
                        continue;
                    }
                    else if (commands[1].StartsWith("AC"))
                    {
                        deviceDict.Add(commands[1], new AirCondition("AirCondition", false, Mode.Low, new Parametr(8, 15, 12), new ChangeSetting()));
                        continue;
                    }
                    else if (commands[1].StartsWith("TV"))
                    {
                        deviceDict.Add(commands[1], new TV("TV", false, new Parametr(0, 100, 3), new Parametr(0, 45, 15), new ChangeSetting(), BrightnessLevel.Default, new ListFunction()));
                        continue;
                    }
                    else if (commands[1].StartsWith("Radio"))
                    {
                        deviceDict.Add(commands[1], new Radio("Radio", false, new Parametr(1, 100, 3), new Parametr(1, 45, 15), new ChangeSetting(), new ListFunction()));
                        continue;
                    }
                    else if (commands[1].StartsWith("Lamp"))
                    {
                        deviceDict.Add(commands[1], new Illuminator("Lamp", false, BrightnessLevel.Medium));
                    }
                    else { }
                }
                if (commands[0].ToLower() == "add" && deviceDict.ContainsKey(commands[1]))
                {
                    Console.WriteLine("Устройство с таким именем уже существует");
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadLine();
                    continue;
                }
                if (!deviceDict.ContainsKey(commands[1]))
                {
                    Help();
                    continue;
                }

                switch (commands[0].ToLower())
                {
                    case "del":
                        deviceDict.Remove(commands[1]);
                        break;

                    case "on":
                        deviceDict[commands[1]].SwtchOn();
                        break;

                    case "off":
                        deviceDict[commands[1]].SwtchOff();
                        break;

                    case "tur":                                            // For Heater  Dehumidifier   AirCondition
                        if (commands[1].StartsWith("Heater"))
                        {
                            ((Heater)deviceDict[commands[1]]).SetMaxMode();//.SetTurboMode();
                        }
                        else if (commands[1].StartsWith("AC"))
                        {
                            ((AirCondition)deviceDict[commands[1]]).SetMaxMode();//.SetTurboMode();
                        }
                        break;

                    case "eco":                                            // For Heater  Dehumidifier   AirCondition
                        if (commands[1].StartsWith("Heater"))
                        {
                            ((Heater)deviceDict[commands[1]]).SetMiddleMode();//.SetEcoMode();
                        }
                        else if (commands[1].StartsWith("AC"))
                        {
                            ((AirCondition)deviceDict[commands[1]]).SetMiddleMode();//.SetEcoMode();
                        }
                        break;

                    case "low":                                             // For Heater  Dehumidifier   AirCondition
                        if (commands[1].StartsWith("Heater"))
                        {
                            ((Heater)deviceDict[commands[1]]).SetMinMode();//.SetLowMode();
                        }
                        else if (commands[1].StartsWith("AC"))
                        {
                            ((AirCondition)deviceDict[commands[1]]).SetMinMode();//.SetLowMode();
                        }
                        break;

                    case "aut":                                             // For Heater  Dehumidifier   AirCondition
                        if (commands[1].StartsWith("Heater"))
                        {
                            ((Heater)deviceDict[commands[1]]).SetAutoMode();
                        }
                        else if (commands[1].StartsWith("AC"))
                        {
                            ((AirCondition)deviceDict[commands[1]]).SetAutoMode();
                        }
                        break;

                    case "inc":                                              // For Heater  Dehumidifier   AirCondition
                        if (commands[1].StartsWith("Heater"))
                        {
                            ((Heater)deviceDict[commands[1]]).IncreaseTemperature();
                        }
                        else if (commands[1].StartsWith("AC"))
                        {
                            ((AirCondition)deviceDict[commands[1]]).IncreaseTemperature();
                        }
                        break;

                    case "dec":
                        if (commands[1].StartsWith("Heater"))
                        {
                            ((Heater)deviceDict[commands[1]]).DecreaseTemperature();
                        }
                        else if (commands[1].StartsWith("AC"))
                        {
                            ((AirCondition)deviceDict[commands[1]]).DecreaseTemperature();
                        }
                        break;

                    case "hse":
                        Console.WriteLine("Введите значение температуры а пределах (10..35)");
                        string tempStr = Console.ReadLine();
                        if (Int32.TryParse(tempStr, out temp))
                        {
                            if (commands[1].StartsWith("Heater"))
                            {
                                ((Heater)deviceDict[commands[1]]).HandSetTemperature(temp);
                            }
                            else if (commands[1].StartsWith("AC"))
                            {
                                ((AirCondition)deviceDict[commands[1]]).HandSetTemperature(temp);
                            }
                        }
                        Console.WriteLine("Неверно введено значение.");
                        break;


                    case "nxt":
                        if (commands[1].StartsWith("TV"))
                        {
                            ((TV)deviceDict[commands[1]]).IncreaseChannel();
                        }
                        else if (commands[1].StartsWith("Radio"))
                        {
                            ((Radio)deviceDict[commands[1]]).IncreaseChannel();
                        }
                        break;

                    case "prv":
                        if (commands[1].StartsWith("TV"))
                        {
                            ((TV)deviceDict[commands[1]]).DecreaseChannel();
                        }
                        else if (commands[1].StartsWith("Radio"))
                        {
                            ((Radio)deviceDict[commands[1]]).DecreaseChannel();
                        }
                        break;

                    case "hsc":
                        Console.WriteLine("Введите номер канала (0..100)");
                        string tempChan = Console.ReadLine();
                        if (Int32.TryParse(tempChan, out temp))
                        {
                            if (commands[1].StartsWith("TV"))
                            {
                                ((TV)deviceDict[commands[1]]).HandSetChannel(temp);
                            }
                            else if (commands[1].StartsWith("Radio"))
                            {
                                ((Radio)deviceDict[commands[1]]).HandSetChannel(temp);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверно введено значение.");
                        }
                        break;

                    case "scl":
                        if (commands[1].StartsWith("TV"))
                        {
                            Console.WriteLine(((TV)deviceDict[commands[1]]).ShowChannelList());
                            Console.ReadLine();
                        }
                        else if (commands[1].StartsWith("Radio"))
                        {
                            Console.WriteLine(((Radio)deviceDict[commands[1]]).ShowChannelList());
                            Console.ReadLine();
                        }
                        break;

                    case "inv":
                        if (commands[1].StartsWith("TV"))
                        {
                            ((TV)deviceDict[commands[1]]).IncreaseVolume();
                        }
                        else if (commands[1].StartsWith("Radio"))
                        {
                            ((Radio)deviceDict[commands[1]]).IncreaseVolume();
                        }
                        break;

                    case "dev":
                        if (commands[1].StartsWith("TV"))
                        {
                            ((TV)deviceDict[commands[1]]).DecreaseVolume();
                        }
                        else if (commands[1].StartsWith("Radio"))
                        {
                            ((Radio)deviceDict[commands[1]]).DecreaseVolume();
                        }
                        break; 

                   case "hsv":
                        Console.WriteLine("Введите уровень громкости (0..45)");
                        string tempVol = Console.ReadLine();
                        if (Int32.TryParse(tempVol, out temp))
                        {
                            if (commands[1].StartsWith("TV"))
                            {
                                ((TV)deviceDict[commands[1]]).HandSetVolume(temp);
                            }
                            else if (commands[1].StartsWith("Radio"))
                            {
                                ((Radio)deviceDict[commands[1]]).HandSetVolume(temp);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверно введено значение.");
                        }
                        break;

                    case "br":
                        if (commands[1].StartsWith("TV"))
                        {
                            ((TV)deviceDict[commands[1]]).SetMaxMode();
                        }
                        else if (commands[1].StartsWith("Lamp"))
                        {
                            ((Illuminator)deviceDict[commands[1]]).SetMaxMode();
                        }
                        break;
                    case "md":
                        if (commands[1].StartsWith("TV"))
                        {
                            ((TV)deviceDict[commands[1]]).SetMiddleMode();
                        }
                        else if (commands[1].StartsWith("Lamp"))
                        {
                            ((Illuminator)deviceDict[commands[1]]).SetMiddleMode();
                        }
                        break;
                    case "lw":
                        if (commands[1].StartsWith("TV"))
                        {
                            ((TV)deviceDict[commands[1]]).SetMinMode();
                        }
                        else if (commands[1].StartsWith("Lamp"))
                        {
                            ((Illuminator)deviceDict[commands[1]]).SetMinMode();
                        }
                        break;
                    case "def":
                        if (commands[1].StartsWith("TV"))
                        {
                            ((TV)deviceDict[commands[1]]).SetAutoMode();
                        }
                        else if (commands[1].StartsWith("Lamp"))
                        {
                            ((Illuminator)deviceDict[commands[1]]).SetAutoMode();
                        }

                        break;
                    default:
                        Help();
                        break;
                }
            }
        }
        private static void Help()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Доступные команды для:");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nОбогреватель/Кондиционер/Телевизор/Радио:");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\tadd nameDevice - Добавить девайс в список");
            Console.WriteLine("\tdel nameDevice - Удалить девайс из списка");
            Console.WriteLine("\ton nameDevice  - Включить девайс");
            Console.WriteLine("\toff nameDevice - Выключить девайс");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nОбогреватель/Кондиционер:");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\ttur nameDevice - переводит девайс в режим Турбо");
            Console.WriteLine("\teco nameDevice - переводит девайс в режим Эко");
            Console.WriteLine("\tlow nameDevice - переводит девайс в режим Слабый");
            Console.WriteLine("\taut nameDevice - переводит девайс в режим Авто");
            Console.WriteLine("\tinc nameDevice - повысить температуру девайса");
            Console.WriteLine("\tdec nameDevice - понизить температуру девайса");
            Console.WriteLine("\thse nameDevice - ручная настройка уровня температуры/ влажности девайса");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nТелевизор/Радио:");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\tnxt nameDevice - переключить на следующий канал/волну");
            Console.WriteLine("\tprv nameDevice - переключить на предыдущий канал/волну");
            Console.WriteLine("\thsc nameDevice - ручной выбор канала/волны");
            Console.WriteLine("\tscl nameDevice - список каналов");
            Console.WriteLine("\tinv nameDevice - увеличить громкость звука");
            Console.WriteLine("\tdev nameDevice - уменьшить громкость звука");
            Console.WriteLine("\thsv nameDevice - ручной выбор уровня громкости");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nТелевизор/Лампа:");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\tbr nameDevice - переводит девайс в режим Ярко");
            Console.WriteLine("\tmd nameDevice - переводит девайс в режим Средняя яркость");
            Console.WriteLine("\tlw nameDevice - переводит девайс в режим Тускло");
            Console.WriteLine("\tdef nameDevice - переводит девайс в режим По умолчанию");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\texit  - Выход из меню");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadLine();
        }
    }
}
