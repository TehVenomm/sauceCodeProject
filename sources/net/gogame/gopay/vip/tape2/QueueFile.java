package net.gogame.gopay.vip.tape2;

import android.support.v4.media.session.PlaybackStateCompat;
import com.google.android.gms.nearby.messages.Strategy;
import java.io.Closeable;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.channels.FileChannel;
import java.util.ConcurrentModificationException;
import java.util.Iterator;
import java.util.NoSuchElementException;

public final class QueueFile implements Closeable, Iterable<byte[]> {
    /* renamed from: i */
    private static final byte[] f1311i = new byte[4096];
    /* renamed from: a */
    final RandomAccessFile f1312a;
    /* renamed from: b */
    boolean f1313b;
    /* renamed from: c */
    int f1314c;
    /* renamed from: d */
    long f1315d;
    /* renamed from: e */
    int f1316e;
    /* renamed from: f */
    C1099a f1317f;
    /* renamed from: g */
    int f1318g;
    /* renamed from: h */
    boolean f1319h;
    /* renamed from: j */
    private C1099a f1320j;
    /* renamed from: k */
    private final byte[] f1321k;
    /* renamed from: l */
    private final boolean f1322l;

    /* renamed from: net.gogame.gopay.vip.tape2.QueueFile$a */
    static class C1099a {
        /* renamed from: a */
        static final C1099a f1304a = new C1099a(0, 0);
        /* renamed from: b */
        final long f1305b;
        /* renamed from: c */
        final int f1306c;

        C1099a(long j, int i) {
            this.f1305b = j;
            this.f1306c = i;
        }

        public String toString() {
            return getClass().getSimpleName() + "[position=" + this.f1305b + ", length=" + this.f1306c + "]";
        }
    }

    /* renamed from: net.gogame.gopay.vip.tape2.QueueFile$b */
    private final class C1100b implements Iterator<byte[]> {
        /* renamed from: a */
        int f1307a = 0;
        /* renamed from: b */
        int f1308b = this.f1309c.f1318g;
        /* renamed from: c */
        final /* synthetic */ QueueFile f1309c;
        /* renamed from: d */
        private long f1310d = this.f1309c.f1317f.f1305b;

        public /* synthetic */ Object next() {
            return m976a();
        }

        C1100b(QueueFile queueFile) {
            this.f1309c = queueFile;
        }

        /* renamed from: b */
        private void m975b() {
            if (this.f1309c.f1318g != this.f1308b) {
                throw new ConcurrentModificationException();
            }
        }

        public boolean hasNext() {
            if (this.f1309c.f1319h) {
                throw new IllegalStateException("closed");
            }
            m975b();
            return this.f1307a != this.f1309c.f1316e;
        }

        /* renamed from: a */
        public byte[] m976a() {
            if (this.f1309c.f1319h) {
                throw new IllegalStateException("closed");
            }
            m975b();
            if (this.f1309c.isEmpty()) {
                throw new NoSuchElementException();
            } else if (this.f1307a >= this.f1309c.f1316e) {
                throw new NoSuchElementException();
            } else {
                try {
                    C1099a a = this.f1309c.m990a(this.f1310d);
                    byte[] bArr = new byte[a.f1306c];
                    this.f1310d = this.f1309c.m992b(a.f1305b + 4);
                    this.f1309c.m991a(this.f1310d, bArr, 0, a.f1306c);
                    this.f1310d = this.f1309c.m992b((a.f1305b + 4) + ((long) a.f1306c));
                    this.f1307a++;
                    return bArr;
                } catch (Throwable e) {
                    throw new RuntimeException("todo: throw a proper error", e);
                }
            }
        }

        public void remove() {
            m975b();
            if (this.f1309c.isEmpty()) {
                throw new NoSuchElementException();
            } else if (this.f1307a != 1) {
                throw new UnsupportedOperationException("Removal is only permitted from the head.");
            } else {
                try {
                    this.f1309c.remove();
                    this.f1308b = this.f1309c.f1318g;
                    this.f1307a--;
                } catch (Throwable e) {
                    throw new RuntimeException("todo: throw a proper error", e);
                }
            }
        }
    }

    public QueueFile(File file) throws IOException {
        this(file, true);
    }

    public QueueFile(File file, boolean z) throws IOException {
        this(file, z, false);
    }

    public QueueFile(File file, boolean z, boolean z2) throws IOException {
        this(m980a(file, z2), z, z2);
    }

