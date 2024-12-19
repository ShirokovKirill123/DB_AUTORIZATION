using Bakery_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class IngredientsTests
    {
        [Fact]
        public void Ingredients_Should_Set_Properties_Correctly()
        {
            var iName = "Сахар";
            var unitOfMeasurement = "кг";
            var pricePerUnit = 10.5m;
            var availableQuantity = 50;

            var ingredients = new Ingredients
            {
                iName = iName,
                unitOfMeasurement = unitOfMeasurement,
                pricePerUnit = pricePerUnit,
                availableQuantity = availableQuantity
            };

            Assert.Equal(iName, ingredients.iName);
            Assert.Equal(unitOfMeasurement, ingredients.unitOfMeasurement);
            Assert.Equal(pricePerUnit, ingredients.pricePerUnit);
            Assert.Equal(availableQuantity, ingredients.availableQuantity);
        }        
    }
}
