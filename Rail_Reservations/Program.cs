using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rail_Reservations
{
    class Program
    {
        static RAIL_RESERVATIONEntities RR = new RAIL_RESERVATIONEntities();
        static TRAIN train = new TRAIN();
        static CLASS_DETAILS cd = new CLASS_DETAILS();
        static void Main(string[] args)
        {
            Console.WriteLine("                               **************************************                                  ");
            Console.WriteLine("                               |   Welcome to Rail Reservation      |                                   ");
            Console.WriteLine("                               |                                    |                                   ");
            Console.WriteLine("                               **************************************                                   ");
            Console.WriteLine("\n1. Enter 1 to Login as Admin");
            Console.WriteLine("\n2. Enter 2 to Login as User");
            Console.WriteLine("\n3. Enter 3 to Exit");
            Console.WriteLine("\n Enter Your Choice : ");
            string LoginType = Console.ReadLine();
            switch (LoginType)
            {
                case "1":
                    AdminCall();
                    break;
                case "2":
                   UserLogin();
                    break;
                case "3":
                    Console.WriteLine("\nExiting From the train ticket booking app...");
                    break;
                default:
                    Console.WriteLine(" \nPlease enter a valid option.");
                    break;
            }
            Console.Read();
        }

 //====================================== Admin login==============================================================================
        public static void AdminCall()
        {
            Console.WriteLine("\nEnter Admin ID:");
            int adminid = int.Parse(Console.ReadLine());

            Console.WriteLine("\nEnter Admin Name:");
            string adminName = Console.ReadLine();

            Console.WriteLine("\nEnter Password:");
            string password = ReadPassword();

            var admin = RR.ADMIN_LOGIN.FirstOrDefault(a => a.ADMIN_NAME == adminName && a.APASSWORD == password);

            if (admin != null)
            {
                Console.WriteLine("\nAdmin login successful!");
                Console.WriteLine("\nPress 1 for Add Trains");
                Console.WriteLine("\nPress 2 for Modify Trains");
                Console.WriteLine("\nPress 3 for Delete train");
                Console.WriteLine("\nPress 4 for Exit");
                string res = Console.ReadLine();
                switch (res)
                {
                    case "1":
                        Console.WriteLine("\nPlease Add The Trains");
                        Add_Trains();
                        Console.WriteLine("\n!!!!!!!!!!!!!!!!!Train Added Successfully!!!!!!!!!!!!!!!!!!!!!!!");
                        DisplayTrains_details();
                        break;
                    case "2":
                        Console.WriteLine("\nPlease modify the train");
                        Modify_train();
                        Console.WriteLine("\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!Trains has been modify successfully!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        DisplayTrains_details();
                        break;
                    case "3":
                        // Prompt for train number to delete
                        Console.WriteLine("\nPlease enter the Train Number to Delete the Train");
                        int trainNoToDelete = int.Parse(Console.ReadLine());
                        // Delete the train
                        Delete_Trains(trainNoToDelete);
                        DisplayTrains_details();
                        Console.WriteLine("\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Train deleted successfully!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        break;
                    case "4":
                        Console.WriteLine("\nExiting from the train ticket booking app...");
                        break;
                    default:
                        Console.WriteLine("Enter valid number");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid admin credentials. Login failed.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
 //===============================================Password ==============================================================================
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                // Ignore any key other than Enter
                if (key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after the user hits Enter
            return password;
        }

 //=========================Add a new train====================================================================================
        static void Add_Trains()
        {
            Console.WriteLine("\nEnter Tno:");
            int TRAIN_NO = int.Parse(Console.ReadLine());

            Console.WriteLine("\nEnter TRAIN_NAME:");
            string TRAIN_NAME = Console.ReadLine();

            Console.WriteLine("\nEnter SOURCE:");
            string SOURCE = Console.ReadLine();

            Console.WriteLine("\nEnter DESTINATION:");
            string DESTINATION = Console.ReadLine();

            Console.WriteLine("\nEnter STATUS Active or Inactive:");
            string STATUS = Console.ReadLine();
            // Create and populate the train object
            var train = new TRAIN
            {
                TRAINNO = TRAIN_NO,
                TRAINNAME = TRAIN_NAME,
                SOURCE = SOURCE,
                DESTINATION = DESTINATION,
                STATUS = STATUS
            };

            RR.TRAINS.Add(train);
            RR.SaveChanges();
            Console.WriteLine("\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Train added successfully!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
           
              var Classes = new string[] { "1tier", "2tier", "Sleeper" };
                foreach (var Classname in Classes)
                {
                    cd.TRAINNO = train.TRAINNO;
                    cd.CLASSNAME = Classname;
                    Console.WriteLine($"\nEnter TOTAL SEATS:  {Classname}:");
                    cd.TOTALSEATS = int.Parse(Console.ReadLine());
                    Console.WriteLine("\nAvailble seats: ");
                    cd.AVAILABLESEATS = int.Parse(Console.ReadLine());
                    Console.WriteLine("\nEnter Amount Of Classes: ");
                    cd.AMOUNT = int.Parse(Console.ReadLine());
                    RR.CLASS_DETAILS.Add(cd);
                    RR.SaveChanges();
                }           
        }
 //=========================================Add class details===================================================
        static void ADD_CLASS_DETAILS()
        {
            Console.WriteLine("\n Add Train Classes Deatils");

            var lastrain = RR.TRAINS.OrderByDescending(t => t.TRAINNO).FirstOrDefault();
            if (lastrain != null)
            {
                Console.WriteLine($" TrainNo: {lastrain.TRAINNO}");
                var Classes = new string[] { "1tier", "2tier", "Sleeper" };
                foreach (var Classname in Classes)
                {
                    cd.TRAINNO = train.TRAINNO;
                    cd.CLASSNAME = Classname;
                    Console.WriteLine($"Enter TOTAL SEATS{Classname}:");
                    cd.TOTALSEATS = int.Parse(Console.ReadLine());
                    Console.WriteLine("availble seats:");
                    cd.AVAILABLESEATS = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Amount Of Classes");
                    cd.AMOUNT = int.Parse(Console.ReadLine());
                    RR.CLASS_DETAILS.Add(cd);
                    RR.SaveChanges();

                }

            }

        }

 //=============================== Display train details==================================================================
        public static void DisplayTrains_details()
        {
            var traindata = RR.TRAINS.ToList();

            Console.WriteLine("=======================================================================================");
            Console.WriteLine("                               ALL TRAIN DETAILS                                        ");
            Console.WriteLine("========================================================================================");

            Console.WriteLine("| Train No |    Train Name    |    Source    |   Destination   |   Status   |");

            foreach (var train in traindata)
            {
                Console.WriteLine("| {0,-8} | {1,-10} | {2,-10} | {3,-12} | {4,-7} |",
                                  train.TRAINNO, train.TRAINNAME, train.SOURCE, train.DESTINATION, train.STATUS);
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }

            Console.ReadLine();
            Environment.Exit(0);
        }

 //=========================== User login=====================================================================================
        public static void UserLogin()
        {
            Console.WriteLine("\n**************************************");
            Console.WriteLine("\nEnter User ID:");
            int userid = int.Parse(Console.ReadLine());

            Console.WriteLine("\n**************************************");
            Console.WriteLine("\nEnter User Name:");
            string userName = Console.ReadLine();

            Console.WriteLine("\n**************************************");
            Console.WriteLine("Enter Password:");
            string password = ReadPassword();

            var user = RR.USER_LOGIN.FirstOrDefault(u => u.UNAME == userName && u.UPASSWORD == password);

            if (user != null)
            {
                Console.WriteLine("User login successful!");

                Console.WriteLine("\nPress 1 to Book Tickets");
                Console.WriteLine("\nPress 2 to Cancel Tickets");
                Console.WriteLine("\nPress 3 to Display All Booking Tickets");
                Console.WriteLine("\nPress  4 to Display All Cancel Tickets");
                string res = Console.ReadLine();
                switch (res)
                {
                    case "1":
                        Console.WriteLine("\nBook Tickets :");
                        BookingTicket();
                        Console.WriteLine("\n!!!!!!!!!!!!!!!!!!!!Booked successfully!!!!!!!!!!!!!!!!!!!!!");
                        //DisplayBooking_Details();
                        break;
                    case "2":
                        Console.WriteLine("\nCancel Tickets: ");;
                        Cancel_train();
                        Console.WriteLine("\n!!!!!!!!!!!!!Canceled successfully!!!!!!!!!");
                        break;
                    case "3":
                        Console.WriteLine("\nDisplay My Booking Details "); ;
                        DisplayAllBooking_Details();
                        Console.WriteLine("\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!    Here Is Your Details   !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        break;
                    case "4":
                        Console.WriteLine("\nDisplay Cancel Tickets Details "); ;
                        DisplayAllCanceltDetails();
                        Console.WriteLine("\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!   Here Is Your Details    !!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        break;
                    default:
                        Console.WriteLine("Enter valid number");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid user credentials. Login failed.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

//=============================Delete trains===============================================
        static void Delete_Trains(int trainNo)
        {
            using (var context = new RAIL_RESERVATIONEntities())
            {
                var trainToDelete = context.TRAINS.FirstOrDefault(t => t.TRAINNO == trainNo);
                if (trainToDelete != null)
                {
                    // Check if there are any related booking tickets for the train
                    var relatedBookings = context.BOOKING_TICKETS.Any(b => b.TRAINNO == trainNo);
                    if (relatedBookings)
                    {
                        Console.WriteLine("\nCannot delete the train because there are related booking tickets.");
                        return;
                    }

                    // Delete related records from the CLASS_DETAILS table
                    var classDetailsToDelete = context.CLASS_DETAILS.Where(cd => cd.TRAINNO == trainNo).ToList();
                    context.CLASS_DETAILS.RemoveRange(classDetailsToDelete);

                    // Delete the train
                    context.TRAINS.Remove(trainToDelete);
                    context.SaveChanges();
                    Console.WriteLine("\nTrain deleted successfully!");
                }
                else
                {
                    Console.WriteLine("\nTrain not found.");
                }
            }
        }

//==============================Booking ticket============================================
        static void BookingTicket()
        {
            try
            {
                Console.WriteLine("Enter Train Number:");
                int trainNo = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Personname:");
                string userName = Console.ReadLine();

                Console.WriteLine("Enter Class Name:");
                string className = Console.ReadLine();

                Console.WriteLine("Enter Number of Tickets:");
                int noOfTickets = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Date of Travel (YYYY-MM-DD):");
                DateTime dateOfTravel = DateTime.Parse(Console.ReadLine());

                RR.BookTicket(userName,trainNo,className,dateOfTravel,noOfTickets);
                RR.SaveChanges();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!Booking successful!!!!!!!!!!!!!!!!!!!!!!!!!");
        }

//=============================Modify Trains===============================================
        static void Modify_train()
        {
            Console.WriteLine("\nEnter Train Number:");
            int trainNo = int.Parse(Console.ReadLine());

            // Retrieve the train details from the database
            var trainToUpdate = RR.TRAINS.FirstOrDefault(t => t.TRAINNO == trainNo);
            if (trainToUpdate != null)
            {
                Console.WriteLine("\nEnter Train Name:");
                trainToUpdate.TRAINNAME = Console.ReadLine();
                Console.WriteLine("\nEnter Train source:");
                trainToUpdate.SOURCE = Console.ReadLine();
                Console.WriteLine("\nEnter Train Destination:");
                trainToUpdate.DESTINATION = Console.ReadLine();
                Console.WriteLine("\nEnter Status:");
                trainToUpdate.STATUS = Console.ReadLine();


                RR.SaveChanges();
                Console.WriteLine("\n!!!!!!!!!!!!!!!!!!Updated Train successfully!!!!!!!!!!!!!!!!!!!");
                DisplayTrains_details();
            }
           
            else
            {
                Console.WriteLine("\n!!!!!!!!!!!!!!!!!Train not found!!!!!!!!!!!!!!!!");
            }

        }

 //==================================Cancel trains=======================================
        static void Cancel_train()
        {
            Console.WriteLine("Enter Train Number:");
            int trainNo = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Number of Seats to Cancel:");
            int noOfSeats = int.Parse(Console.ReadLine());

            // Assume you have a method to calculate the refund amount based on the number of canceled seats
            decimal refundAmount = CalculateRefundAmount(trainNo, noOfSeats);

            Console.WriteLine("Enter Booking ID:");
            int bookingId = int.Parse(Console.ReadLine());

            try
            {
                // Call the stored procedure to cancel the ticket
                RR.CANCELTICKET(trainNo, noOfSeats, refundAmount, bookingId);
                RR.SaveChanges();

                // Deduct the refund amount from the total booking amount
                var booking = RR.BOOKING_TICKETS.FirstOrDefault(b => b.BOOK_ID == bookingId);
                if (booking != null)
                {
                    booking.BOOK_AMOUNT -= refundAmount;
                    RR.SaveChanges();
                }

                Console.WriteLine("Cancellation successful!");
                Console.WriteLine("Ticket Canceled!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

  // Method to calculate the refund amount based on the number of canceled seats
        static decimal CalculateRefundAmount(int trainNo, int noOfSeats)
        {
            // Assume a simple refund policy where each canceled seat gets a fixed refund amount
            decimal refundPerSeat = 50.00m; // Adjust this value based on your policy
            return noOfSeats * refundPerSeat;
        }
 //============================Display All Booking Details======================================
        static void DisplayAllBooking_Details()
        {
            try 
            { 

                Console.WriteLine("                   *************************************               ");
                Console.WriteLine("                           BOOKING DETAILS                              ");
                Console.WriteLine("                   **************************************               ");


                Console.WriteLine("\n| Booking ID | Train ID | User Name | Class Name | Total Tickets | Booking Status | Book Amount |");

                foreach (var booking in RR.BOOKING_TICKETS)
                {

                    Console.WriteLine("\n| {0,-10} | {1,-8} | {2,-10} | {3,-12} | {4,-14} | {5,-15} | {6,-14} |",
                              booking.BOOK_ID, booking.TRAINNO, booking.username, booking.CLASSNAME,
                              booking.No_OF_TICKETS, booking.Status, booking.BOOK_AMOUNT);
                }
                if (!RR.BOOKING_TICKETS.Any())
                {
                    Console.WriteLine("\nNo bookings found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }

  //====================================Display All Cancel Tickets========================================
        static void DisplayAllCanceltDetails()
        {
            try
            {
                Console.WriteLine("\n==============================================================================");
                Console.WriteLine("                          ALL CANCELED TICKET DETAILS                         ");
                Console.WriteLine("\n==============================================================================");

                Console.WriteLine("\n| Cancel ID |     Date of Cancel    | Train ID | No. of Seats | Refund Amount | Booking ID |");

                foreach (var canceledTicket in RR.CANCEL_TICKET)
                {
                    Console.WriteLine("\n| {0,-9} | {1,-15} | {2,-8} | {3,-12} | {4,-14} | {5,-11} |",
                        canceledTicket.CANCEL_ID, canceledTicket.DATE_OF_CANCEL, canceledTicket.TRAINNO,
                        canceledTicket.NO_OF_SEATS, canceledTicket.REFUND, canceledTicket.BOOK_ID);
                }

                if (!RR.CANCEL_TICKET.Any())
                {
                    Console.WriteLine("\nNo canceled tickets found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }
    }
}
