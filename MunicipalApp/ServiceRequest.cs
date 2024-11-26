using System;
using System.Collections.Generic;

namespace MunicipalApp
{
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public int RequestID { get; set; }
        public string Category { get; set; } = "General";
        public string Status { get; set; } = "Pending";
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }
        public List<int> Dependencies { get; set; } = new List<int>();

        // Default constructor is now sufficient for object initialization
        public ServiceRequest() { }

        public int CompareTo(ServiceRequest other)
        {
            if (other == null) return 1; // Handle null comparison
            return SubmissionDate.CompareTo(other.SubmissionDate); // Sort by submission date
        }

        public override string ToString()
        {
            return $"RequestID: {RequestID}, Category: {Category}, Status: {Status}, SubmissionDate: {SubmissionDate}, Dependencies: {string.Join(", ", Dependencies)}";
        }
    }
}
//Part of this code was adapted from Coderanch
//https://coderanch.com/t/625189/java/Writing-compareTo-equals-method-Object
//Accessed 29 October 2024
