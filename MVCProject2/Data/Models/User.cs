namespace MVCProject2.Data.Models
{
    public class User
    {
        public int Id { set; get;  }
        public string Username { set; get;  }
        public string Firstname { set; get;  }
        public string Lastname { set; get;  }
        public string Email { set; get;  }
        public string Password { set; get;  }
        public bool IsActive { set; get;  }
        public Guid ActivationCode { set; get;  }
        public virtual ICollection<Role> Roles { set; get;  }

    }
}
