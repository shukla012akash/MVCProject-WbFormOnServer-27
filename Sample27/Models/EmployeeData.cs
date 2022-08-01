using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Sample27.Models
{
    public class EmployeeData
    {
        
        public int Id { get; set; }
        [MaxLength(10, ErrorMessage = "First Name should not be more than 10 char")]
        public string FName { get; set; }
        [MaxLength(10, ErrorMessage = "Last Name should not be more than 10 char")]
        public string LName { get; set; }

        public string FullName
        {
            get
            {
                return FName + " " + LName;
            }
        }


        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public long Phone { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }


    }
}
