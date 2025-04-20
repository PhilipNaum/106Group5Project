using System.Runtime.InteropServices;

namespace EnemyWriter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Filename?: ");
            string fileName = CleanResponse()+".data";
            int enemyCount = 0;
            string[] questions = { "Starting X pos (in tiles)?: ", "Starting Y pos (in tiles)?: ", "Range (in tiles)?: ","Size?: ", "Health?: " };
            FileStream outStream = File.OpenWrite(fileName);
            BinaryWriter output = new BinaryWriter(outStream);

            Console.Write("How many enemies?: ");
            enemyCount = int.Parse(CleanResponse());
            output.Write(enemyCount);
            Console.WriteLine();

            for (int i = 0; i < enemyCount; i++)
            {
                for (int j = 0; j < questions.Length; j++)
                {
                    Console.Write(questions[j]);
                    if (j<4)
                    {
                        try
                        {
                            output.Write(32 * int.Parse(CleanResponse()));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    else
                    {
                        try
                        {
                            output.Write(int.Parse(CleanResponse()));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
                Console.WriteLine();
            }
            output.Close();

            Console.WriteLine("Successfully created enemy info file!");
        }

        /// <summary>
        /// returns a response in all lower case with no spaces
        /// </summary>
        /// <returns></returns>
        public static string CleanResponse()
        {
            return Console.ReadLine()!.Trim();
        }
    }
}
