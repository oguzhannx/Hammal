﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hammal.Models
{
    public class UserAbility
    {
        [Key]
        public int Id { get; set; }
        
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public int AltCategoryId { get; set; }
        [ForeignKey("AltCategoryId")]
        [ValidateNever]
        public AltCategory AltCategory { get; set; }
    }
}
