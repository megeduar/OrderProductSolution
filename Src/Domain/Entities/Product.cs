using System;

namespace Domain.Entities
{
    public class Product
    {
        #region Properties
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public int Count { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        #endregion

        #region Properties Navigator
        #endregion
    }
}