    /* renamed from: a */
    private static RandomAccessFile m980a(File file, boolean z) throws IOException {
        if (!file.exists()) {
            File file2 = new File(file.getPath() + ".tmp");
            RandomAccessFile a = m979a(file2);
            try {
                a.setLength(PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM);
                a.seek(0);
                if (z) {
                    a.writeInt(4096);
                } else {
                    a.writeInt(-2147483647);
                    a.writeLong(PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM);
                }
                a.close();
                if (!file2.renameTo(file)) {
                    throw new IOException("Rename failed!");
                }
            } catch (Throwable th) {
                a.close();
            }
        }
        return m979a(file);
    }

    /* renamed from: a */
    private static RandomAccessFile m979a(File file) throws FileNotFoundException {
        return new RandomAccessFile(file, "rwd");
    }

    QueueFile(RandomAccessFile randomAccessFile, boolean z, boolean z2) throws IOException {
        long b;
        long b2;
        this.f1321k = new byte[32];
        this.f1318g = 0;
        this.f1312a = randomAccessFile;
        this.f1322l = z;
        randomAccessFile.seek(0);
        randomAccessFile.readFully(this.f1321k);
        boolean z3 = (z2 || (this.f1321k[0] & 128) == 0) ? false : true;
        this.f1313b = z3;
        if (this.f1313b) {
            this.f1314c = 32;
            int a = m977a(this.f1321k, 0) & Strategy.TTL_SECONDS_INFINITE;
            if (a != 1) {
                throw new IOException("Unable to read version " + a + " format. Supported versions are 1 and legacy.");
            }
            this.f1315d = m986b(this.f1321k, 4);
            this.f1316e = m977a(this.f1321k, 12);
            b = m986b(this.f1321k, 16);
            b2 = m986b(this.f1321k, 24);
        } else {
            this.f1314c = 16;
            this.f1315d = (long) m977a(this.f1321k, 0);
            this.f1316e = m977a(this.f1321k, 4);
            b = (long) m977a(this.f1321k, 8);
            b2 = (long) m977a(this.f1321k, 12);
        }
        if (this.f1315d > randomAccessFile.length()) {
            throw new IOException("File is truncated. Expected length: " + this.f1315d + ", Actual length: " + randomAccessFile.length());
        } else if (this.f1315d <= ((long) this.f1314c)) {
            throw new IOException("File is corrupt; length stored in header (" + this.f1315d + ") is invalid.");
        } else {
            this.f1317f = m990a(b);
            this.f1320j = m990a(b2);
        }
    }

    /* renamed from: a */
    private static void m983a(byte[] bArr, int i, int i2) {
        bArr[i] = (byte) (i2 >> 24);
        bArr[i + 1] = (byte) (i2 >> 16);
        bArr[i + 2] = (byte) (i2 >> 8);
        bArr[i + 3] = (byte) i2;
    }

    /* renamed from: a */
    private static int m977a(byte[] bArr, int i) {
        return ((((bArr[i] & 255) << 24) + ((bArr[i + 1] & 255) << 16)) + ((bArr[i + 2] & 255) << 8)) + (bArr[i + 3] & 255);
    }

    /* renamed from: a */
    private static void m984a(byte[] bArr, int i, long j) {
        bArr[i] = (byte) ((int) (j >> 56));
        bArr[i + 1] = (byte) ((int) (j >> 48));
        bArr[i + 2] = (byte) ((int) (j >> 40));
        bArr[i + 3] = (byte) ((int) (j >> 32));
        bArr[i + 4] = (byte) ((int) (j >> 24));
        bArr[i + 5] = (byte) ((int) (j >> 16));
        bArr[i + 6] = (byte) ((int) (j >> 8));
        bArr[i + 7] = (byte) ((int) j);
    }

    /* renamed from: b */
    private static long m986b(byte[] bArr, int i) {
        return ((((((((((long) bArr[i]) & 255) << 56) + ((((long) bArr[i + 1]) & 255) << 48)) + ((((long) bArr[i + 2]) & 255) << 40)) + ((((long) bArr[i + 3]) & 255) << 32)) + ((((long) bArr[i + 4]) & 255) << 24)) + ((((long) bArr[i + 5]) & 255) << 16)) + ((((long) bArr[i + 6]) & 255) << 8)) + (((long) bArr[i + 7]) & 255);
    }

