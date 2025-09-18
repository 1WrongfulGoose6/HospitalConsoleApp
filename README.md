# Hospital Management System - ConsoleApp

C# console application that manages roles between doctor, administrator, patients and appointments. Data is stored across 3 .txt files for credentials, user data and appointment data with UserIDs acting as the foreign key. Covers fundamental programming concepts (OOs, Data structures, etc) and the .NET framework.


## Features

- Book appointments
- Add doctors/patients into the system
- Search for doctors/patients
- List appointments for specific doctor/patients
- Assigning doctors to patients


## Documentation

When the .exe is run for the first time (with no supporting txt files) it will generate all the necessary files and initialise an admin user with the userID: `10000000` and password: `password`. All subsequent userIDs will be incremented (e.g. 10000001).


## Roadmap

- Strong input filtering
- Bug fixes
- Ability to change passwords and assigned doctors
- Receptionist role


