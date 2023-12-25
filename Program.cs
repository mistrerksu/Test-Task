using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Test_Task
{
    internal class Program
    {
        static ListFrame listCl;

        static void Main(string[] args)
        {
            listCl = new ListFrame();
            string path = "E:\\задача\\test.raw";
            string pathTxt = "E:\\задача\\test.txt";

            listCl.LoadFromFile(path);
            listCl.SaveToText(pathTxt);
        }

    }
}
