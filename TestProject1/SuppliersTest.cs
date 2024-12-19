using Bakery_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class SuppliersTest
    {
        [Fact]
        public void Suppliers_Should_Set_Properties_Correctly()
        {
            var sName = "Поставщик А";
            var contactInformation = "123-456-789";

            var suppliers = new Suppliers
            {
                sName = sName,
                contactInformation = contactInformation
            };

            Assert.Equal(sName, suppliers.sName);
            Assert.Equal(contactInformation, suppliers.contactInformation);
        }      
    }
}