    /* renamed from: a */
    private void m981a(long j, int i, long j2, long j3) throws IOException {
        this.f1312a.seek(0);
        if (this.f1313b) {
            m983a(this.f1321k, 0, -2147483647);
            m984a(this.f1321k, 4, j);
            m983a(this.f1321k, 12, i);
            m984a(this.f1321k, 16, j2);
            m984a(this.f1321k, 24, j3);
            this.f1312a.write(this.f1321k, 0, 32);
            return;
        }
        m983a(this.f1321k, 0, (int) j);
        m983a(this.f1321k, 4, i);
        m983a(this.f1321k, 8, (int) j2);
        m983a(this.f1321k, 12, (int) j3);
        this.f1312a.write(this.f1321k, 0, 16);
    }

    /* renamed from: a */
    C1099a m990a(long j) throws IOException {
        if (j == 0) {
            return C1099a.f1304a;
        }
        m991a(j, this.f1321k, 0, 4);
        return new C1099a(j, m977a(this.f1321k, 0));
    }

    /* renamed from: b */
    long m992b(long j) {
        return j < this.f1315d ? j : (((long) this.f1314c) + j) - this.f1315d;
    }

    /* renamed from: b */
    private void m987b(long j, byte[] bArr, int i, int i2) throws IOException {
        long b = m992b(j);
        if (((long) i2) + b <= this.f1315d) {
            this.f1312a.seek(b);
            this.f1312a.write(bArr, i, i2);
            return;
        }
        int i3 = (int) (this.f1315d - b);
        this.f1312a.seek(b);
        this.f1312a.write(bArr, i, i3);
        this.f1312a.seek((long) this.f1314c);
        this.f1312a.write(bArr, i + i3, i2 - i3);
    }

    /* renamed from: a */
    private void m982a(long j, long j2) throws IOException {
        long j3 = j;
        while (j2 > 0) {
            int min = (int) Math.min(j2, (long) f1311i.length);
            m987b(j3, f1311i, 0, min);
            j2 -= (long) min;
            j3 += (long) min;
        }
    }

    /* renamed from: a */
    void m991a(long j, byte[] bArr, int i, int i2) throws IOException {
        long b = m992b(j);
        if (((long) i2) + b <= this.f1315d) {
            this.f1312a.seek(b);
            this.f1312a.readFully(bArr, i, i2);
            return;
        }
        int i3 = (int) (this.f1315d - b);
        this.f1312a.seek(b);
        this.f1312a.readFully(bArr, i, i3);
        this.f1312a.seek((long) this.f1314c);
        this.f1312a.readFully(bArr, i + i3, i2 - i3);
    }

    public void add(byte[] bArr) throws IOException {
        add(bArr, 0, bArr.length);
    }

    public void add(byte[] bArr, int i, int i2) throws IOException {
        if (bArr == null) {
            throw new NullPointerException("data == null");
        } else if ((i | i2) < 0 || i2 > bArr.length - i) {
            throw new IndexOutOfBoundsException();
        } else if (this.f1319h) {
            throw new IOException("closed");
        } else {
            long j;
            m988c((long) i2);
            boolean isEmpty = isEmpty();
            if (isEmpty) {
                j = (long) this.f1314c;
            } else {
                j = m992b((this.f1320j.f1305b + 4) + ((long) this.f1320j.f1306c));
            }
            C1099a c1099a = new C1099a(j, i2);
            m983a(this.f1321k, 0, i2);
            m987b(c1099a.f1305b, this.f1321k, 0, 4);
            m987b(c1099a.f1305b + 4, bArr, i, i2);
            m981a(this.f1315d, this.f1316e + 1, isEmpty ? c1099a.f1305b : this.f1317f.f1305b, c1099a.f1305b);
            this.f1320j = c1099a;
            this.f1316e++;
            this.f1318g++;
            if (isEmpty) {
                this.f1317f = this.f1320j;
            }
        }
    }

    /* renamed from: a */
    private long m978a() {
        if (this.f1316e == 0) {
            return (long) this.f1314c;
        }
        if (this.f1320j.f1305b >= this.f1317f.f1305b) {
            return (((this.f1320j.f1305b - this.f1317f.f1305b) + 4) + ((long) this.f1320j.f1306c)) + ((long) this.f1314c);
        }
        return (((this.f1320j.f1305b + 4) + ((long) this.f1320j.f1306c)) + this.f1315d) - this.f1317f.f1305b;
    }

