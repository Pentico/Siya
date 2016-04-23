using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

/**

    Where i didn't comment means is either self explainetory 
    or i already comment the condition somewhere in this class fuck up called a Game !!!!!!!!!!!!
    
    #my little brother can make a better code than this ever heard of OOP ?? 
    #
    # add methods to separate the logic
    # 
*/
/**

    struct Object carries the information about the '@' u control
    x,y are the x and y coordinates
    char c = '@' 
    ConsoleColor DISPLAY '@' YELLOW ETC.

 */
struct Object
{
    public int x;
    public int y;
    public char c;
    public ConsoleColor color;
}

class Program
{
    /**
        METHOD PRINT USES OUTPUTS THE 

    */
    static void PrintOnPosition(int x, int y, char c,
        ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(c);
    }

    static void PrintStringOnPosition(int x, int y, string str,
        ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }

    static void Main()
    {
        double speed = 100.0;
        double acceleration = 0.5;
        int playfieldWidth = 5; //grid width
        int livesCount = 5;
        
        //Dimensions of the console 
        Console.BufferHeight = Console.WindowHeight = 20;
        Console.BufferWidth = Console.WindowWidth = 30;
        //struct named userCar
 
       //Initializing the userCar 
        Object userCar = new Object();
        userCar.x = 2; 
        userCar.y = Console.WindowHeight - 1;
        userCar.c = '@';
        userCar.color = ConsoleColor.Yellow;

        //going to be used to decide if it should make a new object of either '-' ,'#', '*'  ...should use time to make it random      
        Random randomGenerator = new Random();
    
            //objects : '#' ,'-' ,'*'
        List<Object> objects = new List<Object>();

        while (true)
        {
            
            speed += acceleration;
            if (speed > 400)
            {
                speed = 400;
            }

            bool hitted = false;
            {
                //random from 0 to 100
                int chance = randomGenerator.Next(0, 100);
                if (chance < 10)
                {
                    Object newObject = new Object();
                    newObject.color = ConsoleColor.Cyan;
                    newObject.c = '-';
                    //randomize where on th x coordinate should it start to come down
                    newObject.x = randomGenerator.Next(0, playfieldWidth);
                    newObject.y = 0;
                    objects.Add(newObject); //add to the objects list
                }
                else if (chance < 20)
                {
                    Object newObject = new Object();
                    newObject.color = ConsoleColor.Cyan;
                    newObject.c = '*';
                    newObject.x = randomGenerator.Next(0, playfieldWidth);
                    newObject.y = 0;
                    objects.Add(newObject);
                }
                else
                {
                    Object newCar = new Object();
                    newCar.color = ConsoleColor.Green;
                    newCar.x = randomGenerator.Next(0, playfieldWidth);
                    newCar.y = 0;
                    newCar.c = '#';
                    objects.Add(newCar);
                }
            }

            //this function is about moving right or left using arrow keys
            while (Console.KeyAvailable)
            {
                //which key you pressed
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                //while (Console.KeyAvailable) Console.ReadKey(true);
                //if its the leftArrow this if statements returns true
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (userCar.x - 1 >= 0)//note that this evaluates to true until the end of left boundry
                    {
                       //changes the object x coordinate -1 moves left
                       //please note this just changes the object information without out putting
                        userCar.x = userCar.x - 1;
                    }
                }
                //same as above but this time the right side 
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    //the right boundry here is playfieldWith
                    if (userCar.x + 1 < playfieldWidth)
                    {
                        userCar.x = userCar.x + 1;
                    }
                }
            }

            //this where the little magic happens ,this shit is poor !!!

            List<Object> newList = new List<Object>(); //makes a list of type Object NB: Generic
            for (int i = 0; i < objects.Count; i++)
            {
                //The oldCar is the objects ,from last Console display
                Object oldCar = objects[i];
                Object newObject = new Object();
                //this copies oldCar to newObject (should used a function or operator overloading ) 
                newObject.x = oldCar.x;
                newObject.y = oldCar.y + 1;
                newObject.c = oldCar.c;         
                newObject.color = oldCar.color;
                //end of copying
                if (newObject.c == '*' && newObject.y == userCar.y && newObject.x == userCar.x)
                {//if the new object is '*' and the coordinates of x,y == those of userCar '@'  
                    speed -= 20;

                }
                if (newObject.c == '-' && newObject.y == userCar.y && newObject.x == userCar.x)
                {//if the new object is '-' the coordinates of x,y == those of userCar '@'
                    livesCount++;
                }
                if (newObject.c == '#' && newObject.y == userCar.y && newObject.x == userCar.x)
                {//if the new object is '#' the coordinates of x,y == those of userCar '@'

                    livesCount--;
                    hitted = true;
                    speed += 50; 

                    if (speed > 400)
                    { //speed can't > 400 ,units of ???
                        speed = 400;
                    }
                    if (livesCount <= 0)
                    {
                        //You many lifes are over
                        //shit says game is over...
                        PrintStringOnPosition(8, 10, "GAME OVER!!!", ConsoleColor.Red);
                        PrintStringOnPosition(8, 12, "Press [enter] to exit", ConsoleColor.Red);
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                //if this obect '*' or '#' and '#' is still in range of the windowsheight
                //add it to the new list
                if (newObject.y < Console.WindowHeight)
                {
                    newList.Add(newObject);
                }
            }
            //populated your new list 
            // clear the console and display a new one
            objects = newList;
            Console.Clear();

            if (hitted)
            {
                //you hitt a '#'
                //all the objects are cleared and x is displayed
                objects.Clear();
                PrintOnPosition(userCar.x, userCar.y, 'X', ConsoleColor.Red);
            }
            else
            {
                //Displays  the car first on the grid ie "@"
                PrintOnPosition(userCar.x, userCar.y, userCar.c, userCar.color);
            }
            foreach (Object car in objects)
            {
                //this for loop add all the other object to the grid
                PrintOnPosition(car.x, car.y, car.c, car.color);
            }

            // Draw info
            //NB: The coordinates are constanst :meaning ..................
            PrintStringOnPosition(8, 4, "Lives: " + livesCount, ConsoleColor.White);
            PrintStringOnPosition(8, 5, "Speed: " + speed, ConsoleColor.White);
            PrintStringOnPosition(8, 6, "Acceleration: " + acceleration, ConsoleColor.White);
            //Console.Beep();
            //blocking thread makes is slow enough for you to comment
            Thread.Sleep((int)(600 - speed));
        }
    }
}