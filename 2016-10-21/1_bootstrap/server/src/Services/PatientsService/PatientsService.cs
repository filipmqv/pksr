﻿using System;
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
	public class PatientsService : Service
	{
		public PatientsService ()
		{
			Db.CreateTableIfNotExists<Patient> ();
		}

		public object Get (DtoPatient req)
		{
			var patients = req.Id == null
				? Db.Select<Patient> ()
				: Db.Select<Patient> (q => q.Id == req.Id);

			var dtoPatients = patients.Select (p => new DtoPatient ().PopulateWith (p)).ToList ();

			return new DtoPatientResponse (dtoPatients);
		}

		/*public object Delete (DtoPatient req) 
		{

		}*/

		[SetStatus(HttpStatusCode.Accepted, "New patient was created")]
		public object Post (DtoPatient dtoPatient)
		{
			Patient newPatient = new Patient ().PopulateWith (dtoPatient);
			Db.Insert<Patient> (newPatient);
			return new DtoPatientResponse (new List<DtoPatient> () { dtoPatient });

		}

		/*public object Put (DtoPatient dtoPatient) 
		{

		}*/
	}
}

