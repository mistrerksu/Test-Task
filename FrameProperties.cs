using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task
{
    internal class FrameProperties
    {
        private int Id;                     // Индекс кадра 
        private string Version;             // Версия 
        private string IntIdentifier;       // Идентификатор
        private string Address;             // Адрес
        private string Team;                // Команда
        private string DataLen;             // Длина данных
        private string Data;                // Данные
        private string CheckSum;            // Контрольная сумма
        private string TerminalSign;        // Признак окончания


        public int id { get => Id; set => Id = value; }
        public string version { get => Version; set => Version = value; }
        public string identifier { get => IntIdentifier; set => IntIdentifier = value; }
        public string address { get => Address; set => Address = value; }
        public string team { get => Team; set => Team = value; }
        public string dataLen { get => DataLen; set => DataLen = value; }
        public string data { get => Data; set => Data = value; }
        public string checkSum { get => CheckSum; set => CheckSum = value; }
        public string terminalSign { get => TerminalSign; set => TerminalSign = value; }


    }
}
