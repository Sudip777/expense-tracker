using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Transaction
    {

        [Key]
        public int TransactionId { get; set; }


        //Category Id
        [Range(0, int.MaxValue, ErrorMessage ="Please Select a Category")]
        public int CategoryId { get; set; }
         
        // setting navigational key property
        // Establish foreign key relationship
        // Same should be same as entity
        public Category? Category { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Amount Should be Grater than 0")]
        public int Amount {  get; set; }

        [Column (TypeName = "nvarchar(75)")]
        public string? Note {  get; set; }  
        public DateTime Date { get; set; } = DateTime.Now;

        /*To encounter Navigational Property Category : foreign Key*/
        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return Category == null ? "" : Category.Icon + " " + Category.Title;
            }
        }


        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                return ((Category == null || Category.Type == "Expense") ? "-" : "+")  + Amount.ToString("C0");
            }
        }
    }
}
