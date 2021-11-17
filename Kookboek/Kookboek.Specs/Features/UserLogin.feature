Feature: UserLogin
	The user can login in to its account

@mytag
Scenario: Actor logs in into the website
	When the actor goes to the login page
	Given the username is TestUser
	And password is TestPassword
	When the login button is pressed
	Then the actor should be logged in	
	
Scenario: Actor fails to log in into the website
	When the actor goes to the login page
	Given the username is test123
	And password is test123
	When the login button is pressed
	Then the actor should not be logged in 