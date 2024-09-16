using Azure.Identity;
using FootballManagerEF.Interfaces;
using MailKit.Security;
using Microsoft.Graph;
using Microsoft.Graph.Users.Item.SendMail;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
////using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

using MimeKitSmtpClient = MailKit.Net.Smtp.SmtpClient;
using System.Linq;
using System.Configuration;
using FootballManagerEF.Extensions;
////using MimeKitSmtpMessage = MailKit.Net.Smtp.Messa;

namespace FootballManagerEF.Classes
{
    public class Mailer : IMailer
    {
        SmtpData _smtpData;
        IMailHelper _mailHelper;

        public Mailer(SmtpData smtpData, IMailHelper mailHelper)
        {
            _smtpData = smtpData;
            _mailHelper = mailHelper;
        }

        public IMailer CreateInstance(SmtpData smtpData, IMailHelper mailHelper)
        {
            return new Mailer(smtpData, mailHelper);
        }

        public bool SendMail()
        {
            var mail = CreateMail();

            var SmtpServer = new SmtpClient(_smtpData.Host)
            {
                Port = int.Parse(_smtpData.Port),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpData.AgentSine, _smtpData.AgentDutyCode),
                EnableSsl = true
            };

            SmtpServer.Send(mail);

            return true;
        }

        ////public async Task<bool> SendMailUsingOAuthAsync()
        ////{
        ////    var result = await GetPublicClientOAuth2CredentialsAsync("SMTP", "jammyfitz@hotmail.com").ConfigureAwait(false);

        ////    // Note: We always use result.Account.Username instead of `Username` because the user may have selected an alternative account.
        ////    var oauth2 = new SaslMechanismOAuth2(result.Account.Username, result.AccessToken);

        ////    using (var client = new MimeKitSmtpClient())
        ////    {
        ////        var address = "smtp.office365.com";
        ////        //var address = "smtp-mail.outlook.com";
        ////        await client.ConnectAsync(address, 587, SecureSocketOptions.StartTls).ConfigureAwait(false);
        ////        ////client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        ////        ////client.AuthenticationMechanisms.Remove("LOGIN");
        ////        await client.AuthenticateAsync(oauth2).ConfigureAwait(false);
        ////        var mimeMessage = GetMimeMessage();
        ////        client.Send(mimeMessage);

        ////        await client.DisconnectAsync(true).ConfigureAwait(false);
        ////    }
        ////    return true;
        ////}

        ////private MimeMessage GetMimeMessage()
        ////{
        ////    var messageToSend = new MimeMessage
        ////    {
        ////        Sender = new MailboxAddress("Sender Mr Octopus", "jammyfitz@hotmail.com"),
        ////        Subject = "Subject Mr Octopus",
        ////    };

        ////    messageToSend.From.Add(new MailboxAddress("From Mr Octopus", "jammyfitz@hotmail.com"));
        ////    messageToSend.Body = new TextPart(TextFormat.Plain) { Text = "Some test body text" };
        ////    messageToSend.To.Add(new MailboxAddress("To Mr Octopus", "jammyfitz@hotmail.com"));

        ////    return messageToSend;
        ////}

        ////static async Task<AuthenticationResult> GetPublicClientOAuth2CredentialsAsync(string protocol, string emailAddress, CancellationToken cancellationToken = default)
        ////{
        ////    var options = new PublicClientApplicationOptions
        ////    {
        ////        ClientId = "5ca58399-4093-4adc-82f0-fe4d7357a173",
        ////        TenantId = "8e9213fa-5e1b-41c3-9e01-b0d015b601a5",
        ////        RedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient"
        ////    };

        ////    var publicClientApplication = PublicClientApplicationBuilder
        ////        .CreateWithApplicationOptions(options)
        ////        .Build();

        ////    string[] scopes;

        ////    if (protocol.Equals("IMAP", StringComparison.OrdinalIgnoreCase))
        ////    {
        ////        scopes = new string[]
        ////        {
        ////            "email",
        ////            "offline_access",
        ////            "https://outlook.office.com/IMAP.AccessAsUser.All"
        ////        };
        ////    }
        ////    else if (protocol.Equals("POP", StringComparison.OrdinalIgnoreCase))
        ////    {
        ////        scopes = new string[]
        ////        {
        ////            "email",
        ////            "offline_access",
        ////            "https://outlook.office.com/POP.AccessAsUser.All"
        ////        };
        ////    }
        ////    else
        ////    {
        ////        ////"https://graph.microsoft.com/SMTP.Send"
        ////        scopes = new string[]
        ////        {
        ////            "email",
        ////            "offline_access",
        ////            "https://outlook.office.com/SMTP.Send"
        ////        };
        ////    }

        ////    try
        ////    {
        ////        // First, check the cache for an auth token.
        ////        return await publicClientApplication.AcquireTokenSilent(scopes, emailAddress).ExecuteAsync(cancellationToken);
        ////    }
        ////    catch (MsalUiRequiredException)
        ////    {
        ////        // If that fails, then try getting an auth token interactively.
        ////        return await publicClientApplication.AcquireTokenInteractive(scopes).WithLoginHint(emailAddress).ExecuteAsync(cancellationToken);
        ////    }
        ////}

        public async void SendEmailViaGraphApi()
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var tenantId = _smtpData.SmtpAgentDutyParam1;
            var clientId = _smtpData.SmtpAgentDutyParam2;
            var clientSecret = _smtpData.SmtpAgentDutyParam3;

            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            var requestBody = new SendMailPostRequestBody
            {
                Message = GetMessage(),
            };

            await graphClient.Users["f9470d39-b9ed-43e3-ad42-724eb250b640"].SendMail.PostAsync(requestBody);
        }

        private Message GetMessage()
        {
            return new Message
            {
                Subject = _mailHelper.GetSubject(),
                Body = new ItemBody
                {
                    ContentType = BodyType.Text,
                    Content = _mailHelper.GetBody(),
                },
                ToRecipients = new List<Recipient>
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = "jammyfitz@hotmail.com",
                        },
                    }
                },
                BccRecipients = new List<Recipient>
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = "charlotteslayford@hotmail.com",
                        },
                    },
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = "charlotteslayford@gmail.com",
                        },
                    }
                },
            };
        }

        private List<Recipient> GetBccRecipients()
        {
            return _mailHelper.GetToAddresses()
                .GetWithout(ConfigurationManager.AppSettings["OrganiserEmail"])
                .Select(x => GetRecipient(x))
                .ToList();
        }

        private static Recipient GetRecipient(string x)
        {
            return new Recipient
            {
                EmailAddress = new EmailAddress
                {
                    Address = x,
                },
            };
        }

        private static List<Recipient> GetToRecipients()
        {
            return new List<Recipient>
            {
                new Recipient
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = ConfigurationManager.AppSettings["OrganiserEmail"],
                    },
                }
            };
        }

        public MailMessage CreateMail()
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_mailHelper.GetFromAddress()),
                Subject = _mailHelper.GetSubject(),
                Body = _mailHelper.GetBody(),
            };

            foreach (var address in _mailHelper.GetToAddresses())
                mail.To.Add(address);

            return mail;
        }

        public Task<bool> SendMailUsingOAuthAsync()
        {
            throw new NotImplementedException();
        }

        ////public bool SendMail()
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public void SendEmailViaGraphApi()
        ////{
        ////    throw new NotImplementedException();
        ////}
    }
}
