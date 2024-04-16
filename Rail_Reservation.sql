CREATE DATABASE RAIL_RESERVATION
USE RAIL_RESERVATION

---------------------------------------- CREATE TABLE FOR ADMIN LOGIN---------------------------------------------------------------------
CREATE TABLE ADMIN_LOGIN (
    ADMIN_ID INT PRIMARY KEY,
    ADMIN_NAME VARCHAR(50) NOT NULL,
    APASSWORD VARCHAR(255) NOT NULL
);

INSERT INTO ADMIN_LOGIN VALUES (1234, 'ADMIN', '7890');

SELECT * FROM ADMIN_LOGIN;

--------------------------------------------CREATE TABLE FOR USER LOGIN--------------------------------------------------------------------------
CREATE TABLE USER_LOGIN (
    ID INT PRIMARY KEY,
    UNAME VARCHAR(255) NOT NULL,
    UPASSWORD VARCHAR(255) NOT NULL
);

INSERT INTO USER_LOGIN VALUES (12, 'simran', '1234');

SELECT * FROM USER_LOGIN;

-------------------------------------------------------------- TRAINS TABLE-------------------------------------------------------
CREATE TABLE TRAINS (
    TRAINNO INT  PRIMARY KEY,
    TRAINNAME VARCHAR(100) NOT NULL,
    SOURCE VARCHAR(100) NOT NULL,
    DESTINATION VARCHAR(100) NOT NULL,
    STATUS VARCHAR(20) CHECK (STATUS IN ('Active', 'Inactive')) DEFAULT 'Active'
);
SELECT * FROM TRAINS

-----------------------------------------PROC OF ADD TRAIN-------------------------------------

CREATE OR ALTER PROC ADD_TRAINS
    @TRAINNO INT,
    @TRAINNAME VARCHAR(100),
    @SOURCE VARCHAR(100),
    @DESTINATION VARCHAR(100)
AS
BEGIN
    INSERT INTO TRAINS(TRAINNO, TRAINNAME, SOURCE, DESTINATION)
    VALUES (@TRAINNO, @TRAINNAME, @SOURCE, @DESTINATION);
    PRINT 'Train inserted successfully.';
END;

EXEC ADD_TRAINS
    @TRAINNO = 12212,
    @TRAINNAME = ' Rajdhani Express',
    @SOURCE = 'Delhi',
    @DESTINATION = 'Mumbai';

SELECT * FROM TRAINS;

------------------------------------------------------------------CLASS DETAILS-------------------------------------------------------

CREATE TABLE CLASS_DETAILS (
    CLASS_ID INT IDENTITY(1, 1) PRIMARY KEY,
    TRAINNO INT,
    CLASSNAME VARCHAR(50),
    TOTALSEATS INT NOT NULL,
    AVAILABLESEATS INT NOT NULL,
    AMOUNT DECIMAL(10, 2),
    FOREIGN KEY (TRAINNO) REFERENCES TRAINS(TRAINNO)
);


------------------------------------------------------------------------PROC OF CLASS DEATLS---------------------------------------------------------
CREATE OR ALTER PROC ADD_CLASS_DETAILS
    @TRAINNO INT,
    @CLASSNAME VARCHAR(50),
    @TOTALSEATS INT,
    @AVAILABLESEATS INT,
    @AMOUNT DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO CLASS_DETAILS (TRAINNO, CLASSNAME, TOTALSEATS, AVAILABLESEATS, AMOUNT)
    VALUES (@TRAINNO, @CLASSNAME, @TOTALSEATS, @AVAILABLESEATS, @AMOUNT);
END; 

-- Execute the stored procedure to add class details-------------------------------------
EXEC ADD_CLASS_DETAILS
    @TRAINNO = 12212,
    @CLASSNAME = '1tier',
    @TOTALSEATS = 50,
    @AVAILABLESEATS = 50,
    @AMOUNT = 1000.00;

-- Check the data in the CLASS_DETAILS table-----------------
SELECT * FROM CLASS_DETAILS;


-----------------------------------------------------------------BOOKING TABLE------------------------------------------------------------------
-- CREATE TABLE FOR BOOKING TICKETS
CREATE TABLE BOOKING_TICKETS (
   BOOK_ID INT identity(1,1) primary key,
    username varchar(50),
    TRAINNO INT NOT NULL,
    CLASSNAME VARCHAR(255) NOT NULL,
    BOOKING_DATE DATETIME DEFAULT GETDATE(),
	DATEOF_TRAVEL DATETIME NOT NULL,
    No_OF_TICKETS INT NOT NULL,
	BOOK_AMOUNT DECIMAL(10, 2),
	Status VARCHAR(20) CHECK (Status IN ('Active', 'Inactive')) DEFAULT 'Active'
    FOREIGN KEY (TRAINNO) REFERENCES TRAINS(TRAINNO)
);

