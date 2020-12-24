using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class Region : IRegion
    {
        public Region(string brand, string country)
        {
            Brand = brand;
            Country = country;
        }

        public string Brand { get; }
        public string Country { get; }


        public override bool Equals(Object obj)
        {
            if(!(obj is Region))
                return false;


            //if obj is Region than
            Region region = (Region)obj;

            //Check whether the compared objects reference the same data
            if (ReferenceEquals(this, region))
                return true;

            //Check whether any of the compared objects is null
            if (this is null || region is null)
                return false;

            //Check whether the region properties are equal
            return Brand == region.Brand && Country == region.Country;
        }
        public override int GetHashCode()
        {
            return Brand.GetHashCode() ^ Country.GetHashCode();
        }
    }
}
