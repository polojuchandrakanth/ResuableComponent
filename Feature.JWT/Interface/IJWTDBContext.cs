using Feature.JWT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.JWT.Interface
{
    public interface IJWTDBContext
    {
        DbSet<UserDetails> UserDetailsTbl { get; set; }
    }
}
