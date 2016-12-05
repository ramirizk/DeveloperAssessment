using AddressBook.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressBookCLI littleBlackBook = new AddressBookCLI();
            littleBlackBook.Run();
        }
    }
}
