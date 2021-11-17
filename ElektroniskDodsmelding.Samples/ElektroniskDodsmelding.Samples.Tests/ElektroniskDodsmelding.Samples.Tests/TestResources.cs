using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;

namespace ElektroniskDodsmelding.Samples.Tests
{
    internal class TestResources
    {
        internal static PractitionerRole ValidPractitionerRole = new()
        {
            Organization = new ResourceReference(null, "ElektroniskDodsmelding.Samples Test Hospital"),
            Location = new List<ResourceReference>
            {
                new(null,"Building A")
            },
            Telecom = new List<ContactPoint>()
            {
                new(ContactPoint.ContactPointSystem.Email, ContactPoint.ContactPointUse.Work, "t@test.no"),
                new(ContactPoint.ContactPointSystem.Phone, ContactPoint.ContactPointUse.Work, "007007")
            }
        };
    }
}