    /* renamed from: b */
    private long m985b() {
        return this.f1315d - m978a();
    }

    public boolean isEmpty() {
        return this.f1316e == 0;
    }

    /* renamed from: c */
    private void m988c(long j) throws IOException {
        long j2 = 4 + j;
        long b = m985b();
        if (b < j2) {
            long j3;
            long j4 = this.f1315d;
            while (true) {
                b += j4;
                j3 = j4 << 1;
                if (b >= j2) {
                    break;
                }
                j4 = j3;
            }
            m989d(j3);
            b = m992b((this.f1320j.f1305b + 4) + ((long) this.f1320j.f1306c));
            if (b <= this.f1317f.f1305b) {
                FileChannel channel = this.f1312a.getChannel();
                channel.position(this.f1315d);
                j2 = b - ((long) this.f1314c);
                if (channel.transferTo((long) this.f1314c, j2, channel) != j2) {
                    throw new AssertionError("Copied insufficient number of bytes!");
                } else if (this.f1322l) {
                    m982a((long) this.f1314c, j2);
                }
            }
            if (this.f1320j.f1305b < this.f1317f.f1305b) {
                long j5 = (this.f1315d + this.f1320j.f1305b) - ((long) this.f1314c);
                m981a(j3, this.f1316e, this.f1317f.f1305b, j5);
                this.f1320j = new C1099a(j5, this.f1320j.f1306c);
            } else {
                m981a(j3, this.f1316e, this.f1317f.f1305b, this.f1320j.f1305b);
            }
            this.f1315d = j3;
        }
    }

    /* renamed from: d */
    private void m989d(long j) throws IOException {
        this.f1312a.setLength(j);
        this.f1312a.getChannel().force(true);
    }

    public byte[] peek() throws IOException {
        if (this.f1319h) {
            throw new IOException("closed");
        } else if (isEmpty()) {
            return null;
        } else {
            int i = this.f1317f.f1306c;
            byte[] bArr = new byte[i];
            m991a(4 + this.f1317f.f1305b, bArr, 0, i);
            return bArr;
        }
    }

    public Iterator<byte[]> iterator() {
        return new C1100b(this);
    }

    public int size() {
        return this.f1316e;
    }

    public void remove() throws IOException {
        remove(1);
    }

    public void remove(int i) throws IOException {
        if (i < 0) {
            throw new IllegalArgumentException("Cannot remove negative (" + i + ") number of elements.");
        } else if (i != 0) {
            if (i == this.f1316e) {
                clear();
            } else if (isEmpty()) {
                throw new NoSuchElementException();
            } else if (i > this.f1316e) {
                throw new IllegalArgumentException("Cannot remove more elements (" + i + ") than present in queue (" + this.f1316e + ").");
            } else {
                long j = this.f1317f.f1305b;
                long j2 = this.f1317f.f1305b;
                int i2 = 0;
                int i3 = this.f1317f.f1306c;
                long j3 = 0;
                while (i2 < i) {
                    j3 += (long) (i3 + 4);
                    long b = m992b((4 + j2) + ((long) i3));
                    m991a(b, this.f1321k, 0, 4);
                    i2++;
                    i3 = m977a(this.f1321k, 0);
                    j2 = b;
                }
                m981a(this.f1315d, this.f1316e - i, j2, this.f1320j.f1305b);
                this.f1316e -= i;
                this.f1318g++;
                this.f1317f = new C1099a(j2, i3);
                if (this.f1322l) {
                    m982a(j, j3);
                }
            }
        }
    }

    public void clear() throws IOException {
        if (this.f1319h) {
            throw new IOException("closed");
        }
        m981a((long) PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM, 0, 0, 0);
        if (this.f1322l) {
            this.f1312a.seek((long) this.f1314c);
            this.f1312a.write(f1311i, 0, 4096 - this.f1314c);
        }
        this.f1316e = 0;
        this.f1317f = C1099a.f1304a;
        this.f1320j = C1099a.f1304a;
        if (this.f1315d > PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM) {
            m989d(PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM);
        }
        this.f1315d = PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM;
        this.f1318g++;
    }

    public void close() throws IOException {
        this.f1319h = true;
        this.f1312a.close();
    }

    public String toString() {
        return getClass().getSimpleName() + "[length=" + this.f1315d + ", size=" + this.f1316e + ", first=" + this.f1317f + ", last=" + this.f1320j + "]";
    }
}
