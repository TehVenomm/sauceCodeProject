package p018jp.colopl.util;

import java.io.FilterInputStream;
import java.io.IOException;
import java.io.InputStream;

/* renamed from: jp.colopl.util.DoneHandlerInputStream */
final class DoneHandlerInputStream extends FilterInputStream {
    private boolean done;

    public DoneHandlerInputStream(InputStream inputStream) {
        super(inputStream);
    }

    public int read(byte[] bArr, int i, int i2) throws IOException {
        if (!this.done) {
            int read = super.read(bArr, i, i2);
            if (read != -1) {
                return read;
            }
        }
        this.done = true;
        return -1;
    }
}
