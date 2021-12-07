﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataApp.Models
{
    public class Operation
    {
        public Guid ID { set; get; }
        public string Expression { set; get; }
        public double Result { set; get; }
        public DateTime Date { set; get; }
        public string IP { set; get; }
        public string Browser { set; get; }
        public string Error { set; get; }
    }
}
