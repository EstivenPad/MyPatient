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

    SELECT * FROM MedicalOrders AS MO
		INNER JOIN Patients AS P ON P.Id = MO.PatientId
		INNER JOIN Doctors AS MA ON MA.Id = MO.MAId
		INNER JOIN MedicalOrderDetails AS MO_DET ON MO_DET.MedicalOrderId = MO.Id
	WHERE MO.Id = @Id AND MO.Type = @Type;
END