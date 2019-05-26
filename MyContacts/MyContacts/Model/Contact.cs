using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyContacts.Model
{
    [Table("Contacts")]
    public class Contact
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Uri { get; set; }
        public int NumberPhone { get; set; }
        public string E_Mail { get; set; }

    }
}
