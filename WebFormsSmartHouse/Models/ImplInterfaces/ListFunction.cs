﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSmartHouse1._0
{
   class ListFunction : IListOrderAble
    {
        public string ShowList(List<string> tempList, string tempListString)
        {
            for (int i = 0; i < tempList.Count; i++)
            {
                tempListString += "\n" + i + " - " + tempList[i];
            }
            return tempListString;
        }
        public void ListLoad(List<string> tempListLoad, string tempListLocation)
        {
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(tempListLocation, System.Text.Encoding.Default))
                {
                    while ((text = sr.ReadLine()) != null)
                    {
                        tempListLoad.Add(text);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
//для записи в файл
//using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
//{
//    sw.WriteLine(text);
//}
//using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
//{
//    sw.WriteLine("Дозапись");
//    sw.Write(4.5);
//}
//}
//catch (Exception e)
//{
//    Console.WriteLine(e.Message);
//}