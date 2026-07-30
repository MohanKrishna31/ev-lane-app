using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Media;

public class MediaPickerService : IMediaPickerService
{
    public async Task<MediaFile?> PickPhotoAsync()
    {
        var file = await FilePicker.Default.PickAsync(
            new PickOptions
            {
                PickerTitle = "Select Profile Photo",
                FileTypes = FilePickerFileType.Images
            });

        if (file == null)
            return null;

        return new MediaFile
        {
            Stream = await file.OpenReadAsync(),
            FileName = file.FileName,
            ContentType = file.ContentType ?? "image/jpeg"
        };
    }
}
