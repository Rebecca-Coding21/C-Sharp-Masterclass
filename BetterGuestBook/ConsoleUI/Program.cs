using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuestBookLibrary.Models;

//Capture the information about each guest (at least one guest and unknown maximum
//Info to capture: First name, last name, message to the host
//Once done, loop through each guest and print their info

namespace ConsoleUI
{
    internal class Program
    {
        private static List<GuestModel> guests = new List<GuestModel>();

        static void Main(string[] args)
        {                     

            GetGuestInformation();

            PrintGuestInformation();

            
            Console.ReadLine();
        }
        private static void GetGuestInformation()
        {
            string moreGuestsComing = "";

            do
            {
                GuestModel guest = new GuestModel();

                guest.FirstName = GetInfoFromConsole("What is your first name: ");
                guest.LastName = GetInfoFromConsole("What is your last name: ");
                guest.MessageToHost = GetInfoFromConsole("What message would you like to tell your host: ");
                moreGuestsComing = GetInfoFromConsole("Are more guests coming (yes/no): ");

                guests.Add(guest);

                Console.Clear();
            } while (moreGuestsComing.ToLower() == "yes");
        }

        private static void PrintGuestInformation()
        {
            foreach (GuestModel guest in guests)
            {
                Console.WriteLine(guest.GuestInfo);
            }
        }

        private static string GetInfoFromConsole(string message)
        {
            string output = "";

            Console.Write(message);
            output = Console.ReadLine();

            return output;
        }
    }
}
