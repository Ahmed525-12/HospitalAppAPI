using HospitalDomain.DTOS;

namespace HospitalAPP.Email.Intrefaces
{
    public interface IEmailSettings
    {
        public void SendEmail(EmailDTO email);
    }
}