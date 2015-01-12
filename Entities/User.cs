using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User
    {
        #region Properties
        [Required()]
        public Guid UserID { get; set; }
        [Required(AllowEmptyStrings=false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings=false)]
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        [EmailAddress()]
        public string Email { get; set; }
        [Required(AllowEmptyStrings=false)]
        public string Title { get; set; }
        public string Address { get; set; }
        public bool Deleted { get; set; }
        #endregion

        #region Validation   /*This should be moved to a abstract class*/
        public List<ValidationResult> ValidationResult;

        public bool IsValid()
        {
            var context = new ValidationContext(this, null, null);
            ValidationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(this, context, ValidationResult, true);
        }
        #endregion
    }
}
