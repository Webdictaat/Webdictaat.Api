
namespace Webdictaat.Core
{
    public class ConfigVariables
    {
        public string DictaatRoot { get; set; }
        public string DictatenDirectory { get; set; }
        public string TemplatesDirectory { get; set; }
        public string MenuConfigName { get; set; }
        public string PagesDirectory { get; set; }
        public string ImagesDirectory { get; set; }
        public string DictaatConfigName { get; set; }
        public object StyleDirectory { get; set; }

        public string JWTSECRET { get; set; }
    }
}
