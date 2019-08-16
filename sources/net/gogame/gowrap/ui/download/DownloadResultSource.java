package net.gogame.gowrap.p019ui.download;

import java.io.IOException;
import java.io.InputStream;
import net.gogame.gowrap.p019ui.utils.ImageUtils.Source;
import net.gogame.gowrap.support.DownloadManager.DownloadResult;

/* renamed from: net.gogame.gowrap.ui.download.DownloadResultSource */
public class DownloadResultSource implements Source {
    private final DownloadResult downloadResult;

    public DownloadResultSource(DownloadResult downloadResult2) {
        this.downloadResult = downloadResult2;
    }

    public InputStream getInputStream() throws IOException {
        return this.downloadResult.getInputStream();
    }

    public void close() throws IOException {
        this.downloadResult.close();
    }
}
