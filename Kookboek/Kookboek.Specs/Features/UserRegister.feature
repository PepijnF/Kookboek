Feature: UserRegister
	The user can create a new account

@mytag
Scenario: The actor can register an account
	When the actor goes to the register page
	Given the new username is testregiser
	And the new password is testregister
	When the register button is pressed
	Then the actor should be logged in 
	