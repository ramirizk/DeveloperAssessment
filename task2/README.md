#Task 2

##Epic -- Address Book Enhancements

The sales team is interested in switching all of their contacts over to the new application, but it is still missing a few features before they can fully commit to switching.

##Stories

###Story 1
As the user, I would like to also read in a list of contacts from a .csv file, in addition to being able to add a contact one at a time. The first line of the file should specify the order of the data, followed by the contacts to be added.

####example.csv
	[last name],[first name],[email],[phone],[organization]
	seinfeld,jerry,jerry@whatsthedealwith.com,212-555-9090,Comedians In Cars Getting Coffee
	kramer,cosmo,cosmo@kramericaindustries.com,212-555-0008,Kramerica Stadium

###Story 2
As the user, I would like to be notified when a contact has missing data or contains incorrect data. The other data should be output so that the contact in question can be updated. This includes:
 * Missing fields
 * Phone number that does not contain 11 numbers, or contains charaters besides numbers, parenthesis, dashes, and spaces
 * Email not contain an `@` symbol and after which, at least one `.` followed by at least 2 alphanumeric characters
 
###Story 3
As the user, I would like to be able to search for a user by first name, last name or organization based on partial values by adding an asterisk. The search should be case-sensative. 

For example searching for 'Ma*' would bring back entries with the first name 'Mark' or 'Mary' or 'Matt' in addition to contacts with the last name 'Madison' or 'Mallard', as well as organizations such as 'Madison Square Garden'.

Searching for '*y' would bring back entries such as 'Mary', 'Tony', 'Waverly' and 'Willoughby'.

