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
    private static final byte[] f3699i = new byte[4096];
    /* renamed from: a */
    final RandomAccessFile f3700a;
    /* renamed from: b */
    boolean f3701b;
    /* renamed from: c */
    int f3702c;
    /* renamed from: d */
    long f3703d;
    /* renamed from: e */
    int f3704e;
    /* renamed from: f */
    C1415a f3705f;
    /* renamed from: g */
    int f3706g;
    /* renamed from: h */
    boolean f3707h;
    /* renamed from: j */
    private C1415a f3708j;
    /* renamed from: k */
    private final byte[] f3709k;
    /* renamed from: l */
    private final boolean f3710l;

    /* renamed from: net.gogame.gopay.vip.tape2.QueueFile$a */
    static class C1415a {
        /* renamed from: a */
        static final C1415a f3692a = new C1415a(0, 0);
        /* renamed from: b */
        final long f3693b;
        /* renamed from: c */
        final int f3694c;

        C1415a(long j, int i) {
            this.f3693b = j;
            this.f3694c = i;
        }

        public String toString() {
            return getClass().getSimpleName() + "[position=" + this.f3693b + ", length=" + this.f3694c + "]";
        }
    }

    /* renamed from: net.gogame.gopay.vip.tape2.QueueFile$b */
    private final class C1416b implements Iterator<byte[]> {
        /* renamed from: a */
        int f3695a = 0;
        /* renamed from: b */
        int f3696b = this.f3697c.f3706g;
        /* renamed from: c */
        final /* synthetic */ QueueFile f3697c;
        /* renamed from: d */
        private long f3698d = this.f3697c.f3705f.f3693b;

        public /* synthetic */ Object next() {
            return m4001a();
        }

        C1416b(QueueFile queueFile) {
            this.f3697c = queueFile;
        }

        /* renamed from: b */
        private void m4000b() {
            if (this.f3697c.f3706g != this.f3696b) {
                throw new ConcurrentModificationException();
            }
        }

        public boolean hasNext() {
            if (this.f3697c.f3707h) {
                throw new IllegalStateException("closed");
            }
            m4000b();
            return this.f3695a != this.f3697c.f3704e;
        }

        /* renamed from: a */
        public byte[] m4001a() {
            if (this.f3697c.f3707h) {
                throw new IllegalStateException("closed");
            }
            m4000b();
            if (this.f3697c.isEmpty()) {
                throw new NoSuchElementException();
            } else if (this.f3695a >= this.f3697c.f3704e) {
                throw new NoSuchElementException();
            } else {
                try {
                    C1415a a = this.f3697c.m4015a(this.f3698d);
                    byte[] bArr = new byte[a.f3694c];
                    this.f3698d = this.f3697c.m4017b(a.f3693b + 4);
                    this.f3697c.m4016a(this.f3698d, bArr, 0, a.f3694c);
                    this.f3698d = this.f3697c.m4017b((a.f3693b + 4) + ((long) a.f3694c));
                    this.f3695a++;
                    return bArr;
                } catch (Throwable e) {
                    throw new RuntimeException("todo: throw a proper error", e);
                }
            }
        }

        public void remove() {
            m4000b();
            if (this.f3697c.isEmpty()) {
                throw new NoSuchElementException();
            } else if (this.f3695a != 1) {
                throw new UnsupportedOperationException("Removal is only permitted from the head.");
            } else {
                try {
                    this.f3697c.remove();
                    this.f3696b = this.f3697c.f3706g;
                    this.f3695a--;
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
        this(m4005a(file, z2), z, z2);
    }

    /* renamed from: a */
    private static RandomAccessFile m4005a(File file, boolean z) throws IOException {
        if (!file.exists()) {
            File file2 = new File(file.getPath() + ".tmp");
            RandomAccessFile a = m4004a(file2);
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
        return m4004a(file);
    }

    /* renamed from: a */
    private static RandomAccessFile m4004a(File file) throws FileNotFoundException {
        return new RandomAccessFile(file, "rwd");
    }

    QueueFile(RandomAccessFile randomAccessFile, boolean z, boolean z2) throws IOException {
        long b;
        long b2;
        this.f3709k = new byte[32];
        this.f3706g = 0;
        this.f3700a = randomAccessFile;
        this.f3710l = z;
        randomAccessFile.seek(0);
        randomAccessFile.readFully(this.f3709k);
        boolean z3 = (z2 || (this.f3709k[0] & 128) == 0) ? false : true;
        this.f3701b = z3;
        if (this.f3701b) {
            this.f3702c = 32;
            int a = m4002a(this.f3709k, 0) & Strategy.TTL_SECONDS_INFINITE;
            if (a != 1) {
                throw new IOException("Unable to read version " + a + " format. Supported versions are 1 and legacy.");
            }
            this.f3703d = m4011b(this.f3709k, 4);
            this.f3704e = m4002a(this.f3709k, 12);
            b = m4011b(this.f3709k, 16);
            b2 = m4011b(this.f3709k, 24);
        } else {
            this.f3702c = 16;
            this.f3703d = (long) m4002a(this.f3709k, 0);
            this.f3704e = m4002a(this.f3709k, 4);
            b = (long) m4002a(this.f3709k, 8);
            b2 = (long) m4002a(this.f3709k, 12);
        }
        if (this.f3703d > randomAccessFile.length()) {
            throw new IOException("File is truncated. Expected length: " + this.f3703d + ", Actual length: " + randomAccessFile.length());
        } else if (this.f3703d <= ((long) this.f3702c)) {
            throw new IOException("File is corrupt; length stored in header (" + this.f3703d + ") is invalid.");
        } else {
            this.f3705f = m4015a(b);
            this.f3708j = m4015a(b2);
        }
    }

    /* renamed from: a */
    private static void m4008a(byte[] bArr, int i, int i2) {
        bArr[i] = (byte) (i2 >> 24);
        bArr[i + 1] = (byte) (i2 >> 16);
        bArr[i + 2] = (byte) (i2 >> 8);
        bArr[i + 3] = (byte) i2;
    }

    /* renamed from: a */
    private static int m4002a(byte[] bArr, int i) {
        return ((((bArr[i] & 255) << 24) + ((bArr[i + 1] & 255) << 16)) + ((bArr[i + 2] & 255) << 8)) + (bArr[i + 3] & 255);
    }

    /* renamed from: a */
    private static void m4009a(byte[] bArr, int i, long j) {
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
    private static long m4011b(byte[] bArr, int i) {
        return ((((((((((long) bArr[i]) & 255) << 56) + ((((long) bArr[i + 1]) & 255) << 48)) + ((((long) bArr[i + 2]) & 255) << 40)) + ((((long) bArr[i + 3]) & 255) << 32)) + ((((long) bArr[i + 4]) & 255) << 24)) + ((((long) bArr[i + 5]) & 255) << 16)) + ((((long) bArr[i + 6]) & 255) << 8)) + (((long) bArr[i + 7]) & 255);
    }

    /* renamed from: a */
    private void m4006a(long j, int i, long j2, long j3) throws IOException {
        this.f3700a.seek(0);
        if (this.f3701b) {
            m4008a(this.f3709k, 0, -2147483647);
            m4009a(this.f3709k, 4, j);
            m4008a(this.f3709k, 12, i);
            m4009a(this.f3709k, 16, j2);
            m4009a(this.f3709k, 24, j3);
            this.f3700a.write(this.f3709k, 0, 32);
            return;
        }
        m4008a(this.f3709k, 0, (int) j);
        m4008a(this.f3709k, 4, i);
        m4008a(this.f3709k, 8, (int) j2);
        m4008a(this.f3709k, 12, (int) j3);
        this.f3700a.write(this.f3709k, 0, 16);
    }

    /* renamed from: a */
    C1415a m4015a(long j) throws IOException {
        if (j == 0) {
            return C1415a.f3692a;
        }
        m4016a(j, this.f3709k, 0, 4);
        return new C1415a(j, m4002a(this.f3709k, 0));
    }

    /* renamed from: b */
    long m4017b(long j) {
        return j < this.f3703d ? j : (((long) this.f3702c) + j) - this.f3703d;
    }

    /* renamed from: b */
    private void m4012b(long j, byte[] bArr, int i, int i2) throws IOException {
        long b = m4017b(j);
        if (((long) i2) + b <= this.f3703d) {
            this.f3700a.seek(b);
            this.f3700a.write(bArr, i, i2);
            return;
        }
        int i3 = (int) (this.f3703d - b);
        this.f3700a.seek(b);
        this.f3700a.write(bArr, i, i3);
        this.f3700a.seek((long) this.f3702c);
        this.f3700a.write(bArr, i + i3, i2 - i3);
    }

    /* renamed from: a */
    private void m4007a(long j, long j2) throws IOException {
        long j3 = j;
        while (j2 > 0) {
            int min = (int) Math.min(j2, (long) f3699i.length);
            m4012b(j3, f3699i, 0, min);
            j2 -= (long) min;
            j3 += (long) min;
        }
    }

    /* renamed from: a */
    void m4016a(long j, byte[] bArr, int i, int i2) throws IOException {
        long b = m4017b(j);
        if (((long) i2) + b <= this.f3703d) {
            this.f3700a.seek(b);
            this.f3700a.readFully(bArr, i, i2);
            return;
        }
        int i3 = (int) (this.f3703d - b);
        this.f3700a.seek(b);
        this.f3700a.readFully(bArr, i, i3);
        this.f3700a.seek((long) this.f3702c);
        this.f3700a.readFully(bArr, i + i3, i2 - i3);
    }

    public void add(byte[] bArr) throws IOException {
        add(bArr, 0, bArr.length);
    }

    public void add(byte[] bArr, int i, int i2) throws IOException {
        if (bArr == null) {
            throw new NullPointerException("data == null");
        } else if ((i | i2) < 0 || i2 > bArr.length - i) {
            throw new IndexOutOfBoundsException();
        } else if (this.f3707h) {
            throw new IOException("closed");
        } else {
            long j;
            m4013c((long) i2);
            boolean isEmpty = isEmpty();
            if (isEmpty) {
                j = (long) this.f3702c;
            } else {
                j = m4017b((this.f3708j.f3693b + 4) + ((long) this.f3708j.f3694c));
            }
            C1415a c1415a = new C1415a(j, i2);
            m4008a(this.f3709k, 0, i2);
            m4012b(c1415a.f3693b, this.f3709k, 0, 4);
            m4012b(c1415a.f3693b + 4, bArr, i, i2);
            m4006a(this.f3703d, this.f3704e + 1, isEmpty ? c1415a.f3693b : this.f3705f.f3693b, c1415a.f3693b);
            this.f3708j = c1415a;
            this.f3704e++;
            this.f3706g++;
            if (isEmpty) {
                this.f3705f = this.f3708j;
            }
        }
    }

    /* renamed from: a */
    private long m4003a() {
        if (this.f3704e == 0) {
            return (long) this.f3702c;
        }
        if (this.f3708j.f3693b >= this.f3705f.f3693b) {
            return (((this.f3708j.f3693b - this.f3705f.f3693b) + 4) + ((long) this.f3708j.f3694c)) + ((long) this.f3702c);
        }
        return (((this.f3708j.f3693b + 4) + ((long) this.f3708j.f3694c)) + this.f3703d) - this.f3705f.f3693b;
    }

    /* renamed from: b */
    private long m4010b() {
        return this.f3703d - m4003a();
    }

    public boolean isEmpty() {
        return this.f3704e == 0;
    }

    /* renamed from: c */
    private void m4013c(long j) throws IOException {
        long j2 = 4 + j;
        long b = m4010b();
        if (b < j2) {
            long j3;
            long j4 = this.f3703d;
            while (true) {
                b += j4;
                j3 = j4 << 1;
                if (b >= j2) {
                    break;
                }
                j4 = j3;
            }
            m4014d(j3);
            b = m4017b((this.f3708j.f3693b + 4) + ((long) this.f3708j.f3694c));
            if (b <= this.f3705f.f3693b) {
                FileChannel channel = this.f3700a.getChannel();
                channel.position(this.f3703d);
                j2 = b - ((long) this.f3702c);
                if (channel.transferTo((long) this.f3702c, j2, channel) != j2) {
                    throw new AssertionError("Copied insufficient number of bytes!");
                } else if (this.f3710l) {
                    m4007a((long) this.f3702c, j2);
                }
            }
            if (this.f3708j.f3693b < this.f3705f.f3693b) {
                long j5 = (this.f3703d + this.f3708j.f3693b) - ((long) this.f3702c);
                m4006a(j3, this.f3704e, this.f3705f.f3693b, j5);
                this.f3708j = new C1415a(j5, this.f3708j.f3694c);
            } else {
                m4006a(j3, this.f3704e, this.f3705f.f3693b, this.f3708j.f3693b);
            }
            this.f3703d = j3;
        }
    }

    /* renamed from: d */
    private void m4014d(long j) throws IOException {
        this.f3700a.setLength(j);
        this.f3700a.getChannel().force(true);
    }

    public byte[] peek() throws IOException {
        if (this.f3707h) {
            throw new IOException("closed");
        } else if (isEmpty()) {
            return null;
        } else {
            int i = this.f3705f.f3694c;
            byte[] bArr = new byte[i];
            m4016a(4 + this.f3705f.f3693b, bArr, 0, i);
            return bArr;
        }
    }

    public Iterator<byte[]> iterator() {
        return new C1416b(this);
    }

    public int size() {
        return this.f3704e;
    }

    public void remove() throws IOException {
        remove(1);
    }

    public void remove(int i) throws IOException {
        if (i < 0) {
            throw new IllegalArgumentException("Cannot remove negative (" + i + ") number of elements.");
        } else if (i != 0) {
            if (i == this.f3704e) {
                clear();
            } else if (isEmpty()) {
                throw new NoSuchElementException();
            } else if (i > this.f3704e) {
                throw new IllegalArgumentException("Cannot remove more elements (" + i + ") than present in queue (" + this.f3704e + ").");
            } else {
                long j = this.f3705f.f3693b;
                long j2 = this.f3705f.f3693b;
                int i2 = 0;
                int i3 = this.f3705f.f3694c;
                long j3 = 0;
                while (i2 < i) {
                    j3 += (long) (i3 + 4);
                    long b = m4017b((4 + j2) + ((long) i3));
                    m4016a(b, this.f3709k, 0, 4);
                    i2++;
                    i3 = m4002a(this.f3709k, 0);
                    j2 = b;
                }
                m4006a(this.f3703d, this.f3704e - i, j2, this.f3708j.f3693b);
                this.f3704e -= i;
                this.f3706g++;
                this.f3705f = new C1415a(j2, i3);
                if (this.f3710l) {
                    m4007a(j, j3);
                }
            }
        }
    }

    public void clear() throws IOException {
        if (this.f3707h) {
            throw new IOException("closed");
        }
        m4006a((long) PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM, 0, 0, 0);
        if (this.f3710l) {
            this.f3700a.seek((long) this.f3702c);
            this.f3700a.write(f3699i, 0, 4096 - this.f3702c);
        }
        this.f3704e = 0;
        this.f3705f = C1415a.f3692a;
        this.f3708j = C1415a.f3692a;
        if (this.f3703d > PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM) {
            m4014d(PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM);
        }
        this.f3703d = PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM;
        this.f3706g++;
    }

    public void close() throws IOException {
        this.f3707h = true;
        this.f3700a.close();
    }

    public String toString() {
        return getClass().getSimpleName() + "[length=" + this.f3703d + ", size=" + this.f3704e + ", first=" + this.f3705f + ", last=" + this.f3708j + "]";
    }
}
