namespace BLL
{
    using System;

    //ListView data helper class
    public class GoodsStatusHelper
    {
        public GoodsStatusHelper()
        {
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string OperationType { get; set; }
        public DateTime DateFrom { get; set; }
    }
}
