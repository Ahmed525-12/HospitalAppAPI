﻿namespace HospitalDomain.DTOS
{
    public class EmployeeDTO
    {
        public string Email { get; set; }
        public int Salary { get; set; }
        public DateTime ShiftOfWork { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}