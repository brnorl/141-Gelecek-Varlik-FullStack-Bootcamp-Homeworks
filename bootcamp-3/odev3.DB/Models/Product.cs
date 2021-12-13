using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


#nullable disable

namespace odev3.DB.Models
{
    public partial class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
