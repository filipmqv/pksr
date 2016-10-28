﻿using System;
using Server.Logic.Patients;
using ServiceStack.FluentValidation;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;

namespace Server.Services.PatientService
{
	public class DtoPatientValidator : BaseValidator<DtoPatient>
	{
		public DtoPatientValidator ()
		{
			RuleSet (ApplyTo.Get, () => RuleFor (r => r.Id).Must (id => id == null || PatientWithIdExistsForSessionOwner(id.Value)));
			RuleSet (ApplyTo.Post, () => RuleFor (r => r.Id).Must (i => i == null));
			RuleSet (ApplyTo.Put | ApplyTo.Delete, () => RuleFor (r => r.Id).Must (id => id != null && PatientWithIdExistsForSessionOwner (id.Value)));
			RuleSet (ApplyTo.Post | ApplyTo.Put, () => 
				{
					RuleFor (r => r.FirstName).NotEmpty ();
					RuleFor (r => r.LastName).NotEmpty ();
				});


		}
	}
}

