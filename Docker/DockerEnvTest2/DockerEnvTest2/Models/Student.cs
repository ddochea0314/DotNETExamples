﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DockerEnvTest2.Models
{
    public partial class Student
    {
        [Key]
        public int Idx { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int Grade { get; set; }
    }
}