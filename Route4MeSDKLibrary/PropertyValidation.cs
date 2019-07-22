using System;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Route4MeSDK
{
    /// <summary>
    /// Validation of the class properties
    /// </summary>
    public static class PropertyValidation
    {
        public static ValidationResult ValidateMonthlyNthN(int N)
        {
            int[] nList = {1,2,3,4,5,-1}; 
            // Perform validation logic here and set isValid to true or false.

            if (Array.IndexOf(nList,N)>=0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    "The selected option is not available for this type of the schedule.");
            }
        }

        public static ValidationResult ValidateScheduleMode(string sMode)
        {
            string[] sList = { "daily", "weekly", "monthly", "annually" };
            // Perform validation logic here and set isValid to true or false.

            if (Array.IndexOf(sList, sMode) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    "The selected option is not available for this type of the schedule.");
            }
        }

        public static ValidationResult ValidateScheduleMonthlyMode(string sMode)
        {
            string[] sList = { "dates", "nth" };
            // Perform validation logic here and set isValid to true or false.

            if (Array.IndexOf(sList, sMode) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    "The selected option is not available for this type of the schedule.");
            }
        }

        public static ValidationResult ValidateEpochTime(object value)
        {
            int iTime = 0;
            
            if (int.TryParse(value.ToString(), out iTime) && value.GetType().ToString()!="System.String")
                return ValidationResult.Success;
            else
            {
                return new ValidationResult(
                    "The property time can not have the value "+value.ToString());
            }
        }

        public static ValidationResult ValidateOverrideAddresses(object value)
        {
            try
            {
                Route4MeSDK.DataTypes.OverrideAddresses oaddr = (Route4MeSDK.DataTypes.OverrideAddresses)value;
                return ValidationResult.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Validation of the override_addresses's value failed. " + ex.Message);
                return new ValidationResult(
                    "The property override_addresses can not have the value " + value.ToString());
            }
        }


    }
}
