using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        #region .ctr
        public User()
        {
            Products = new HashSet<Product>();
            Orders = new HashSet<Order>();
        }
        #endregion

        public Guid UserId { get; set; }

        #region Properties Navigator
        public virtual ICollection<Product> Products { get; private set; }
        public virtual ICollection<Order> Orders { get; private set; }
        #endregion
    }
}