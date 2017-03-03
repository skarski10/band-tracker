# Hair Salon
**C# fourth week Friday code review for Epicodus, 03.03.17**
### By Kory Skarbek
## Description
Write a program to track bands and the venues where they've played concerts. Make a Venue class and a Band class.

* Build full CRUD functionality for Venues. Create, Read (show all, show single), Update, Delete.
* Allow a user to create Bands that have played at a Venue. Don't worry about building out updating or deleting for bands.
* There is a many-to-many relationship between bands and concert venues, so a venue can host many bands, and a band can play at many venues. Create a join table to store these relationships.
* When a user is viewing a single concert venue, list out all of the bands that have played at that venue so far and allow them to add a band to that venue. Create a method to get the bands who have played at a venue, and use a join statement in it.
* When a user is viewing a single Band, list out all of the Venues that have hosted that band and allow them to add a Venue to that Band. Use a join statement in this method too

## Specs

Check to see if band table database is empty
* **Input:** ""
* **Output:** true

Program recognizes two band instances as equal if they have the same name
* **Input:** Tiny Rick, Tiny Rick
* **Output:** true

Program will save entries into the band table
* **Input:** Tiny Rick
* **Output:** Tiny Rick

Program will return true if a band has a unique id and has been saved to an object
* **Input:** Tiny Rick
* **Output:** true

Program will return true if the band item has been found in the database
* **Input:** Tiny Rick
* **Output:** true

Check to see if venues table database is empty
* **Input:** ""
* **Output:** true

Program recognizes two venues instances as equal if they have the same name
* **Input:** The Pentagon, The Pentagon
* **Output:** true

Program will return true if a venues has a unique id and has been saved to an object
* **Input:** The Pentagon
* **Output:** true

Program will return true if the venues item has been found in the database
* **Input:** The Pentagon
* **Output:** true

Program will add a band to a venue
* **Input:** Tiny Rick
* **Output:** Tiny Rick was added to The Pentagon

Program will return all the band in a venue
* **Input:** The Pentagon
* **Output:** all The Pentagon bands

Program will add a venue to a band
* **Input:** The Pentagon
* **Output:** The Pentagon was added to Tiny Rick

Program will return all the venues a band has
* **Input:** The Pentagon
* **Output:** The Pentagon, Tiny Rick

Program will be able to edit single venues entries.
* **Input:** Pentagon
* **Output:** The Pentagon

Program will be able to delete single venues entries.
* **Input:** The Pentagon
* **Output:** (no entry)

<!--Ice Box-->

Program will be able to edit single band entries.
* **Input:** Rick
* **Output:** Tiny Rick

Program will be able to delete single band entries.
* **Input:** Tiny Rick
* **Output:** (no entry)

## Setup
* Open up the terminal.
* Clone this repository.
* Compile program
* Go to localhost:5004
#### Importing databases with SSMS
* Open SSMS
* Select File > Open > File and select your .sql file.
* If the database does not already exist, add the following lines to the top of the script file
* CREATE DATABASE band_tracker
* GO
* Save the file.
* Click ! Execute.
* Verify that the database has been created and the schema and/or data imported.

## Technologies Used
C#
Microsoft SQL
HTML
CSS
Bootstrap

## Legal
Copyright(c) 2017 Kory Skarbek
This software is licensed under the MIT license.
