using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copernicus_download
{
    /// <summary>
    /// 用于球面墨卡托投影坐标系（EPSG:3857）的瓦片系统数学运算
    /// Tile System math for the Spherical Mercator projection coordinate system (EPSG:3857)
    /// </summary>
    public static class TileMath
    {
        //Earth radius in meters.以米为单位的地球半径
        private const double EarthRadius = 6378137;

        private const double MinLatitude = -85.05112878;
        private const double MaxLatitude = 85.05112878;
        private const double MinLongitude = -180;
        private const double MaxLongitude = 180;

        /// <summary>
        /// 将一个数字剪切到指定的最小值和最大值。
        /// Clips a number to the specified minimum and maximum values.
        /// </summary>
        /// <param name="n">The number to clip.</param>
        /// <param name="minValue">Minimum allowable value.</param>
        /// <param name="maxValue">Maximum allowable value.</param>
        /// <returns>The clipped value.</returns>
        private static double Clip(double n, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(n, minValue), maxValue);
        }

        /// <summary>
        /// 以像素为单位计算特定缩放级别（-180 度至 180 度）下地图的宽度和高度。
        /// Calculates width and height of the map in pixels at a specific zoom level from -180 degrees to 180 degrees.
        /// </summary>
        /// <param name="zoom">Zoom Level to calculate width at</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <returns>Width and height of the map in pixels</returns>
        public static double MapSize(double zoom, int tileSize)
        {
            return Math.Ceiling(tileSize * Math.Pow(2, zoom));
        }

        /// <summary>
        /// 计算特定纬度的地面分辨率（单位：米/像素）。
        /// Calculates the Ground resolution at a specific degree of latitude in meters per pixel.
        /// </summary>
        /// <param name="latitude">Degree of latitude to calculate resolution at</param>
        /// <param name="zoom">Zoom level to calculate resolution at</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <returns>Ground resolution in meters per pixels</returns>
        public static double GroundResolution(double latitude, double zoom, int tileSize)
        {
            latitude = Clip(latitude, MinLatitude, MaxLatitude);
            return Math.Cos(latitude * Math.PI / 180) * 2 * Math.PI * EarthRadius / MapSize(zoom, tileSize);
        }

        /// <summary>
        /// 确定指定纬度、详细程度和屏幕分辨率下的地图比例尺。
        /// Determines the map scale at a specified latitude, level of detail, and screen resolution.
        /// </summary>
        /// <param name="latitude">Latitude (in degrees) at which to measure the map scale.</param>
        /// <param name="zoom">Level of detail, from 1 (lowest detail) to 23 (highest detail).</param>
        /// <param name="screenDpi">Resolution of the screen, in dots per inch.</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <returns>The map scale, expressed as the denominator N of the ratio 1 : N.</returns>
        public static double MapScale(double latitude, double zoom, int screenDpi, int tileSize)
        {
            return GroundResolution(latitude, zoom, tileSize) * screenDpi / 0.0254;
        }

        /// <summary>
        /// 全局 在指定缩放级别下将像素坐标转换为地理空间坐标。 
        /// 全局像素坐标相对于地图左上角（90, -180）
        /// Global Converts a Pixel coordinate into a geospatial coordinate at a specified zoom level. 
        /// Global Pixel coordinates are relative to the top left corner of the map (90, -180)
        /// </summary>
        /// <param name="pixel">Pixel coordinates in the format of [x, y].</param>  
        /// <param name="zoom">Zoom level</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <returns>A position value in the format [longitude, latitude].</returns>
        public static double[] GlobalPixelToPosition(double[] pixel, double zoom, int tileSize)
        {
            var mapSize = MapSize(zoom, tileSize);

            var x = (Clip(pixel[0], 0, mapSize - 1) / mapSize) - 0.5;
            var y = 0.5 - (Clip(pixel[1], 0, mapSize - 1) / mapSize);

            return new double[] {
                360 * x,    //Longitude
                90 - 360 * Math.Atan(Math.Exp(-y * 2 * Math.PI)) / Math.PI  //Latitude
            };
        }

        /// <summary>
        /// 将一个点从经纬度 WGS-84 坐标（以度为单位）转换为指定详细程度的像素 XY 坐标。
        /// Converts a point from latitude/longitude WGS-84 coordinates (in degrees) into pixel XY coordinates at a specified level of detail.
        /// </summary>
        /// <param name="position">格式为 [longitude, latitude] 的位置坐标 
        /// Position coordinate in the format [longitude, latitude]</param>
        /// <param name="zoom">Zoom level.</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param> 
        /// <returns>A global pixel coordinate.</returns>
        public static double[] PositionToGlobalPixel(double[] position, int zoom, int tileSize)
        {
            var latitude = Clip(position[1], MinLatitude, MaxLatitude);
            var longitude = Clip(position[0], MinLongitude, MaxLongitude);

            var x = (longitude + 180) / 360;
            var sinLatitude = Math.Sin(latitude * Math.PI / 180);
            var y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            var mapSize = MapSize(zoom, tileSize);

            return new double[] {
                 Clip(x * mapSize + 0.5, 0, mapSize - 1),
                 Clip(y * mapSize + 0.5, 0, mapSize - 1)
            };
        }

        /// <summary>
        /// 将像素的 XY 坐标转换为包含指定像素的瓦片的 XY 坐标。
        /// Converts pixel XY coordinates into tile XY coordinates of the tile containing the specified pixel.
        /// </summary>
        /// <param name="pixel">Pixel coordinates in the format of [x, y].</param>  
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <param name="tileX">Output parameter receiving the tile X coordinate.</param>
        /// <param name="tileY">Output parameter receiving the tile Y coordinate.</param>
        public static void GlobalPixelToTileXY(double[] pixel, int tileSize, out int tileX, out int tileY)
        {
            tileX = (int)(pixel[0] / tileSize);
            tileY = (int)(pixel[1] / tileSize);
        }

        /// <summary>
        /// 从一个缩放级别到另一个缩放级别，对全局像素值执行缩放变换。
        /// Performs a scale transform on a global pixel value from one zoom level to another.
        /// </summary>
        /// <param name="pixel">Pixel coordinates in the format of [x, y].</param>  
        /// <param name="oldZoom">The zoom level in which the input global pixel value is from.</param>  
        /// <returns>A scale pixel coordinate.</returns>
        public static double[] ScaleGlobalPixel(double[] pixel, double oldZoom, double newZoom)
        {
            var scale = Math.Pow(2, oldZoom - newZoom);

            return new double[] { pixel[0] * scale, pixel[1] * scale };
        }

        /// <summary>
        /// 在一组全局像素值上执行从一个缩放级别到另一个缩放级别的缩放变换。
        /// Performs a scale transform on a set of global pixel values from one zoom level to another.
        /// </summary>
        /// <param name="pixels">A set of global pixel value from the old zoom level. Points are in the format [x,y].</param>
        /// <param name="oldZoom">The zoom level in which the input global pixel values is from.</param>
        /// <param name="newZoom">The new zoom level in which the output global pixel values should be aligned with.</param>
        /// <returns>A set of global pixel values that has been scaled for the new zoom level.</returns>
        public static double[][] ScaleGlobalPixels(double[][] pixels, double oldZoom, double newZoom)
        {
            var scale = Math.Pow(2, oldZoom - newZoom);

            var output = new System.Collections.Generic.List<double[]>();
            foreach (var p in pixels)
            {
                output.Add(new double[] { p[0] * scale, p[1] * scale });
            }

            return output.ToArray();
        }

        /// <summary>
        /// 将磁贴 XY 坐标转换为指定磁贴左上角像素的全局像素 XY 坐标。
        /// Converts tile XY coordinates into a global pixel XY coordinates of the upper-left pixel of the specified tile.
        /// </summary>
        /// <param name="tileX">Tile X coordinate.</param>
        /// <param name="tileY">Tile Y coordinate.</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <param name="pixelX">Output parameter receiving the X coordinate of the point, in pixels.</param>  
        /// <param name="pixelY">Output parameter receiving the Y coordinate of the point, in pixels.</param>  
        public static double[] TileXYToGlobalPixel(int tileX, int tileY, int tileSize)
        {
            return new double[] { tileX * tileSize, tileY * tileSize };
        }

        /// <summary>
        /// 将磁贴 XY 坐标转换为指定详细程度的 quadkey。
        /// Converts tile XY coordinates into a quadkey at a specified level of detail.
        /// </summary>
        /// <param name="tileX">瓦片 X 坐标
        /// Tile X coordinate.</param>
        /// <param name="tileY">瓦片 Y 坐标
        /// Tile Y coordinate.</param>
        /// <param name="zoom">缩放级别
        /// Zoom level</param>
        /// <returns>A string containing the quadkey.</returns>
        public static string TileXYToQuadKey(int tileX, int tileY, int zoom)
        {
            var quadKey = new StringBuilder();
            for (int i = zoom; i > 0; i--)
            {
                char digit = '0';
                int mask = 1 << (i - 1);
                if ((tileX & mask) != 0)
                {
                    digit++;
                }
                if ((tileY & mask) != 0)
                {
                    digit++;
                    digit++;
                }
                quadKey.Append(digit);
            }
            return quadKey.ToString();
        }

        /// <summary>
        /// 将quadkey转换为瓦片 XY 坐标。
        /// Converts a quadkey into tile XY coordinates.
        /// </summary>
        /// <param name="quadKey">Quadkey of the tile.</param>
        /// <param name="tileX">Output parameter receiving the tile X coordinate.</param>
        /// <param name="tileY">Output parameter receiving the tile Y coordinate.</param>
        /// <param name="zoom">Output parameter receiving the zoom level.</param>
        public static void QuadKeyToTileXY(string quadKey, out int tileX, out int tileY, out int zoom)
        {
            tileX = tileY = 0;
            zoom = quadKey.Length;
            for (int i = zoom; i > 0; i--)
            {
                int mask = 1 << (i - 1);
                switch (quadKey[zoom - i])
                {
                    case '0':
                        break;

                    case '1':
                        tileX |= mask;
                        break;

                    case '2':
                        tileY |= mask;
                        break;

                    case '3':
                        tileX |= mask;
                        tileY |= mask;
                        break;

                    default:
                        throw new ArgumentException("Invalid QuadKey digit sequence.");
                }
            }
        }

        /// <summary>
        /// 计算特定缩放级别下坐标所属的 XY 瓦片坐标。
        /// Calculates the XY tile coordinates that a coordinate falls into for a specific zoom level.
        /// </summary>
        /// <param name="position">Position coordinate in the format [longitude, latitude]</param>
        /// <param name="zoom">Zoom level</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <param name="tileX">Output parameter receiving the tile X position.</param>
        /// <param name="tileY">Output parameter receiving the tile Y position.</param>
        public static void PositionToTileXY(double[] position, int zoom, int tileSize, out int tileX, out int tileY)
        {
            var latitude = Clip(position[1], MinLatitude, MaxLatitude);
            var longitude = Clip(position[0], MinLongitude, MaxLongitude);

            var x = (longitude + 180) / 360;
            var sinLatitude = Math.Sin(latitude * Math.PI / 180);
            var y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            //tileSize 需要在计算中使用，因为在极少数情况下，乘法/四舍五入/除法可能会产生一个像素的差异，从而导致完全不同的瓦片。
            //tileSize needed in calculations as in rare cases the multiplying/rounding/dividing can make the difference of a pixel which can result in a completely different tile. 
            var mapSize = MapSize(zoom, tileSize);
            tileX = (int)Math.Floor(Clip(x * mapSize + 0.5, 0, mapSize - 1) / tileSize);
            tileY = (int)Math.Floor(Clip(y * mapSize + 0.5, 0, mapSize - 1) / tileSize);
        }

        /// <summary>
        ///  计算指定地图窗口内的瓦片 quadkey 字符串。
        /// Calculates the tile quadkey strings that are within a specified viewport.
        /// </summary>
        /// <param name="position">Position coordinate in the format [longitude, latitude]</param>
        /// <param name="zoom">Zoom level</param>
        /// <param name="width">The width of the map viewport in pixels.</param>
        /// <param name="height">The height of the map viewport in pixels.</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <returns>A list of quadkey strings that are within the specified viewport.</returns>
        public static string[] GetQuadkeysInView(double[] position, int zoom, int width, int height, int tileSize)
        {
            var p = PositionToGlobalPixel(position, zoom, tileSize);

            var top = p[1] - height * 0.5;
            var left = p[0] - width * 0.5;

            var bottom = p[1] + height * 0.5;
            var right = p[0] + width * 0.5;

            var tl = GlobalPixelToPosition(new double[] { left, top }, zoom, tileSize);
            var br = GlobalPixelToPosition(new double[] { right, bottom }, zoom, tileSize);

            //方框格式： [西、南、东、北]；
            //Boudning box in the format: [west, south, east, north];
            var bounds = new double[] { tl[0], br[1], br[0], tl[1] };

            return GetQuadkeysInBoundingBox(bounds, zoom, tileSize);
        }

        /// <summary>
        /// 计算在特定缩放级别下位于边界框内的瓦片quadkey字符串。
        /// Calculates the tile quadkey strings that are within a bounding box at a specific zoom level.
        /// </summary>
        /// <param name="bounds">以 [西、南、东、北] 格式定义的数字数组边界框。
        /// A bounding box defined as an array of numbers in the format of [west, south, east, north].</param>
        /// <param name="zoom">Zoom level to calculate tiles for.</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <returns>A list of quadkey strings.</returns>
        public static string[] GetQuadkeysInBoundingBox(double[] bounds, int zoom, int tileSize)
        {
            var keys = new System.Collections.Generic.List<string>();

            if (bounds != null && bounds.Length >= 4)
            {
                PositionToTileXY(new double[] { bounds[3], bounds[0] }, zoom, tileSize, out int tlX, out int tlY);
                PositionToTileXY(new double[] { bounds[1], bounds[2] }, zoom, tileSize, out int brX, out int brY);

                for (int x = tlX; x <= brX; x++)
                {
                    for (int y = tlY; y <= brY; y++)
                    {
                        keys.Add(TileXYToQuadKey(x, y, zoom));
                    }
                }
            }

            return keys.ToArray();
        }

        /// <summary>
        /// 计算瓦片的边界框。
        /// Calculates the bounding box of a tile.
        /// </summary>
        /// <param name="tileX">Tile X coordinate</param>
        /// <param name="tileY">Tile Y coordinate</param>
        /// <param name="zoom">Zoom level</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <returns>以 [西、南、东、北] 格式定义为数字数组的瓦片边框。
        /// A bounding box of the tile defined as an array of numbers in the format of [west, south, east, north].</returns>
        public static double[] TileXYToBoundingBox(int tileX, int tileY, double zoom, int tileSize)
        {
            //左上角像素坐标
            //Top left corner pixel coordinates
            var x1 = (double)(tileX * tileSize);
            var y1 = (double)(tileY * tileSize);

            //右下角像素坐标
            //Bottom right corner pixel coordinates
            var x2 = (double)(x1 + tileSize);
            var y2 = (double)(y1 + tileSize);

            var nw = GlobalPixelToPosition(new double[] { x1, y1 }, zoom, tileSize);
            var se = GlobalPixelToPosition(new double[] { x2, y2 }, zoom, tileSize);

            return new double[] { nw[0], se[1], se[0], nw[1] };
        }

        /// <summary>
        /// 计算地图上边界框的最佳地图视图（中心、缩放）。
        /// Calculates the best map view (center, zoom) for a bounding box on a map.
        /// </summary>
        /// <param name="bounds">A bounding box defined as an array of numbers in the format of [west, south, east, north].</param>
        /// <param name="mapWidth">Map width in pixels.</param>
        /// <param name="mapHeight">Map height in pixels.</param>
        /// <param name="padding">
        /// 用于在地图周围创建缓冲区的像素宽度。这是为了防止标记在边缘被切断
        /// Width in pixels to use to create a buffer around the map. This is to keep markers from being cut off on the edge</param>
        /// <param name="tileSize">The size of the tiles in the tile pyramid.</param>
        /// <param name="latitude">Output parameter receiving the center latitude coordinate.</param>
        /// <param name="longitude">Output parameter receiving the center longitude coordinate.</param>
        /// <param name="zoom">Output parameter receiving the zoom level</param>
        public static void BestMapView(double[] bounds, double mapWidth, double mapHeight, int padding, int tileSize, out double centerLat, out double centerLon, out double zoom)
        {
            if (bounds == null || bounds.Length < 4)
            {
                centerLat = 0;
                centerLon = 0;
                zoom = 1;
                return;
            }

            double boundsDeltaX;

            //Check if east value is greater than west value which would indicate that bounding box crosses the antimeridian.
            if (bounds[2] > bounds[0])
            {
                boundsDeltaX = bounds[2] - bounds[0];
                centerLon = (bounds[2] + bounds[0]) / 2;
            }
            else
            {
                boundsDeltaX = 360 - (bounds[0] - bounds[2]);
                centerLon = ((bounds[2] + bounds[0]) / 2 + 360) % 360 - 180;
            }

            var ry1 = Math.Log((Math.Sin(bounds[1] * Math.PI / 180) + 1) / Math.Cos(bounds[1] * Math.PI / 180));
            var ry2 = Math.Log((Math.Sin(bounds[3] * Math.PI / 180) + 1) / Math.Cos(bounds[3] * Math.PI / 180));
            var ryc = (ry1 + ry2) / 2;

            centerLat = Math.Atan(Math.Sinh(ryc)) * 180 / Math.PI;

            var resolutionHorizontal = boundsDeltaX / (mapWidth - padding * 2);

            var vy0 = Math.Log(Math.Tan(Math.PI * (0.25 + centerLat / 360)));
            var vy1 = Math.Log(Math.Tan(Math.PI * (0.25 + bounds[3] / 360)));
            var zoomFactorPowered = (mapHeight * 0.5 - padding) / (40.7436654315252 * (vy1 - vy0));
            var resolutionVertical = 360.0 / (zoomFactorPowered * tileSize);

            var resolution = Math.Max(resolutionHorizontal, resolutionVertical);

            zoom = Math.Log(360 / (resolution * tileSize), 2);
        }
    }
}
