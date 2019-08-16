package net.gogame.gopay.vip.tape2;

import android.support.p000v4.media.session.PlaybackStateCompat;
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
    private static final byte[] f1373i = new byte[4096];

    /* renamed from: a */
    final RandomAccessFile f1374a;

    /* renamed from: b */
    boolean f1375b;

    /* renamed from: c */
    int f1376c;

    /* renamed from: d */
    long f1377d;

    /* renamed from: e */
    int f1378e;

    /* renamed from: f */
    C1667a f1379f;

    /* renamed from: g */
    int f1380g;

    /* renamed from: h */
    boolean f1381h;

    /* renamed from: j */
    private C1667a f1382j;

    /* renamed from: k */
    private final byte[] f1383k;

    /* renamed from: l */
    private final boolean f1384l;

    /* renamed from: net.gogame.gopay.vip.tape2.QueueFile$a */
    static class C1667a {

        /* renamed from: a */
        static final C1667a f1385a = new C1667a(0, 0);

        /* renamed from: b */
        final long f1386b;

        /* renamed from: c */
        final int f1387c;

        C1667a(long j, int i) {
            this.f1386b = j;
            this.f1387c = i;
        }

        public String toString() {
            return getClass().getSimpleName() + "[position=" + this.f1386b + ", length=" + this.f1387c + "]";
        }
    }

    /* renamed from: net.gogame.gopay.vip.tape2.QueueFile$b */
    private final class C1668b implements Iterator<byte[]> {

        /* renamed from: a */
        int f1388a = 0;

        /* renamed from: b */
        int f1389b = QueueFile.this.f1380g;

        /* renamed from: d */
        private long f1391d = QueueFile.this.f1379f.f1386b;

        C1668b() {
        }

        /* renamed from: b */
        private void m1005b() {
            if (QueueFile.this.f1380g != this.f1389b) {
                throw new ConcurrentModificationException();
            }
        }

        public boolean hasNext() {
            if (QueueFile.this.f1381h) {
                throw new IllegalStateException("closed");
            }
            m1005b();
            return this.f1388a != QueueFile.this.f1378e;
        }

        /* renamed from: a */
        public byte[] next() {
            if (QueueFile.this.f1381h) {
                throw new IllegalStateException("closed");
            }
            m1005b();
            if (QueueFile.this.isEmpty()) {
                throw new NoSuchElementException();
            } else if (this.f1388a >= QueueFile.this.f1378e) {
                throw new NoSuchElementException();
            } else {
                try {
                    C1667a a = QueueFile.this.mo22788a(this.f1391d);
                    byte[] bArr = new byte[a.f1387c];
                    this.f1391d = QueueFile.this.mo22792b(a.f1386b + 4);
                    QueueFile.this.mo22789a(this.f1391d, bArr, 0, a.f1387c);
                    this.f1391d = QueueFile.this.mo22792b(a.f1386b + 4 + ((long) a.f1387c));
                    this.f1388a++;
                    return bArr;
                } catch (IOException e) {
                    throw new RuntimeException("todo: throw a proper error", e);
                }
            }
        }

        public void remove() {
            m1005b();
            if (QueueFile.this.isEmpty()) {
                throw new NoSuchElementException();
            } else if (this.f1388a != 1) {
                throw new UnsupportedOperationException("Removal is only permitted from the head.");
            } else {
                try {
                    QueueFile.this.remove();
                    this.f1389b = QueueFile.this.f1380g;
                    this.f1388a--;
                } catch (IOException e) {
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
        this(m992a(file, z2), z, z2);
    }

    /* JADX INFO: finally extract failed */
    /* renamed from: a */
    private static RandomAccessFile m992a(File file, boolean z) throws IOException {
        if (!file.exists()) {
            File file2 = new File(file.getPath() + ".tmp");
            RandomAccessFile a = m991a(file2);
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
                throw th;
            }
        }
        return m991a(file);
    }

    /* renamed from: a */
    private static RandomAccessFile m991a(File file) throws FileNotFoundException {
        return new RandomAccessFile(file, "rwd");
    }

    QueueFile(RandomAccessFile randomAccessFile, boolean z, boolean z2) throws IOException {
        long a;
        long a2;
        this.f1383k = new byte[32];
        this.f1380g = 0;
        this.f1374a = randomAccessFile;
        this.f1384l = z;
        randomAccessFile.seek(0);
        randomAccessFile.readFully(this.f1383k);
        this.f1375b = !z2 && (this.f1383k[0] & 128) != 0;
        if (this.f1375b) {
            this.f1376c = 32;
            int a3 = m989a(this.f1383k, 0) & Integer.MAX_VALUE;
            if (a3 != 1) {
                throw new IOException("Unable to read version " + a3 + " format. Supported versions are 1 and legacy.");
            }
            this.f1377d = m998b(this.f1383k, 4);
            this.f1378e = m989a(this.f1383k, 12);
            a = m998b(this.f1383k, 16);
            a2 = m998b(this.f1383k, 24);
        } else {
            this.f1376c = 16;
            this.f1377d = (long) m989a(this.f1383k, 0);
            this.f1378e = m989a(this.f1383k, 4);
            a = (long) m989a(this.f1383k, 8);
            a2 = (long) m989a(this.f1383k, 12);
        }
        if (this.f1377d > randomAccessFile.length()) {
            throw new IOException("File is truncated. Expected length: " + this.f1377d + ", Actual length: " + randomAccessFile.length());
        } else if (this.f1377d <= ((long) this.f1376c)) {
            throw new IOException("File is corrupt; length stored in header (" + this.f1377d + ") is invalid.");
        } else {
            this.f1379f = mo22788a(a);
            this.f1382j = mo22788a(a2);
        }
    }

    /* renamed from: a */
    private static void m995a(byte[] bArr, int i, int i2) {
        bArr[i] = (byte) (i2 >> 24);
        bArr[i + 1] = (byte) (i2 >> 16);
        bArr[i + 2] = (byte) (i2 >> 8);
        bArr[i + 3] = (byte) i2;
    }

    /* renamed from: a */
    private static int m989a(byte[] bArr, int i) {
        return ((bArr[i] & 255) << 24) + ((bArr[i + 1] & 255) << 16) + ((bArr[i + 2] & 255) << 8) + (bArr[i + 3] & 255);
    }

    /* renamed from: a */
    private static void m996a(byte[] bArr, int i, long j) {
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
    private static long m998b(byte[] bArr, int i) {
        return ((((long) bArr[i]) & 255) << 56) + ((((long) bArr[i + 1]) & 255) << 48) + ((((long) bArr[i + 2]) & 255) << 40) + ((((long) bArr[i + 3]) & 255) << 32) + ((((long) bArr[i + 4]) & 255) << 24) + ((((long) bArr[i + 5]) & 255) << 16) + ((((long) bArr[i + 6]) & 255) << 8) + (((long) bArr[i + 7]) & 255);
    }

    /* renamed from: a */
    private void m993a(long j, int i, long j2, long j3) throws IOException {
        this.f1374a.seek(0);
        if (this.f1375b) {
            m995a(this.f1383k, 0, -2147483647);
            m996a(this.f1383k, 4, j);
            m995a(this.f1383k, 12, i);
            m996a(this.f1383k, 16, j2);
            m996a(this.f1383k, 24, j3);
            this.f1374a.write(this.f1383k, 0, 32);
            return;
        }
        m995a(this.f1383k, 0, (int) j);
        m995a(this.f1383k, 4, i);
        m995a(this.f1383k, 8, (int) j2);
        m995a(this.f1383k, 12, (int) j3);
        this.f1374a.write(this.f1383k, 0, 16);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: a */
    public C1667a mo22788a(long j) throws IOException {
        if (j == 0) {
            return C1667a.f1385a;
        }
        mo22789a(j, this.f1383k, 0, 4);
        return new C1667a(j, m989a(this.f1383k, 0));
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: b */
    public long mo22792b(long j) {
        return j < this.f1377d ? j : (((long) this.f1376c) + j) - this.f1377d;
    }

    /* renamed from: b */
    private void m999b(long j, byte[] bArr, int i, int i2) throws IOException {
        long b = mo22792b(j);
        if (((long) i2) + b <= this.f1377d) {
            this.f1374a.seek(b);
            this.f1374a.write(bArr, i, i2);
            return;
        }
        int i3 = (int) (this.f1377d - b);
        this.f1374a.seek(b);
        this.f1374a.write(bArr, i, i3);
        this.f1374a.seek((long) this.f1376c);
        this.f1374a.write(bArr, i + i3, i2 - i3);
    }

    /* renamed from: a */
    private void m994a(long j, long j2) throws IOException {
        long j3 = j;
        while (j2 > 0) {
            int min = (int) Math.min(j2, (long) f1373i.length);
            m999b(j3, f1373i, 0, min);
            j2 -= (long) min;
            j3 += (long) min;
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: a */
    public void mo22789a(long j, byte[] bArr, int i, int i2) throws IOException {
        long b = mo22792b(j);
        if (((long) i2) + b <= this.f1377d) {
            this.f1374a.seek(b);
            this.f1374a.readFully(bArr, i, i2);
            return;
        }
        int i3 = (int) (this.f1377d - b);
        this.f1374a.seek(b);
        this.f1374a.readFully(bArr, i, i3);
        this.f1374a.seek((long) this.f1376c);
        this.f1374a.readFully(bArr, i + i3, i2 - i3);
    }

    public void add(byte[] bArr) throws IOException {
        add(bArr, 0, bArr.length);
    }

    public void add(byte[] bArr, int i, int i2) throws IOException {
        long b;
        if (bArr == null) {
            throw new NullPointerException("data == null");
        } else if ((i | i2) < 0 || i2 > bArr.length - i) {
            throw new IndexOutOfBoundsException();
        } else if (this.f1381h) {
            throw new IOException("closed");
        } else {
            m1000c((long) i2);
            boolean isEmpty = isEmpty();
            if (isEmpty) {
                b = (long) this.f1376c;
            } else {
                b = mo22792b(this.f1382j.f1386b + 4 + ((long) this.f1382j.f1387c));
            }
            C1667a aVar = new C1667a(b, i2);
            m995a(this.f1383k, 0, i2);
            m999b(aVar.f1386b, this.f1383k, 0, 4);
            m999b(aVar.f1386b + 4, bArr, i, i2);
            m993a(this.f1377d, this.f1378e + 1, isEmpty ? aVar.f1386b : this.f1379f.f1386b, aVar.f1386b);
            this.f1382j = aVar;
            this.f1378e++;
            this.f1380g++;
            if (isEmpty) {
                this.f1379f = this.f1382j;
            }
        }
    }

    /* renamed from: a */
    private long m990a() {
        if (this.f1378e == 0) {
            return (long) this.f1376c;
        }
        if (this.f1382j.f1386b >= this.f1379f.f1386b) {
            return (this.f1382j.f1386b - this.f1379f.f1386b) + 4 + ((long) this.f1382j.f1387c) + ((long) this.f1376c);
        }
        return (((this.f1382j.f1386b + 4) + ((long) this.f1382j.f1387c)) + this.f1377d) - this.f1379f.f1386b;
    }

    /* renamed from: b */
    private long m997b() {
        return this.f1377d - m990a();
    }

    public boolean isEmpty() {
        return this.f1378e == 0;
    }

    /* renamed from: c */
    private void m1000c(long j) throws IOException {
        long j2;
        long j3 = 4 + j;
        long b = m997b();
        if (b < j3) {
            long j4 = this.f1377d;
            while (true) {
                b += j4;
                j2 = j4 << 1;
                if (b >= j3) {
                    break;
                }
                j4 = j2;
            }
            m1001d(j2);
            long b2 = mo22792b(this.f1382j.f1386b + 4 + ((long) this.f1382j.f1387c));
            if (b2 <= this.f1379f.f1386b) {
                FileChannel channel = this.f1374a.getChannel();
                channel.position(this.f1377d);
                long j5 = b2 - ((long) this.f1376c);
                if (channel.transferTo((long) this.f1376c, j5, channel) != j5) {
                    throw new AssertionError("Copied insufficient number of bytes!");
                } else if (this.f1384l) {
                    m994a((long) this.f1376c, j5);
                }
            }
            if (this.f1382j.f1386b < this.f1379f.f1386b) {
                long j6 = (this.f1377d + this.f1382j.f1386b) - ((long) this.f1376c);
                m993a(j2, this.f1378e, this.f1379f.f1386b, j6);
                this.f1382j = new C1667a(j6, this.f1382j.f1387c);
            } else {
                m993a(j2, this.f1378e, this.f1379f.f1386b, this.f1382j.f1386b);
            }
            this.f1377d = j2;
        }
    }

    /* renamed from: d */
    private void m1001d(long j) throws IOException {
        this.f1374a.setLength(j);
        this.f1374a.getChannel().force(true);
    }

    public byte[] peek() throws IOException {
        if (this.f1381h) {
            throw new IOException("closed");
        } else if (isEmpty()) {
            return null;
        } else {
            int i = this.f1379f.f1387c;
            byte[] bArr = new byte[i];
            mo22789a(4 + this.f1379f.f1386b, bArr, 0, i);
            return bArr;
        }
    }

    public Iterator<byte[]> iterator() {
        return new C1668b();
    }

    public int size() {
        return this.f1378e;
    }

    public void remove() throws IOException {
        remove(1);
    }

    public void remove(int i) throws IOException {
        if (i < 0) {
            throw new IllegalArgumentException("Cannot remove negative (" + i + ") number of elements.");
        } else if (i != 0) {
            if (i == this.f1378e) {
                clear();
            } else if (isEmpty()) {
                throw new NoSuchElementException();
            } else if (i > this.f1378e) {
                throw new IllegalArgumentException("Cannot remove more elements (" + i + ") than present in queue (" + this.f1378e + ").");
            } else {
                long j = this.f1379f.f1386b;
                long j2 = this.f1379f.f1386b;
                int i2 = 0;
                int i3 = this.f1379f.f1387c;
                long j3 = 0;
                while (i2 < i) {
                    j3 += (long) (i3 + 4);
                    long b = mo22792b(4 + j2 + ((long) i3));
                    mo22789a(b, this.f1383k, 0, 4);
                    i2++;
                    i3 = m989a(this.f1383k, 0);
                    j2 = b;
                }
                m993a(this.f1377d, this.f1378e - i, j2, this.f1382j.f1386b);
                this.f1378e -= i;
                this.f1380g++;
                this.f1379f = new C1667a(j2, i3);
                if (this.f1384l) {
                    m994a(j, j3);
                }
            }
        }
    }

    public void clear() throws IOException {
        if (this.f1381h) {
            throw new IOException("closed");
        }
        m993a((long) PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM, 0, 0, 0);
        if (this.f1384l) {
            this.f1374a.seek((long) this.f1376c);
            this.f1374a.write(f1373i, 0, 4096 - this.f1376c);
        }
        this.f1378e = 0;
        this.f1379f = C1667a.f1385a;
        this.f1382j = C1667a.f1385a;
        if (this.f1377d > PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM) {
            m1001d(PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM);
        }
        this.f1377d = PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM;
        this.f1380g++;
    }

    public void close() throws IOException {
        this.f1381h = true;
        this.f1374a.close();
    }

    public String toString() {
        return getClass().getSimpleName() + "[length=" + this.f1377d + ", size=" + this.f1378e + ", first=" + this.f1379f + ", last=" + this.f1382j + "]";
    }
}
