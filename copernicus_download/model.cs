using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copernicus_download
{
    class model
    {
    }

    public class postObjModel
    {
        public Input input { get; set; }
        public Output output { get; set; }
        public string evalscript { get; set; }
    }

    public class Input
    {
        public Bounds bounds { get; set; }
        public Datum[] data { get; set; }
    }

    public class Bounds
    {
        public Propertes properties { get; set; }
        public double[] bbox { get; set; }
    }

    public class Propertes
    {
        public string crs { get; set; }
    }

    public class Datum
    {
        public Datafilter dataFilter { get; set; }
        public Processing processing { get; set; }
        public string type { get; set; }
    }

    public class Datafilter
    {
        public Timerange timeRange { get; set; }
        public string mosaickingOrder { get; set; }
        public string previewMode { get; set; }
    }

    public class Timerange
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }

    public class Processing
    {
        public string upsampling { get; set; }
        public string downsampling { get; set; }
    }

    public class Output
    {
        public int width { get; set; }
        public int height { get; set; }
        public Respons[] responses { get; set; }
    }

    public class Respons
    {
        public string identifier { get; set; }
        public Format format { get; set; }
    }

    public class Format
    {
        public string type { get; set; }
    }

}
