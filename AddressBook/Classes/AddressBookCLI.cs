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
            Console.WriteLine("(1) Add Contact to Book\n\n(2) View Contacts in Book\n\n (3) Search Contacts in Organization\n\n (4) Update Contact\n\n (q) Quit\n");
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



        //Adds a contact to the address book. And makes sure it is valid data before saving to csv.
        public void AddContact()
        {
            Contact contact = new Contact();

            Console.Clear();
            Console.WriteLine("Enter the new contact's information");
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
                    orgFound = true;
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

            Contact contact = new Contact();

            Console.Clear();


            Console.WriteLine("Enter the Contact's First and Last names and information you would like update");
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

       
    }
}
