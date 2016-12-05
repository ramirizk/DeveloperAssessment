using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressBook.Classes
{
    class AddressBookCLI
    {
        AddressBook addressBook = new AddressBook();
        private string mainSelection;
        private bool inMainMenu = true;
        private bool modelValid = false;

        //Run runs the main menu for the address book program until the user quits.
        public void Run()
        {
            Console.WriteLine("(1) Add Contact to Book\n\n(2) View Contacts in Book\n\n (3) Search Contacts in Organization\n\n (4) Update Contact\n\n (5) Search First Names, Last Names, and Organizations\n\n (q) Quit\n");
            mainSelection = Console.ReadLine();
            Console.WriteLine("\n");
            while (inMainMenu == true)
            {
                MainMenu();
            }
            Environment.Exit(0);
        }

        //MainMenu checks user input in mainmenu and controls logic of what methods to call.

        private void MainMenu()
        {
            switch (mainSelection)
            {
                case "1":
                    AddContact();
                    AfterProcessingUserSelection();
                    break;

                case "2":
                    ViewAllContacts();
                    AfterProcessingUserSelection();
                    break;

                case "3":
                    SearchByOrg();
                    AfterProcessingUserSelection();
                    break;
                case "4":
                    UpdateContact();
                    AfterProcessingUserSelection();
                    break;
                case "5":
                    SearchFirstLastOrg();
                    AfterProcessingUserSelection();
                    break;
                case "q":
                    inMainMenu = false;
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("That is not a valid option. Please try again.\n");
                    Console.WriteLine("(1) Add Contact to Book\n\n(2) View Contacts in Book\n\n (3) Search Contacts in Organization\n\n (4) Update Contact\n\n (5) Search First Names, Last Names, and Organizations\n\n (q) Quit\n");

                    mainSelection = Console.ReadLine();

                    Console.WriteLine("\n");
                    break;
            }
        }

        //Searches first name, last name, and Organization for partial string provided by user and returns the results.
        private void SearchFirstLastOrg()
        {
            Console.Clear();
            Console.WriteLine("Search first name, last name, or organization based on partial value, using *. \n For Example: Ma*");
            string searchString = Console.ReadLine();
            string[] searchParse = searchString.Split('*');
            string searchPhrase = searchParse[0];
            List<Contact> allContacts = addressBook.AddressBookReader();
            bool matchFound = false; 

            for (int i = 0; i < allContacts.Count; i++)
            {
                if (allContacts[i].FirstName.Contains(searchPhrase) || allContacts[i].LastName.Contains(searchPhrase) || allContacts[i].Organization.Contains(searchPhrase))
                {
                    matchFound = true;

                    Console.WriteLine("Contact " + (i + 1) + ":");
                    Console.WriteLine("First Name: " + allContacts[i].FirstName);
                    Console.WriteLine("Last Name: " + allContacts[i].LastName);
                    Console.WriteLine("Organization: " + allContacts[i].Organization);
                    Console.WriteLine("Phone Number: " + allContacts[i].PhoneNumber);
                    Console.WriteLine("Email: " + allContacts[i].Email);
                    Console.WriteLine();
                }
            }
            if (!matchFound)
            {
                Console.WriteLine("There is no match for that search");
            }
            Console.Write("Press enter to return to main menu ...");
            Console.ReadLine();
        }

        //Adds a contact to the address book. And makes sure it is valid data before saving to csv.
        public void AddContact()
        {
            Contact contact = new Contact();
            string header = "Enter the new contact's information";
            while (modelValid == false)
            {
                Console.Clear();
                Console.WriteLine(header);
                Console.WriteLine("Enter First Name:");
                contact.FirstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name:");
                contact.LastName = Console.ReadLine();
                Console.WriteLine("Enter Contact's Organization:");
                contact.Organization = Console.ReadLine();
                Console.WriteLine("Enter Contact's Phone Number:");
                contact.PhoneNumber = Console.ReadLine();
                Console.WriteLine("Enter Contact's Email:");
                contact.Email = Console.ReadLine();

                contact.PhoneNumber = Regex.Replace(contact.PhoneNumber, "[^0-9]", "");
                header = ValidateInputs(contact);
            }
            addressBook.AddressBookWriter(contact);
        }

        //Displays all contacts from the address book csv
        public void ViewAllContacts()
        {
            Console.Clear();
            List<Contact> allContacts = addressBook.AddressBookReader();
            for (int i = 0; i < allContacts.Count; i++)
            {
                Console.WriteLine("Contact " + (i + 1) + ":");
                Console.WriteLine("First Name: " + allContacts[i].FirstName);
                Console.WriteLine("Last Name: " + allContacts[i].LastName);
                Console.WriteLine("Organization: " + allContacts[i].Organization);
                Console.WriteLine("Phone Number: " + allContacts[i].PhoneNumber);
                Console.WriteLine("Email: " + allContacts[i].Email);
                Console.WriteLine();
            }
            Console.Write("Press enter to return to main menu ...");
            Console.ReadLine();
        }

        //Returns all contacts belonging to an organization.
        public void SearchByOrg()
        {
            Console.Clear();
            Console.WriteLine("Please Enter Organization to Search For: ");
            string searchKey = Console.ReadLine().ToLower();
            List<Contact> allContacts = addressBook.AddressBookReader();
            bool orgFound = false;
            for (int i = 0; i < allContacts.Count; i++)
            {
                if (searchKey == allContacts[i].Organization.ToLower())
                {
                    orgFound=true;
                    Console.WriteLine("Contact " + (i + 1) + ":");
                    Console.WriteLine("First Name: " + allContacts[i].FirstName);
                    Console.WriteLine("Last Name: " + allContacts[i].LastName);
                    Console.WriteLine("Organization: " + allContacts[i].Organization);
                    Console.WriteLine("Phone Number: " + allContacts[i].PhoneNumber);
                    Console.WriteLine("Email: " + allContacts[i].Email);
                    Console.WriteLine();
                }
            }
            if (!orgFound)
            {
                Console.WriteLine("No one in your address book is part of " + searchKey + " organization");
            }
            Console.Write("Press enter to return to main menu ...");
            Console.ReadLine();

        }


        //Updates contact info when same first and last name are provided.
        public void UpdateContact()
        {
            modelValid = false;
            Contact contact = new Contact();

            string header = "Enter the Contact's First and Last names and information you would like update";
            while (modelValid == false)
            {
                Console.Clear();
                Console.WriteLine(header);

                
                Console.WriteLine("Enter First Name:");
                contact.FirstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name:");
                contact.LastName = Console.ReadLine();
                Console.WriteLine("Enter Contact's Organization:");
                contact.Organization = Console.ReadLine();
                Console.WriteLine("Enter Contact's Phone Number:");
                contact.PhoneNumber = Console.ReadLine();
                contact.PhoneNumber = Regex.Replace(contact.PhoneNumber, "[^0-9]", "");
                Console.WriteLine("Enter Contact's Email:");
                contact.Email = Console.ReadLine();
                header = ValidateInputs(contact);
            }
            addressBook.AddressBookWriter(contact);
            bool updated = addressBook.UpdateExistingContact(contact);
            if (updated)
            {
                Console.Write("Contact Updated.  Press enter to return to main menu ...");
                Console.ReadLine();
            }
            else
            {
                Console.Write("No contact found by that first and last name.  Press enter to return to main menu ...");
                Console.ReadLine();
            }
        }


        //Method to call main menu strings after user completes other tasks.
        public void AfterProcessingUserSelection()
        {
            Console.Clear();
            modelValid = false;
            Console.WriteLine("(1) Add Contact to Book\n\n(2) View Contacts in Book\n\n (3) Search Contacts in Organization\n\n (4) Update Contact\n\n (5) Search First Names, Last Names, and Organizations\n\n (q) Quit\n");
            mainSelection = Console.ReadLine();
        }

        //Validates Users data that is entered.
        public string ValidateInputs(Contact contact)
        {
            string message = "";
            if (contact.FirstName != "" && contact.LastName != "" && contact.Organization != "" && contact.Email != "" && contact.PhoneNumber != "")
            {
                
                char[] emailChar = new char[] { '.', '@' };
                string[] emailFormat = contact.Email.Split(emailChar);
                if (!Regex.IsMatch(contact.Email, @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", RegexOptions.IgnoreCase))
                {
                    if (contact.PhoneNumber.Length != 11)
                    {
                        return message = "Phone number must be 11 digits\n Not a valid email address\n Please Try Again";
                    }
                    else
                    {
                        return message = "Not a valid email address\n Please Try Again";
                    }
                }
                else
                {
                    if (contact.PhoneNumber.Length != 11)
                    {
                        return message = "Phone number must be 11 digits\n Please Try Again";
                    }
                    else
                    {
                        modelValid = true;
                        return message = "";
                    }

                }
            }
            else
            {
                return message = "One or more of your fields is blank correct the error and try again.";
            }
        }
    }
}
