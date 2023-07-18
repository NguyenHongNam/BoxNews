﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxNews.Models.Domain
{
    [Table("tblAccounts")]
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        public string UserName { get; set; }

        public int UserPassword { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string RoleID { get; set; }
        public int LastLogin { get; set; }
    }
}