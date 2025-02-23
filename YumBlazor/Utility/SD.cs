using YumBlazor.Data;

namespace YumBlazor.Utility
{
    public static class SD
    {
        public static string Role_Admin= "Admin";
        public static string Role_Customer = "Customer";

        public static string StatusPending = "Pending";
        public static string StatusReadyForPickUp = "ReadyForPickUp";
        public static string StatusCompleted = "Completed";
        public static string StatusCancelled = "Cancelled";

        public static List<OrderDetail>ConvertShoppingCartListToOrderDetails(List<ShoppingCart> shoppingCartList)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartList)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    Product = item.Product,
                    Price = (double)item.Product.Price,
                    Count = item.Count,
                    ProductName= item.Product.Name
                };
                orderDetails.Add(orderDetail);
            }
            return orderDetails;
        }
    }
}
