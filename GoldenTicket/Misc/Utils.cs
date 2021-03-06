﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoldenTicket.DAL;
using GoldenTicket.Models;

namespace GoldenTicket.Misc
{
    /**
     * <summary>
     * Has an assortment of helper methods used across the application. That didn't
     * cleanly fit into other areas of the application.
     * </summary>
     */
    public class Utils
    {
        // Database connection
        private static GoldenTicketDbContext db = new GoldenTicketDbContext();

        /**
         * <summary>Get Applicant objects from Selected objects</summary>
         * <param name="selecteds">Selected objects with Applicant fields</param>
         * <returns>List of applicants</returns>
         */
        public static List<Applicant> GetApplicants(IEnumerable<Selected> selecteds)
        {
            var applicants = new List<Applicant>();
            foreach (var s in selecteds)
            {
                applicants.Add(s.Applicant);
            }

            return applicants;
        }
        /**
         * <summary>Get Applicant objects from Shuffled objects</summary>
         * <param name="shuffleds">Shuffled objects with Applicant fields</param>
         * <returns>List of applicants</returns>
         */
        public static List<Applicant> GetApplicants(IEnumerable<Shuffled> shuffleds)
        {
            var applicants = new List<Applicant>();
            foreach (var s in shuffleds)
            {
                applicants.Add(s.Applicant);
            }

            return applicants;
        }

        /**
         * <summary>Get Applicant objects from Waitlisted objects</summary>
         * <param name="waitlisteds">Waitlisted objects with Applicant fields</param>
         * <returns>List of applicants</returns>
         */
        public static List<Applicant> GetApplicants(IEnumerable<Waitlisted> waitlisteds)
        {
            var applicants = new List<Applicant>();
            foreach (var w in waitlisteds)
            {
                applicants.Add(w.Applicant);
            }

            return applicants;
        }


        /**
         * <summary>Get Applicant objects from Applieds objects</summary>
         * <param name="waitlisteds">Waitlisted objects with Applicant fields</param>
         * <returns>List of applicants</returns>
         */
        public static List<Applicant> GetApplicants(IEnumerable<Applied> applieds)
        {
            var applicants = new List<Applicant>();
            foreach (var a in applieds)
            {
                applicants.Add(a.Applicant);
            }

            return applicants;
        }

        /**
         * <summary>Get School objects from Applied objects</summary>
         * <param name="applieds">Applied objects with School fields</param>
         * <returns>List of Schools</returns>
         */
        public static List<School> GetSchools(IEnumerable<Applied> applieds)
        {
            var schools = new List<School>();
            foreach(var a in applieds)
            {
                schools.Add(a.School);
            }

            return schools;
        }

        /**
         * <summary>Get School objects from Waitlisted objects</summary>
         * <param name="waitlisted">Waitlisted objects with School fields</param>
         * <returns>List of Schools</returns>
         */
        public static List<School> GetSchools(IEnumerable<Waitlisted> waitlisteds)
        {
            var schools = new List<School>();
            foreach (var a in waitlisteds)
            {
                schools.Add(a.School);
            }

            return schools;
        }

