Feature: Producer Resource

Scenario: Get Producer All
	Given I am a client
	When I make GET Request '/api/producers'
	Then response code must be '200'
	And response data must look like '[{"id":1,"name":"p1","bio":"p1's bio","dob":"0001-01-01T00:00:00","gender":"male"},{"id":3,"name":"p3","bio":"p3's bio","dob":"0001-01-01T00:00:00","gender":"male"}]'

Scenario: Get Producer by Id
	Given I am a client
	When I make GET Request '/api/producers/1'
	Then response code must be '200'
	And response data must look like '{"id":1,"name":"p1","bio":"p1's bio","dob":"0001-01-01T00:00:00","gender":"male"}'

Scenario: Delete producer
	Given I am a client
	When I make Delete Request '/api/producers/3'
	Then response code must be '200'
	And response data must look like 'producer id = 3 has been deleted successfully'

Scenario: Create producer
	Given I am a client
	When I am making a post request to '/api/producers' with the following Data '{"Name":"Allu Arjun","Bio":"p2's bio","DOB":"11/03/2002","Gender":"male"}'
	Then response code must be '200'
	And response data must look like 'Producer successfully added and the id of producer is 2'

Scenario: Update producer
	Given I am a client
	When I make PUT Request '/api/producers/1' with the following Data with the following Data '{"Name":"Ram Charan","Bio":"p1's bio","DOB":"11/03/2002","Gender":"male"}'
	Then response code must be '200'
	And response data must look like 'Producer successfully updated'


Scenario: Delete producer with invalid id
	Given I am a client
	When I make Delete Request '/api/producers/100'
	Then response code must be '400'
	And response data must look like 'there is not producer with provided id = 100'


Scenario: Get Producer by invalid Id
	Given I am a client
	When I make GET Request '/api/producers/100'
	Then response code must be '400'
	And response data must look like 'there is no producer with id = 100'


Scenario Outline: Update producer with invalid data
	Given I am a client
	When I make PUT Request '/api/producers/100' with the following Data with the following Data '<data>'
	Then response code must be '400'
	And response data must look like '<response>'
Examples:
	| data                                                                      | response                                                                  |
	| {"Name":"Ram Charan","Bio":"p1's bio","DOB":"11/03/2002","Gender":"male"} | there is no producer with  id = 100                                       |
	| {"Name":"","Bio":"p1's bio","DOB":"11/03/2002","Gender":"male"}           | producer name is empty                                                    |
	| {"Name":"Ram Charan","Bio":"","DOB":"11/03/2002","Gender":"male"}         | producer Bio is empty                                                     |
	| {"Name":"Ram Charan","Bio":"p1's bio","DOB":"11/03/2002","Gender":""}     | producer Gender is empty                                                  |
	| {"Name":"Allu Arjun","Bio":"p2's bio","DOB":"2002/03/11","Gender":"male"} | producer dob is not valid, please provide valid date in dd/mm/yyyy format |




Scenario: Create producer with invalid data
	Given I am a client
	When I am making a post request to '/api/producers' with the following Data '<data>'
	Then response code must be '400'
	And response data must look like '<response>'

Examples:
	| data                                                                      | response                                                                  |
	| {"Name":"","Bio":"p2's bio","DOB":"11/03/2002","Gender":"male"}           | producer name is empty                                                    |
	| {"Name":"Allu Arjun","Bio":"","DOB":"11/03/2002","Gender":"male"}         | producer Bio is empty                                                     |
	| {"Name":"Allu Arjun","Bio":"p2's bio","DOB":"11/03/2002","Gender":""}     | producer Gender is empty                                                  |
	| {"Name":"Allu Arjun","Bio":"p2's bio","DOB":"2002/03/11","Gender":"male"} | producer dob is not valid, please provide valid date in dd/mm/yyyy format |
	
