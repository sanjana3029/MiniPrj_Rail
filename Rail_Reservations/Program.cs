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
            Console.WriteLine("**************************************");
            Console.WriteLine("       Welcome to Rail Reservation    ");
            Console.WriteLine("**************************************");
            Console.WriteLine("\n1. Enter 1 to Login as Admin");
            Console.WriteLine("\n2. Enter 2 to Login as User");
            Console.WriteLine("\n3. Enter 3 to Exit");

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

        // Admin login
        public static void AdminCall()
        {
            Console.WriteLine("Enter Admin ID:");
            int adminid = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Admin Name:");
            string adminName = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();

            var admin = RR.ADMIN_LOGIN.FirstOrDefault(a => a.ADMIN_NAME == adminName && a.APASSWORD == password);

            if (admin != null)
            {
                Console.WriteLine("Admin login successful!");

                Console.WriteLine("Press 1 for Add Trains");
                Console.WriteLine("Press 2 for Modify Trains");
                Console.WriteLine("Press 3 for Exit");
                string res = Console.ReadLine();
                switch (res)
                {
                    case "1":
                        Console.WriteLine("Please Add The Trains");
                        Add_Trains();
                        DisplayTrains_details();
                        break;
                    case "2":
                        Console.WriteLine("Please modify the train");
                        Modify_train();
                        Console.WriteLine("Trains has been modify successfully");
                        DisplayTrains_details();
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

        // Add a new train
        static void Add_Trains()
        {
            Console.WriteLine("Enter Tno:");
            int TRAIN_NO = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter TRAIN_NAME:");
            string TRAIN_NAME = Console.ReadLine();

            Console.WriteLine("Enter SOURCE:");
            string SOURCE = Console.ReadLine();

            Console.WriteLine("Enter DESTINATION:");
            string DESTINATION = Console.ReadLine();

            Console.WriteLine("Enter STATUS Active or Inactive:");
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
            Console.WriteLine("Train added successfully!");
            
                
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

            
            //ADD_CLASS_DETAILS();
        }

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
        // Display train details
        public static void DisplayTrains_details()
        {
            var traindata = RR.TRAINS.ToList();
            foreach (var e in traindata)
            {
                Console.WriteLine($"{e.TRAINNO},{e.TRAINNAME},{e.SOURCE},{e.DESTINATION},{e.STATUS}");
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        // User login
        public static void UserLogin()
        {
            Console.WriteLine("Enter User ID:");
            int userid = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter User Name:");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();

            var user = RR.USER_LOGIN.FirstOrDefault(u => u.UNAME == userName && u.UPASSWORD == password);

            if (user != null)
            {
                Console.WriteLine("User login successful!");

                Console.WriteLine("Press 1 to Book Tickets");
                Console.WriteLine("Press 2 to Cancel Tickets");
                Console.WriteLine("Press 3 to Display Tickets");
                string res = Console.ReadLine();
                switch (res)
                {
                    case "1":
                        Console.WriteLine("Book Tickets :");
                        BookingTicket();
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!Booked successfully!!!!!!!!!!!!!!!!!!!!!");
                        break;
                    case "2":
                        Console.WriteLine("Cancel Tickets: ");;
                        Cancel_train();
                        Console.WriteLine("!!!!!!!!!!!!!Canceled successfully!!!!!!!!!");
                        break;
                    case "3":
                        Console.WriteLine("Display My Booking Details "); ;
                        DisplayBooking_Details();
                        Console.WriteLine("!!!!!!!!!!!!!Here Is Your Details!!!!!!!!!");
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

        //  Booking ticket
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

        static void Modify_train()
        {
            Console.WriteLine("Enter Train Number:");
            int trainNo = int.Parse(Console.ReadLine());

            // Retrieve the train details from the database
            var trainToUpdate = RR.TRAINS.FirstOrDefault(t => t.TRAINNO == trainNo);
            if (trainToUpdate != null)
            {
                Console.WriteLine("Enter Train Name:");
                trainToUpdate.TRAINNAME = Console.ReadLine();
                Console.WriteLine("Enter Train source:");
                trainToUpdate.SOURCE = Console.ReadLine();
                Console.WriteLine("Enter Train Destination:");
                trainToUpdate.DESTINATION = Console.ReadLine();
                Console.WriteLine("Enter Status:");
                trainToUpdate.STATUS = Console.ReadLine();


                RR.SaveChanges();
                Console.WriteLine("!!!!!!!!!!!!!!!!!!Updated Train successfully!!!!!!!!!!!!!!!!!!!");
                DisplayTrains_details();
            }
           
            else
            {
                Console.WriteLine("!!!!!!!!!!!!!!!!!Train not found!!!!!!!!!!!!!!!!");
            }

        }

        static void Cancel_train()
        { 

                Console.WriteLine("Enter Train Number:");
                int trainNo = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Number of Seats to Cancel:");
                int noOfSeats = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Refund Amount:");
                decimal refundAmount = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter Booking ID:");
                int bookingId = int.Parse(Console.ReadLine());
            try
            {
                // Call the stored procedure to cancel the ticket
                RR.CANCELTICKET(trainNo, noOfSeats, refundAmount,bookingId);
                RR.SaveChanges();
               Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Cancellation successful!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!Ticket Canceled!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }

        static void DisplayBooking_Details()
        {

            //Console.WriteLine("Enter Train Numbe");
            //train.TrainNo = int.Parse(Console.ReadLine());
            //var trainVal = db.TicketClassDetails.Where(t => t.TrainNo == train.TrainNo).ToList();
            Console.WriteLine("Enter Your Booking ID");
            int bookingId = int.Parse(Console.ReadLine());
            var booking = RR.BOOKING_TICKETS.FirstOrDefault(b => b.BOOK_ID == bookingId);
            if (booking != null)
            {

                Console.WriteLine("*************************************");
                Console.WriteLine("           BOOKING DETAILS           ");
                Console.WriteLine("**************************************");
                Console.WriteLine($"Booking ID: {booking.BOOK_ID}");
                Console.WriteLine("**************************************");
                Console.WriteLine($"Train ID: {booking.TRAINNO}");
                Console.WriteLine("**************************************");
                Console.WriteLine($"User Name: {booking.username}");
                Console.WriteLine("**************************************");
                Console.WriteLine($"Berth Class: {booking.CLASSNAME}");
                Console.WriteLine("**************************************");
                Console.WriteLine($"Total Tickets: {booking.No_OF_TICKETS}");
                Console.WriteLine("**************************************");
                Console.WriteLine($"Booking Status: {booking.Status}");
                Console.WriteLine("**************************************");
                Console.WriteLine($"Payment Amount: ${booking.BOOK_AMOUNT}");
                Console.WriteLine("***************************************");
                
            }
            else
            {
                Console.WriteLine("Booking not found.");
            }

        }


    }
}
