﻿using PCHI.WcfServices.API.PCHIServices.InterfaceContracts.Base;
using PCHI.WcfServices.API.PCHIServices.InterfaceContracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PCHI.WcfServices.API.PCHIServices.InterfaceContracts.Users
{
    /// <summary>
    /// Defines the patient functionality
    /// </summary>
    [ServiceContract]
    public interface IPatientService : IBaseService
    {
        /// <summary>
        /// Creates a new user Account if a user with the given external ID doesn't exist
        /// </summary>
        /// <param name="externalId">The external Id of the patient</param>
        /// <param name="userName">The requested username</param>        
        /// <param name="email">The email of the user</param>
        /// <param name="title">The title for the user</param>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="dateOfBirth">The date of birth</param>
        /// <param name="mobilePhone">The mobile phone number</param>
        /// <returns>An operation result indicating success or failure and the errors. The Data variable contains the new Patient Id</returns>
        [OperationContract(Name = "CreateAccountForExternal")]
        OperationResultAsString CreatePatient(string externalId, string userName, string email, string title, string firstName, string lastName, DateTime dateOfBirth, string mobilePhone);

        /// <summary>
        /// Gets the details of the given user
        /// </summary>
        /// <param name="patientId">The Id of the Patient to get the details for</param>
        /// <returns>The OperationResults continaining Patient Details (if successful)</returns>        
        [OperationContract]
        OperationResultAsUserDetails GetDetailsForPatient(string patientId);

        /// <summary>
        /// Updates the patient settings
        /// </summary>
        /// <param name="patientId">The Id of the patient to save the settings for</param>
        /// <param name="details">The details of the patient</param>
        /// <returns>An operation result indicating success or failure</returns>
        [OperationContract]
        OperationResult SavePatientDetails(string patientId, PatientDetails details);

        /// <summary>
        /// Finds all Patients that meet the given criteria
        /// </summary>
        /// <param name="firstName">The first name of the Patient</param>
        /// <param name="lastName">The last name of the Patient</param>
        /// <param name="dob">The date of birth of the Patient</param>
        /// <param name="email">The Patient email</param>
        /// <param name="phoneNumber">The phone number of the Patient</param>
        /// <param name="externalId">The external ID of the Patient</param>
        /// <returns>The list of Patients</returns>
        [OperationContract]
        OperationResultAsLists FindPatient(string firstName, string lastName, DateTime? dob, string email, string phoneNumber, string externalId);
    }
}
