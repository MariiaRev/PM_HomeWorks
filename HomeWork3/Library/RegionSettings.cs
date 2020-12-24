
namespace Library
{
    public class RegionSettings : IRegionSettings
    {
        public string WebSite { get; }

        public RegionSettings(string site)
        {
            WebSite = site;
        }
    }
}
