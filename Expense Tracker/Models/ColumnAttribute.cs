namespace Expense_Tracker.Models
{
    internal class ColumnAttribute : Attribute
    {
        public string? TypeName { get; set; }
    }
}