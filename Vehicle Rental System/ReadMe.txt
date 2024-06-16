VEHICLE RENTAL SYSTEM


OVERVIEW
This application is designed to help users calculate the cost of renting different types of vehicles based on the rental period, their age, experience, and vehicle type.


USAGE
The application recieve's input of 10 parameters:

*First name(string): user's first name.
*Last name(string): user's lastname. 
*Start date(DateTime): reservation's start date.
*End date(DateTime): reservation's end date.
*Number(int): safety rating if the vehicle is a ''Car'', driver's age if the vehicle is a''motorcycle'', driver's experience if the vehicle is a''cargovan''.
*Vehicle type(string): vehicle's type. Available vehicle types are: ''Car'', ''Motorcycle'' and ''Cargovan''.
*Brand(string): vehicle's brand.
*Model(string): vehicle's model.
*Price(decimal): vehicle's price.
*Actual return date(DateTime): the actual return date of the vehicle.

The input data and output data are placed in folder named ''Files''.
 
The input data should be inserted in- ''..\Files\input.txt'' as an array of string, separated by '', ''.
After the application is started the output data will appear in- ''..\Files\output.txt''.

Example of input data- John, Doe, 2024-06-03, 2024-06-13, 3, Car, Mitsubishi, Mirage, 15000, 2024-06-13 


TECHNOLOGIES USED

.Net 6 Framework
C#

