using Bakery_Project;

namespace TestProject1
{
    public class OrdersTest
    {
        [Fact]
        public void Orders_Should_Set_Properties_Correctly()
        {
            var orderDate = DateTime.Now;
            var condition = "Новый";

            var orders = new Orders
            {
                orderDate = orderDate,
                condition = condition
            };

            Assert.Equal(orderDate, orders.orderDate);
            Assert.Equal(condition, orders.condition);
        }     
    }
}