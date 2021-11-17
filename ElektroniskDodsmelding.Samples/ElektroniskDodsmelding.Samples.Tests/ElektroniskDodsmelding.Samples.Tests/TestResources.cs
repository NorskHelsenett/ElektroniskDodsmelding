using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace ElektroniskDodsmelding.Samples.Tests
{
    internal class TestResources
    {
        internal static PractitionerRole ValidPractitionerRole = new ()
        {
            Organization = new ResourceReference(null, "ElektroniskDodsmelding.Samples Test Hospital"),
            Location = new List<ResourceReference>
            {
                new(null,"Building A")
            },
            Telecom = new List<ContactPoint>()
            {
                new (ContactPoint.ContactPointSystem.Email, ContactPoint.ContactPointUse.Work, "t@test.no"),
                new (ContactPoint.ContactPointSystem.Phone, ContactPoint.ContactPointUse.Work, "007007")
            }
        };

        /// <summary>
        /// Valid person that is alive in Test PREG.
        /// FNR: 16047508839
        /// Name: ROBERT STEFANI
        /// </summary>
        internal static Patient ValidPatient = new ()
        {
            Identifier = new List<Identifier>()
            {
                new (Constants.Identifier.FNRSystem, "16047508839")
            },
            Deceased = new FhirDateTime(new DateTimeOffset(DateTime.Now.Date)),
            GeneralPractitioner = null
        };
    }
}
