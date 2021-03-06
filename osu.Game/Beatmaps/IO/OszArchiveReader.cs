﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Ionic.Zip;
using osu.Game.Beatmaps.Formats;
using osu.Game.Database;

namespace osu.Game.Beatmaps.IO
{
    public sealed class OszArchiveReader : ArchiveReader
    {
        public static void Register()
        {
            AddReader<OszArchiveReader>((storage, path) =>
            {
                using (var stream = storage.GetStream(path))
                    return ZipFile.IsZipFile(stream, false);
            });
            OsuLegacyDecoder.Register();
        }

        private Stream archiveStream;
        private ZipFile archive;
        private Beatmap firstMap;

        public OszArchiveReader(Stream archiveStream)
        {
            this.archiveStream = archiveStream;
            archive = ZipFile.Read(archiveStream);
            BeatmapFilenames = archive.Entries.Where(e => e.FileName.EndsWith(@".osu"))
                .Select(e => e.FileName).ToArray();
            if (BeatmapFilenames.Length == 0)
                throw new FileNotFoundException(@"This directory contains no beatmaps");
            StoryboardFilename = archive.Entries.Where(e => e.FileName.EndsWith(@".osb"))
                .Select(e => e.FileName).FirstOrDefault();
            using (var stream = new StreamReader(GetStream(BeatmapFilenames[0])))
            {
                var decoder = BeatmapDecoder.GetDecoder(stream);
                firstMap = decoder.Decode(stream);
            }
        }

        public override Stream GetStream(string name)
        {
            ZipEntry entry = archive.Entries.SingleOrDefault(e => e.FileName == name);
            if (entry == null)
                throw new FileNotFoundException();
            return entry.OpenReader();
        }

        public override BeatmapMetadata ReadMetadata()
        {
            return firstMap.BeatmapInfo.Metadata;
        }

        public override void Dispose()
        {
            archive.Dispose();
            archiveStream.Dispose();
        }
    }
}