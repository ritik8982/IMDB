CREATE DATABASE IMDB;

USE IMDB;

CREATE TABLE Actors (
	Id INT IDENTITY(1, 1) PRIMARY KEY
	,Name VARCHAR(100)
	,Bio VARCHAR(100)
	,DOB DATE
	,Gender VARCHAR(100)
	);

CREATE TABLE Producers (
	Id INT IDENTITY(1, 1) PRIMARY KEY
	,Name VARCHAR(100)
	,Bio VARCHAR(100)
	,DOB DATE
	,Gender VARCHAR(100)
	);

CREATE TABLE Genres (
	Id INT IDENTITY(1, 1) PRIMARY KEY
	,Name VARCHAR(100)
	);

CREATE TABLE Movies (
	Id INT IDENTITY(1, 1) PRIMARY KEY
	,Name VARCHAR(100)
	,YearOfRelease INT
	,Plot VARCHAR(100)
	,ProducerId INT FOREIGN KEY REFERENCES Producers(Id) ON DELETE CASCADE
	,CoverImage VARCHAR(100)
	);

CREATE TABLE Movies_Actors (
	movieId INT FOREIGN KEY REFERENCES Movies(Id) ON DELETE CASCADE
	,actorId INT FOREIGN KEY REFERENCES Actors(Id) ON DELETE CASCADE
	,CONSTRAINT pk_Movies_Actors PRIMARY KEY (
		movieId
		,actorId
		)
	);

CREATE TABLE Movies_Genres (
	movieId INT FOREIGN KEY REFERENCES Movies(Id) ON DELETE CASCADE
	,genreId INT FOREIGN KEY REFERENCES Genres(Id) ON DELETE CASCADE
	,CONSTRAINT pk_Movies_Genres PRIMARY KEY (
		movieId
		,genreId
		)
	);

	

	CREATE TABLE Reviews ( Id INT IDENTITY(1, 1) PRIMARY KEY, Message varchar(100), 
	movieId INT FOREIGN KEY REFERENCES Movies(Id) ON DELETE CASCADE);


ALTER PROCEDURE usp_movie_insert
	 @Id int
	,@Name VARCHAR(100)
	,@YearOfRelease INT
	,@Plot VARCHAR(400)
	,@CoverImage VARCHAR(100)	
	,@ProducerId INT
	,@ActorIds VARCHAR(100)
	,@GenreIds VARCHAR(100)
	,@Result INT OUTPUT -- Change: Use OUTPUT parameter for result
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
	INSERT INTO Movies (
		Name
		,YearOfRelease
		,Plot
		,CoverImage
		,ProducerId
		)
	VALUES (
		@Name
		,@YearOfRelease
		,@Plot
		,@CoverImage
		,@ProducerId
		);

	DECLARE @movieId INT;

	SET @movieId = CAST(SCOPE_IDENTITY() AS INT);


	INSERT INTO Movies_Genres (
		movieId
		,genreId
		)
	SELECT @movieId
		,value AS genreId
	FROM STRING_SPLIT(@genreIds, ',');

	INSERT INTO Movies_Actors (
		movieId
		,actorId
		)
	SELECT @movieId
		,value AS actorId
	FROM STRING_SPLIT(@actorIds, ',');

	 COMMIT TRANSACTION
        SET @Result = @movieId;  -- Set the result to @movieId on successful update
    END TRY

    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Result = -1;
    END CATCH
END



ALTER PROCEDURE usp_movie_update
    @movieId INT
    ,@Name VARCHAR(100)
    ,@YearOfRelease INT
    ,@Plot VARCHAR(MAX)
    ,@CoverImage VARCHAR(MAX)
    ,@ProducerId INT
    ,@actorIds VARCHAR(100)
    ,@genreIds VARCHAR(100)
    ,@Result INT OUTPUT -- Change: Use OUTPUT parameter for result
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        UPDATE dbo.Movies
        SET Name = @Name
            ,YearOfRelease = @YearOfRelease
            ,Plot = @Plot
            ,CoverImage = @CoverImage
            ,ProducerId = @ProducerId
        WHERE Id = @movieId;

        DELETE
        FROM dbo.Movies_Genres
        WHERE movieId = @movieId;

        DELETE
        FROM dbo.Movies_Actors
        WHERE movieId = @movieId;

        INSERT INTO dbo.Movies_Genres (
            movieId
            ,genreId
            )
        SELECT @movieId
            ,value AS genreId
        FROM STRING_SPLIT(@genreIds, ',');

        INSERT INTO dbo.Movies_Actors (
            movieId
            ,actorId
            )
        SELECT @movieId
            ,value AS actorId
        FROM STRING_SPLIT(@actorIds, ','); 

        COMMIT TRANSACTION
        SET @Result = @movieId;  -- Set the result to @movieId on successful update
    END TRY

    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Result = -1;
    END CATCH
END
