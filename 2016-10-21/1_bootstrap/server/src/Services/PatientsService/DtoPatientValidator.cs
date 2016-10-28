using System;
using Server.Logic.Patients;
using ServiceStack.FluentValidation;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;

namespace Server.Services.PatientService
{
	public class DtoPatientValidator : AbstractValidator<DtoPatient>
	{
		public DtoPatientValidator ()
		{
			RuleSet (ApplyTo.Post, () => RuleFor (r => r.Id).Must (i => i == null));
		}
	}
}

