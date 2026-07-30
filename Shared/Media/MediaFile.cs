using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Media;

public class MediaFile
{
    public Stream Stream { get; init; } = default!;

    public string FileName { get; init; } = string.Empty;

    public string ContentType { get; init; } = string.Empty;
}
