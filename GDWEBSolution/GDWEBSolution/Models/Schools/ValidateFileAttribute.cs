using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Schools
{
    public class ValidateFileAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            int MaxContentLength = 1024 * 1024 * 3; //3 MB
            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".JPG", ".GIF", ".PNG" };

            var file = value as HttpPostedFileBase;

            if (file == null)
                //  return false;
                return true;
            else if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions);
                return false;
            }
            else if (file.ContentLength > MaxContentLength)
            {
                ErrorMessage = "Your Photo is too large, maximum allowed size is : " + (MaxContentLength / 1024).ToString() + "MB";
                return false;
            }
            else
                return true;
        }






    }


   

    //public class MaxValueAttribute : ValidationAttribute
    //{
    //    private readonly int _maxValue;

    //    public MaxValueAttribute(int maxValue)
    //    {
    //        _maxValue = maxValue;
    //    }

    //    public override bool IsValid(object value)
    //    {
    //        return (int)value <= _maxValue;
    //    }
    //}

    //public class MinValueAttribute : ValidationAttribute
    //{
    //    private readonly int _minValue;

    //    public MinValueAttribute(int maxValue)
    //    {
    //        _minValue = maxValue;
    //    }

    //    public override bool IsValid(object value)
    //    {
    //        return (int)value >= _minValue;
    //    }
    //}
}