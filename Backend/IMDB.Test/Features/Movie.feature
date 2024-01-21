Feature: Movie Resource

Scenario: Get Movie All
	Given I am a client
	When I make GET Request '/api/movies'
	Then response code must be '200'
	And response data must look like '[{"id":1,"name":"M1","yearOfRelease":2012,"plot":"--","coverImage":"--","producerId":1,"actors":[],"genres":[]},{"id":2,"name":"M2","yearOfRelease":2017,"plot":"--","coverImage":"--","producerId":2,"actors":[],"genres":[]}]'

Scenario: Get Movie by Id
	Given I am a client
	When I make GET Request '/api/movies/1'
	Then response code must be '200'
	And response data must look like '{"id":1,"name":"M1","yearOfRelease":2012,"plot":"--","coverImage":"--","producerId":1,"actors":[],"genres":[]}'

Scenario: Delete Movie
	Given I am a client
	When I make Delete Request '/api/movies/2'
	Then response code must be '200'
	And response data must look like 'movie id = 2 has been deleted successfully'

Scenario: Create Movie
	Given I am a client
	When I am making a post request to '/api/movies' with the following Data '{"name":"M3","Plot":"--","CoverImage" : "--","YearOfRelease" : 2012,"ProducerId" : 1}'
	Then response code must be '200'
	And response data must look like 'Movie successfully added and the id of movie is 2'

Scenario: Update Movie
	Given I am a client
	When I make PUT Request '/api/movies/1' with the following Data with the following Data '{"name":"Mm1","Plot":"--","CoverImage":"--","YearOfRelease":2012,"ProducerId":1}'
	Then response code must be '200'
	And response data must look like 'Movie successfully updated'


Scenario: Delete Movie with invalid id
	Given I am a client
	When I make Delete Request '/api/movies/100'
	Then response code must be '400'
	And response data must look like 'there is no movie with id = 100'


Scenario: Get Movie by invalid Id
	Given I am a client
	When I make GET Request '/api/movies/100'
	Then response code must be '400'
	And response data must look like 'there is no movie with id = 100'


Scenario Outline: Update Movie with invalid data
	Given I am a client
	When I make PUT Request '<endpoints>' with the following Data with the following Data '<data>'
	Then response code must be '400'
	And response data must look like '<response>'
Examples:
	| endpoints       | data                                                                                   | response                             |
	| /api/movies/100 | {"name":"M1","Plot":"--","CoverImage" : "--","YearOfRelease" : 2012,"ProducerId" : 1}  | there is no movie with id = 100      |
	| /api/movies/1   | {"name":"","Plot":"--","CoverImage" : "--","YearOfRelease" : 2012,"ProducerId" : 1}    | movie's name is empty                |
	| /api/movies/1   | {"name":"M1","Plot":"","CoverImage" : "--","YearOfRelease" : 2012,"ProducerId" : 1}    | movie's plot is empty                |
	| /api/movies/1   | {"name":"M1","Plot":"--","CoverImage" : "","YearOfRelease" : 2012,"ProducerId" : 1}    | Movie's cover image is empty         |
	| /api/movies/1   | {"name":"M1","Plot":"--","CoverImage" : "--","YearOfRelease" : 1800,"ProducerId" : 1}  | Movie's year of release is not valid |
	| /api/movies/1   | {"name":"M1","Plot":"--","CoverImage" : "--","YearOfRelease" : 2002,"ProducerId" : -1} | Movie's producer id is not valid     |



Scenario: Create Movie with invalid data
	Given I am a client
	When I am making a post request to '/api/movies' with the following Data '<data>'
	Then response code must be '400'
	And response data must look like '<response>'

Examples:
	| data                                                                                   | response                             |
	| {"name":"","Plot":"--","CoverImage" : "--","YearOfRelease" : 2012,"ProducerId" : 1}    | movie's name is empty                |
	| {"name":"M1","Plot":"","CoverImage" : "--","YearOfRelease" : 2012,"ProducerId" : 1}    | movie's plot is empty                |
	| {"name":"M1","Plot":"--","CoverImage" : "","YearOfRelease" : 2012,"ProducerId" : 1}    | Movie's cover image is empty         |
	| {"name":"M1","Plot":"--","CoverImage" : "--","YearOfRelease" : 1800,"ProducerId" : 1}  | Movie's year of release is not valid |
	| {"name":"M1","Plot":"--","CoverImage" : "--","YearOfRelease" : 2002,"ProducerId" : -1} | Movie's producer id is not valid     |

