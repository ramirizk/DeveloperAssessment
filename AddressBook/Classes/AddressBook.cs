using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Classes
{
    class AddressBook
    {
        public StringBuilder csv = new StringBuilder();
        
        //Checks if file exists, if not creates file with headers.
        public AddressBook()
        {
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "AddressBook.csv")))
            {
                csv.AppendLine("First Name,Last Name,Organization,Phone Number,Email");
                File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "AddressBook.csv"), csv.ToString());
                csv.Clear();
            }
        }

        //Reads the addressbook file and returns all contacts in alphabetical order by last name.
        public List<Contact> AddressBookReader()
        {
            List<Contact> outputCSV = new List<Contact>();
            using(StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "AddressBook.csv")))
            {
                try
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        
                        string contactInfo = sr.ReadLine();
                        string[] contactInfoParse = contactInfo.Split(',');
                        outputCSV.Add(new Contact(){

                            FirstName = contactInfoParse[0],
                            LastName = contactInfoParse[1],
                            Organization = contactInfoParse[2],
                            PhoneNumber = contactInfoParse[3],
                            Email = contactInfoParse[4]
                        });

                    }


                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            List<Contact> outputOrdered = outputCSV.OrderBy(x => x.LastName).ToList();
            return outputOrdered;

        }

        //Writes a new contact to address book file
        public void AddressBookWriter(Contact contact)
        {

            csv.AppendLine($"{contact.FirstName},{contact.LastName},{contact.Organization},{contact.PhoneNumber},{contact.Email}");
            File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "AddressBook.csv"), csv.ToString());
            csv.Clear();
        }

        //Upates existing contact given same first and last names
        public bool UpdateExistingContact(Contact contact)
        {
            List<Contact> allCurrent = new List<Contact>();
            csv.Clear();
            bool updated =false;
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "AddressBook.csv"));
            
            foreach(var line in lines)
            {
                if (line.Contains(contact.LastName) && line.Contains(contact.FirstName))
                {
                    csv.AppendLine($"{contact.FirstName},{contact.LastName},{contact.Organization},{contact.PhoneNumber},{contact.Email}");
                    updated = true;     
                }
                else
                {
                    csv.AppendLine(line);
                    
                }
            }
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "AddressBook.csv"), csv.ToString());
            csv.Clear();
            return updated;
        }
    }
}
