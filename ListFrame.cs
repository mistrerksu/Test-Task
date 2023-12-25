using System;
using System.Collections.Generic;
using System.IO;

namespace Test_Task
{

    class ListFrame
    {
        private static List<FrameProperties> list = new List<FrameProperties>();

        enum DictionaryData
        {
            Version,        // Версия 
            Identifier,     // Идентификатор
            Null,           // Ненужная информация
            Address,        // Адрес
            Team,           // Команда
            DataLen,        // Длина данных
            Data,           // Данные
            CheckSum,       // Контрольная сумма
            TerminalSign    // Признак окончания
        }

        public void LoadFromFile(string fileName)
        {

            // длины свойств (байтов) по порядку перечисления DictionaryData
            int[] countBytes = {2, 2, 18, 2, 2, 4, 0, 2, 2 };
            Dictionary<string, int> dictionaryData = new Dictionary<string, int>();

            int index = 0;

            // заполнения словаря
            foreach (string keys in Enum.GetNames(typeof(DictionaryData)))
            {
                dictionaryData.Add(keys, countBytes[index]);
                index++;
            }

            // массив байтов из файла
            byte[] dataInFile = File.ReadAllBytes(fileName);

            string key = "Version";

            // количество байтов для свойства
            int countBytesKeys = dictionaryData[key];
            int Id = 1;

            FrameProperties frameProperties = new FrameProperties();

            for (int iterationData = 0; iterationData < dataInFile.Length; iterationData++)
            {
                switch (key)
                {
                    case "Version":
                        frameProperties.version += dataInFile[iterationData].ToString("X2") + " ";
                        break;
                    case "Identifier":
                        frameProperties.identifier += dataInFile[iterationData].ToString("X2") + " ";
                        break;
                    case "Address":
                        frameProperties.address += dataInFile[iterationData].ToString("X2") + " ";
                        break;
                    case "Team":
                        frameProperties.team += dataInFile[iterationData].ToString("X2") + " ";
                        break;
                    case "DataLen":
                        frameProperties.dataLen += dataInFile[iterationData].ToString("X2");
                        break;
                    case "Data":
                        frameProperties.data += dataInFile[iterationData].ToString("X2") + " ";
                        break;
                    case "CheckSum":
                        frameProperties.checkSum += dataInFile[iterationData].ToString("X2") + " ";
                        break;
                    case "TerminalSign":
                        frameProperties.terminalSign += dataInFile[iterationData].ToString("X2") + " ";
                        break;
                }

                countBytesKeys--;

                // когда нужное количество байтов записалось в свойство переходим к след свойству
                if (countBytesKeys <= 0)
                {
                    key = NextKey(key, dictionaryData);
                    countBytesKeys = dictionaryData[key];

                    // Если начался новый кадр:
                    // - индекс кадра меняется
                    // - созданный экземпляр добавляется в список
                    // - создается новый экземпляр

                    if (key == "Version")
                    {
                        frameProperties.id = Id;
                        Id++;
                        list.Add(frameProperties);
                        frameProperties = new FrameProperties();
                    }

                    // Динамическое вычисление длины Данных по свойству "dataLen"
                    if (key == "Data")
                    {
                        countBytesKeys = Convert.ToInt32(frameProperties.dataLen, 16);

                        // Если длина данных = 0, переходим к заполнению след свойства
                        if (countBytesKeys == 0)
                        {
                            key = NextKey(key, dictionaryData);
                            countBytesKeys = dictionaryData[key];
                        }
                    }
                }
            }
        }

        public void SaveToText(string txtfileName)
        {
            // полная перезапись файла
            using (StreamWriter writer = new StreamWriter(txtfileName, false))
            {
                writer.WriteLine("Индекс кадра - Идентификатор - Адрес - Команда  -  Длина данных");

                foreach (var frame in list)
                {
                    writer.WriteLine(frame.id + " -  " + frame.identifier + "  - " + frame.address + " - " + frame.team + " - " + frame.dataLen);
                }
            }

            Console.WriteLine("Файл успешно записан !");
            Console.Read();

        }

        // Метод для определения следующего по списку ключа из словаря
        // Если элемент последний, то возвращается первый элемент "Version"
        private static string NextKey(string key, Dictionary<string, int> dictionaryData)
        {

            bool equality = false;
            string str = "Version";
            foreach (string dicKey in dictionaryData.Keys)
            {
                if (equality)
                {
                    str = dicKey;
                    break;
                }

                if (key == dicKey)
                {
                    equality = true;
                }
            }

            return str;
        }
    }
}
