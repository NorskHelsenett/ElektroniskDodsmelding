namespace ElektroniskDodsmelding.Samples.Tests
{
    internal class Constants
    {
        internal const string BaseUrl = "https://dodsmelding-fhir.utvikling.nhn.no";

        internal static class Identifier
        {
            public const string FNRSystem = "urn:oid:2.16.578.1.12.4.1.4.1";
            public const string DNRSystem = "urn:oid:2.16.578.1.12.4.1.4.2";
            public const string ForeignIdentifier = ""; //DUF have no fixed value for Identifier.System in https://simplifier.net/guide/MasterPersonIndexR4/gd-Identifier-fregprofile
        }
    }
}