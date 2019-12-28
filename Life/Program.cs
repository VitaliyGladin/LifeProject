using System;
using Life.Model;

namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            int len = 0;

            if (args.Length > 0 && int.TryParse(args[0], out len) && len > 3)
                Field.Init(len);
            else
                Field.Init();
            Console.WriteLine(Field.Instance + "\n");
            while (Field.hasAlive&&Console.Read() != 'q')
            {
                Field.Iterate();
                Console.WriteLine(Field.Instance + "\n");
            }
        }
    }
}
