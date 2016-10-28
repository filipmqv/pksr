using System;
using System.Linq;
using System.Net;
using Server.Logic.Patients;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace Server.Services.PatientService
{
	[Authenticate]
	public class PatientsService : BaseService
	{
		public PatientsService ()
		{
			Db.CreateTableIfNotExists<Patient> ();
		}

		[RequiredPermission("read")]
		public object Get (DtoPatient req)
		{
			var patients = req.Id == null
				? Db.Select<Patient> ()
				: Db.Select<Patient> (q => q.Id == req.Id);

			var dtoPatients = patients.Select (p => new DtoPatient ().PopulateWith (p)).ToList ();

			return new DtoPatientResponse (dtoPatients);
		}
			
		public object Delete (DtoPatient req) 
		{
			Db.DeleteById<Patient> (req.Id);
			return new HttpResult(HttpStatusCode.OK, "Patient deleted");
		}

		[SetStatus(HttpStatusCode.Accepted, "New patient created")]
		[RequiredRole("editor")]
		public object Post (DtoPatient dtoPatient)
		{
			Patient newPatient = new Patient ().PopulateWith (dtoPatient);
			Db.Insert<Patient> (newPatient);
			return new DtoPatientResponse (new List<DtoPatient> () { dtoPatient });

		}

		[SetStatus(HttpStatusCode.OK, "Patient updated")]
		public object Put (DtoPatient dtoPatient) 
		{
			Patient updatePatient = new Patient ().PopulateWith (dtoPatient);
			Db.Update<Patient> (updatePatient);
			return new DtoPatientResponse (new List<DtoPatient> () { dtoPatient });
		}
	}
}

