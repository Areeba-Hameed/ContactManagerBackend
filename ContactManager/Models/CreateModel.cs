using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class CreateModel
    {
        public CreateModel()
        {
            this.Salutation = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
            // this.DisplayName = string.Empty;
            this.Phonenumber = string.Empty;
            this.LastChangeTimestamp = DateTime.UtcNow;
        }
        public int ID { get; set; }

        [MinLength(2, ErrorMessage = "Minimum length should be 2 characters.")]
        [Required]
        public string Salutation { get; set; }

        [MinLength(2, ErrorMessage = "Minimum length should be 2 characters.")]
        [Required]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Minimum length should be 2 characters.")]
        [Required]
        public string LastName { get; set; }


        private string displayName;

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(displayName))
                {
                    return Salutation + " " + FirstName + " " + LastName;
                }
                else
                {
                    return displayName;
                }
            }
            set
            {
                displayName = value;
            }
        }

        private DateTime birthdate;

        [BindNever]
        public DateTime Birthdate
        {
            get
            {

                return birthdate.AddDays(1);

            }
            set
            {
                birthdate = value;
            }
        }

        public DateTime createdate = DateTime.UtcNow.AddDays(1);


        public DateTime CreationTimestamp
        {
            get
            {

                return createdate;

            }
            set
            {
                createdate = value;
            }
        }
        [BindNever]
        public DateTime? LastChangeTimestamp { get; set; } //= DateTime.UtcNow.AddDays(1);


        private string days;
        [BindNever]
        public string NotifyHasBirthdaySoon
        {
            get
            {
                int monthDiff = Math.Abs((DateTime.UtcNow.Month - Birthdate.Month) + 12 * (DateTime.UtcNow.Year - Birthdate.Year));
                int dayDiff = Math.Abs((DateTime.UtcNow.Day - Birthdate.Day));

                // var s = (DateTime.UtcNow - Birthdate).TotalDays;
                if (Birthdate > DateTime.UtcNow)
                {
                    if ((DateTime.UtcNow.Month == Birthdate.Month) || ((DateTime.UtcNow.Month - 1) == Birthdate.Month))
                    {
                        if (dayDiff <= 14)
                        {
                            return "True";
                        }
                        else
                        {
                            return "False";
                        }
                    }
                    else
                    {
                        return "False";
                    }
                }
                else
                {
                    return "False";
                }

            }
            set
            {
                days = value;
            }
        }

        [Required]
        public string Email { get; set; }

        [BindNever]
        public string Phonenumber { get; set; }

    }
}
