using System.Collections.Generic;

namespace Library.ERPReports
{
    public class ProductExtendedEqualityComparer : IEqualityComparer<ProductExtended>
    {
        public bool Equals(ProductExtended pr1, ProductExtended pr2)
        {
            //Check whether the compared objects reference the same data
            if (ReferenceEquals(pr1, pr2))
                return true;

            //Check whether any of the compared objects is null
            if (pr1 is null || pr2 is null)
                return false;

            //Check whether the products' properties are equal
            return pr1.Id == pr2.Id && pr1.Location == pr2.Location;
        }


        public int GetHashCode(ProductExtended product)
        {
            //Check whether the object is null
            if (product is null)
                return 0;

            //Get hash code for the Id field (Id is never null)
            int hashProductId = product.Id.GetHashCode();

            //Get hash code for the Location field if it is not null
            int hashProductLocation = product.Location == null ? 0 : product.Location.GetHashCode();

            //Get hash code for the Brand field if it is not null
            int hashProductBrand = product.Brand == null ? 0 : product.Brand.GetHashCode();
            
            //Get hash code for the Model field if it is not null
            int hashProductModel = product.Model == null ? 0 : product.Model.GetHashCode();


            //Calculate the hash code for the product
            return hashProductId ^ hashProductLocation ^ hashProductBrand ^ hashProductModel;
        }
    }
}
