using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity1
{

    //Это мои попытки создания вручную

    //public class Users
    //{
    //    [Key]
    //    public int id { get; set; }
    //    public string userLogin { get; set; }
    //    public string userPassword { get; set; }
    //    [ForeignKey("Contact")]
    //    public int individual { get; set; }
    //    public DateTime? Create_Date { get; set; }
    //    public DateTime? Update_Date { get; set; }
    //   public virtual Contact Contact { get; set; }


    //}

    //public class Account
    //{
    //    public int id { get; set; }
    //    public string AccountName { get; set; }
    //    public DateTime Create_Date { get; set; }
    //    public DateTime Update_Date { get; set; }
    //}

    //public class Contact
    //{
     
    //    [Key]
    //    public int id { get; set; }
    //    public string ContactSurname { get; set; }
    //    public string ContactName { get; set; }
    //    public int? legalUser { get; set; }
    //    public DateTime? Create_Date { get; set; }
    //    public DateTime? Update_Date { get; set; }
    //    public virtual ICollection<Users> Users { get; set; }


    //}
    public class UserContext : DbContext
    {
        public UserContext() :
            base("name=UserDB")
        { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Adress> Adresses { get; set; }


    }
    class Program
    {
        static void Main(string[] args)
        {
            using (UserContext db = new UserContext())
            {
               //Я не уверен что судь задания заключалась в добавлении БД таким образом, но прописать стрингу на базу я не смог,
               //а в гугле для DBFirst нашел такой вот метод
                var users = db.Users;
                 foreach (Users u in users)
                {
                    Console.WriteLine($"ID {u.id} Login {u.userLogin} Password {u.userPassword}");
                }
            }
        }
    }
}
