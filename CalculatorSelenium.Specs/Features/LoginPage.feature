@Login
Feature: Login
![Login]
Kientic Login

Scenario: (1) Login into Kinetic with wrong creds
	Given the username is motion4
	And the password is transformWork
	When the login button is clicked
	Then the result should be Invalid username or password.

	Scenario: (2) Login into Kinetic with empty username field
	// Given the username is <>
	Given the password is transformWork
	When the login button is clicked
	Then the result should be The Username field is required.

	Scenario: (3) Login into Kinetic with empty password field
	Given the username is motion4	
	When the login button is clicked
	Then the result should be The Password field is required.
	
Scenario: (4) Login into Kinetic with actual  creds
	Given the username is motion
	And the password is TransformWork
	When the login button is clicked
	Then the result should be null