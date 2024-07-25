using HospitalDomain.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPP.Email.Intrefaces
{
    public interface IEmailSettings
    {
        public void SendEmail(EmailDTO email);
    }
}