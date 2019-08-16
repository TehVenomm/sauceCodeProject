package net.gogame.gowrap.support;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.UUID;
import p017io.fabric.sdk.android.services.network.HttpRequest;

public class MultipartUtility {
    private static final String LINE_FEED = "\r\n";
    private final String boundary = ("===" + UUID.randomUUID().toString() + "-" + System.currentTimeMillis() + "===");
    private String charset;
    private HttpURLConnection httpConn;
    private OutputStream outputStream;
    private PrintWriter writer;

    public MultipartUtility(String str, String str2) throws IOException {
        this.charset = str2;
        this.httpConn = (HttpURLConnection) new URL(str).openConnection();
        this.httpConn.setUseCaches(false);
        this.httpConn.setDoOutput(true);
        this.httpConn.setDoInput(true);
        this.httpConn.setRequestProperty(HttpRequest.HEADER_CONTENT_TYPE, "multipart/form-data; boundary=" + this.boundary);
        this.outputStream = this.httpConn.getOutputStream();
        this.writer = new PrintWriter(new OutputStreamWriter(this.outputStream, str2), true);
    }

    private static String toString(InputStream inputStream) throws IOException {
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        byte[] bArr = new byte[4096];
        while (true) {
            int read = inputStream.read(bArr);
            if (read <= 0) {
                return byteArrayOutputStream.toString("UTF-8");
            }
            byteArrayOutputStream.write(bArr, 0, read);
        }
    }

    public void addFormField(String str, String str2) {
        this.writer.append("--").append(this.boundary).append(LINE_FEED).append("Content-Disposition: form-data; name=\"").append(str).append("\"").append(LINE_FEED).append("Content-Type: text/plain; charset=").append(this.charset).append(LINE_FEED).append(LINE_FEED).append(str2).append(LINE_FEED).flush();
    }

    public void addFilePart(String str, byte[] bArr) throws IOException {
        this.writer.append("--").append(this.boundary).append(LINE_FEED).append("Content-Disposition: form-data; name=\"").append(str).append("\"; filename=\"").append("attachment.jpeg").append("\"").append(LINE_FEED).append("Content-Type: image/jpeg").append(LINE_FEED).append("Content-Transfer-Encoding: binary").append(LINE_FEED).append(LINE_FEED).flush();
        ByteArrayInputStream byteArrayInputStream = new ByteArrayInputStream(bArr);
        byte[] bArr2 = new byte[4096];
        while (true) {
            int read = byteArrayInputStream.read(bArr2);
            if (read != -1) {
                this.outputStream.write(bArr2, 0, read);
            } else {
                this.outputStream.flush();
                byteArrayInputStream.close();
                this.writer.append(LINE_FEED);
                this.writer.flush();
                return;
            }
        }
    }

    public void addHeaderField(String str, String str2) {
        this.writer.append(str).append(": ").append(str2).append(LINE_FEED).flush();
    }

    public String finish() throws IOException {
        this.writer.append(LINE_FEED).flush();
        this.writer.append("--").append(this.boundary).append("--").append(LINE_FEED).close();
        int responseCode = this.httpConn.getResponseCode();
        if (responseCode == 200) {
            String multipartUtility = toString(this.httpConn.getInputStream());
            this.httpConn.disconnect();
            if (multipartUtility.length() == 0 || multipartUtility.equals("")) {
                return null;
            }
            return multipartUtility;
        }
        throw new IOException("Server returned non-OK status: " + responseCode);
    }
}
