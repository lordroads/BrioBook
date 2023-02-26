using Azure.Core;
using Brio.Confirm.Models;
using Brio.Confirm.Templates;
using Brio.Database.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Brio.Confirm.Services.Impl;

public class ConfirmService : IConfirmService
{
    private readonly IConfirmRepository _confirmRepository;
    private readonly IUserRepository _userRepository;

    public ConfirmService(IConfirmRepository confirmRepository, IUserRepository userRepository)
    {
        _confirmRepository = confirmRepository;
        _userRepository = userRepository;
    }

    public Guid Create(int userId)
    {
        var confirmId = _confirmRepository.Create(new ConfirmId
        {
            UserId = userId
        });

        SendEmail(_userRepository.Get(userId).Login, confirmId);

        return confirmId;
    }

    public bool SendEmail(string mail, Guid confirmId)
    {
        try
        {


            string from = "info@briogrunge.ru";

            MailAddress addressFrom = new MailAddress(from, "Briogrunge Informatio");
            MailAddress addressTo = new MailAddress(mail);
            MailMessage message = new MailMessage(addressFrom, addressTo);

            message.IsBodyHtml = true;
            message.Subject = "Подтверждение почты с сайта Briogrunge.";

            string htmlString = new MailBuilder()
                .SetAddressHost("https://localhost:7190/Account/Confirm?ConfirmId=")
                .SetConfirmId(confirmId.ToString())
                .BuildMail();
            message.Body = htmlString;

            using SmtpClient client = new SmtpClient("smtp.timeweb.ru", 25);
            client.Credentials = new NetworkCredential(addressFrom.Address, "Br109run9E");
            client.EnableSsl = true;
            client.Send(message);

            return true;

        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public IList<BigData> GetAll()
    {
        var confirmIds = _confirmRepository.GetAll();
        var users = _userRepository.GetAll();

        return confirmIds.Select(item => new BigData 
        { 
            Id = item.Id,
            UserId = item.UserId,
            User = users.FirstOrDefault(user => user.Id == item.UserId)
        }).ToList();
    }

    public bool SetConfirm(Guid confirmId)
    {
        var data = _confirmRepository.Get(confirmId);

        if (data is null)
        {
            return false;
        }

        var user = _userRepository.Get(data.UserId);


        if (user is null)
        {
            return false;
        }

        user.ConfirmEmail = true;

        return _userRepository.Update(user);
    }
}