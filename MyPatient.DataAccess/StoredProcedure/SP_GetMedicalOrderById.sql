USE [MyPatientDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].SP_GetMedicalOrderById
(
    @Id BIGINT,
	@Type INT
)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT * FROM MedicalOrders AS mo
		INNER JOIN Patients AS P ON mo.PatientId = P.Id
		INNER JOIN MAs AS ma ON mo.MAId = ma.Id
		INNER JOIN MedicalOrderDetails AS mo_det ON mo_det.MedicalOrderId = mo.Id
	WHERE mo.Id = @Id AND mo.Type = @Type;
END