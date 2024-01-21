Feature: Actor Resource

Scenario: Get Actor All
	Given I am a client
	When I make GET Request '/api/actors'
	Then response code must be '200'
	And response data must look like '[{"id":1,"name":"A1","bio":"--","dob":"0001-01-01T00:00:00","gender":"male"},{"id":3,"name":"A3","bio":"--","dob":"0001-01-01T00:00:00","gender":"male"}]'

Scenario: Get Actor by Id
	Given I am a client
	When I make GET Request '/api/actors/1'
	Then response code must be '200'
	And response data must look like '{"id":1,"name":"A1","bio":"--","dob":"0001-01-01T00:00:00","gender":"male"}'

Scenario: Delete actor
	Given I am a client
	When I make Delete Request '/api/actors/3'
	Then response code must be '200'
	And response data must look like 'actor id = 3 has been deleted successfully'

Scenario: Create actor
	Given I am a client
	When I am making a post request to '/api/actors' with the following Data '{"Name":"Allu Arjun","Bio":"--","DOB":"11/03/2002","Gender":"male"}'
	Then response code must be '200'
	And response data must look like 'actor successfully added and the id of actor is 2'

Scenario: Update actor
	Given I am a client
	When I make PUT Request '/api/actors/1' with the following Data with the following Data '{"Name":"Ram Charan","Bio":"--","DOB":"11/03/2002","Gender":"male"}'
	Then response code must be '200'
	And response data must look like 'actor successfully updated'


Scenario: Delete actor with invalid id
	Given I am a client
	When I make Delete Request '/api/actors/100'
	Then response code must be '400'
	And response data must look like 'there is not actor with provided id = 100'


Scenario: Get Actor by invalid Id
	Given I am a client
	When I make GET Request '/api/actors/100'
	Then response code must be '400'
	And response data must look like 'there is no actor with id = 100'


Scenario Outline: Update actor with invalid data
	Given I am a client
	When I make PUT Request '/api/actors/100' with the following Data with the following Data '<data>'
	Then response code must be '400'
	And response data must look like '<response>'
Examples:
	| data                                                                | response                                                               |
	| {"Name":"Ram Charan","Bio":"--","DOB":"11/03/2002","Gender":"male"} | there is not actor with provided id = 100                              |
	| {"Name":"","Bio":"--","DOB":"11/03/2002","Gender":"male"}           | actor name is empty                                                    |
	| {"Name":"Ram Charan","Bio":"","DOB":"11/03/2002","Gender":"male"}   | actor Bio is empty                                                     |
	| {"Name":"Ram Charan","Bio":"---","DOB":"11/03/2002","Gender":""}    | actor Gender is empty                                                  |
	| {"Name":"Allu Arjun","Bio":"--","DOB":"2002/03/11","Gender":"male"} | actor dob is not valid, please provide valid date in dd/mm/yyyy format |
	



Scenario: Create actor with invalid data
	Given I am a client
	When I am making a post request to '/api/actors' with the following Data '<data>'
	Then response code must be '400'
	And response data must look like '<response>'

Examples:
	| data                                                                | response                                                               |
	| {"Name":"","Bio":"--","DOB":"11/03/2002","Gender":"male"}           | actor name is empty                                                    |
	| {"Name":"Allu Arjun","Bio":"","DOB":"11/03/2002","Gender":"male"}   | actor Bio is empty                                                     |
	| {"Name":"Allu Arjun","Bio":"--","DOB":"11/03/2002","Gender":""}     | actor Gender is empty                                                  |
	| {"Name":"Allu Arjun","Bio":"--","DOB":"2002/03/11","Gender":"male"} | actor dob is not valid, please provide valid date in dd/mm/yyyy format |
	
