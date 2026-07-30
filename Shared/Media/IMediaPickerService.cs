using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Media;

public interface IMediaPickerService
{
    Task<MediaFile?> PickPhotoAsync();
}
