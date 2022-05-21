namespace WordStore {
    public class AppConstants {
        public static FilePickerFileType TxtFileType = new(new Dictionary<DevicePlatform, IEnumerable<string>> {
            { DevicePlatform.iOS, new[] { "UTType.Text" } },
            { DevicePlatform.Android, new[] { "text/plain" } },
            { DevicePlatform.WinUI, new[] { ".txt"} }
        });
		public static FilePickerFileType EpubFileType = new(new Dictionary<DevicePlatform, IEnumerable<string>> {
			{ DevicePlatform.iOS, new[] { "UTType.epub" } },
			{ DevicePlatform.Android, new[] { "application/epub+zip" } },
			{ DevicePlatform.WinUI, new[] { ".epub"} }
		});
	}
}
