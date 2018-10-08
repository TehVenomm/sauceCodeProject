package net.gogame.gowrap.ui.download;

import java.io.IOException;
import java.io.InputStream;
import net.gogame.gowrap.support.DownloadManager.DownloadResult;
import net.gogame.gowrap.ui.utils.ImageUtils.Source;

public class DownloadResultSource implements Source {
    private final DownloadResult downloadResult;

    public DownloadResultSource(DownloadResult downloadResult) {
        this.downloadResult = downloadResult;
    }

    public InputStream getInputStream() throws IOException {
        return this.downloadResult.getInputStream();
    }

    public void close() throws IOException {
        this.downloadResult.close();
    }
}