----------------------------------------------------------------------PROC BOOK TABLE----------------------------------------------

CREATE  or alter PROCEDURE BookTicket
    @username VARCHAR(50),
    @TRAINNO INT,
    @CLASSNAME VARCHAR(255),
    @DATEOF_TRAVEL DATETIME,
    @No_OF_TICKETS INT
AS
BEGIN
    DECLARE @BOOK_AMOUNT DECIMAL(10, 2)
    DECLARE @CLASS_ID INT
    DECLARE @AVAILABLE_SEATS INT
    DECLARE @FINAL_AMOUNT DECIMAL(10, 2)

    -- Calculate booking amount
    SELECT @BOOK_AMOUNT = AMOUNT * @No_OF_TICKETS
    FROM CLASS_DETAILS
    WHERE TRAINNO = @TRAINNO AND CLASSNAME = @CLASSNAME

 -- Check if there are enough available seats
 SELECT @CLASS_ID = CLASS_ID,
           @AVAILABLE_SEATS = AVAILABLESEATS
FROM CLASS_DETAILS
        WHERE TRAINNO = @TRAINNO AND CLASSNAME = @CLASSNAME
		`
  IF @AVAILABLE_SEATS < @No_OF_TICKETS
   BEGIN
        PRINT 'Not enough available seats in the selected class.'
        RETURN
    END
 -- Deduct booked seats from available seats
 UPDATE CLASS_DETAILS
   SET AVAILABLESEATS = AVAILABLESEATS - @No_OF_TICKETS
    WHERE CLASS_ID = @CLASS_ID

SET @FINAL_AMOUNT = @No_OF_TICKETS * @BOOK_AMOUNT

SELECT @FINAL_AMOUNT AS 'FINALAMOUNT'
 -- Insert booking details into BOOKING_TICKETS table
   INSERT INTO BOOKING_TICKETS (username, TRAINNO, CLASSNAME, DATEOF_TRAVEL, No_OF_TICKETS, BOOK_AMOUNT)
    VALUES (@username, @TRAINNO, @CLASSNAME, @DATEOF_TRAVEL, @No_OF_TICKETS, @FINAL_AMOUNT)
END
	EXEC BookTicket
    @TRAINNO = 12212,
	@username='simran',
    @CLASSNAME = '1tier',
    @NO_OF_TICKETS = 2,
    @DATEOF_TRAVEL = '2024-04-16'

SELECT * FROM BOOKING_TICKETS;


---------------------------------------------------------------------- CANCELING TABLE----------------------------------------------------------
CREATE TABLE CANCEL_TICKET (
    CANCEL_ID INT  IDENTITY(1,1) PRIMARY KEY,
    DATE_OF_CANCEL DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    TRAINNO INT NOT NULL,
    NO_OF_SEATS INT NOT NULL,
    REFUND DECIMAL(10, 2) NOT NULL,
    BOOK_ID INT NOT NULL,
    FOREIGN KEY (BOOK_ID) REFERENCES BOOKING_TICKETS(BOOK_ID),
    FOREIGN KEY (TRAINNO) REFERENCES TRAINS(TRAINNO)
);
---------------------------------------------------------------------------------------PROC OF CANCEL-------------------------------

CREATE or alter PROC CANCELTICKET
    @TRAINNO INT,
    @NOOFSEATS INT,
    @REFUND DECIMAL(10, 2),
	@BOOK_ID INT  
AS
BEGIN
 UPDATE CLASS_DETAILS
    SET AVAILABLESEATS = AVAILABLESEATS + @NOOFSEATS
    WHERE TRAINNO = @TRAINNO;

    -- Insert cancellation details into CANCEL_TICKET table
    INSERT INTO CANCEL_TICKET(DATE_OF_CANCEL, TRAINNO, NO_OF_SEATS, REFUND,BOOK_ID)
    VALUES (CURRENT_TIMESTAMP, @TRAINNO, @NOOFSEATS, @REFUND,@BOOK_ID);
END;

---------------------------------------------------------- Execute the cancel ticket procedure---------------------------------------
EXEC CANCELTICKET
    @TRAINNO =12212,
    @NOOFSEATS = 2,
    @REFUND = 2000.00,
    @BOOK_ID =1;

	--------------------------------------------------------------------display all --------------------------------------------------------------
   select* from cancel_ticket
   SELECT * FROM BOOKING_TICKETS;
   SELECT * FROM CLASS_DETAILS;
   select* from trains
 







