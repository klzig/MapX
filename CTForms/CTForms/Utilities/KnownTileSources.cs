// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.
// Modified by Kurt Ziegler

using System;
using BruTile;
using BruTile.Cache;
using BruTile.Predefined;
using BruTile.Web;

namespace CTForms.Utilities
{
    /// <summary>
    /// Known tile sources
    /// </summary>
    public enum KnownTileSource
    {
        OpenCycleMap,
        ThunderforestTransport,
        ThunderforestOutdoors,
        ThunderforestLandscape,
        BingAerial,
        BingHybrid,
        BingRoads
    }

    public static class KnownTileSources
    {
        private static readonly Attribution OpenStreetMapAttribution = new Attribution(
            "© OpenStreetMap contributors", "https://www.openstreetmap.org/copyright");

        /// <summary>
        /// Static factory method for known tile services
        /// </summary>
        /// <param name="source">The source</param>
        /// <param name="apiKey">An (optional) API key</param>
        /// <param name="persistentCache">A place to permanently store tiles (file of database)</param>
        /// <param name="tileFetcher">Option to override the web request</param>
        /// <returns>The tile source</returns>
        public static HttpTileSource Create(KnownTileSource source = KnownTileSource.OpenCycleMap, string apiKey = null,
            IPersistentCache<byte[]> persistentCache = null, Func<Uri, byte[]> tileFetcher = null)
        {
            switch (source)
            {
                case KnownTileSource.OpenCycleMap:
                    return new HttpTileSource(new GlobalSphericalMercator(0, 17),
                        "https://{s}.tile.thunderforest.com/cycle/{z}/{x}/{y}.png?apikey={k}",
                        new[] { "a", "b", "c" }, apiKey: apiKey, name: source.ToString(),
                        persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: OpenStreetMapAttribution);
                case KnownTileSource.ThunderforestTransport:
                    return new HttpTileSource(new GlobalSphericalMercator(0, 20),
                        "https://{s}.tile.thunderforest.com/transport/{z}/{x}/{y}.png?apikey={k}",
                        new[] { "a", "b", "c" }, apiKey: apiKey, name: source.ToString(),
                        persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: OpenStreetMapAttribution);
                case KnownTileSource.ThunderforestOutdoors:
                    return new HttpTileSource(new GlobalSphericalMercator(0, 20),
                        "https://{s}.tile.thunderforest.com/transport/{z}/{x}/{y}.png?apikey={k}",
                        new[] { "a", "b", "c" }, apiKey: apiKey, name: source.ToString(),
                        persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: OpenStreetMapAttribution);
                case KnownTileSource.ThunderforestLandscape:
                    return new HttpTileSource(new GlobalSphericalMercator(0, 20),
                        "https://{s}.tile.thunderforest.com/transport/{z}/{x}/{y}.png?apikey={k}",
                        new[] { "a", "b", "c" }, apiKey: apiKey, name: source.ToString(),
                        persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: OpenStreetMapAttribution);
                case KnownTileSource.BingAerial:
                    return new HttpTileSource(new GlobalSphericalMercator(1),
                        "http://t{s}.tiles.virtualearth.net/tiles/a{quadkey}.jpeg?g=517&token={k}",
                        new[] { "0", "1", "2", "3", "4", "5", "6", "7" }, apiKey, source.ToString(),
                        persistentCache, tileFetcher, new Attribution("© Microsoft"));
                case KnownTileSource.BingHybrid:
                    return new HttpTileSource(new GlobalSphericalMercator(1),
                        "http://t{s}.tiles.virtualearth.net/tiles/h{quadkey}.jpeg?g=517&token={k}",
                        new[] { "0", "1", "2", "3", "4", "5", "6", "7" }, apiKey, source.ToString(),
                        persistentCache, tileFetcher, new Attribution("© Microsoft"));
                case KnownTileSource.BingRoads:
                    return new HttpTileSource(new GlobalSphericalMercator(1),
                        "http://t{s}.tiles.virtualearth.net/tiles/r{quadkey}.jpeg?g=517&token={k}",
                        new[] { "0", "1", "2", "3", "4", "5", "6", "7" }, apiKey, source.ToString(),
                        persistentCache, tileFetcher, new Attribution("© Microsoft"));
                default:
                    throw new NotSupportedException("KnownTileSource not known");
            }
        }
    }
}
