using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrKouk.Api.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emailFrom,string emailTo, string subject, string message);
    }
}
