namespace ClinicManagementSystem.Exceptions
{
    //* Exception class for invalid phone number

    public class InvalidPhoneNumberException(string msg) : Exception(msg) {}

    //* Exception class for invalid gender/sex
    public class InvalidGenderException(string msg) : Exception(msg) {}

    //* Exception class for invalid patient type
    public class InvalidPatientTypeException(string msg) : Exception(msg) { }
}
