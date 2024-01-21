Feature: Genre Resource

Scenario: Get All genre
	Given I am a client
	When I make GET Request '/api/genres'
	Then response code must be '200'
	And response data must look like '[{"id":1,"name":"G1"},{"id":2,"name":"G2"}]'

Scenario: Get Genre by Id
	Given I am a client
	When I make GET Request '/api/genres/1'
	Then response code must be '200'
	And response data must look like '{"id":1,"name":"G1"}'

Scenario: Delete Genre
	Given I am a client
	When I make Delete Request '/api/genres/2'
	Then response code must be '200'
	And response data must look like 'genre id = 2 has been deleted successfully'

Scenario: Create Genre
	Given I am a client
	When I am making a post request to '/api/genres' with the following Data '{"Name":"comedy"}'
	Then response code must be '200'
	And response data must look like 'Genre successfully added and the id of genre is 2'

Scenario: Update Genre
	Given I am a client
	When I make PUT Request '/api/genres/1' with the following Data with the following Data '{"Name":"Action"}'
	Then response code must be '200'
	And response data must look like 'Genre successfully updated'


Scenario: Delete Genre with invalid id
	Given I am a client
	When I make Delete Request '/api/genres/100'
	Then response code must be '400'
	And response data must look like 'there is no genre with provided id = 100'


Scenario: Get Genre by invalid Id
	Given I am a client
	When I make GET Request '/api/genres/100'
	Then response code must be '400'
	And response data must look like 'there is no genre with id = 100'


Scenario Outline: Update Genre with invalid data
	Given I am a client
	When I make PUT Request '<endpoints>' with the following Data with the following Data '<data>'
	Then response code must be '400'
	And response data must look like '<response>'
Examples:
	| endpoints       | data             | response                                 |
	| /api/genres/100 | {"Name":"Drama"} | there is no genre with provided id = 100 |
	| /api/genres/1   | {"Name":""}      | genre name is empty                      |



Scenario: Create Genre with invalid data
	Given I am a client
	When I am making a post request to '/api/genres' with the following Data '{"Name":""}'
	Then response code must be '400'
	And response data must look like 'genre name is empty'
	
