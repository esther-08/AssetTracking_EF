# AssetTracking_EF
This project is a  console app aimed at building of an Asset Tracking database using the entity framework. 
Asset Tracking is a way to keep track of the company assets, like Laptops, Stationary computers, phones and so on... 

## Assumptions
- All assets have an end of life of 3 years. 
- Assets stored are laptops and mobile phones in different parts of the world.
- Each asset has the following properties;
  - Purchase date
  - Price
  - Model name
  - Location
- The assets are created and operations on them are done by the program hence no user input.

## Functionality
The program displays: 
- A sorted list (computers first, then phones) 
- A list sorted by purchase date. Items are; 
  - *RED* if purchase date is less than 3 months away from 3 years.
  - *Yellow* if date less than 6 months away from 3 years. 
- A list sorted by location
  - *RED* if purchase date is less than 3 months away from 3 years.
  - *Yellow* if date less than 6 months away from 3 years. 
  - Each item should have currency according to country

CRUD Database operations include
- Create: Added by the program.
- Read: Read and display the database contents.
- Update: Update the location of the first element in the database and display the database contents.
- Delete: Delete a given item and display the database contents.
