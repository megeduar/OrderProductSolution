using System;

namespace Domain.Entities
{
    public enum OrderState
    {
        CREATED, CONFIRMED, CANCELLED
    }

    public class Order
    {
        #region .ctr
        public Order()
        {
        }
        #endregion

        #region Properties
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }
        public int Count { get; set; }
        #endregion

        #region Properties Navigator
        public Product Product { get; set; }
        #endregion
    }
}