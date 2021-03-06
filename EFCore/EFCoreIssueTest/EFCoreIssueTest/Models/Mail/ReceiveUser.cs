using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreIssueTest.Models.Mail
{
    public class ReceiveUser
    {
        public int TemplateID { get; set; }

        public string UserID { get; set; }

        public string Name { get; set; }
        public string Addr { get; set; }

        public int Step { get; set; }
    }
}
