using System;
using System.Collections.Generic;
using System.IO;

namespace HomeFlooringInformation
{
   /*
   ****************************************************************
   * Title: Home Flooring Information                             *
   * Application Type: (framework – Console, WinForms, UWP, etc.) *
   * Description: This application collects information from the  *
   *            user to calculate the square footage of a home    *
   *            and provide them with the cost to floor it.       *
   * Author: Elliott, Kelsi                                       *
   * Date Created: 06/25/2020                                     *
   * Last Modified: 06/28/2020                                    *
   ****************************************************************
   */

    class Program
    {
        static void Main(string[] args)
        {
            SetTheme();
            DisplayWelcomeScreen();
            DisplayMainMenu();
            DisplayClosingScreen();
        }

        /// <summary>
        /// ***********************************************************
        /// *                         Main Menu                       *
        /// ***********************************************************
        /// </summary>
        static void DisplayMainMenu()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;
            int numberOfRooms = 0;
            double flooringCost = 0;
            double totalCost = 0;

            do
            {
                DisplayScreenHeader("Main Menu");

                // get user menu choice
                Console.WriteLine("\ta) Number of Rooms");
                Console.WriteLine("\tb) Input Home Dimensions");
                Console.WriteLine("\tc) Flooring Selections");
                Console.WriteLine("\td) View Selctions");
                Console.WriteLine("\te) Cost Breakdown");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                // process user menu choice
                switch (menuChoice)
                {
                    case "a":
                        numberOfRooms = DisplayNumberOfRooms();
                        break;

                    case "b":
                        DisplayInputHomeDimensions(numberOfRooms);
                        break;

                    case "c":
                        flooringCost = DisplayFloorSelections(flooringCost);
                        break;

                    case "d":
                        DisplayViewSelections(numberOfRooms, flooringCost);
                        break;

                    case "e":
                        totalCost = DisplayCostBreakDown(numberOfRooms, flooringCost, totalCost);
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region APPLICATION
        static int DisplayNumberOfRooms()
        {
            int numberOfRooms;
            bool validResponse;
            string userResponse;
            DisplayScreenHeader("Number of Rooms");

            do
            {
                validResponse = true;

                Console.WriteLine();
                Console.WriteLine("\tHow many rooms would you like to input dimentions for?");
                userResponse = Console.ReadLine();

                if (!int.TryParse(userResponse, out numberOfRooms))
                {
                    validResponse = false;

                    Console.WriteLine();
                    Console.WriteLine("It appears you have entered an invalid number for rooms." +
                    "Please enter a positive integer.");

                    Console.WriteLine();
                    Console.WriteLine("\tPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (!validResponse);

            Console.WriteLine($"You will input dimensions for {numberOfRooms} room(s).");

            DisplayContinuePrompt();
            return numberOfRooms;
        }

        static void DisplayInputHomeDimensions(int numberOfRooms)
        {
            string dataPath = @"Data/Dimensions.txt";
            string roomDimensions;
            string roomName;
            string length;
            string width;
            double length2;
            double width2;
            string userResponse;
            bool validResponse;

            DisplayScreenHeader("Input Home Dimensions");

            for (int i = 0; i < numberOfRooms; i++)
            {
                Console.WriteLine("\tEnter room name:");
                roomName = Console.ReadLine();

                do
                {
                    validResponse = true;

                    Console.WriteLine("\tEnter length (in feet):");
                    userResponse = Console.ReadLine();

                    if (!double.TryParse(userResponse, out length2))
                    {
                        validResponse = false;

                        Console.WriteLine();
                        Console.WriteLine("It appears you have entered an invalid number for length." +
                        "Please enter a positive number.");

                        Console.WriteLine();
                        Console.WriteLine("\tPress any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while (!validResponse);
                length = length2.ToString();

                do
                {
                    validResponse = true;

                    Console.WriteLine("\tEnter width (in feet):");
                    userResponse = Console.ReadLine();

                    if (!double.TryParse(userResponse, out width2))
                    {
                        validResponse = false;

                        Console.WriteLine();
                        Console.WriteLine("It appears you have entered an invalid number for width." +
                        "Please enter a positive number.");

                        Console.WriteLine();
                        Console.WriteLine("\tPress any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while (!validResponse);
                width = width2.ToString();
                roomDimensions = roomName + "," + length + "," + width;
                File.AppendAllText(dataPath, roomDimensions + "\n");
            }

            DisplayContinuePrompt();
            Console.Clear();

        }

        static double DisplayFloorSelections(double flooringCost)
        {
            bool validResponse;
            string userResponse;
            DisplayScreenHeader("Flooring Selection");

            do
            {
                validResponse = true;

                Console.WriteLine();
                Console.WriteLine("\tEnter price per square foot of flooring choice:");
                userResponse = Console.ReadLine();

                if (!double.TryParse(userResponse, out flooringCost))
                {
                    validResponse = false;

                    Console.WriteLine();
                    Console.WriteLine("It appears you have entered an invalid number for the flooring cost." +
                    "Please enter a positive number.");

                    Console.WriteLine();
                    Console.WriteLine("\tPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (!validResponse);

            Console.WriteLine($"The cost of flooring per square foot is ${flooringCost}.");

            DisplayContinuePrompt();
            return flooringCost;
        }

        static List<(string roomName, string length, string width)> DisplayViewSelections(int numberOfRooms, double flooringCost)
        {
            string dataPath = @"Data/Dimensions.txt";
            string[] dimensionsArray = new string[numberOfRooms];
            (string roomName, string length, string width) dimensionsTuple;
            List<(string roomName, string length, string width)> dimensionsInfo =
                new List<(string roomName, string length, string width)>();
            dimensionsArray = File.ReadAllLines(dataPath);

            DisplayScreenHeader("Rooms in Home");

            foreach (string dimensionsInfoText in dimensionsArray)
            {
                dimensionsArray = dimensionsInfoText.Split(',');

                dimensionsTuple.roomName = dimensionsArray[0];
                dimensionsTuple.length = dimensionsArray[1];
                dimensionsTuple.width = dimensionsArray[2];

                dimensionsInfo.Add(dimensionsTuple);

                Console.WriteLine($"\tRoom Name: {dimensionsArray[0]}");
                Console.WriteLine();
                Console.WriteLine($"\tLength: {dimensionsArray[1]}");
                Console.WriteLine($"\tWidth: {dimensionsArray[2]}");
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("\t\tFlooring Choice");
            Console.WriteLine($"\tThe price per square foot is ${flooringCost}");

            DisplayMenuPrompt("Main Menu");
            return dimensionsInfo;
        }

        static double DisplayCostBreakDown(int numberOfRooms, double flooringCost, double totalCost)
        {
            double length2;
            double width2;
            double roomCost;
            string dataPath = @"Data/Dimensions.txt";
            string[] dimensionsArray = new string[numberOfRooms];
            (string roomName, string length, string width) dimensionsTuple;
            List<(string roomName, string length, string width)> dimensionsInfo =
                new List<(string roomName, string length, string width)>();
            dimensionsArray = File.ReadAllLines(dataPath);

            DisplayScreenHeader("Cost to Floor Breakdown");

            //display table headers
            Console.WriteLine(
                "Room Type".PadLeft(15) +
                "Dimensions".PadLeft(15) +
                "Cost to Floor".PadLeft(15)
                );
            Console.WriteLine(
                "-----------".PadLeft(15) +
                "-----------".PadLeft(15) +
                "-----------".PadLeft(15)
                );
            foreach (string dimensionsInfoText in dimensionsArray)
            {
                dimensionsArray = dimensionsInfoText.Split(',');

                dimensionsTuple.roomName = dimensionsArray[0];
                dimensionsTuple.length = dimensionsArray[1];
                dimensionsTuple.width = dimensionsArray[2];

                length2 = double.Parse(dimensionsArray[1]);
                width2 = double.Parse(dimensionsArray[2]);
                roomCost = (length2 * width2 * flooringCost);

                dimensionsInfo.Add(dimensionsTuple);

                Console.WriteLine(
                dimensionsArray[0].PadLeft(15) +
                $"{dimensionsArray[1]} X {dimensionsArray[2]}".PadLeft(15) +
                $"${roomCost}".PadLeft(15)
                );
            }

            totalCost = CalculateCost(numberOfRooms, flooringCost);

            Console.WriteLine($"The total cost to floor your home is ${totalCost}");


            DisplayContinuePrompt();
            return totalCost;
        }

        static double CalculateCost(int numberOfRooms, double flooringCost)
        {
            double totalLength = 0;
            double totalWidth = 0;
            double length2;
            double width2;
            double roomArea;
            double totalCost;
            string dataPath = @"Data/Dimensions.txt";
            string[] dimensionsArray = new string[numberOfRooms];
            (string roomName, string length, string width) dimensionsTuple;
            List<(string roomName, string length, string width)> dimensionsInfo =
                new List<(string roomName, string length, string width)>();
            dimensionsArray = File.ReadAllLines(dataPath);

            foreach (string dimensionsInfoText in dimensionsArray)
            {
                dimensionsArray = dimensionsInfoText.Split(',');

                dimensionsTuple.roomName = dimensionsArray[0];
                dimensionsTuple.length = dimensionsArray[1];
                dimensionsTuple.width = dimensionsArray[2];

                length2 = double.Parse(dimensionsArray[1]);
                width2 = double.Parse(dimensionsArray[2]);
                roomArea = (length2 * width2 * flooringCost);

                dimensionsInfo.Add(dimensionsTuple);

                totalLength += length2;
                totalWidth += width2;
            }

            totalCost = (totalLength * totalWidth * flooringCost);
            return totalCost;
        }

        #endregion

        #region USER INTERFACE
        /// <summary>
        /// ***********************************************************
        /// *                         Set Theme                       *
        /// ***********************************************************
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.BackgroundColor = ConsoleColor.Black;

        }

        /// <summary>
        /// ***********************************************************
        /// *                       Welcome Screen                    *
        /// ***********************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.WriteLine();
            Console.WriteLine("\t\t\t Home Flooring Information Application");
            Console.WriteLine("\tThis application will calculate the square footage of a home based on user input");
            Console.WriteLine("\tin order to calulate the cost to floor the home. The application will show the user");
            Console.WriteLine("\tthe cost to floor each room as well as a grand total.");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// ***********************************************************
        /// *                     Continue Prompt                     *
        /// ***********************************************************
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// ***********************************************************
        /// *                    Display Menu Prompt                  *
        /// ***********************************************************
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// ***********************************************************
        /// *                   Display Screen Header                 *
        /// ***********************************************************
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"\t\tThank you for using the Home Flooring Calculator!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        #endregion
    }

}
