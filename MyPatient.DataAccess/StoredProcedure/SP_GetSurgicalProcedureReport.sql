USE MyPatientDB
GO
CREATE PROCEDURE SP_GetSurgicalProcedureReport
@ResidentId BIGINT
AS
SELECT DISTINCT
	SP.Id AS SurgicalProcedureID,
	SP.CreatedDate,
	P.Name AS Name_Patient,
	P.Age AS Age_Patient,
	SP.Diagnostic,
	SP.[Procedure],
	R.Id AS ID_Resident,
	CONCAT(CASE
				WHEN R.Sex = 0 THEN 'Dr. '
				ELSE 'Dra. '
			END,
	R.FirstName, ' ', R.LastName) AS Name_Resident,
	R.Type AS Level_Resident,
	SPD.Id AS ID_Discovery,
	SPD.Description AS Discovery,
	CONCAT(CASE
				WHEN MA.Sex = 0 THEN 'Dr. '
				ELSE 'Dra. '
			END,
	MA.FirstName, ' ', MA.LastName) AS Name_MA,
	MA.Type AS Level_MA
FROM Doctor_SurgicalProcedure AS CB
	INNER JOIN Doctor_SurgicalProcedure AS D_SP ON D_SP.SurgicalProdecureId = CB.SurgicalProdecureId
	INNER JOIN SurgicalProcedures AS SP ON SP.Id = CB.SurgicalProdecureId
	INNER JOIN SurgicalProceduresDiscoveries AS SPD ON SPD.SurgicalProcedureId = SP.Id 
	INNER JOIN Patients AS P ON SP.PatientId = P.Id
	INNER JOIN Doctors AS MA ON MA.Id = P.MAId
	INNER JOIN Doctors AS R ON R.Id = D_SP.DoctorId
WHERE CB.DoctorId = @ResidentId
GO;