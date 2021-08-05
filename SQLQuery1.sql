CREATE PROCEDURE dbo.UpdateDetails
	@id int,
	@name varchar(50),
	@Base_pay float

AS
BEGIN
UPDATE employee_payroll SET BasicPay=@Base_pay WHERE id=@id AND name=@name
END;
------
CREATE PROCEDURE dbo.retriveBasedOnName
	@name varchar(30)

AS
BEGIN
SELECT * FROM employee_payroll WHERE name=@name
END;