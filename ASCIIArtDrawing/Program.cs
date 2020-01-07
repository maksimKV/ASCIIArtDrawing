using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASCIIArtDrawing
{
    class Program
    {
        private static int height = 0;
        private static int width = 0;
        private static string colour = "x";
        private static HashSet<Point> FilledPoints = new HashSet<Point>();

        static void Main(string[] args)
        {
            string welcome = "Welcome to my ASCII drawing program. To use this program you will have to enter one of the following codes. \"N (width) (height)\" in order to draw a new canvas with the given dimensions. \"C (colour)\" sets the value of the colour. \"L x1 y1 x2 y2\" Create a new line from (x1,y1) to (x2,y2). Lines will be drawn using the selected colour. \"R x1 y1 x2 y2\" Create a new rectangle from (left,top) (x1,y1) to (right,bottom) (x2,y2). Lines will be drawn using the selected colour. \"B x y\" Fill the entire area connected to (x,y) with the selected color. This is the same as a \"bucket fill\" tool in paint programs. \"Q\" To quit the program.";

            Console.Write(WordWrap(welcome));

            string userInput = Console.ReadLine();
            ValidateCommand(userInput);
        }

        /// <summary>
        /// Wraps a string into paragraph.
        /// </summary>
        /// <param name="paragraph">String to wrap.</param>
        private static string WordWrap(string paragraph)
        {
            paragraph = new Regex(@" {2,}").Replace(paragraph.Trim(), @" ");
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            List<string> lines = new List<string>();

            for (var i = 0; paragraph.Length > 0; i++)
            {
                lines.Add(paragraph.Substring(0, Math.Min(Console.WindowWidth, paragraph.Length)));
                int length = lines[i].LastIndexOf(" ", StringComparison.Ordinal);
                if (length > 0) lines[i] = lines[i].Remove(length);
                paragraph = paragraph.Substring(Math.Min(lines[i].Length + 1, paragraph.Length));
                Console.SetCursorPosition(left, top + i); Console.WriteLine(lines[i]);
            }

            return paragraph;
        }

        /// <summary>
        /// Creates new canvas based of user input.
        /// </summary>
        /// <param name="userInput">User input data.</param>
        private static void NewCanvas(string userInput)
        {
            string[] userData = userInput.Split(null);

            if(userData.Length < 3)
            {
                Console.WriteLine("Wrong canvas dimension or command order. Please try again.");
                string newInput = Console.ReadLine();
                ValidateCommand(newInput);
            }
            else
            {
                int newWidth;
                bool isWidthNumeric = int.TryParse(userData[1], out newWidth);

                int newHeight;
                bool isHeightNumeric = int.TryParse(userData[2], out newHeight);

                if (isWidthNumeric && isHeightNumeric)
                {
                    width = newWidth;
                    height = newHeight;

                    Console.WriteLine("New canvas has been generated with the following parameters. Width: " + width + " Height: " + height);
                    Draw();

                    string newInput = Console.ReadLine();
                    ValidateCommand(newInput);
                }
                else
                {
                    Console.WriteLine("Wrong canvas command order or missing parameters. Please try again.");
                    string newInput = Console.ReadLine();
                    ValidateCommand(newInput);
                }
            }
        }

        /// <summary>
        /// Changes colour based of user input.
        /// </summary>
        /// <param name="userInput">User input data.</param>
        private static void ChangeColour(string userInput)
        {
            string[] userData = userInput.Split(null);

            if (userData.Length < 2)
            {
                Console.WriteLine("Wrong colour value or command order. Please try again.");
                string newInput = Console.ReadLine();
                ValidateCommand(newInput);
            }
            else
            {
                string newColour = userData[1].Substring(0, 1);
                colour = newColour;

                Console.WriteLine("New colour has been created with the value of " + colour);
                string newInput = Console.ReadLine();
                ValidateCommand(newInput);
            }
        }

        /// <summary>
        /// Draws a line. Only horizontal or vertical lines are supported.
        /// </summary>
        /// <param name="userInput">User input data.</param>
        private static void DrawLine(string userInput)
        {
            string[] userData = userInput.Split(null);

            if (userData.Length < 5)
            {
                Console.WriteLine("Wrong line value or command order. Please try again.");
                string newInput = Console.ReadLine();
                ValidateCommand(newInput);
            }
            else
            {
                int y1;
                bool isY1Numeric = int.TryParse(userData[1], out y1);

                int x1;
                bool isX1Numeric = int.TryParse(userData[2], out x1);

                int y2;
                bool isY2Numeric = int.TryParse(userData[3], out y2);

                int x2;
                bool isX2Numeric = int.TryParse(userData[4], out x2);

                if (isX1Numeric && isY1Numeric && isX2Numeric && isY2Numeric)
                {
                    if (x1 == x2) // Verical line
                    {
                        for (int i = y1; y2 >= i; i++)
                        {
                            Point currentPoint = new Point(x1, i, colour);
                            FilledPoints.Add(currentPoint);
                        }

                        Console.WriteLine("New vertical line has been added");
                        Draw();

                        string newInput = Console.ReadLine();
                        ValidateCommand(newInput);
                    }
                    else if (y1 == y2) // Horizontal line
                    {
                        for (int i = x1; x2 >= i; i++)
                        {
                            Point currentPoint = new Point(i, y1, colour);
                            FilledPoints.Add(currentPoint);
                        }

                        Console.WriteLine("New vertical line has been added");
                        Draw();

                        string newInput = Console.ReadLine();
                        ValidateCommand(newInput);
                    }
                    else
                    {
                        Console.WriteLine("Only horizontal or vertical lines are supported. Please try again.");
                        string newInput = Console.ReadLine();
                        ValidateCommand(newInput);
                    }
                }
                else
                {
                    Console.WriteLine("Wrong line command order or missing parameters. Please try again.");
                    string newInput = Console.ReadLine();
                    ValidateCommand(newInput);
                }
            }
        }

        /// <summary>
        /// Draws a rectangle from the user input.
        /// </summary>
        /// <param name="userInput">User input data.</param>
        private static void DrawRectangle(string userInput)
        {
            string[] userData = userInput.Split(null);

            if (userData.Length < 5)
            {
                Console.WriteLine("Wrong line value or command order. Please try again.");
                string newInput = Console.ReadLine();
                ValidateCommand(newInput);
            }
            else
            {
                int y1;
                bool isY1Numeric = int.TryParse(userData[1], out y1);

                int x1;
                bool isX1Numeric = int.TryParse(userData[2], out x1);

                int y2;
                bool isY2Numeric = int.TryParse(userData[3], out y2);

                int x2;
                bool isX2Numeric = int.TryParse(userData[4], out x2);

                if (isX1Numeric && isY1Numeric && isX2Numeric && isY2Numeric)
                {
                    for (int i = x1; i <= x2; i++)
                    {
                        Point currentPoint = new Point(i, y1, colour);
                        FilledPoints.Add(currentPoint);
                    }

                    for (int n = y1; n <= y2; n++)
                    {
                        Point currentPoint = new Point(x1, n, colour);
                        FilledPoints.Add(currentPoint);
                    }

                    for (int m = x2; m >= x1; m--)
                    {
                        Point currentPoint = new Point(m, y2, colour);
                        FilledPoints.Add(currentPoint);
                    }

                    for (int z = y2; z >= y1; z--)
                    {
                        Point currentPoint = new Point(x2, z, colour);
                        FilledPoints.Add(currentPoint);
                    }

                    Console.WriteLine("New rectangle has been added");
                    Draw();

                    string newInput = Console.ReadLine();
                    ValidateCommand(newInput);
                }
                else
                {
                    Console.WriteLine("Wrong line command order or missing parameters. Please try again.");
                    string newInput = Console.ReadLine();
                    ValidateCommand(newInput);
                }
            }
        }

        /// <summary>
        /// Fills spaces with colour from the user input.
        /// </summary>
        /// <param name="userInput">User input data.</param>
        private static void FillColour(string userInput)
        {
            string[] userData = userInput.Split(null);

            if (userData.Length < 2)
            {
                Console.WriteLine("Wrong command order or value. Please try again.");
                string newInput = Console.ReadLine();
                ValidateCommand(newInput);
            }
            else
            {
                int newX;
                bool isWidthNumeric = int.TryParse(userData[1], out newX);

                int  newY;
                bool isHeightNumeric = int.TryParse(userData[2], out newY);

                if (isWidthNumeric && isHeightNumeric)
                {
                    if (FilledPoints.Any(p => p.Height == newY && p.Width == newX))
                    {
                        Console.WriteLine("There is already a shape on these coordinates. Only empty spaces can be filled. Please try again.");
                        string newInput = Console.ReadLine();
                        ValidateCommand(newInput);
                    }
                    else
                    {
                        // Adding points to the bottom
                        for (int i = newY; i <= height + 1; i++)
                        {
                            HashSet<Point> temporary1Points = new HashSet<Point>();
                            HashSet<Point> temporary2Points = new HashSet<Point>();

                            bool path1blocked = false;
                            bool path2blocked = false;

                            for (int n = newX; n <= width; n++)
                            {
                                Point newPoint = new Point(i, n, colour);

                                if (!FilledPoints.Any(p => p.Height == i && p.Width == n) && !path1blocked)
                                {
                                    FilledPoints.Add(newPoint);
                                }
                                else if (FilledPoints.Any(p => p.Height == i && p.Width == n) && !path1blocked)
                                {
                                    path1blocked = true;
                                }
                                else if (!FilledPoints.Any(p => p.Height == i && p.Width == n) && path1blocked)
                                {
                                    temporary1Points.Add(newPoint);
                                }
                                else if (FilledPoints.Any(p => p.Height == i && p.Width == n) && path1blocked)
                                {
                                    path1blocked = false;
                                    temporary1Points = new HashSet<Point>();
                                }

                                if (n == width && !path1blocked)
                                {
                                    foreach (Point point in temporary1Points)
                                    {
                                        FilledPoints.Add(point);
                                    }
                                }
                            }

                            for (int m = newX + 1; m >= 0; m--)
                            {
                                Point newPoint = new Point(i, m, colour);

                                if (!FilledPoints.Any(p => p.Height == i && p.Width == m) && !path2blocked)
                                {
                                    FilledPoints.Add(newPoint);
                                }
                                else if (FilledPoints.Any(p => p.Height == i && p.Width == m) && !path2blocked)
                                {
                                    path2blocked = true;
                                }
                                else if (!FilledPoints.Any(p => p.Height == i && p.Width == m) && path2blocked)
                                {
                                    temporary2Points.Add(newPoint);
                                }
                                else if (FilledPoints.Any(p => p.Height == i && p.Width == m) && path2blocked)
                                {
                                    path2blocked = false;
                                    temporary2Points = new HashSet<Point>();
                                }

                                if (m == 0 && !path2blocked)
                                {
                                    foreach (Point point in temporary2Points)
                                    {
                                        FilledPoints.Add(point);
                                    }
                                }
                            }
                        }

                        // Addig points to the top
                        for (int i = newY; i >= 0; i--)
                        {
                            HashSet<Point> temporary1Points = new HashSet<Point>();
                            HashSet<Point> temporary2Points = new HashSet<Point>();

                            bool path1blocked = false;
                            bool path2blocked = false;

                            for (int n = newX; n <= width; n++)
                            {
                                Point newPoint = new Point(i, n, colour);

                                if (!FilledPoints.Any(p => p.Height == i && p.Width == n) && !path1blocked)
                                {
                                    FilledPoints.Add(newPoint);
                                }
                                else if (FilledPoints.Any(p => p.Height == i && p.Width == n) && !path1blocked)
                                {
                                    path1blocked = true;
                                }
                                else if (!FilledPoints.Any(p => p.Height == i && p.Width == n) && path1blocked)
                                {
                                    temporary1Points.Add(newPoint);
                                }
                                else if (FilledPoints.Any(p => p.Height == i && p.Width == n) && path1blocked)
                                {
                                    path1blocked = false;
                                    temporary1Points = new HashSet<Point>();
                                }

                                if (n == width && !path1blocked)
                                {
                                    foreach (Point point in temporary1Points)
                                    {
                                        FilledPoints.Add(point);
                                    }
                                }
                            }

                            for (int m = newX; m >= 0; m--)
                            {
                                Point newPoint = new Point(i, m, colour);

                                if (!FilledPoints.Any(p => p.Height == i - 1 && p.Width == m) && !path2blocked)
                                {
                                    FilledPoints.Add(newPoint);
                                }
                                else if (FilledPoints.Any(p => p.Height == i - 1 && p.Width == m) && !path2blocked)
                                {
                                    path2blocked = true;
                                }
                                else if (!FilledPoints.Any(p => p.Height == i - 1 && p.Width == m) && path2blocked)
                                {
                                    temporary2Points.Add(newPoint);
                                }
                                else if (FilledPoints.Any(p => p.Height == i - 1 && p.Width == m) && path2blocked)
                                {
                                    path2blocked = false;
                                    temporary2Points = new HashSet<Point>();
                                }

                                if (m == 0 && !path2blocked)
                                {
                                    foreach (Point point in temporary2Points)
                                    {
                                        FilledPoints.Add(point);
                                    }
                                }
                            }
                        }

                        Console.WriteLine("Space has been filled.");
                        Draw();

                        string newInput = Console.ReadLine();
                        ValidateCommand(newInput);
                    }
                }
                else
                {
                    Console.WriteLine("Wrong command order or missing parameters. Please try again.");
                    string newInput = Console.ReadLine();
                    ValidateCommand(newInput);
                }
            }
        }

        /// <summary>
        /// Validates the command.
        /// </summary>
        /// <param name="userInput">User input data.</param>
        private static void ValidateCommand(string userInput)
        {
            string command = userInput.Substring(0, 1);

            if (command == "N" || command == "n")
            {
                NewCanvas(userInput);
            }
            else if (command == "C" || command == "c")
            {
                ChangeColour(userInput);
            }
            else if (command == "L" || command == "l")
            {
                DrawLine(userInput);
            }
            else if (command == "R" || command == "r")
            {
                DrawRectangle(userInput);
            }
            else if (command == "B" || command == "b")
            {
                FillColour(userInput);
            }
            else if (command == "Q" || command == "q")
            {
                // It's sufficient to leave this empty and the application will close.
            }
            else
            {
                Console.WriteLine("Wrong command. Please try again.");
                string newInput = Console.ReadLine();
                ValidateCommand(newInput);
            } 
         }

        /// <summary>
        /// Draws the current canvas and shapes.
        /// </summary>
        private static void Draw()
        {
            int numColumns = height + 1;
            int numRows = width + 2;

            for (int i = 0; i <= numColumns; i++)
            {
                for (int n = 0; n <= numRows; n++)
                {
                    Point currentPoint = new Point(i, n, colour);

                    if (n == numRows)
                    {
                        Console.WriteLine("\r");
                    } 
                    else if (i == 0 || i == numColumns)
                    {
                        Console.Write("-");
                    }
                    else if (n == 0 || n == numRows - 1)
                    {
                        Console.Write("|");
                    }
                    else if (FilledPoints.Any(p => p.Height == currentPoint.Height && p.Width == currentPoint.Width))
                    {
                        Point foundPoint = FilledPoints.Where(p => p.Height == currentPoint.Height && p.Width == currentPoint.Width).FirstOrDefault();

                        Console.Write(foundPoint.Colour);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
        }
    }
}