        /**
         * <summary>
         * Creates a string of applicants serialized to CSV format. Each applicant
         * is separated by a newline character. The first line is a header row. The last column
         * is optionally a list of schools the applicant applied for (semi-colon delimited).
         * </summary>
         * 
         * <param name="applicants">List of applicants</param>
         * <param name="printSchoolList">True if the list of schools applied to should be included</param>
         * <returns>CSV string</returns>
         */
        public static string ApplicantsToCsv(IEnumerable<Applicant> applicants, bool printSchoolList = true)
        {
            var csvText = new StringBuilder();

            // Header row
            csvText.Append("Confirmation Code,");
            csvText.Append("Student First Name,");
            csvText.Append("Student Middle Name,");
            csvText.Append("Student Last Name,");
            csvText.Append("Student Birthday,");
            csvText.Append("Student Gender,");
            csvText.Append("Student Street Address 1,");
            csvText.Append("Student Street Address 2,");
            csvText.Append("Student City,");
            csvText.Append("Student ZIP Code,");

            csvText.Append("Contact 1 First Name,");
            csvText.Append("Contact 1 Last Name,");
            csvText.Append("Contact 1 Phone,");
            csvText.Append("Contact 1 Email,");
            csvText.Append("Contact 1 Relationship,");

            csvText.Append("Contact 2 First Name,");
            csvText.Append("Contact 2 Last Name,");
            csvText.Append("Contact 2 Phone,");
            csvText.Append("Contact 2 Email,");
            csvText.Append("Contact 2 Relationship,");

            csvText.Append("Household Members,");
            csvText.Append("Household Monthly Income,");

            if (printSchoolList)
            {
                csvText.Append("Schools Applied");
            }

            csvText.Append('\n');
                
            // Loop through the applicants
            foreach (var a in applicants)
            {
                csvText.Append(a.ConfirmationCode); csvText.Append(',');
                csvText.Append(a.StudentFirstName); csvText.Append(',');
                csvText.Append(a.StudentMiddleName); csvText.Append(',');
                csvText.Append(a.StudentLastName); csvText.Append(',');
                csvText.Append(a.StudentBirthday.Value.ToString("MM/dd/yyyy")); csvText.Append(',');
                csvText.Append(a.StudentGender); csvText.Append(',');
                csvText.Append(a.StudentStreetAddress1); csvText.Append(',');
                csvText.Append(a.StudentStreetAddress2); csvText.Append(',');
                csvText.Append(a.StudentCity); csvText.Append(',');
                csvText.Append(a.StudentZipCode); csvText.Append(',');

                csvText.Append(a.Contact1FirstName); csvText.Append(',');
                csvText.Append(a.Contact1LastName); csvText.Append(',');
                csvText.Append(a.Contact1Phone); csvText.Append(',');
                csvText.Append(a.Contact1Email); csvText.Append(',');
                csvText.Append(a.Contact1Relationship); csvText.Append(',');

                csvText.Append(a.Contact2FirstName); csvText.Append(',');
                csvText.Append(a.Contact2LastName); csvText.Append(',');
                csvText.Append(a.Contact2Phone); csvText.Append(',');
                csvText.Append(a.Contact2Email); csvText.Append(',');
                csvText.Append(a.Contact2Relationship); csvText.Append(',');

                csvText.Append(a.HouseholdMembers); csvText.Append(',');
                csvText.Append(a.HouseholdMonthlyIncome); csvText.Append(',');

                if (printSchoolList)
                {
                    var applieds =
                        db.Applieds.Where(applied => applied.ApplicantID == a.ID)
                            .OrderBy(applied => applied.School.Name)
                            .ToList();

                    foreach (var applied in applieds)
                    {
                        csvText.Append(applied.School.Name);
                        csvText.Append(';');
                    }
                    csvText.Append(',');
                }

                csvText.Append('\n');
            }

            return csvText.ToString();
        }

        /**
         * <summary>Compares two applicants' to see if they're potentially duplicants</summary>
         * <param name="a1">An applicant</param>
         * <param name="a2">Another applicant (surprise! :-P)</param>
         * <returns>True if the applicants might be duplicates</returns>
         */
        public static bool ArePossiblyDuplicates(Applicant a1, Applicant a2)
        {
            return a1.Checksum() == a2.Checksum();
        }

        /**
         * <summary>Identifies possible duplicate applicants within a list of applicants</summary>
         * <param name="applicants">Applicants</param>
         * <returns>A list of possible duplicate applicants</returns>
         */
        public static List<Applicant> GetPossibleDuplicateApplicants(IEnumerable<Applicant> applicants)
        {
            var applicantsCopy = new List<Applicant>(applicants); // needed to prevent concurrent iteration of the same list during count
            var duplicates = new List<Applicant>();
            foreach (var a in applicants)
            {
                var checksum = a.Checksum();
                var count = applicantsCopy.Count(applicant => applicant.Checksum() == checksum);

                if (count > 1)
                {
                    duplicates.Add(a);
                }
            }

            return duplicates;            
        }

    }
}