using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace ElektroniskDodsmelding.Samples.Tests
{
    /// <summary>
    /// Runs tests for reporting a person dead.
    /// </summary>
    /// <remarks>
    ///  Requires valid JSON Web Token in the <see cref="TestJwt.ValidTokenWithDoctorRoleSecurityLevel4"/> from a HelseId client.
    /// </remarks>
    public class DeathMessageTest
    {
        private readonly FhirClient _fhirClient;

        public DeathMessageTest()
        {
            _fhirClient = new FhirClient(Constants.BaseUrl, messageHandler: new AuthorizedHttpClientHandler(TestJwt.ValidTokenWithDoctorRoleSecurityLevel4));
        }


        /// <summary>
        /// Run the process of reporting a person by reporting a death message.
        /// 1. Create PractitionerRole that holds the contact details for the reported doctor
        /// 2. Search in Patient by Identifier (FNR) to see if the person is already reported as dead. Only send death message if the person is currently registered as alive.
        /// 3. Create Patient with deceased date to report the person as dead. Creates the death message and the patients current life status wi
        /// </summary>
        /// <remarks>
        /// OBS! Wait for ca 10 min to hit a second run of this test.
        /// In our test environment, the person will be revived to life after ca 10 min.
        /// Alternative, pick a new person to kill by making changes in the <see cref="TestResources.ValidPatient"/>
        /// </remarks>
        [Fact]
        public async Task CreateDeathMessage_With_PractitionerRole_And_Patient_RetiurnOk()
        {
            var practitionerRoleResponse = await _fhirClient.CreateAsync(TestResources.ValidPractitionerRole);
            Assert.NotNull(practitionerRoleResponse.Id);

            try
            {
                // Currently the search will throw exception if the deceased don't exist as a patient already, and that's a good thing.
                var searchPatientResult = await _fhirClient.SearchAsync<Patient>(new SearchParams("identifier", TestResources.ValidPatient.Identifier.First().Value));
                var existingPatient = searchPatientResult?.Entry
                    .FirstOrDefault(p => p.Resource.TypeName == nameof(Patient))
                    ?.Resource as Patient;

                var deceasedAsFhirBoolean = existingPatient?.Deceased as FhirBoolean;

                Assert.True(deceasedAsFhirBoolean?.Value == false, "Person is already reported as dead");
            }
            catch(FhirOperationException){}
            
            var patientToKill = TestResources.ValidPatient;
            patientToKill.GeneralPractitioner = new List<ResourceReference>() { new ($"{nameof(PractitionerRole)}/{practitionerRoleResponse.Id}") };

            var patientResponse = await _fhirClient.CreateAsync(patientToKill);
            Assert.NotNull(patientResponse.Id);
            Assert.False(patientResponse.Deceased.TypeName == nameof(DateTime), "The patient should currently have time of death registered, but has not");
        }
    }
}
