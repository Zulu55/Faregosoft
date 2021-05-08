﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Faregosoft.NewApi.Models
{
    public class AddProductImageRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public byte[] Image { get; set; }

        public Guid Guid { get; set; }
    }
}
