using System;
using System.Drawing;
using System.IO;

namespace Editor
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "Image Editor";

            Console.WriteLine("Image Editor for custom items by kevz#1227 \n");

            if (args.Length == 0)
            {
                Console.WriteLine("File not found! Don't open this app without dragging image, instead drag ur png into the .exe and it will automatically opened for you");
                Console.ReadKey();
                Environment.Exit(1);
            }

            if (args.Length == 1)
            {
                string path = args[0];

                if (path.EndsWith(".png") || path.EndsWith(".jpg"))
                {

                    Image image = Image.FromFile(path);

                    if (image.Width != 1024 && image.Height != 1024)
                    {
                        Console.WriteLine($"File not supported! Make sure its 1024x1024 pixels! Current pixels : {image.Size.Width}x{image.Size.Height}");
                        Console.ReadKey();
                        return;
                    }

                    Console.WriteLine("File supported!");
                    Console.WriteLine("Drag your custom 32x32 pixels to this app, then press enter!");

                    string path2 = Console.ReadLine();

                    if (File.Exists(path2))
                    {
                        if (path2.EndsWith(".png") || path2.EndsWith(".jpg"))
                        {

                            Image toPut = Image.FromFile(path2);

                            if (toPut.Width == 32 && toPut.Height == 32)
                            {

                                Console.WriteLine("Fill in the image position ! (STARTS AT 0)");

                                Console.Write("\nPosition X : ");
                                string strX = Console.ReadLine();

                                Console.Write("Position Y : ");
                                string strY = Console.ReadLine();

                                if (int.TryParse(strX, out var x) && int.TryParse(strY, out var y))
                                {
                                    Console.WriteLine("Processing!");
                                    ProcessImage(image, toPut, x, y);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid x / y input!");
                                    Console.ReadKey();
                                    return;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"File not supported! Make sure its 32x32 pixels! Current pixels : {toPut.Width}x{toPut.Height}");
                                Console.ReadKey();
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not supported file extension! Make sure it is .png or .jpg");
                            Console.ReadKey();
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Path to that file not found!");
                        Console.ReadKey();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Not supported file extension! Make sure it is .png or .jpg");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.ReadKey();
                return;
            }
        }

        private static void ProcessImage(Image image, Image toPut, int x, int y)
        {
            try
            {
                Graphics g = Graphics.FromImage(image);

                g.DrawImage(toPut, x * 32, y * 32);

                if (File.Exists("result.png")) File.Delete("result.png");

                image.Save("result.png");

                Console.WriteLine("Image saved!");
                Console.WriteLine("Don't forget to credit me ! (kevz #1227)");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured at processing image! Please try again..");
                Console.WriteLine($"Error message: \"{e.Message}\"");
                Console.ReadKey();
                return;
            }
        }
    }
}
