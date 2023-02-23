using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Entity.Entities
{
    public class Role : BaseEntity
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }

    }
}
