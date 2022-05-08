namespace WordStore {
    public class AppConstants {
        public static FilePickerFileType TxtFileType = new(new Dictionary<DevicePlatform, IEnumerable<string>> {
            { DevicePlatform.iOS, new[] { "UTType.Text" } },
            { DevicePlatform.Android, new[] { "text/plain" } },
            { DevicePlatform.WinUI, new[] { ".txt"} }
        });
    }
}
