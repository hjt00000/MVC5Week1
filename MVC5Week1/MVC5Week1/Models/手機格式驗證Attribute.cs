﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MVC5Week1.Models
{
    public class 手機格式驗證Attribute : DataTypeAttribute
    {
        private static Regex _regex = new Regex("\\d{4}-\\d{6}", RegexOptions.IgnoreCase);

        public 手機格式驗證Attribute() : base(DataType.PhoneNumber)
        {
            
        }

        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return true;
            }
            string valueAsString = value as string;
            return valueAsString != null && _regex.Match(valueAsString).Length > 0;
        }
    }
}